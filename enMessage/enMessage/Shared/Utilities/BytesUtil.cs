using System.Runtime.Serialization.Formatters.Binary;

namespace enMessage.Shared.Utilities
{
    public static class BytesUtil
    {
        public static byte[] ConvertToBytes(object item)
        {
            if(item == null)
                throw new ArgumentNullException("item is null!");

            BinaryFormatter bf = new BinaryFormatter();
            using(MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, item);
                return ms.ToArray();
            }
        }

        public static T ConvertFromBytes<T>(byte[] data)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(data, 0, data.Length);
                ms.Seek(0, SeekOrigin.Begin);
                return (T) bf.Deserialize(ms);
            }
        }
    }
}
