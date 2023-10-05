namespace Bidder.Application.Common.Redis.Interface
{
    public interface IDistributedCacheManager
    {
        byte[] Get(string key);
        T Get<T>(string key);
        void Set(string key, object value, int? expireTime = null, ExpireTimeUnit timeUnit = ExpireTimeUnit.Minutes);
        void Refresh(string key);
        bool Any(string key);
        void Remove(string key);
    }
}
