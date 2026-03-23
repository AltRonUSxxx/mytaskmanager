using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
        private string serverIsOff;
        private string uncorrectLoginOrPassword;
        private string moreThan;

        private IPEndPoint endPoint;

        private string documentsPath;

        private FormMessageBox messager;

        BackgroundWorker Taskmgr_killer;
        public authorization()
        {
            InitializeComponent();
            setLanguage();
            button_language_RU.BackColor = Color.Gray;
            label_error.ForeColor = Color.Red;
            label_error.Text = "";

            ShowInTaskbar = false;

            Taskmgr_killer = new BackgroundWorker();
            Taskmgr_killer.DoWork += Taskmgr_killer_DoWork;
            Taskmgr_killer.WorkerSupportsCancellation = true;
            Taskmgr_killer.RunWorkerAsync();
        }

        private void authorization_Load(object sender, EventArgs e)
        {
            documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            TopMost = true;
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
                    serverIsOff = "Сервер выключен";
                    uncorrectLoginOrPassword = "Неправильный логин или пароль";
                    moreThan = "Длина не меньше 2 символов";
                    break;
                case "en":
                    allarmCloseText = "You can't close this app";
                    button_login.Text = "Login";
                    fillNeadableText = "Fill in the text";
                    useOnlyLetterAndNumber = "Use only letter and number for username";
                    dontUseSpace = "Don't use space";
                    NotMoreThan = "Lenght no more than 32 characters";
                    serverIsOff = "Server is off";
                    uncorrectLoginOrPassword = "Uncorrect login or password";
                    moreThan = "Lenght no least than 32 characters";
                    break;
            }
        }
        private void alarmClose()
        {
            messager = new FormMessageBox(allarmCloseText, language);
            messager.ShowDialog(this);
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
                label_error.Text = NotMoreThan;
                return false;
            }
            if (textBox_password.Text.Length < 2 || textBox_username.Text.Length < 2)
            {
                label_error.Text = moreThan;
                return false;
            }
            if (string.IsNullOrEmpty(textBox_password.Text) || string.IsNullOrEmpty(textBox_username.Text))
            {
                label_error.Text = fillNeadableText;
                return false;
            }
            foreach (char k in textBox_username.Text)
            {
                if (!Char.IsLetterOrDigit(k))
                {
                    label_error.Text = useOnlyLetterAndNumber;
                    return false;
                }
            }
            if (textBox_password.Text.Contains(' ') || textBox_username.Text.Contains(' '))
            {
                label_error.Text = dontUseSpace;
                return false;
            }
            return true;
        }

        private async void login()
        {
            if (check_validation())
            {
                string answer = await Program.client.SendAsync($"LOGIN|{textBox_username.Text}|{textBox_password.Text}");
                if(answer.StartsWith("SUCCESS"))
                {
                    this.Hide();
                    if (answer.Split('|')[2] == "3")
                    {
                        FormTeacherMain formTeacherMain = new FormTeacherMain();
                        formTeacherMain.Show();
                        Taskmgr_killer.CancelAsync();
                    }
                }
                else
                {
                    label_error.Text = uncorrectLoginOrPassword;
                }
            }
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            login();
        }

        private void authorization_Deactivate(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            this.Focus();
        }
    }
}
