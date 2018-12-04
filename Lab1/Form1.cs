using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        List<int[]> data = new List<int[]>();
        string[] months = new string[] {"January","February","March","April","May","June","July","August","September",
                "Ocotober","November","December"
            };

        public Form1()
        {
            InitializeComponent();
            for(int i = 0; i < 7; i++)
            {
                data.Add(new int[12]);
            }

        }

        private void clearTxts()
        {
            txtAgent.Clear();
            txtMonth.Clear();
            txtShowing.Clear();
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            clearTxts();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = "";

                int agentId;
                int.TryParse(txtAgent.Text, out agentId);
                if (agentId < 1 || agentId > 7)
                {
                    msg += "Please enter a valid Agent ID" + Environment.NewLine;
                    txtAgent.Clear();
                }
                int month;
                int.TryParse(txtMonth.Text, out month);
                if (month < 1 || month > 12)
                {
                    msg += "Please enter a valid Month" + Environment.NewLine;
                    txtMonth.Clear();
                }
                int showing;
                int.TryParse(txtShowing.Text, out showing);
                if (showing == 0)
                {
                    msg += "Please enter a valid number of showings" + Environment.NewLine;
                    txtShowing.Clear();
                }

                if(msg == "")
                {
                    data[agentId - 1][month - 1] = showing;
                    clearTxts();
                }

                MessageBox.Show(msg == "" ? "Saved: " + showing + " showings by Agent #" + agentId + " in " + months[month - 1] : msg);

            }
            catch
            {
                MessageBox.Show("Cannot Insert new record, please contact Admin for help");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            data.Clear();
            for (int i = 0; i < 7; i++)
            {
                data.Add(new int[12]);
            }
            clearTxts();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "txt files (*.txt)|*.txt";
                sfd.DefaultExt = "txt";
                sfd.ShowDialog();


                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write("Showings Tracking Information ({0}){1}{2}", DateTime.Now.Date.ToShortDateString(),Environment.NewLine,Environment.NewLine );
                    for (int i = 0; i < data.Count; i++)
                    {
                        sw.Write("Agent #{0}{1}",(i + 1),Environment.NewLine);
                        for (int m = 0; m < data[i].Length; m++)
                        {
                            sw.Write("\t{0}: {1}{2}", months[m],data[i][m], Environment.NewLine);
                        }
                        sw.Write(Environment.NewLine);

                    }

                }
                fs.Close();
            }
            catch
            {
                MessageBox.Show("Cannot save the file, please contact the Admin for help");
            }
        }
    }
}
