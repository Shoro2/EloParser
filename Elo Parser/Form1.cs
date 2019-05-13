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
                    string[] players = new string[8];
                    int[] elos = new int[8];

                    string players_input = webBrowser1.Document.GetElementById("players").InnerText;
                    string elos_input = webBrowser1.Document.GetElementById("elos").InnerText;
                    int counter = 0;
                    while (players_input.Contains(","))
                    {
                        players[counter] = players_input.Substring(0, players_input.IndexOf(","));
                        players_input = players_input.Substring(players_input.IndexOf(",") + 1);
                        counter++;
                    }
                    players[counter] = players_input;
                    counter = 0;
                    while (elos_input.Contains(","))
                    {
                        elos[counter] = Int32.Parse(elos_input.Substring(0, elos_input.IndexOf(",")));
                        elos_input = elos_input.Substring(elos_input.IndexOf(",") + 1);
                        counter++;
                    }
                    elos[counter] = Int32.Parse(elos_input);
                    if (webBrowser1.Document.GetElementById("elo").InnerText != null)
                    {
                        System.IO.File.WriteAllText(@""+name+".txt", elo);
                        int anzahl_spieler = 1;
                        textBox2.Text = elo;
                        
                        for (int i = 0; i < 8; i++)
                        {
                            if (elos[i] > 0) anzahl_spieler++;
                        }
                        textBox3.Text = (anzahl_spieler-1).ToString();
                        if (anzahl_spieler==5)
                        {
                            System.IO.File.WriteAllText(@"" + name + "_livegame.txt", (players[0] + "(" + elos[0] + "), " + players[1] + "(" + elos[1] + "), " + players[2] + "(" + elos[2] + "), " + players[3] + "(" + elos[3] + ")"));
                        }
                        else if (anzahl_spieler == 8)
                        {
                            System.IO.File.WriteAllText(@"" + name + "_livegame.txt", (players[0] + "(" + elos[0] + "), " + players[1] + "(" + elos[1] + "), " + players[2] + "(" + elos[2] + "), " + players[3] + "(" + elos[3] + "), "+ players[4] + "(" + elos[4] + "), " + players[5] + "(" + elos[5] + "), " + players[6] + "(" + elos[6] + "), " + players[7] + "(" + elos[7] + ")"));
                        }
                    }
                }
                else
                {
                    textBox3.Text = webBrowser1.Url.ToString();
                }
            }
        }

        void aTimer_Tick(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://ltdstats.com/api/playerElo?playername=" + name);
        }
    }
}