using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SiteClicker_Parser.Logger;
using static SiteClicker_Parser.SettingsStorage;
using static SiteClicker_Parser.TelegramMessagingProcessor;
using static SiteClicker_Parser.WebDriverExtensions;

namespace SiteClicker_Parser
{
    public partial class MainForm : Form
    {
        private readonly ToolTip toolTip;
        public MainForm()
        {
            InitializeComponent();

            Control DebugCheckBox = Controls.Cast<Control>().FirstOrDefault(x => x.Name == "DebugCheckBox");
            toolTip = new ToolTip();

            toolTip.SetToolTip(DebugCheckBox, "And in this mode all the notifications would be sent to Telegram");
            Controls.Add(DebugCheckBox);
        }

        private async void StartButton_ClickAsync(object sender, EventArgs e)
        {
            if (!IsRunning)
            {
                LogInfo("\n");
                LogInfo("**********************************************************************************");
                LogInfo("Task been started");
            }


            IWebDriver driver;
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            ChromeOptions options = new ChromeOptions();

            if (!IsDebug)
            {
                options.AddArgument("window-position=0,0");
                options.AddArgument("headless");
                options.AddArgument("disable-gpu");
                options.AddArgument("no-sandbox");
                options.AddArgument("--window-size=640x480");
                chromeDriverService.HideCommandPromptWindow = true;
            }

            ChangeAppStateAndButtonName();

        _Link_ExceptionCase:
            try
            {
                Set_RequestRepeatTime(TimeBox.Text);
                while (IsRunning)
                {
                    driver = new ChromeDriver(chromeDriverService, options);

                    driver.Navigate().GoToUrl(WEB_ADDRESS);
                    Thread.Sleep(new Random().Next(DELAY, MAX_DELAY));
                    await ClickElement_ById(driver, "cookie_msg_btn_no");

                    for (int i = 1; i < 4; i++)
                    {
                        await MainLogic_GoingThroughSiteAsync(driver, i);
                        if (!IsRunning) break;
                    }

                    driver.Quit();
                    await Task.Delay(REQUEST_REPEAT_TIME);
                }

                LogInfo("Task finished");
            }
            catch (Exception ex)
            {
                await Task.Run(() => LogInfo(ex?.ToString()));
                await Task.Run(() => LogInfo(ex?.Message));
                await Task.Run(() => LogInfo(ex?.InnerException?.ToString()));
                IsException = true;
            }
            if (IsException)
            {
                IsException = false;
                goto _Link_ExceptionCase; //Why this exception been not processed before - idk (probably loss of internet connection), cause it shall be processed :\
            }
        }

        private async Task MainLogic_GoingThroughSiteAsync(IWebDriver driver, int Iteration_TeamNumber)
        {
            try
            {
                LogInfo("Current iteration number: " + _IterationNumber);
                Thread.Sleep(new Random().Next(DELAY, MAX_DELAY));

                foreach (string id in IdsList)
                {
                    string idToProcess = id;
                    if (idToProcess.Contains("plus"))
                    {
                        ///EXPLANATION: 264 = TEAM 1 BUTTON, 267 - TEAM 2, 268 - TEAM 3
                        switch (Iteration_TeamNumber)
                        {
                            case 2:
                                idToProcess = id.Replace("264", "267");
                                break;
                            case 3:
                                idToProcess = id.Replace("264", "268");
                                break;
                            default:
                                break;
                        }
                    }

                    await ClickElement_ById(driver, idToProcess);
                }

                //await ClickElement_ByCssSelector(driver, CSS_SELECTOR);
                string info_TerminAvailability = await ClickElement_ByClassName(driver, CLASS_NAME);
                string message = "(Team N" + Iteration_TeamNumber + ") " + info_TerminAvailability;

                await Task.Run(() => LogInfo(message));

                /*                 
                 * DO NOT FORGET TO COMMENT THE MANUAL ASSIGNATION OF VALUE TO VARIABLE IsDebug AFTER TESTS
                 * THIS MANUAL ASSIGNATION USED TO TEST TELEGRAM NOTIFICATIONS WITHOUT CALLING THE CHROME WINDOW AND TERMINAL WINDOW                 
                 */

                //IsDebug = true;
                if (!message.ToLower().Contains("kein") || IsDebug)
                {
                    message = !message.ToLower().Contains("kein") ? "There's available time slot(s) for Team N" + Iteration_TeamNumber + ". Go and take it!" : message;
                    await SendMessageToTelegramAsync(message);
                }
                //IsDebug = false;

                //Here we go to the start page
                driver.Navigate().GoToUrl(WEB_ADDRESS);
            }
            catch (Exception ex)
            {
                await Task.Run(() => LogInfo(ex?.ToString()));
                await Task.Run(() => LogInfo(ex?.Message));
                await Task.Run(() => LogInfo(ex?.InnerException?.ToString()));
                await SendMessageToTelegramAsync("Error during the runtime. Check the logs");
            }
            finally
            {
                _IterationNumber++;
            }
        }

        private void ChangeAppStateAndButtonName()
        {
            if (StartButton.Text.Contains("Start"))
            {
                DebugCheckBox.Enabled = false;
                IsRunning = true;
                StartButton.Text = "Stop";
                TimeBox.Enabled = false;
            }
            else
            {
                DebugCheckBox.Enabled = true;
                ImmediateStart = false;
                IsRunning = false;
                StartButton.Text = "Start";
                TimeBox.Enabled = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lockFile = new FileStream(LOCK_FILE_PATH, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            if (ImmediateStart) StartButton_ClickAsync(this, EventArgs.Empty);
        }

        private void DebugCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox DebugCheckBox = sender as CheckBox;
            IsDebug = DebugCheckBox.Checked;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Delete_LockFile();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Delete_LockFile();
        }

        private void Delete_LockFile()
        {
            if (lockFile != null && File.Exists(LOCK_FILE_PATH))
            {
                lockFile.Close();
                lockFile = null;
                File.Delete(LOCK_FILE_PATH);
            }
        }
    }
}
