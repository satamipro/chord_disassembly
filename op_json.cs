using Newtonsoft.Json;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Threading.Tasks.Dataflow;

namespace Op_Json
{
    public class Op_Json
    {
        public static List<string> GetAllJsonFileNames(string dirPath = "", bool allDirectory = true)
        {
            string searchFolder = Directory.GetCurrentDirectory() + dirPath;
            string[] jsonFiles;
            List<string> ans = new List<string>();
            if (allDirectory)
            {
                jsonFiles = Directory.GetFiles(searchFolder, "*.json", SearchOption.AllDirectories);
            }
            else
            {
                jsonFiles = Directory.GetFiles(searchFolder, "*.json");
            }
            foreach (var jsonFile in jsonFiles)
            {
                ans.Add(Path.GetFileName(jsonFile));
            }
            return ans;

        }

        //rootfolder以下のすべてのjsonファイルの相対パスを取得
        public static string[] GetAllJsonFilePath(string dirPath = "", bool relativePath = true, bool allDirectory = true)
        {
            string rootFolder = Directory.GetCurrentDirectory();
            string searchFolder = Directory.GetCurrentDirectory() + dirPath;
            Console.WriteLine(rootFolder);
            string[] jsonFiles;
            if (allDirectory)
            {
                jsonFiles = Directory.GetFiles(searchFolder, "*.json", SearchOption.AllDirectories);
            }
            else
            {
                jsonFiles = Directory.GetFiles(searchFolder, "*.json");
            }

            if (relativePath)
            {
                for (int i = 0; i < jsonFiles.Length; i++)
                {
                    jsonFiles[i] = Path.GetRelativePath(rootFolder, jsonFiles[i]);
                }
            }
            return jsonFiles;

        }

        public static List<string[]> ReadAllJsonFiles(string[] filePath)
        {
            var objects = new List<string[]>();
            foreach (var jsonFile in filePath)
            {
                var content = File.ReadAllText(jsonFile);
                var obj = JsonConvert.DeserializeObject<string[]>(content)!;
                objects.Add(obj);
            }
            return objects;
        }
    }
}