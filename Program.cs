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
            //###############################################################################################################
            //読み込みファイル内にあるすべてのfixedなjsonファイルからトレーニングデータと教師データを作成し、jsonファイルに書き出すプログラム。
            //必要に応じてvocabularyも書き出す
            //###############################################################################################################

            //###############################################################################################################
            //ここから実行に必要な変数定義（適宜改変して使う事！)
            string folderName = "test1";
            //###############################################################################################################

            //jsonファイルの読み込み
            string[] filePaths = GetAllJsonFilePath("/in");
            List<string> filenames = GetAllJsonFileNames("/in");
            List<string[]> rawdata = ReadAllJsonFiles(filePaths);

            //変数の定義
            string json;

            //ここにvocabularyの作成
            string[] vocabulary = MakeVocabulary(rawdata);
            json = JsonConvert.SerializeObject(vocabulary, Formatting.Indented);
            File.WriteAllText("out/" + folderName + "/vocabulary.json", json);

            //ここにfixeddataからchordsListの作成
            Directory.CreateDirectory("out/" + folderName + "/chordList/");
            for (int i = 0; i < rawdata.Count; i++)
            {
                //基本的にfixedなデータしか受け付けないが、それを確認するためにもう一度整理する。
                //ここで止まったらデータに誤りがある。
                string[] chords = AlignChordsFromString(rawdata[i]).ToArray();
                json = JsonConvert.SerializeObject(chords, Formatting.Indented);
                File.WriteAllText("out/" + folderName + "/chordList/" + filenames[i], json);
            }

            //ここにchordsListからtrainDataの作成
            Directory.CreateDirectory("out/" + folderName + "/trainData");
            for (int i = 0; i < rawdata.Count; i++)
            {
                int[][] trainData = new int[rawdata[i].Length][];
                for (int j = 0; j < rawdata[i].Length; j++)
                {
                    trainData[j] = DisassembleChord(GetChord(rawdata[i][j]));
                }
                json = JsonConvert.SerializeObject(trainData, Formatting.Indented);
                File.WriteAllText("out/" + folderName + "/trainData/" + filenames[i], json);
            }

            //ここにchordListからteachDataの作成
            Directory.CreateDirectory("out/" + folderName + "/teachData");
            for (int i = 0; i < rawdata.Count; i++)
            {
                int[][] teachData = MakeTeachData(rawdata[i], vocabulary);
                json = JsonConvert.SerializeObject(teachData, Formatting.Indented);
                File.WriteAllText("out/" + folderName + "/teachData/" + filenames[i], json);
            }


        }
    }
}