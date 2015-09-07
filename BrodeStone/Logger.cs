using System;
using System.IO;

namespace BrodeStone
{
    public static class Logger
    {
        private static readonly string _logoutput = "H:\\MyFileOutput.txt";

        static Logger()
        {
            AppendLine("Starting...");
            AppendLine(string.Format("WorkingDirectory:{0}", Environment.CurrentDirectory));
        }

        public static void AppendLine(string s)
        {
            File.AppendAllText(_logoutput, s);
            File.AppendAllText(_logoutput, Environment.NewLine);
        }
    }
}