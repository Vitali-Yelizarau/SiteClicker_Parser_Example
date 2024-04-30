using Newtonsoft.Json;
using SiteClicker_Parser.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SiteClicker_Parser.SettingsStorage;

namespace SiteClicker_Parser
{
    internal class TelegramSettings
    {
        public TelegramSettings(string pathToSettingsFile) 
        {
            if (File.Exists(pathToSettingsFile))
            {
                string json = File.ReadAllText(pathToSettingsFile);
                var tempSettings = JsonConvert.DeserializeObject<TelegramSettings>(json);

                TOKEN = API_Token = tempSettings.API_Token;
                CHAT_ID = ChatId = tempSettings.ChatId;

            }
            else
            {
                TOKEN = API_Token = string.Empty;
                CHAT_ID = ChatId = -1;
            }
        }
        public string API_Token { get; private set; }
        public long ChatId { get; private set; }
    }
}
