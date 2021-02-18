using System;
using System.IO;
using System.Diagnostics;
using CSharpTest.Net.Collections;
using System.Linq;
using LumenWorks.Framework.IO.Csv;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace AdvanceQuestion5
{
    class Program
    {
        const int MaxRam = 4;
        const int BytesInGB = 1073741824;
        static double[] SortCSVFile(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            int FileInGigaBytes = (int)(fileInfo.Length / BytesInGB);
            if (FileInGigaBytes > MaxRam)
            {
                return SplitLargeFile(path);
            }
            var lines = File.ReadAllText(path).Trim();
            double[] numbers = Array.ConvertAll(lines.Split(','), double.Parse);
            Array.Sort(numbers);
            return numbers;
            
        }
        static double[] SplitLargeFile(string path)
        {

            long byteCount = 0;
            List<double> chunk = new List<double>();
            foreach(var line in File.ReadLines(path))
            {
                var howManyBytes = (long)line.Length * sizeof(char);
                if(howManyBytes > (MaxRam * (long)BytesInGB))
                {

                    chunk.Clear();
                    byteCount = 0;
                }
                else
                {
                    byteCount += howManyBytes;
                    chunk.AddRange(Array.ConvertAll(line.Split(','), double.Parse));
                }
            }
            return default;
        }
        static void Main(string[] args)
        {

            
            Console.ReadKey();
        }
    }
}
