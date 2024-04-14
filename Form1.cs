using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
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
        _Link_ExceptionCase:
            try
            {
                if (!SettingsStorage.IsRunning)
                {
                    Logger.LogInfo("\n");
                    Logger.LogInfo("**********************************************************************************");
                    Logger.LogInfo("Task been started");
                }
                ChangeAppStateAndButtonName();
                SettingsStorage.Set_RequestRepeatTime(TimeBox.Text);
                while (SettingsStorage.IsRunning)
                {
                    IWebDriver driver = new ChromeDriver();
                    try
                    {
                        driver.Navigate().GoToUrl(SettingsStorage.WEB_ADDRESS);
                        Thread.Sleep(new Random().Next(SettingsStorage.DELAY, SettingsStorage.MAX_DELAY));

                        foreach (string id in SettingsStorage.IdsList)
                        {
                            await WebDriverExtensions.ClickElement_ById(driver, id);
                        }

                        await WebDriverExtensions.ClickElement_ByCssSelector(driver, SettingsStorage.CSS_SELECTOR);
                        var classText = await WebDriverExtensions.ClickElement_ByClassName(driver, SettingsStorage.CLASS_NAME);

                        await Task.Run(() => Logger.LogInfo(classText));
                    }
                    catch (Exception ex)
                    {
                        await Task.Run(() => Logger.LogInfo(ex.ToString()));
                        await Task.Run(() => Logger.LogInfo(ex.Message));
                        await Task.Run(() => Logger.LogInfo(ex.InnerException.ToString()));
                    }
                    finally
                    {
                        driver.Quit();
                    }

                    await Task.Delay(SettingsStorage.REQUEST_REPEAT_TIME);
                }

                Logger.LogInfo("Task finished");
            }
            catch (Exception ex)
            {
                await Task.Run(() => Logger.LogInfo(ex.ToString()));
                await Task.Run(() => Logger.LogInfo(ex.Message));
                await Task.Run(() => Logger.LogInfo(ex.InnerException.ToString()));
                goto _Link_ExceptionCase; //Why this exception been not processed before - idk (probably loss of internet connection), cause it shall be processed :\
            }
        }

        private void ChangeAppStateAndButtonName()
        {
            if (!SettingsStorage.IsRunning)
            {
                SettingsStorage.IsRunning = true;
                StartButton.Text = "Stop";
                TimeBox.Enabled = false;
            }
            else
            {
                SettingsStorage.IsRunning = false;
                StartButton.Text = "Start";
                TimeBox.Enabled = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (SettingsStorage.ImmediateStart) StartButton_ClickAsync(this, EventArgs.Empty);
        }
    }
}
