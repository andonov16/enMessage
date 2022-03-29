using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;

namespace enMessage.Shared.Utilities
{
    public static class JsonUtil
    {
        public static string ConvertToJson(object item)
        {
            if(item == null)
                throw new ArgumentNullException("item is null!");

            return JsonConvert.SerializeObject(item);
        }

        public static T ConvertFromJson<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
