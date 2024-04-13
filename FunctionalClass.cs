using OpenQA.Selenium;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SiteClicker_Parser
{
    public static class FunctionalClass
    {
        public static async Task ClickElement_ById(IWebDriver driver, string ElementId)
        {
            IWebElement _Action = driver.FindElement(By.Id(ElementId));
            await Task.Run(() => AwaitingCompletion(_Action));
        }

        public static async Task ClickElement_ByCssSelector(IWebDriver driver, string CssElement)
        {
            IWebElement _Action = driver.FindElement(By.CssSelector(CssElement));
            await Task.Run(() => AwaitingCompletion(_Action));
        }

        public static async Task<string> ClickElement_ByClassName(IWebDriver driver, string ClassName)
        {
            IWebElement _Action = driver.FindElement(By.ClassName(ClassName));
            await Task.Run(() => AwaitingCompletion(_Action));
            return _Action.Text;
        }

        public static void AwaitingCompletion(IWebElement _Action)
        {
            _Action.Click();
            Thread.Sleep(new Random().Next(SettingsStorage.DELAY, SettingsStorage.MAX_DELAY));
        }
    }
}
