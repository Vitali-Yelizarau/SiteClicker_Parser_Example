using System;
using System.IO;
using static SiteClicker_Parser.SettingsStorage;

namespace SiteClicker_Parser
{
    public static class Logger
    {
        public static void LogInfo(string info)
        {
            LogFileChecker(LOG_PATH);
            using (StreamWriter sw = File.AppendText(LOG_PATH))
            {
                sw.WriteLine(DateTime.Now.ToString() + "    " + info);
            }
        }

        private static void LogFileChecker(string logPath)
        {
            if (!File.Exists(logPath))
            {
                File.Create(logPath).Close();
            }

            FileInfo fileInfo = new FileInfo(logPath);
            if (fileInfo.Length > LOGFILE_MAXSIZE_BYTES)
            {
                RenameCurrentAndCreateNewLogFile();
                File.Create(logPath).Close();
            }
        }
        private static void RenameCurrentAndCreateNewLogFile()
        {
            var newLogPath = LOG_PATH.Substring(0, LOG_PATH.LastIndexOf('\\') + 1)
                           + DateTime.Now.ToString("_dd.MM.yyyy_HH.mm.ss_")
                           + LOG_PATH.Substring(LOG_PATH.LastIndexOf('\\') + 1);
            File.Move(LOG_PATH, newLogPath);
        }
    }
}
