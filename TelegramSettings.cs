using Newtonsoft.Json;
using System.IO;

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

                API_Token = tempSettings.API_Token;
                ChatId = tempSettings.ChatId;
            }
            else
            {
                API_Token = string.Empty;
                ChatId = -1;
            }
        }
        [JsonProperty("API_Token")]
        public string API_Token { get; private set; }
        [JsonProperty("ChatId")]
        public long ChatId { get; private set; }
    }
}
