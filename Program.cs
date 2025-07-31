using System;
using System.IO;
using System.Text;

namespace EZHeader
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            args = new string[] { "G:\\Coding\\C++\\MysteryMemeware2\\Code\\EZTokens.cpp" };

            if (args is null || args.Length <= 0 || args.Length > 2)
            {
                Console.WriteLine("USAGE: EZHeader.exe C:\\Path\\To\\Source.cpp");
                Console.WriteLine("USAGE: EZHeader.exe C:\\Path\\To\\Source.cpp C:\\Path\\To\\GeneratedHeader.h");
            }
            else if (args.Length == 1)
            {
                string sourceRooted = Path.GetFullPath(args[0]);
                string headerPath = Path.GetDirectoryName(sourceRooted) + "\\" + Path.GetFileNameWithoutExtension(sourceRooted) + ".h";
                GenerateHeader(sourceRooted, headerPath);
            }
            else if (args.Length == 2)
            {
                string sourceRooted = Path.GetFullPath(args[0]);
                GenerateHeader(sourceRooted, sourceRooted);
            }
        }
        public static void GenerateHeader(string sourcePath, string headerPath)
        {
            string source = File.ReadAllText(sourcePath);
            StringBuilder header = new StringBuilder();
            int depth = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == '{')
                {
                    depth++;
                }else if (source[i] == '}')
                {
                    depth--;
                    if(depth == 0)
                    {
                        header.Append(';');
                    }
                }else if(depth == 0)
                {
                    header.Append(source[i]);
                }
            }
            File.WriteAllText(headerPath, header.ToString());
        }
    }
}
