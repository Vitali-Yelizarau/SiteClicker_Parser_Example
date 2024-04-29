using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SiteClicker_Parser.SettingsStorage;
using static SiteClicker_Parser.Logger;
using static SiteClicker_Parser.WebDriverExtensions;
using System.Runtime.CompilerServices;

namespace SiteClicker_Parser
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static async Task Main(string[] args)
        {
        //-StartNow -delay_* <=== implemented startup params, * sign in -delay_ param means some number between 1 and 9
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
                await Task.Run(() => LogInfo(ex.ToString()));
                await Task.Run(() => LogInfo(ex.Message));
                await Task.Run(() => LogInfo(ex.InnerException.ToString()));
                IsException = true;
                Form.ActiveForm.Close();
            }
            if (IsException)
            {
                IsException = false;
                goto _Link_ExceptionCase; //Why this exception been not processed before - idk (probably loss of internet connection), cause it shall be processed :\
            }

        }

        private static void Set_DefaultDelayTimeOnStartup(MainForm mainForm, string delay)
        {
            Control timeBox = mainForm.Controls.Cast<Control>().FirstOrDefault(x => x.Name == "TimeBox");
            Set_RequestRepeatTime(delay);
            timeBox.Text = (REQUEST_REPEAT_TIME / 1000 / 60).ToString();
        }
    }
}
