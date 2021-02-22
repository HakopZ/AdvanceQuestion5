using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdvanceQuestion5
{
    public static class FileSort
    {
        const double MaxRam = 4;
        const int BytesInGB = 1073741824;
        static Dictionary<long, string> ByteCountToFile = new Dictionary<long, string>();
        static Random random = new Random();
        public static double[] SortCSVFile(string path)
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
            foreach (var line in File.ReadLines(path))
            {
                var howManyBytes = (long)(line.Length * sizeof(char));
                if (howManyBytes + byteCount > ((MaxRam/2) * BytesInGB))
                {
                    string fileName = $"{RandomString(4)}.txt";
                    ByteCountToFile.Add(byteCount, fileName);
                    File.WriteAllBytes(fileName, Sorter.MergeSort(chunk.ToArray()).SelectMany(value => BitConverter.GetBytes(value)).ToArray());
                    chunk.Clear();
                    byteCount = 0;
                }
                else
                {
                    byteCount += howManyBytes;
                    chunk.AddRange(Array.ConvertAll(line.Split(','), double.Parse));
                }
            }
            if(chunk.Count > 0)
            {
                string fileName = $"{RandomString(4)}.txt";
                ByteCountToFile.Add(byteCount, fileName);
                File.WriteAllBytes(fileName, Sorter.MergeSort(chunk.ToArray()).SelectMany(value => BitConverter.GetBytes(value)).ToArray());
            }
            MergeFiles();
            return default;
        }
        public static void MergeFiles()
        {
            ByteCountToFile.OrderBy(x => x.Key);
              
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string temp = "";
            do
            {
                temp = new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            } while (ByteCountToFile.Values.Contains(temp));
            return temp; 
        }
    }
}
