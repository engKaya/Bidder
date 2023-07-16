using Bidder.UserService.Api.Helpers.Request.Base;
using Newtonsoft.Json;

namespace Bidder.UserService.Api.Helpers.Request
{
    public class RequestMessage : BaseRequestMessage
    {
        public string Operation { get; set; } = string.Empty;
        public object? ObjectList { get; set; }

        public T GetData<T>()
        {
            var typeOfT = typeof(T);
            var fullnameofT = $"{typeOfT.Namespace}.{typeOfT.Name}";
            if (ObjectList == null) return default(T);

            var objectsOfList = GetPropertyKeysForDynamic(ObjectList); 

            if (!objectsOfList.Any(x=>x == fullnameofT))
                return default(T);

            var data = GetProperty(ObjectList, fullnameofT);

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(data));

        }
    }
}
