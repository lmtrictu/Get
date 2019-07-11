using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PrintV7
{
    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            // string url = @"G:\C#-Optimus\print.html";

            string[] passInArgs = Environment.GetCommandLineArgs();

            int numberPrint = 1;
            if (passInArgs.Length == 4)
            {
                numberPrint = Int16.Parse(passInArgs[3]);
            }

            string url = passInArgs[2];

            foreach (string _passInArgs in passInArgs)
            {
                if (_passInArgs == "-u")     //kiểm tra sự tồn tại của -u
                {
                    for (int i = 1; i <= numberPrint; i++)
                        PrintPage(url, 1);
                }
                else
                    PrintPage(url, 0);
            }
            formClose.Interval = 10000;
            formClose.Tick += new EventHandler(formClose_Tick);
            formClose.Start();

        }
        Timer formClose = new Timer();
        void formClose_Tick(object sender, EventArgs e) //set time close app
        {
            formClose.Stop();
            formClose.Tick -= new EventHandler(formClose_Tick);
            this.Close();
        }


        private void PrintPage(string url, int isPrint)
        {
            WebBrowser wb = new WebBrowser();
            wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(PrintDocument);
            //wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);

            // wb.Url = new Uri(url);
            wb.Navigate(url);
            if (isPrint == 0)
            {
                wb.Print();
                wb.Dispose();
            }
            string keyName = @"Software\Microsoft\Internet Explorer\PageSetup";
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true))
            {
                if (key != null)
                {
                    key.SetValue("margin_bottom", "0");
                    key.SetValue("margin_left", "0");
                    key.SetValue("margin_right", "0");
                    key.SetValue("margin_top", "0");

                }
            }
        }
        private void PrintDocument(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //webBrowser1.ScrollBarsEnabled = false;
            // webBrowser1.ScriptErrorsSuppressed = true;
            // Print the document now that it is fully loaded.
            ((WebBrowser)sender).Print();
            ((WebBrowser)sender).ShowPageSetupDialog();

            // Dispose the WebBrowser now that the task is complete. 
            //((WebBrowser)sender).Dispose();
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            //webBrowser1.Print();
            webBrowser1.ScrollBarsEnabled = false;
            webBrowser1.ScriptErrorsSuppressed = true;
            //webBrowser1.ShowPrintPreviewDialog();
            // webBrowser1.Print();
            // webBrowser1.Dispose();
        }


    }
}
