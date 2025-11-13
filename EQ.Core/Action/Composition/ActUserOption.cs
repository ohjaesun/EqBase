using EQ.Core.Actions;
using EQ.Domain.Interface; // IDataStorage
using EQ.Domain.Entities; // UserOption
using System;
using System.Collections.Generic;
using System.Linq; // List

namespace EQ.Core.Action
{
    /// <summary>
    /// UserOption을 '하이브리드' 방식으로 관리하는 Action 클래스
    /// (내부: 딕셔너리 / 외부: 속성)
    /// </summary>
    public class ActUserOption : ActComponent
    {
        // 1. [엔진] 확장성을 위한 제네릭 딕셔너리 (내부)
        private readonly Dictionary<Type, object> _optionCache;
        private readonly Dictionary<Type, object> _storageServices;
        private readonly ACT _act; // _act.Recipe.GetCurrentRecipePath() 호출용

        // 2. [편의성] '바로 가기(Shortcut)' 속성 (외부)
        public UserOption1 Option1 => Get<UserOption1>();
        public UserOption2 Option2 => Get<UserOption2>();
        public UserOption3 Option3 => Get<UserOption3>();
        public UserOption4 Option4 => Get<UserOption4>();
        public List<UserOptionUI> OptionUI => Get<List<UserOptionUI>>();

        public ActUserOption(ACT act) : base(act)
        {
            _act = act; // Recipe 접근용
            _optionCache = new Dictionary<Type, object>();
            _storageServices = new Dictionary<Type, object>();
        }

        // --- 내부 로직 (제네릭) ---

        public void RegisterStorageService<T>(IDataStorage<T> storageService) where T : class, new()
        {
            _storageServices[typeof(T)] = storageService;
        }

        public void LoadAllOptionsFromStorage()
        {
            // [핵심] ActRecipe로부터 현재 레시피 경로를 가져옴
            string path = _act.Recipe.GetCurrentRecipePath();
            if (string.IsNullOrEmpty(path)) return;

            _optionCache.Clear();
            foreach (var kvp in _storageServices)
            {
                Type optionType = kvp.Key;
                dynamic storage = kvp.Value;
                string key = GetStorageKey(optionType);

                object loadedOptions = storage.Load(path, key);

                if (loadedOptions == null)
                {
                    loadedOptions = Activator.CreateInstance(optionType);
                }
                _optionCache.Add(optionType, loadedOptions);
            }
        }

        /// <summary>
        /// [엔진] 캐시에서 제네릭으로 값을 가져옵니다. (바로 가기 속성이 사용)
        /// </summary>
        public T Get<T>() where T : class, new()
        {
            if (_optionCache.TryGetValue(typeof(T), out object options))
            {
                return (T)options;
            }
            // (로드된 적이 없으면, 새 인스턴스를 만들고 캐시에 등록)
            var newInstance = new T();
            _optionCache[typeof(T)] = newInstance;
            return newInstance;
        }

        /// <summary>
        /// [엔진] 캐시된 객체를 제네릭으로 저장합니다.
        /// </summary>
        public void Save<T>() where T : class, new()
        {
            Type optionType = typeof(T);

            if (!_storageServices.TryGetValue(optionType, out dynamic storage)) return;
            if (!_optionCache.TryGetValue(optionType, out object optionsToSave)) return;

            string path = _act.Recipe.GetCurrentRecipePath();
            string key = GetStorageKey(optionType);

            storage.Save((T)optionsToSave, path, key);
        }

        /// <summary>
        /// (신규) 타입에 맞는 저장 키(파일명)를 생성합니다.
        /// </summary>
        private string GetStorageKey(Type optionType)
        {
            if (optionType.IsGenericType && optionType.GetGenericTypeDefinition() == typeof(List<>))
            {
                // "List<UserOptionUI>" -> "List_UserOptionUI"
                return $"List_{optionType.GetGenericArguments()[0].Name}";
            }
            // "UserOption" -> "UserOption"
            return optionType.Name;
        }

        /// <summary>       
        /// OptionUI 리스트에서 'name'으로 설정을 찾아 GetValue<T>()를 반환합니다.
        /// </summary>
        /// <typeparam name="T">변환할 타입 (int, bool, string...)</typeparam>
        /// <param name="name">찾을 컨트롤의 'name' (키)</param>
        /// <param name="defaultValue">찾지 못했거나 변환 실패 시 반환할 기본값</param>
        /// <returns>변환된 값 또는 기본값</returns>
        public T GetUIValueByName<T>(string name, T defaultValue = default(T))
        {
            // 1. '바로 가기 속성'을 통해 캐시된 List<UserOptionUI>를 가져옵니다.
            List<UserOptionUI> uiSettings = this.OptionUI;

            if (uiSettings == null)
            {
                return defaultValue;
            }

            // 2. LINQ를 사용해 이름으로 설정 객체를 찾습니다.
            UserOptionUI setting = uiSettings.FirstOrDefault(x => x.name == name);

            if (setting != null)
            {
                // 3. 찾았으면 GetValue<T>()를 호출합니다.
                // (GetValue<T>는 변환 실패 시 알아서 default(T)를 반환합니다)
                return setting.GetValue<T>();
            }
            else
            {
                // 4. 이름을 찾지 못했으면 매개변수로 받은 기본값을 반환합니다.
                // (로그) Log.Instance.Debug($"OptionUI에서 name '{name}'을(를) 찾을 수 없습니다.");
                return defaultValue;
            }
        }
    }
}