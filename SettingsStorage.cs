using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace SiteClicker_Parser
{
    public class SettingsStorage
    {
        public static readonly string CSS_SELECTOR = "input.btn.btn-primary.onehundred.pull-right";
        public static readonly string CLASS_NAME = "h1like";
        public static int DELAY = 2000;
        public static int MAX_DELAY = 5000;
        public static int REQUEST_REPEAT_TIME = 0;
        public static string WEB_ADDRESS = "https://termine.staedteregion-aachen.de/auslaenderamt/";
        public static readonly string APP_PATH = Assembly.GetExecutingAssembly().Location;
        public static readonly string LOG_PATH = APP_PATH + ".log";

        public static IReadOnlyList<string> IdsList = new List<string>
        {
            "buttonfunktionseinheit-1",
            "cookie_msg_btn_no",
            "header_concerns_accordion-340",
            "button-plus-268",
            "WeiterButton",
            "OKButton"
        };
    }
}
