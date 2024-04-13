using System;
using System.IO;

namespace SiteClicker_Parser
{
    public static class Logger
    {
        private const long MAXFILESIZE_BYTES = 50 * 1024 * 1024; //50 Mbytes
        public static void LogInfo(string info)
        {
            LogFileChecker(SettingsStorage.LOG_PATH);
            using (StreamWriter sw = File.AppendText(SettingsStorage.LOG_PATH))
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
            if (fileInfo.Length > MAXFILESIZE_BYTES)
            {
                RenameCurrentAndCreateNewLogFile();
                File.Create(logPath).Close();
            }
        }
        private static void RenameCurrentAndCreateNewLogFile()
        {
            var newLogPath = SettingsStorage.LOG_PATH.Substring(0, SettingsStorage.LOG_PATH.LastIndexOf('\\') + 1)
                           + DateTime.Now.ToString("_dd.MM.yyyy_HH.mm.ss_")
                           + SettingsStorage.LOG_PATH.Substring(SettingsStorage.LOG_PATH.LastIndexOf('\\') + 1);
            File.Move(SettingsStorage.LOG_PATH, newLogPath);
        }
    }
}
