using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiteClicker_Parser
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void StartButton_ClickAsync(object sender, EventArgs e)
        {
            
            while (true)
            {
                IWebDriver driver = new ChromeDriver();
                try
                {
                    driver.Navigate().GoToUrl(SettingsStorage.WEB_ADDRESS);
                    Thread.Sleep(new Random().Next(SettingsStorage.DELAY, SettingsStorage.MAX_DELAY));

                    foreach (string id in SettingsStorage.IdsList) 
                    {
                        await FunctionalClass.ClickElement_ById(driver, id);
                    }

                    await FunctionalClass.ClickElement_ByCssSelector(driver, SettingsStorage.CSS_SELECTOR);
                    var classText = await FunctionalClass.ClickElement_ByClassName(driver, SettingsStorage.CLASS_NAME);

                    Logger.LogInfo(classText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    driver.Quit();
                }
            }
        }
    }
}
