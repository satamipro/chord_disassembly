using System.IO;
using Newtonsoft.Json;
using static Op_Json.Op_Json;
using static Other.Other;
using static ChordAnalysis.ChordAnalysis;
using Microsoft.VisualBasic;
using System.Security.AccessControl;
using System.Threading.Tasks.Dataflow;

namespace Program
{
    class Program
    {
        public static void Main()
        {

            //読み込みファイル内にあるすべてのjsonファイルからトレーニングデータと教師データを作成し、jsonファイルに書き出すプログラム。
            //必要に応じてvocabularyも書き出す
            //jsonファイルの読み込み
            string[] filePaths = GetAllJsonFilePath("/in");
            List<string> filenames = GetAllJsonFileNames("/in");
            List<string[]> rawdata = ReadAllJsonFiles(filePaths);

            //vocabularyの作成
            HashSet<string> vocabulary = [];
            string json;

            //ここにrawdtaからchordsListの作成

            //ここにchordsListからtrainDataの作成

            //ここにhordListからteachDataの作成

            //

            //one-hot chordvectorの作成
            List<int[][]> one_HotChordVector = GetOne_HotChordVector(rawdata);
            for (int i = 0; i < rawdata.Count; i++)
            {
                string writePath = filePaths[i].Replace(filenames[i], "").Replace("in\\", "out\\");
                Directory.CreateDirectory(writePath + "teachdata\\");
                json = JsonConvert.SerializeObject(one_HotChordVector[i], Formatting.Indented);
                File.WriteAllText(writePath + "teachdata\\teach" + filenames[i], json);
            }

            //コードベクターの取得,書き込み
            for (int i = 0; i < filePaths.Length; i++)
            {
                int length = rawdata[i].Length;
                List<int[]> chordVector = [];
                for (int j = 0; j < length; j++)
                {
                    chordVector.Add(DisassembleChord(GetChord(rawdata[i][j])));
                    vocabulary.Add(rawdata[i][j]);
                }
                string writePath = filePaths[i].Replace(filenames[i], "").Replace("in\\", "out\\");
                Directory.CreateDirectory(writePath + "chordVector\\");
                json = JsonConvert.SerializeObject(chordVector, Formatting.Indented);
                File.WriteAllText(writePath + "chordVector\\Vec" + filenames[i], json);
            }


            json = JsonConvert.SerializeObject(vocabulary, Formatting.Indented);
            File.WriteAllText("out//vocabulary.json", json);
        }
    }
}