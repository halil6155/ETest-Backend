using System.IO;

namespace Core.Utilities.FormFiles
{
    public class FileHelper
    {
        public static bool Delete(string path)
        {
            if (!File.Exists(path))
                return false;
            File.Delete(path);
            return true;

        }
    }
}