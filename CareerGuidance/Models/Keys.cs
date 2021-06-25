namespace CareerGuidance.Models
{
    public interface IKeys
    {
        public byte[] EncryptionKey { get; }
        public byte[] TokenKey { get; }
        public string MeiliKey { get; }
        public string AlgoliaKey { get; }
    }
    public class Keys: IKeys
    {
        public Keys(byte[] encryptionKey, byte[] tokenKey, string meiliKey, string algoliaKey)
        {
            EncryptionKey = encryptionKey;
            TokenKey = tokenKey;
            MeiliKey = meiliKey;
            AlgoliaKey = algoliaKey;
        }
        public byte[] EncryptionKey { get; }
        public byte[] TokenKey { get; }
        public string MeiliKey { get; }
        public string AlgoliaKey { get; }
    }
}