using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SiteClicker_Parser
{
    public class SettingsStorage
    {
        public const int DELAY = 2000;
        public const int MAX_DELAY = 5000;
        public const string DEFAULT_REQUEST_REPEAT_TIME_STRING = "3";
        public const int DEFAULT_REQUEST_REPEAT_TIME = 3 * 60 * 1000; //3 minutes
        public const long LOGFILE_MAXSIZE_BYTES = 50 * 1024 * 1024; //50 Mbytes
        public const string WEB_ADDRESS = "https://termine.staedteregion-aachen.de/auslaenderamt/";
        public const string CSS_SELECTOR = "input.btn.btn-primary.onehundred.pull-right";
        public const string CLASS_NAME = "h1like";

        public static readonly string APP_PATH = Assembly.GetExecutingAssembly().Location;
        public static readonly string APP_FOLDER_PATH = APP_PATH.Substring(0, APP_PATH.LastIndexOf('\\') + 1);
        public static readonly string LOG_PATH = APP_PATH + ".log";
        public static readonly string TG_SETTINGS_PATH = APP_FOLDER_PATH + "TelegramSettings.json";
        public static readonly string LOCK_FILE_PATH = APP_FOLDER_PATH + "base.dll";

        public static bool IsDebug = false;
        public static bool IsException = false;
        public static bool IsRunning = false;
        public static bool ImmediateStart = false;
        public static int REQUEST_REPEAT_TIME = 0;
        public static int _IterationNumber = 1;

        public static FileStream lockFile = null;

        public static IReadOnlyList<string> IdsList = new List<string>
        {
            "buttonfunktionseinheit-1",
            "header_concerns_accordion-340",
            "button-plus-264",
            "WeiterButton",
            "OKButton"
        };

        public static void Set_RequestRepeatTime(string timeString)
        {
            int.TryParse(timeString, out int time);
            REQUEST_REPEAT_TIME = time <= 0 || time >= 10 ? DEFAULT_REQUEST_REPEAT_TIME
                                                          : time * 60 * 1000;
        }
    }
}
