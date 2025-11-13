using EQ.Domain.Interface;
using Newtonsoft.Json;
using System.IO;

namespace EQ.Infra.Storage
{
    public class JsonFileStorage<T> : IDataStorage<T> where T : class, new()
    {
        // [변경] 생성자에서 경로를 받지 않습니다.
        public JsonFileStorage()
        {
        }

        public void Save(T data, string path, string key)
        {
            // 'path' (예: ...\Recipes\Recipe_A)가 동적으로 주입됨
            Directory.CreateDirectory(path);
            var filePath = Path.Combine(path, $"{key}.json");

            string strJson = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, strJson);
        }

        public T Load(string path, string key)
        {
            var filePath = Path.Combine(path, $"{key}.json");
            if (!File.Exists(filePath))
            {
                return new T(); // 파일이 없으면 기본 인스턴스 반환
            }
            string strJson = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(strJson);
        }
    }
}