using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

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
            string originalPath = @"C:\Temp";
            string destinationDir = @"C:\TempCopy";

            if (!System.IO.Directory.Exists(destinationDir))
            {
                System.IO.Directory.CreateDirectory(destinationDir);
            }

            
            System.IO.DirectoryInfo root = new System.IO.DirectoryInfo(originalPath);
            WalkDirectoryTree(root, destinationDir);
            //TraverseTree(@"C:\Temp");
            //TranverseAllDictonaries(@"C:\Temp");
        }

        //recusion
        static void WalkDirectoryTree(System.IO.DirectoryInfo specifiedDir,string destFolder) {
            int i = 0;
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
                    //Console.WriteLine(fi.FullName);
                    string destFilePath;
                    destFilePath = System.IO.Path.Combine(destFolder, fi.Name);
                    try
                    {
                        System.IO.File.Copy(fi.FullName, destFilePath, false);

                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                        i++;
                        try
                        {
                            File.Move(destFilePath, destFilePath.ToString().Split('.')[0]+"_"+i+".prefab");
                        }
                        catch (IOException w)
                        {
                            Console.WriteLine(w.Message);
                        }

                    }
                }
            }

            subDirs = specifiedDir.GetDirectories();

            foreach (System.IO.DirectoryInfo dirInfo in subDirs)
            {
                WalkDirectoryTree(dirInfo, destFolder);
            }
        }

        //without recusion
        public static void TraverseTree(string root)
        {
            // Data structure to hold names of subfolders to be
            // examined for files.
            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(root))
            {
                throw new ArgumentException();
            }
            dirs.Push(root);
            Console.WriteLine("dir count=="+dirs.Count);

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                Console.WriteLine("-------------------------------------"+currentDir);
                string[] subDirs;
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                string[] files = null;
                try
                {
                    files = System.IO.Directory.GetFiles(currentDir);
                }

                catch (UnauthorizedAccessException e)
                {

                    Console.WriteLine(e.Message);
                    continue;
                }

                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                // Perform the required action on each file here.
                // Modify this block to perform your required task.
                foreach (string file in files)
                {
                    try
                    {
                        // Perform whatever action is required in your scenario.
                        System.IO.FileInfo fi = new System.IO.FileInfo(file);
                        Console.WriteLine("{0}: {1}, {2}", fi.Name, fi.Length, fi.CreationTime);
                    }
                    catch (System.IO.FileNotFoundException e)
                    {
                        // If file was deleted by a separate application
                        //  or thread since the call to TraverseTree()
                        // then just continue.
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }

                // Push the subdirectories onto the stack for traversal.
                // This could also be done before handing the files.
                foreach (string str in subDirs) {
                    dirs.Push(str);
                }
            }
        }

        public static void TranverseAllDictonaries(string root) {
            Stack<string> dirs = new Stack<string>(20) ;
            dirs.Push(root);
            while (dirs.Count>0)
            {
                string[] subDirs = new string[] { };
                string currentDir = dirs.Pop();
                Console.WriteLine(currentDir);
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }

                foreach (string item in subDirs)
                    dirs.Push(item);
            }
        }
    }


}
