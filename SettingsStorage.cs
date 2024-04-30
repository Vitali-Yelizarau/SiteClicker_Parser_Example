using System.Collections.Generic;
using System.Reflection;
using Telegram.Bot;
using static SiteClicker_Parser.TelegramMessagingProcessor;

namespace SiteClicker_Parser
{
    public class SettingsStorage
    {
        public static bool IsDebug = false;
        public static bool IsException = false;
        public static bool IsRunning = false;
        public static bool ImmediateStart = false;
        public static int DELAY = 2000;
        public static int MAX_DELAY = 5000;
        public static int REQUEST_REPEAT_TIME = 0;
        public static readonly string DEFAULT_REQUEST_REPEAT_TIME_STRING = "3";
        public static readonly int DEFAULT_REQUEST_REPEAT_TIME = 3 * 60 * 1000; //3 minutes
        public static readonly long LOGFILE_MAXSIZE_BYTES = 50 * 1024 * 1024; //50 Mbytes
        public static int _IterationNumber = 1;

        public static string WEB_ADDRESS = "https://termine.staedteregion-aachen.de/auslaenderamt/";
        public static readonly string APP_PATH = Assembly.GetExecutingAssembly().Location;
        public static readonly string LOG_PATH = APP_PATH + ".log";
        public static readonly string TG_SETTINGS_PATH = APP_PATH.Substring(0, APP_PATH.LastIndexOf('\\') + 1) + "TelegramSettings.json";
        public static readonly string CSS_SELECTOR = "input.btn.btn-primary.onehundred.pull-right";
        public static readonly string CLASS_NAME = "h1like";

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
