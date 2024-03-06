using Newtonsoft.Json;

namespace Project.Common.Json
{
    public static class JsonHelper
    {
        public static T ConvertObjectJson<T>(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
