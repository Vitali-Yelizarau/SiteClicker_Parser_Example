using System.Collections.Generic;
using System.Reflection;

namespace SiteClicker_Parser
{
    public class SettingsStorage
    {
        public static bool IsRunning = false;
        public static bool ImmediateStart = false;
        public static int DELAY = 2000;
        public static int MAX_DELAY = 5000;
        public static int REQUEST_REPEAT_TIME = 0;
        public static readonly int DEFAULT_REQUEST_REPEAT_TIME = 3 * 60 * 1000; //3 minutes
        public static string WEB_ADDRESS = "https://termine.staedteregion-aachen.de/auslaenderamt/";
        public static readonly string APP_PATH = Assembly.GetExecutingAssembly().Location;
        public static readonly string LOG_PATH = APP_PATH + ".log";
        public static readonly string CSS_SELECTOR = "input.btn.btn-primary.onehundred.pull-right";
        public static readonly string CLASS_NAME = "h1like";

        public static IReadOnlyList<string> IdsList = new List<string>
        {
            "buttonfunktionseinheit-1",
            "cookie_msg_btn_no",
            "header_concerns_accordion-340",
            "button-plus-268",
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
