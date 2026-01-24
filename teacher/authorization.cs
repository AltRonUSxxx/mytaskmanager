using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace teacher
{
    public partial class authorization : Form
    {
        bool allowClose = false;
        string language = "ru";
        string allarmCloseText;
        public authorization()
        {
            InitializeComponent();
            setLanguage();

            BackgroundWorker Taskmgr_killer = new BackgroundWorker();
            Taskmgr_killer.DoWork += Taskmgr_killer_DoWork;
            Taskmgr_killer.RunWorkerAsync();
        }

        private void Taskmgr_killer_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker Taskmgr_killer = sender as BackgroundWorker;
            while (!Taskmgr_killer.CancellationPending)
            {
                Process[] processes = Process.GetProcesses();
                foreach (Process process in processes)
                {
                    if (process.ProcessName.ToLower().Contains("taskmgr"))
                    {
                        try
                        {
                            process.Kill();
                        }
                        catch
                        {

                        }
                    }
                }
            }

        }
        private void setLanguage()
        {
            switch (language)
            {
                case "ru":
                    allarmCloseText = "Программу нельзя закрыть";
                    button_login.Text = "Войти";
                    break;
                case "en":
                    allarmCloseText = "You can't close this app";
                    button_login.Text = "Login";
                    break;
            }
        }
        private void alarmClose()
        {
            switch (language)
            {
                case "ru":
                    MessageBox.Show(allarmCloseText);
                    break;
                case "en":
                    MessageBox.Show(allarmCloseText);
                    break;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            alarmClose();
        }

        Point lastPoint;
        private void pictureBox_uppestPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
            Thread.Sleep(10);
        }

        private void pictureBox_uppestPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
            Thread.Sleep(10);
        }

        private void authorization_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!allowClose)
            {
                e.Cancel = true;
                alarmClose();
            }
        }

        private void authorization_Leave(object sender, EventArgs e)
        {
            MessageBox.Show("es");
        }

        private void authorization_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.F15)
            {
                allowClose = !allowClose;
            }
        }

        private void allButton_White()
        {
            button_language_EN.BackColor = Color.White;
            button_language_RU.BackColor = Color.White;
        }

        private void button_language_RU_Click(object sender, EventArgs e)
        {
            allButton_White();
            button_language_RU.BackColor = Color.Red;
            language = "ru";
            setLanguage();
        }
    }
}
