using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace gitignore_parser
{
    class main
    {
        static void Main(string[] args)
        {
            string root = @"F:\github6\gitignore-parser\";
            //root = @"F:\github\UnrealEngine\";
            Parser p = new Parser(root + @".gitignore");
            //parser p = new parser(root + @"test-regex.txt");
            LoopCheck(p, root, root);
        }
        static void LoopCheck(Parser p, string loopdir, string baseroot)
        {
            var all = Directory.GetFileSystemEntries(loopdir);
            List<string> Folders = new List<string>();

            foreach (var one in all)
            {
                string relative = one.Substring(baseroot.Length);

                bool folder = Directory.Exists(one);
                if (folder)
                {
                    relative = relative + "/";
                }

                if (!p.IsMatch(relative))
                {
                    Console.WriteLine("匹配通过：" + relative);
                    if (folder)
                    {
                        Folders.Add(one);
                    }
                }
            }

            foreach (var folder in Folders)
            {
                LoopCheck(p, folder, baseroot);
            }

        }
    }
}
