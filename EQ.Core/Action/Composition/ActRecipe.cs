using EQ.Core.Actions;
using System.IO;

namespace EQ.Core.Action
{
    /// <summary>
    /// 현재 레시피의 이름과 경로를 관리하는 Action 클래스
    /// </summary>
    public class ActRecipe : ActComponent
    {
        private string _baseRecipeFolder;

        // (예: "Recipe_A")
        public string CurrentRecipeName { get; private set; } = "DefaultRecipe";

        public ActRecipe(ACT act) : base(act) { }

        /// <summary>
        /// (UI 시작 시 호출) 레시피의 최상위 폴더 경로를 설정합니다.
        /// </summary>
        public void Initialize(string baseRecipeFolder)
        {
            _baseRecipeFolder = baseRecipeFolder;
            Directory.CreateDirectory(_baseRecipeFolder);
        }

        /// <summary>
        /// (UI에서 호출) 현재 레시피를 변경합니다.
        /// </summary>
        public void SetCurrentRecipe(string recipeName)
        {
            CurrentRecipeName = recipeName;
            // (필요시) Log.Instance.Info($"레시피 변경: {recipeName}");
        }

        /// <summary>
        /// 현재 레시피의 전체 경로를 반환합니다.
        /// (예: E:\Project\Recipes\Recipe_A)
        /// </summary>
        public string GetCurrentRecipePath()
        {
            if (string.IsNullOrEmpty(_baseRecipeFolder)) return "";
            return Path.Combine(_baseRecipeFolder, CurrentRecipeName);
        }
    }
}