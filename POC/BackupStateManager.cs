using Newtonsoft.Json;
using System.Configuration;
using System.IO;

namespace BackupUtilityLib
{
    public class BackupStateManager
    {
        private static string statesDir;

        public static void StoreFolderHash(HashBasedFolderCheck input)
        {
            statesDir = $"{ConfigurationManager.AppSettings["statesPath"]}data{input.SourceName}.txt";
            using (FileStream fs = File.Open(@statesDir, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;

                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, input);
            }
        }

        public static HashBasedFolderCheck ReadFolderHash(HashBasedFolderCheck input)
        {
            statesDir = $"{ConfigurationManager.AppSettings["statesPath"]}data{input.SourceName}.txt";
            try
            {
                using (StreamReader file = File.OpenText(@statesDir))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    HashBasedFolderCheck hashBasedFC = (HashBasedFolderCheck)serializer.Deserialize(file, typeof(HashBasedFolderCheck));
                    return hashBasedFC;
                }
            }
            catch(FileNotFoundException ex)
            {
                return null;
            }
        }
    }
}
