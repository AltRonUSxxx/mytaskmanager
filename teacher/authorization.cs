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
using System.Drawing.Drawing2D;
using System.Net.Sockets;
using System.Net;

namespace teacher
{
    public partial class authorization : Form
    {
        private bool allowClose = false;
        private string language = "ru";
        private string allarmCloseText;
        private string fillNeadableText;
        private string useOnlyLetterAndNumber;
        private string dontUseSpace;
        private string NotMoreThan;
        private Socket client;
        private IPEndPoint endPoint;

        public authorization()
        {
            endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            InitializeComponent();
            setLanguage();
            button_language_RU.BackColor = Color.Gray;

            BackgroundWorker Taskmgr_killer = new BackgroundWorker();
            Taskmgr_killer.DoWork += Taskmgr_killer_DoWork;
            Taskmgr_killer.RunWorkerAsync();

        }

        private void authorization_Load(object sender, EventArgs e)
        {
            client = new Socket
                (
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
                );
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
                    fillNeadableText = "Заполните поля";
                    useOnlyLetterAndNumber = "Используйте только буквы и цифры для имени пользователя";
                    dontUseSpace = "Не используйте пробелы";
                    NotMoreThan = "Длина не больше 32 символовов";
                    break;
                case "en":
                    allarmCloseText = "You can't close this app";
                    button_login.Text = "Login";
                    fillNeadableText = "Fill in the text";
                    useOnlyLetterAndNumber = "Use only letter and number for username";
                    dontUseSpace = "Don't use space";
                    NotMoreThan = "Lenght no more than 32 characters";
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

            button_language_EN.ForeColor = Color.Black;
            button_language_RU.ForeColor = Color.Black;
        }

        private void button_language_RU_Click(object sender, EventArgs e)
        {
            allButton_White();
            button_language_RU.BackColor = Color.Gray;
            button_language_RU.ForeColor = Color.White;
            language = "ru";
            setLanguage();
        }

        private void button_language_EN_Click(object sender, EventArgs e)
        {
            allButton_White();
            button_language_EN.BackColor = Color.Gray;
            button_language_EN.ForeColor = Color.White;
            language = "en";
            setLanguage();
        }

        private void authorization_Resize(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            int radius = 20;

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();

            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(ClientSize.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(ClientSize.Width - radius, ClientSize.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, ClientSize.Height - radius, radius, radius, 90, 90);

            path.CloseFigure();
            this.Region = new Region(path);
        }

        private bool check_validation()
        {
            if(textBox_password.Text.Length > 32 || textBox_username.Text.Length > 32)
            {
                MessageBox.Show(NotMoreThan);
                return false;
            }
            if (string.IsNullOrEmpty(textBox_password.Text) || string.IsNullOrEmpty(textBox_username.Text))
            {
                MessageBox.Show(fillNeadableText);
                return false;
            }
            foreach (char k in textBox_username.Text)
            {
                if (!Char.IsLetterOrDigit(k))
                {
                    MessageBox.Show(useOnlyLetterAndNumber);
                    return false;
                }
            }
            if (textBox_password.Text.Contains(' ') || textBox_username.Text.Contains(' '))
            {
                MessageBox.Show(dontUseSpace);
                return false;
            }
            return true;
        }

        private void login()
        {
            if (check_validation())
            {
                client.Connect(endPoint);
                string message = $"{textBox_username.Text} {textBox_password.Text}";
                byte
            }
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            login();
        }
    }
}
