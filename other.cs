using System.IO;
using System.Collections.Generic;
using System.Xml;
using Microsoft.VisualBasic;
using System.IO.Pipes;


namespace Other
{
    public static class Other
    {
        //特定の配列を引数にとり、それの要素をコンソールに出力する
        public static void Printa<T>(T[] array, Boolean newLine = false)
        {
            if (newLine)
            {
                for (var i = 0; i < array.Length; i++)
                {
                    Console.WriteLine("[" + i + "]: " + array[i]);
                }
            }
            else
            {
                for (var i = 0; i < array.Length; i++)
                {

                    Console.Write("[" + i + "]: " + array[i] + "\t");
                    if (Console.WindowWidth - 20 < Console.CursorLeft)
                    {
                        Console.Write("\n");
                    }
                }
            }
            Console.Write("\n");
        }

        //string配列内の特定の文字を置換する。
        public static string[] ReplaceAll(string[] a, string oldstr, string newstr)
        {
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = a[i].Replace(oldstr, newstr);
            }
            return a;
        }

        //string配列内の特定の文字を置換する。
        public static List<string> ReplaceAll(List<string> a, string oldstr, string newstr)
        {
            for (int i = 0; i < a.Count; i++)
            {
                a[i] = a[i].Replace(oldstr, newstr);
            }
            return a;
        }


    }
}
