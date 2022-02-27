namespace DataAccess.Utilities
{
    public static class FileUtil
    {
        public static async Task SaveFileAsync(string URL, byte[] data)
        {
            await File.WriteAllBytesAsync(URL, data);   
        }

        //DataType, Data
        public static async Task<KeyValuePair<string, byte[]>> ReadFileAsync(string URL)
        {
            if(!Directory.Exists(URL))
                Directory.CreateDirectory(URL);

            byte[] data =await File.ReadAllBytesAsync(URL);
            return new KeyValuePair<string, byte[]>(Path.GetExtension(URL), data);
        }
    }
}
