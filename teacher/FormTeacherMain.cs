using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace teacher
{
    public partial class FormTeacherMain : Form
    {
        private string language;

        private string allarmCloseText;
        private string fillNeadableText;
        private string useOnlyLetterAndNumber;
        private string dontUseSpace;
        private string NotMoreThan;
        private string serverIsOff;
        private string uncorrectLoginOrPassword;
        private string moreThan;
        private string shouldSame;
        private string wrongEmail;
        private string successAdding;
        private string alreadyTakenUsername;
        private string alreadyTakenEmail;
        private string failedAdding;
        private string fullname_student;
        private string login_student;
        private string status_student;
        private string group_student;
        private string online_student;
        private string offline_student;
        private string select_row;
        private string successRemoving;
        private string not_found;
        private string group_name_already_taken;
        private string group_students_count;


        public FormTeacherMain(string language_out, string[] alertMessages)
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            panel_students.Visible = false;
            label_student_add_menu_error.Text = "";
            makeSquareCorner();
            language = language_out;
            initDataGridView(dataGridView_groups);
            initDataGridView(dataGridView_students);
            initLanguage(language_out, alertMessages);
            comboBox_student_add_menu_group.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private async void initDataGridView(DataGridView dataGridView)
        {
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);

            dataGridView.DefaultCellStyle.BackColor = Color.White;
            dataGridView.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 12);

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;

            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Только горизонтальные линии
            dataGridView.GridColor = Color.DarkGray;

            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.MultiSelect = false;

            
        }

        private async void initLanguage(string language, string[] alertMessages)
        {
            int index = 0;
            allarmCloseText = alertMessages[index++];
            fillNeadableText = alertMessages[index++];
            useOnlyLetterAndNumber = alertMessages[index++];
            dontUseSpace = alertMessages[index++];
            NotMoreThan = alertMessages[index++];
            serverIsOff = alertMessages[index++];
            uncorrectLoginOrPassword = alertMessages[index++];
            moreThan = alertMessages[index++];

            switch (language)
            {
                case "ru":
                    shouldSame = "Пароли должны быть одинаковыми!";
                    wrongEmail = "Некорректная почта";
                    successAdding = "Успешное добавление!";
                    alreadyTakenUsername = "Этот логин уже занят!";
                    alreadyTakenEmail = "Эта почта уже занята!";
                    failedAdding = "Неизвестная ошибка!";
                    fullname_student = "ФИО";
                    login_student = "Логин";
                    status_student = "Статус";
                    group_student = "Группа";
                    offline_student = "Не в сети";
                    online_student = "В сети";
                    select_row = "Выберете строку";
                    successRemoving = "Успешное удаление";
                    not_found = "не найдено";
                    group_name_already_taken = "Это имя уже занято!";
                    group_students_count = "Учеников";



                    button_groups.Text = "Группы";

                    button_students.Text = "Студенты";
                    label_student_add_menu_first_name.Text = "Имя";
                    label_student_add_menu_last_name.Text = "Фамилия";
                    label_student_add_menu_middle_name.Text = "Отчество";
                    label_student_add_menu_group.Text = "Группа";
                    label_student_add_menu_username.Text = "*Логин";
                    label_student_add_menu_password.Text = "*Пароль";
                    label_student_add_menu_password_confirm.Text = "*Потверждение пароля";
                    label_student_manangment.Text = "Управление студентами";
                    label_student_add_menu_email.Text = "*Почта";
                    button_students_add.Text = "Добавить";
                    button_student_managment_add_menu_add.Text = "Добавить";
                    button_students_remove.Text = "Удалить";
                    button_student_managment_add_menu_cancel.Text = "Отмена";
                    break;
            }

            dataGridView_students.Columns.Add("student_fullname", fullname_student);
            dataGridView_students.Columns.Add("student_login", login_student);
            dataGridView_students.Columns.Add("student_status", status_student);
            dataGridView_students.Columns.Add("student_group", group_student);
            dataGridView_students.Columns[0].Width = 257;
            dataGridView_students.Columns[1].Width = 110;
            dataGridView_students.Columns[2].Width = 100;
            dataGridView_students.Columns[3].Width = 90;

            dataGridView_groups.Columns.Add("group_name", group_student);
            dataGridView_groups.Columns.Add("population", group_students_count);
        }

        private void hideAllPanels()
        {
            panel_students.Visible=false;
            panel_students.Location = new System.Drawing.Point(-368, 33);

            panel_groups.Visible = false;
            panel_groups.Location = new System.Drawing.Point(-368, 33);

        }

        private void disableAllButtons()
        {
            disableButton(button_groups);
            disableButton(button_students);
        }

        Point lastPoint;
        async private void pictureBox_uppestPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
            Thread.Sleep(10);
        }

        async private void pictureBox_uppestPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
            Thread.Sleep(10);
        }

        private void makeSquareCorner()
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                int r = 15;
                Rectangle bounds = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);

                path.AddArc(bounds.X, bounds.Y, r * 2, r * 2, 180, 90);
                path.AddLine(bounds.X + r, bounds.Y, bounds.Right - r, bounds.Y);
                path.AddArc(bounds.Right - r * 2, bounds.Y, r * 2, r * 2, 270, 90);
                path.AddLine(bounds.Right, bounds.Y + r, bounds.Right, bounds.Bottom - r);
                path.AddArc(bounds.Right - r * 2, bounds.Bottom - r * 2, r * 2, r * 2, 0, 90);
                path.AddLine(bounds.Right - r, bounds.Bottom, bounds.X + r, bounds.Bottom);
                path.AddArc(bounds.X, bounds.Bottom - r * 2, r * 2, r * 2, 90, 90);
                path.AddLine(bounds.X, bounds.Bottom - r, bounds.X, bounds.Y + r);

                path.CloseFigure();

                this.Region = new Region(path);
            }
        }

        private async void show_panel(Panel panel)
        {
            panel.Visible = true;
            int x = panel.Location.X;
            int y = panel.Location.Y;
            while (x <= 216)
            {
                panel.Location = new Point(x, y);
                x += 20;
                await Task.Delay(5);
            }
            panel.Location = new Point(216, y);
        }

        private async void hide_panel(Panel panel)
        {
            panel.Visible = true;
            int x = panel.Location.X;
            int y = panel.Location.Y;
            while (x >= -368)
            {
                panel.Location = new Point(x, y);
                x -= 20;
                await Task.Delay(5);
            }
            panel.Location = new Point(-368, y);
            panel.Visible = false;
        }

        private void disableButton(System.Windows.Forms.Button button)
        {
            button.BackColor = Color.Silver;
            button.ForeColor = Color.Black;
        }

        private void enableButton(System.Windows.Forms.Button button)
        {
            button.BackColor = Color.White;
            button.ForeColor = Color.Black;
        }

        private async void button_students_Click(object sender, EventArgs e)
        {
            loadStudents();
            hideAllPanels();
            disableAllButtons();
            show_panel(panel_students);
            enableButton(button_students);
        }

        private async void loadStudents()
        {
            string answer = await Program.client.SendAsync($"GET_STUDENTS");
            dataGridView_students.Rows.Clear();
            string[] students = answer.Split('/');
            foreach (string student in students)
            {
                string[] thisStud = student.Split('|');
                thisStud[2] = thisStud[2].Replace("ONLINE", online_student);
                thisStud[2] = thisStud[2].Replace("OFFLINE", offline_student);
                dataGridView_students.Rows.Add(thisStud);
            }
        }

        private async void loadGroups()
        {
            string answer = await Program.client.SendAsync($"GET_GROUPS");
            dataGridView_groups.Rows.Clear();
            string[] groups = answer.Split('/');
            foreach (string student in groups)
            {
                dataGridView_groups.Rows.Add(student.Split('|'));
            }
        }



        private void button_groups_Click(object sender, EventArgs e)
        {
            loadGroups();
            hideAllPanels();
            disableAllButtons();
            show_panel(panel_groups);
            enableButton(button_groups);
        }

        private void button_students_add_Click(object sender, EventArgs e)
        {
            panel_students_add_menu.Visible = true;
        }

        private void button_student_managment_add_menu_cancel_Click(object sender, EventArgs e)
        {
            panel_students_add_menu.Visible = false;
        }

        private async void button_student_managment_add_menu_add_Click(object sender, EventArgs e)
        {
            string username = textBox_student_add_menu_username.Text;
            string password = textBox_student_add_menu_password.Text;
            string password_confirm = textBox_student_add_menu_password_confirm.Text;
            string email = textBox_student_add_menu_email.Text;

            string firstname = textBox_student_add_menu_first_name.Text;
            string lastname = textBox_student_add_menu_last_name.Text;
            string middlename = textBox_student_add_menu_middle_name.Text;
            string group = comboBox_student_add_menu_group.Text;

            if(password != password_confirm)
            {
                label_student_add_menu_error.Text = shouldSame;
                return;
            }
            if(!isCorrectEmail(email))
            {
                label_student_add_menu_error.Text = wrongEmail;
                return;
            }
            else if (check_validation(password,username))
            {
                string answer = await Program.client.SendAsync($"REGISTER|{username}|{password}|{email}|{firstname}|{lastname}|{middlename}|{group}");
                switch(answer)
                {
                    case "SUCCESS":
                        loadStudents();
                        textBox_student_add_menu_username.Text = "";
                        textBox_student_add_menu_password.Text = "";
                        textBox_student_add_menu_password_confirm.Text = "";
                        textBox_student_add_menu_first_name.Text = "";
                        textBox_student_add_menu_middle_name.Text = "";
                        textBox_student_add_menu_last_name.Text = "";
                        comboBox_student_add_menu_group.Text = "";
                        FormMessageBox message = new FormMessageBox(successAdding, language);
                        message.ShowDialog();
                        panel_students_add_menu.Visible = false;
                        break;
                    case "USERNAME_ALREADY_TAKEN":
                        label_student_add_menu_error.Text = alreadyTakenUsername;
                        break;
                    case "EMAIL_ALREADY_TAKEN":
                        label_student_add_menu_error.Text = alreadyTakenEmail;
                        break;
                    default:
                        label_student_add_menu_error.Text = failedAdding;
                        break;
                }
            }
        }

        private bool isCorrectEmail(string email)
        {
            try
            {
                MailAddress mailCheck = new MailAddress(email);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool check_validation(string password, string username)
        {
            if (password.Length > 32 || username.Length > 32)
            {
                label_student_add_menu_error.Text = NotMoreThan;
                return false;
            }
            if (password.Length < 2 || username.Length < 2)
            {
                label_student_add_menu_error.Text = moreThan;
                return false;
            }
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(username))
            {
                label_student_add_menu_error.Text = fillNeadableText;
                return false;
            }
            foreach (char k in username)
            {
                if (!Char.IsLetterOrDigit(k))
                {
                    label_student_add_menu_error.Text = useOnlyLetterAndNumber;
                    return false;
                }
            }
            if (password.Contains(' ') || username.Contains(' '))
            {
                label_student_add_menu_error.Text = dontUseSpace;
                return false;
            }
            return true;
        }

        private static void showMessage(string line, string language)
        {
            FormMessageBox messageBox = new FormMessageBox(line, language);
            messageBox.ShowDialog();
        }

        private async void button_students_remove_Click(object sender, EventArgs e)
        {
            if(dataGridView_students.SelectedCells.Count > 0)
            {
                int selectedIndex = dataGridView_students.SelectedCells[0].RowIndex;
                string username = dataGridView_students.Rows[selectedIndex].Cells[1].Value.ToString();
                string answer = await Program.client.SendAsync($"REMOVE|{username}");
                switch(answer)
                {
                    case "SUCCESS":
                        loadStudents();
                        showMessage(successRemoving, language);
                        break;
                    case "UNEXPECTED_ERROR":
                        showMessage(failedAdding, language);
                        break;
                    case "NOT_FOUND":
                        showMessage(not_found,language);
                        break;
                }
            }
            else
            {
                showMessage(select_row, language);
            }

        }

        private void button_groups_add_Click(object sender, EventArgs e)
        {
            panel_group_managment_add_menu.Visible = true;
        }

        private async void button_groups_remove_Click(object sender, EventArgs e)
        {
            if (dataGridView_groups.SelectedCells.Count > 0)
            {
                int selectedIndex = dataGridView_groups.SelectedCells[0].RowIndex;
                string username = dataGridView_groups.Rows[selectedIndex].Cells[0].Value.ToString();
                string answer = await Program.client.SendAsync($"REMOVE_GROUP|{username}");
                switch (answer)
                {
                    case "SUCCESS":
                        loadGroups();
                        showMessage(successRemoving, language);
                        break;
                    case "UNEXPECTED_ERROR":
                        showMessage(failedAdding, language);
                        break;
                    case "NOT_FOUND":
                        showMessage(not_found, language);
                        break;
                }
            }
            else
            {
                showMessage(select_row, language);
            }
        }

        private async void button_group_managment_add_menu_add_Click(object sender, EventArgs e)
        {
            if(textBox_group_managment_add_menu_name.Text.Length < 3)
            {
                showMessage(moreThan, language);
                return;
            }
            string name = textBox_group_managment_add_menu_name.Text;
            string answer = await Program.client.SendAsync($"ADD_GROUP|{name}");
            switch(answer)
            {
                case "NAME_ALREADY_TAKEN":
                    label_group_managment_add_menu_error.Text = group_name_already_taken;
                    break;

                case "SUCCESS":
                    loadGroups();
                    textBox_group_managment_add_menu_name.Text = "";
                    panel_group_managment_add_menu.Visible=false;
                    showMessage(successAdding, language);
                    break;
            }
        }

        private void button_group_managment_add_menu_cancel_Click(object sender, EventArgs e)
        {
            panel_group_managment_add_menu.Visible = false;
        }

        private async void comboBox_student_add_menu_group_Click(object sender, EventArgs e)
        {
            string answer = await Program.client.SendAsync($"GET_GROUPS_NAME");
            comboBox_student_add_menu_group.Items.Clear();
            foreach(string group_name in answer.Split('|'))
            {
                comboBox_student_add_menu_group.Items.Add(group_name);
            }
        }
    }
}
