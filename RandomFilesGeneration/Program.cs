using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RandomFilesGeneration
{
    class Program
    {
        private static Data _data;
        private static Random _rand;
        static void Main(string[] args)
        {
            var json = File.ReadAllText("words.json");
            _data = JsonConvert.DeserializeObject<Data>(json);
            _rand = new Random();

            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var dir = Directory.CreateDirectory(Path.Combine(desktopPath, $"Randomly Generated Files")).FullName;

            Create(dir);
        }

        private static void Create(string dir, int dir1Lev=0, int dir2Lev=0, int dir3Lev=0)
        {
            for (int i = 0; i < 5; i++)
            {
                var file = GetFileName(dir);
                File.Create(file);
                Console.WriteLine($"Creating {file}");
                if (dir1Lev < 3)
                {
                    Create(CreateDir(dir), ++dir1Lev, dir2Lev, dir3Lev);
                }
                if (dir2Lev < 3)
                {
                    Create(CreateDir(dir), dir1Lev, ++dir2Lev, dir3Lev);
                }
                if (dir3Lev < 3)
                {
                    Create(CreateDir(dir), dir1Lev, dir2Lev, ++dir3Lev);
                }
            }
        }

        private static string CreateDir(string dir) => Directory.CreateDirectory(Path.Combine(dir, _data.Words[_rand.Next(0, _data.Words.Count - 1)])).FullName;

        public static string GetFileName(string dir)
        {
            var file = Path.Combine(dir, $"{_data.Words[_rand.Next(0, _data.Words.Count - 1)]}.{_data.Extensions[_rand.Next(0, _data.Extensions.Count - 1)]}");
            return !File.Exists(file) ? file : GetFileName(dir);
        }
    }
}