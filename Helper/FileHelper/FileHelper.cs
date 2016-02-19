using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FileHelper
{
    public static class FileHelper
    {
        private const string Pattern = "^[a-z]:\\(?:[^\\/:*?\"<>|\r\n]+\\)*[^\\/:*?\"<>|\r\n]*$";
        private const string PatternDir = "^[a-z]:\\(?:[^\\/:*?\"<>|\r\n]+\\)*$";
        private const string PatternDrive = "^[a-z]:\\$";
        private const string PatternFolder = "^(?:[^\\/:*?\"<>|\r\n]+\\)*$";
        private const string PatternFile = "^[^\\/:*?\"<>|\r\n]*$";

        public static bool IsValidFullPath(string fullPath)
        {
            if (!Regex.IsMatch(fullPath, Pattern))
            {
                return false;
            }
            return true;
        }

        public static DirectoryInfo CreateDir(string dirName)
        {
            return !Directory.Exists(dirName) ? Directory.CreateDirectory(dirName) : new DirectoryInfo(dirName);
        }

        public static void CreateFile(string path)
        {
            CreateFile(path,false);
        }

        public static void CreateFile(string path,bool createNew)
        {
            if (!File.Exists(path))
            {
                var fs = new FileStream(path,createNew?FileMode.CreateNew:FileMode.Create);
                fs.Close();
                fs.Dispose();
            }
        }

        #region WriteFile
        public static void Write(string path, string content)
        {
            Write(path,content,true);
        }

        public static void Write(string path, string content, bool append)
        {
            Write(path,content,append,Encoding.UTF8);
        }

        public static void Write(string path, string content, bool append,Encoding encoding)
        {
            var sw = new StreamWriter(path, append, encoding);
            sw.Write(content);
            sw.Close();
            sw.Dispose();
        }

        public static void WriteLine(string path, string content)
        {
            WriteLine(path, content, true);
        }

        public static void WriteLine(string path, string content,bool append)
        {
            WriteLine(path, content, append,Encoding.UTF8);
        }

        public static void WriteLine(string path, string content, bool append,Encoding encoding)
        {
            var sw = new StreamWriter(path, append,encoding);
            sw.WriteLine(content);
            sw.Close();
            sw.Dispose();
        }


        #endregion

        #region ReadFile

        public static int Read(string path, char[] buffer,int index,int count)
        {
            var sr = new StreamReader(path);
            int totalCount = sr.Read(buffer,index,count);
            sr.Close();
            sr.Dispose();
            return totalCount;
        }

        public static string ReadLine(string path, string content)
        {
            var sr = new StreamReader(path);
            string str = sr.ReadLine();
            sr.Close();
            sr.Dispose();
            return str;
        }

        public static string ReadToEnd(string path, string content)
        {
            var sr = new StreamReader(path);
            string str = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            return str;
        }

        #endregion
    }
}