using System;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace Ui
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[] dirs = Directory.GetDirectories("C:\\Users\\akbar\\Videos\\JavaScriptCourses");

            foreach(var arg in args) 
            {
                if(Directory.Exists(arg))
                {
                    // This path is a directory
                    ProcessDirectory(arg);
                }
                else
                {
                    Console.WriteLine("{0} is not a valid file or directory.", arg);
                }

            }
        }

        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        public static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string [] fileEntries = Directory.GetFiles(targetDirectory);
            var i = 0;
            while(i < fileEntries.Length)
            {
                var file = fileEntries[i];
                var audio = fileEntries[i+1];
                var output = GetFileName(targetDirectory, file);

                var cmd = string.Format(" -i \"{0}\" -i \"{1}\" -map 0:V:0 -map 1:a:0 -c copy -f mp4 -movflags +faststart \"{2}\"", file, audio, output);

                Process.Start("ffmpeg", cmd);

                i += 2;
            }

            // foreach(string fileName in fileEntries)
            //     ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string [] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach(string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        public static string GetFileName(string path, string fullpath) 
        {
            var fullname = fullpath.Split("\\");
            var filename = fullname[fullname.Length - 1];

            var sb = new StringBuilder();
            sb.Append(path);
            sb.Append("\\");
            sb.Append(filename.Substring(0, filename.Length - 3));
            sb.Append(".mp4");

            return sb.ToString();
        }

        // Insert logic for processing found files here.
        public static void ProcessFile(string path)
        {
            Console.WriteLine("Processed file '{0}'.", path);	
        }
    }
}
