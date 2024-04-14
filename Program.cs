using System;
using System.Linq;
using System.Windows.Forms;
using static SiteClicker_Parser.SettingsStorage;

namespace SiteClicker_Parser
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //-StartNow -delay_* <=== implemented startup params, * sign in -delay_ param means some number between 1 and 9
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm mainForm = new MainForm();

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
                        Control timeBox = mainForm.Controls.Cast<Control>().FirstOrDefault(x => x.Name == "TimeBox");
                        Set_RequestRepeatTime(delay);
                        timeBox.Text = (REQUEST_REPEAT_TIME / 1000 / 60).ToString();
                    }
                }
            }

            Application.Run(mainForm);
        }
    }
}
