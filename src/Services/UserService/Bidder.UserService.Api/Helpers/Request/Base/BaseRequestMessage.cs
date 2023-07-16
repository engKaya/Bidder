using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Bidder.UserService.Api.Helpers.Request.Base
{
    public class BaseRequestMessage
    {
        public static List<string> GetPropertyKeysForDynamic(object ob)
        {

            var list = new List<string>();
            if (ob == null)
                return list;
            foreach ((string k, object o) in JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(ob)))
            {
                list.Add(k.ToString());
            }
            return list;
        }

        public static object GetProperty(object target, string name)
        {
            var strdata = JsonSerializer.Serialize(target);
            JObject jobj = JObject.Parse(strdata);
            return jobj[name];
        } 
    }
}
