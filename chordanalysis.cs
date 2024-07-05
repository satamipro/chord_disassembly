

using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace ChordAnalysis
{

    public static class ChordAnalysis
    {
        public struct Chord
        {
            public string all;
            public string root;
            public string third;
            public string seventh;
            public string susaug;
            public string tention;
            public string onchord;
        }
        //コードのHashsetの定義
        private static HashSet<string> banList = ["(N.C)", "(2/4)", "/", "(5/4)", ".9", ";", "(N.A)", "N.C", ".", "(6/4)", "・", "(3/4)", "（N･C）", "(N.C.)", "(3/8)", "（FadeIn）", "(4/4)"];
        private static HashSet<string> rootList = ["A♭", "A#", "A", "B♭", "B#", "B", "C♭", "C#", "C", "D♭", "D#", "D", "E♭", "E#", "E", "F♭", "F#", "F", "G♭", "G#", "G"];
        private static HashSet<string> thirdList = ["m", "aug", "dim"];
        private static HashSet<string> seventhList = ["69", "6(9)", "6", "M7", "7", "M9", "9", "11", "13"];
        private static HashSet<string> susaddList = ["sus4", "add4", "add9", "add11"];
        private static HashSet<string> tentionList = ["-5", "+5", "(♭5)", "(#5)", "-9", "+9", "(♭9)", "(9)", "(#9)", "-11", "+11", "(♭11)", "(11)", "(#11)", "-13", "+13", "(♭13)", "(13)", "(#13)"];
        private static HashSet<string> onchordList = ["onA♭", "onA#", "onA", "onB♭", "onB#", "onB", "onC♭", "onC#", "onC", "onD♭", "onD#", "onD", "onE♭", "onE#", "onE", "onF♭", "onF#", "onF", "onG♭", "onG#", "onG"];
        // , "/A♭", "/A#", "/A", "/B♭", "/B#", "/B", "/C♭", "/C#", "/C", "/C♭", "/C#", "/C", "/D♭", "/D#", "/D", "/E♭", "/E#", "/E", "/F♭", "/F#", "/F", "/G♭", "/G#", "/G"


        //コードから構成音を特定
        public static int[] DisassembleChord(Chord chord)
        {
            //index[0]はC
            HashSet<int> elements = [0, 4, 7];
            int offset = 0;
            int[] chordVector = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            switch (chord.third)
            {
                case "m":
                    elements.Remove(4);
                    elements.Add(3);
                    break;
                case "aug":
                    elements.Remove(7);
                    elements.Add(8);
                    break;
                case "dim":
                    elements.Remove(4);
                    elements.Add(3);
                    elements.Remove(7);
                    elements.Add(6);
                    break;
            }
            switch (chord.seventh)
            {
                case "6(9)":
                case "69":
                    elements.Add(9);
                    elements.Add(2);
                    break;
                case "M7":
                    elements.Add(11);
                    break;
                case "7":
                    elements.Add(10);
                    break;
                case "M9":
                    elements.Add(11);
                    elements.Add(2);
                    break;
                case "9":
                    elements.Add(10);
                    elements.Add(2);
                    break;
                case "11":
                    elements.Add(10);
                    elements.Add(2);
                    elements.Add(5);
                    break;
                case "13":
                    elements.Add(10);
                    elements.Add(2);
                    elements.Add(5);
                    elements.Add(9);
                    break;
            }
            switch (chord.susaug)
            {
                case "sus4":
                    elements.Remove(4);
                    elements.Add(5);
                    break;
                case "add4":
                    elements.Add(5);
                    break;
                case "add9":
                    elements.Add(2);
                    break;
                case "add11":
                    elements.Add(5);
                    break;
            }
            switch (chord.tention)
            {
                case "-5":
                case "(♭5)":
                    elements.Remove(7);
                    elements.Add(6);
                    break;
                case "+5":
                case "(#5)":
                    elements.Remove(7);
                    elements.Add(8);
                    break;
                case "-9":
                case "(♭9)":
                    elements.Add(1);
                    break;
                case "+9":
                case "(#9)":
                    elements.Add(3);
                    break;
                case "-11":
                case "(♭11)":
                    elements.Add(4);
                    break;
                case "+11":
                case "(#11)":
                    elements.Add(6);
                    break;
                case "-13":
                case "(♭13)":
                    elements.Add(8);
                    break;
                case "+13":
                case "(#13)":
                    elements.Add(10);
                    break;
                case "(9)":
                    elements.Add(2);
                    break;
                case "(11)":
                    elements.Add(5);
                    break;
                case "(13)":
                    elements.Add(9);
                    break;
            }
            switch (chord.root)
            {
                case "C":
                case "B#":
                    offset = 0;
                    break;
                case "C#":
                case "D♭":
                    offset = 1;
                    break;
                case "D":
                    offset = 2;
                    break;
                case "D#":
                case "E♭":
                    offset = 3;
                    break;
                case "E":
                case "F♭":
                    offset = 4;
                    break;
                case "E#":
                case "F":
                    offset = 5;
                    break;
                case "F#":
                case "G♭":
                    offset = 6;
                    break;
                case "G":
                    offset = 7;
                    break;
                case "G#":
                case "A♭":
                    offset = 8;
                    break;
                case "A":
                    offset = 9;
                    break;
                case "A#":
                case "B♭":
                    offset = 10;
                    break;
                case "B":
                case "C♭":
                    offset = 11;
                    break;
            }
            foreach (int i in elements)
            {
                chordVector[(i + offset) % 12] = 1;
            }
            switch (chord.root)
            {
                case "C":
                case "B#":
                    chordVector[0] = 1;
                    break;
                case "C#":
                case "D♭":
                    chordVector[1] = 1;
                    break;
                case "D":
                    chordVector[2] = 1;
                    break;
                case "D#":
                case "E♭":
                    chordVector[3] = 1;
                    break;
                case "E":
                case "F♭":
                    chordVector[4] = 1;
                    break;
                case "E#":
                case "F":
                    chordVector[5] = 1;
                    break;
                case "F#":
                case "G♭":
                    chordVector[6] = 1;
                    break;
                case "G":
                    chordVector[7] = 1;
                    break;
                case "G#":
                case "A♭":
                    chordVector[8] = 1;
                    break;
                case "A":
                    chordVector[9] = 1;
                    break;
                case "A#":
                case "B♭":
                    chordVector[10] = 1;
                    break;
                case "B":
                case "C♭":
                    chordVector[11] = 1;
                    break;
            }
            return chordVector;

        }

        //コード配列の解析
        public static List<String> AlignChordsFromString(string[] q)
        {
            int index = 0;
            int length;
            List<string> ans = [];
            string chord = string.Join("", q);
            chord = chord.Replace("＃", "#").Replace("／", "").Replace("…", "").Replace("♯", "#").Replace("699", "69").Replace("susu", "sus").Replace("sug", "aug").Replace("agu", "aug").Replace("※", "").Replace("O", "o");
            Chord tmp = new Chord();
            // Console.WriteLine(chord);
            while (true)
            {

                tmp.root = "";
                tmp.third = "";
                tmp.seventh = "";
                tmp.tention = "";
                tmp.onchord = "";
                length = 1;

                //banListに合致するかの判定

                do
                {
                    length = StartsWithElementFromSet(chord, index, banList);
                    index += length;
                } while (length != 0);

                index += length;

                //rootの判定
                length = StartsWithElementFromSet(chord, index, rootList);
                tmp.root = chord.Substring(index, length);
                index += length;

                if (tmp.root != "")
                {
                    length = StartsWithElementFromSet(chord, index, thirdList);
                    tmp.third = chord.Substring(index, length);
                    index += length;

                    length = StartsWithElementFromSet(chord, index, seventhList);
                    tmp.seventh = chord.Substring(index, length);
                    index += length;

                    length = StartsWithElementFromSet(chord, index, susaddList);
                    tmp.susaug = chord.Substring(index, length);
                    index += length;

                    length = StartsWithElementFromSet(chord, index, tentionList);
                    tmp.tention = chord.Substring(index, length);
                    index += length;

                    length = StartsWithElementFromSet(chord, index, onchordList);
                    tmp.onchord = chord.Substring(index, length);
                    index += length;
                    ans.Add(tmp.root + tmp.third + tmp.seventh + tmp.susaug + tmp.tention + tmp.onchord);
                    // Console.Write("written:");
                    // PrintChord(tmp);
                }
                else
                {
                    if (index >= chord.Length)
                    {
                        return ans;
                    }
                    Console.WriteLine(chord.Substring(Math.Max(index - 5, 0), Math.Min(10, chord.Length - index + 5)));
                    Console.WriteLine("throw: " + chord[index]);
                    Console.ReadLine();
                    index += 1;

                }


            }
        }

        //vocabularyの作成
        public static string[] MakeVocabulary(List<string[]> q)
        {
            string[] vocabulary = [];
            HashSet<string> hashset = [];
            foreach (var i in q)
            {
                foreach (var j in i)
                {
                    hashset.Add(j);
                }
            }
            vocabulary = hashset.ToArray();
            return vocabulary;
        }

        //chordの表示
        public static void PrintChord(Chord q)
        {
            Console.WriteLine(q.root + q.third + q.seventh + q.susaug + q.tention + q.onchord);
        }

        //chordの分解
        public static Chord GetChord(string chord)
        {
            Chord ans = new Chord();
            ans.root = "";
            ans.third = "";
            ans.seventh = "";
            ans.susaug = "";
            ans.tention = "";
            ans.onchord = "";


            int index = 0;
            int length;
            length = StartsWithElementFromSet(chord, index, rootList);
            ans.root = chord.Substring(index, length);
            index += length;
            if (ans.root != "")
            {
                length = StartsWithElementFromSet(chord, index, thirdList);
                ans.third = chord.Substring(index, length);
                index += length;

                length = StartsWithElementFromSet(chord, index, seventhList);
                ans.seventh = chord.Substring(index, length);
                index += length;

                length = StartsWithElementFromSet(chord, index, susaddList);
                ans.susaug = chord.Substring(index, length);
                index += length;

                length = StartsWithElementFromSet(chord, index, tentionList);
                ans.tention = chord.Substring(index, length);
                index += length;

                length = StartsWithElementFromSet(chord, index, onchordList);
                ans.onchord = chord.Substring(index, length);
                index += length;
            }
            return ans;
        }

        public static int StartsWithElementFromSet(string str, int n, HashSet<string> set)
        {
            if (n > str.Length)
            {
                return 0;
            }

            foreach (var element in set)
            {
                if (str.Substring(n).StartsWith(element))
                {
                    return element.Length;
                }
            }

            return 0;
        }

        public static List<int[][]> MakeTeachData(List<string[]> chords)
        {
            string[] voclabulary = MakeVocabulary(chords);
            int length = voclabulary.Length;
            List<int[][]> one_HotChordVector = new List<int[][]>();
            for (int i = 0; i < chords.Count; i++)
            {
                int[][] songChordVector = new int[chords[i].Length][];
                for (int j = 0; j < chords[i].Length; j++)
                {
                    songChordVector[j] = new int[length];
                    songChordVector[j][Array.IndexOf(voclabulary, chords[i][j])] = 1;
                }
                one_HotChordVector.Add(songChordVector);
            }
            return one_HotChordVector;
        }

        public static int[][] MakeTeachData(string[] chords, string[] vocabulary)
        {
            int length = vocabulary.Length;
            int[][] teachData = new int[chords.Length][];
            for (int i = 0; i < chords.Length; i++)
            {
                teachData[i] = new int[length];
                teachData[i][Array.IndexOf(vocabulary, chords[i])] = 1;
            }
            return teachData;
        }
    }
}
