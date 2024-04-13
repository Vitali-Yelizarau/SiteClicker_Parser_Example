using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteClicker_Parser
{
    public static class Logger
    {
        private const long MAXFILESIZE_BYTES = 50*1024*1024; //50 Mbytes
        public static void LogInfo(string info)
        {
            FileInfo fileInfo = new FileInfo(SettingsStorage.LOG_PATH);

            if (!fileInfo.Exists && fileInfo.Length > MAXFILESIZE_BYTES)
            {
                File.Create(SettingsStorage.LOG_PATH).Close();
            }

            using (StreamWriter sw = File.AppendText(SettingsStorage.LOG_PATH))
            {
                sw.WriteLine(DateTime.Now.ToString() + "    " + info);
            }
        }
    }
}
