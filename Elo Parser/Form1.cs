using System;
using System.Timers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Elo_Parser
{
    public partial class EloParser : Form
    {
        string name = "";
        Boolean go = false;
        System.Timers.Timer aTimer = new System.Timers.Timer();

        public EloParser()
        {
            InitializeComponent();
        }

        private void EloParser_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!go)
            {
                go = true;
                button1.Text = "Stop";
                name = textBox1.Text;
                webBrowser1.Navigate("https://ltdstats.com/api/playerElo?playername=" + name);
                aTimer.Elapsed += new ElapsedEventHandler(aTimer_Tick);
                aTimer.Interval = 60000;
                aTimer.Enabled = true;
            }
            else
            {
                go = false;
                button1.Text = "Start";
            }
        }

        public void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (go)
            {
                if (webBrowser1.Url.ToString().Contains("api"))
                {
                    string elo = webBrowser1.Document.GetElementById("elo").InnerText.ToString();
                    if(webBrowser1.Document.GetElementById("elo").InnerText != null)
                    {
                        System.IO.File.WriteAllText(@""+name+".txt", elo);
                        textBox2.Text = elo;
                    }
                    
                }
            }
        }

        void aTimer_Tick(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://ltdstats.com/api/playerElo?playername=" + name);
        }
    }
}