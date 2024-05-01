using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SiteClicker_Parser.Logger;
using static SiteClicker_Parser.SettingsStorage;
using static SiteClicker_Parser.TelegramMessagingProcessor;

namespace SiteClicker_Parser
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static Task Main(string[] args)
        {
            //-StartNow -delay_* <=== implemented startup params, * sign in -delay_ param means some number between 1 and 9

            AppDomain.CurrentDomain.UnhandledException += (sender, arguments) =>
            {
                string exceptionText = arguments.ExceptionObject?.ToString();
                LogInfo(exceptionText);
                SendMessageToTelegram("Unhandled exception been catched and handled. Check the logs for more info");
            };

            Application.ThreadException += (sender, arguments) =>
            {
                string exceptionText = arguments.Exception?.ToString();
                LogInfo(exceptionText);
                SendMessageToTelegram("Unhandled thread exception been catched and handled. Check the logs for more info");
            };

            TaskScheduler.UnobservedTaskException += (sender, arguments) =>
            {
                string exceptionText = arguments.Exception?.ToString();
                LogInfo(exceptionText);
                SendMessageToTelegram("Unobserved task exception been catched and handled. Check the logs for more info");
                arguments.SetObserved();
            };


        _Link_ExceptionCase:
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                MainForm mainForm = new MainForm();

                if (args.Length > 0)
                {
                    foreach (string arg in args)
                    {
                        string processedArg = arg.Trim().ToLowerInvariant();

                        if (processedArg == "-startnow") ImmediateStart = true;

                        if (processedArg.Contains("-delay"))
                        {
                            if (processedArg.Length < 8)
                            {
                                MessageBox.Show("Provide correct value in parameter \"delay\". Shutting down the app...");
                                Environment.Exit(0);
                            }
                            processedArg = processedArg.Substring(0, 8);
                            int symbolIdx = processedArg.IndexOf("_") + 1;
                            string delay = processedArg.Substring(symbolIdx == -1 ? 0 : symbolIdx);
                            bool successfulConversion = int.TryParse(delay, out _);
                            if (successfulConversion)
                            {
                                Set_DefaultDelayTimeOnStartup(mainForm, delay);
                            }
                            else
                            {
                                delay = "3";
                                Set_DefaultDelayTimeOnStartup(mainForm, delay);
                            }
                        }
                    }
                }
                else
                {
                    Set_DefaultDelayTimeOnStartup(mainForm, DEFAULT_REQUEST_REPEAT_TIME_STRING);
                }

                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                LogInfo(ex?.ToString());
                LogInfo(ex?.Message);
                LogInfo(ex?.InnerException?.ToString());
                SendMessageToTelegram("Error during the runtime. Check the logs");
                IsException = true;
                Form.ActiveForm?.Close();
            }
            if (IsException)
            {
                IsException = false;
                goto _Link_ExceptionCase; //Why this exception been not processed before - idk (probably loss of internet connection), cause it shall be processed :\
            }

            return Task.CompletedTask;
        }

        private static void Set_DefaultDelayTimeOnStartup(MainForm mainForm, string delay)
        {
            Control timeBox = mainForm.Controls.Cast<Control>().FirstOrDefault(x => x.Name == "TimeBox");
            Set_RequestRepeatTime(delay);
            timeBox.Text = (REQUEST_REPEAT_TIME / 1000 / 60).ToString();
        }
    }
}
