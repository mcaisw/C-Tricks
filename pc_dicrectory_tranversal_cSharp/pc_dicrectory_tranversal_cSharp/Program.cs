using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

/*
https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-iterate-through-a-directory-tree
这个脚本实现把一个目录下面，所有同类型的文件（包含子目录），复制到另外一个文件夹下。
     */
namespace pc_dicrectory_tranversal_cSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Temp";
            string targetPath = @"C:\TempCopy";

            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            System.IO.DirectoryInfo root = new System.IO.DirectoryInfo(path);
            WalkDirectoryTree(root, targetPath);
        }

        static void WalkDirectoryTree(System.IO.DirectoryInfo specifiedDir,string destFolder) {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            try
            {
                files = specifiedDir.GetFiles("*.prefab");
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    Console.WriteLine(fi.FullName);
                    string destFile;
                    destFile = System.IO.Path.Combine(destFolder, fi.Name);
                    System.IO.File.Copy(fi.FullName, destFile,true);
                }
            }

            subDirs = specifiedDir.GetDirectories();
            foreach (System.IO.DirectoryInfo dirInfo in subDirs)
            {
                WalkDirectoryTree(dirInfo, destFolder);
            }
        }
    }
}
