using System;
using System.Windows.Forms;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            MainForm mainForm = new MainForm();

            foreach (string arg in args) 
            {
                string processedArg = arg.Trim().ToLowerInvariant();

                if (processedArg == "-startnow") SettingsStorage.ImmediateStart = true;

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
                    foreach (Control item in mainForm.Controls)
                    {                            
                        if(item is TextBox && 
                           item.Name == "TimeBox" && 
                           successfulConversion)
                        {
                            SettingsStorage.Set_RequestRepeatTime(delay);
                            item.Text = (SettingsStorage.REQUEST_REPEAT_TIME / 1000 / 60).ToString();
                        }
                    }
                }
            }

            Application.Run(mainForm);
        }
    }
}
