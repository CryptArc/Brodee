using System;
using System.IO;
using UnityEngine;

namespace Brodee
{
    public static class Logger
    {
        private static readonly string _logoutput = "MyFileOutput.txt";

        static Logger()
        {
            AppendLine("Starting...");
            AppendLine($"WorkingDirectory:{Environment.CurrentDirectory}");
        }

        public static void AppendLine(string s)
        {
            File.AppendAllText(_logoutput, s);
            File.AppendAllText(_logoutput, Environment.NewLine);
        }
    }
}