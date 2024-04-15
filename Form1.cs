using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SiteClicker_Parser.SettingsStorage;
using static SiteClicker_Parser.Logger;

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
                if (!IsRunning)
                {
                    LogInfo("\n");
                    LogInfo("**********************************************************************************");
                    LogInfo("Task been started");
                }
                ChangeAppStateAndButtonName();
                Set_RequestRepeatTime(TimeBox.Text);
                while (IsRunning)
                {
                    //ChromeOptions options = new ChromeOptions();
                    //options.AddArgument("--headless");
                    IWebDriver driver = new ChromeDriver();
                    try
                    {
                        LogInfo("Current iteration number: " + _IterationNumber);
                        driver.Navigate().GoToUrl(WEB_ADDRESS);
                        Thread.Sleep(new Random().Next(DELAY, MAX_DELAY));

                        foreach (string id in IdsList)
                        {
                            await WebDriverExtensions.ClickElement_ById(driver, id);
                        }

                        await WebDriverExtensions.ClickElement_ByCssSelector(driver, CSS_SELECTOR);
                        var classText = await WebDriverExtensions.ClickElement_ByClassName(driver, CLASS_NAME);

                        await Task.Run(() => LogInfo(classText));
                    }
                    catch (Exception ex)
                    {
                        await Task.Run(() => LogInfo(ex.ToString()));
                        await Task.Run(() => LogInfo(ex.Message));
                        await Task.Run(() => LogInfo(ex.InnerException.ToString()));
                    }
                    finally
                    {
                        driver.Quit();
                        _IterationNumber++;
                    }

                    await Task.Delay(REQUEST_REPEAT_TIME);
                }

                LogInfo("Task finished");
            }
            catch (Exception ex)
            {
                await Task.Run(() => LogInfo(ex.ToString()));
                await Task.Run(() => LogInfo(ex.Message));
                await Task.Run(() => LogInfo(ex.InnerException.ToString()));
                IsRunning = false;
                goto _Link_ExceptionCase; //Why this exception been not processed before - idk (probably loss of internet connection), cause it shall be processed :\
            }
        }

        private void ChangeAppStateAndButtonName()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                StartButton.Text = "Stop";
                TimeBox.Enabled = false;
            }
            else
            {
                ImmediateStart = false;
                IsRunning = false;
                StartButton.Text = "Start";
                TimeBox.Enabled = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (ImmediateStart) StartButton_ClickAsync(this, EventArgs.Empty);
        }
    }
}
