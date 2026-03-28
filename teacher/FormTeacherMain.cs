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
        private string[] idToReverse;

        private bool isUserRedacting;
        private bool isLessonRedacting;

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
        private string unexpected_error;
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
        private string lessons_theme;
        private string lessons_group_id;
        private string lessons_start_date;
        private string lessons_start_time;
        private string lesson_status;
        private string status_canceled;
        private string status_completed;
        private string status_procesing;
        private string status_watiting;
        private string student_redact;
        private string student_add;
        private string old_password;
        private string new_password;
        private string student_password;
        private string student_password_confirm;
        private string students_student_id;
        private string by;
        private string groups_group_id;



        public FormTeacherMain(string language_out, string[] alertMessages)
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            panel_students.Visible = false;
            label_student_add_menu_error.Text = "";
            idToReverse = new string[0];
            isUserRedacting = false;
            isLessonRedacting = false;
            makeSquareCorner();
            language = language_out;
            initDataGridView(dataGridView_groups);
            initDataGridView(dataGridView_students);
            initDataGridView(dataGridView_lessons);

            initDataGridView(dataGridView_groups_redact_group_in_group);
            initDataGridView(dataGridView_groups_redact_group_not_in_group);
            initLanguage(language_out, alertMessages);

            initInOutGrop(dataGridView_groups_redact_group_in_group);
            initInOutGrop(dataGridView_groups_redact_group_not_in_group);

            comboBox_student_add_menu_group.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_lessons_add_menu_group.DropDownStyle = ComboBoxStyle.DropDownList;
            
            datetimepicker_lessons_managment_add_menu_start.Format = DateTimePickerFormat.Custom;
            datetimepicker_lessons_managment_add_menu_end.Format = DateTimePickerFormat.Custom;
            datetimepicker_lessons_managment_add_menu_start.CustomFormat = "HH:mm";
            datetimepicker_lessons_managment_add_menu_end.CustomFormat = "HH:mm";
            datetimepicker_lessons_managment_add_menu_start.ShowUpDown = true;
            datetimepicker_lessons_managment_add_menu_end.ShowUpDown = true;
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
                    unexpected_error = "Неизвестная ошибка!";
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
                    lessons_theme = "Тема";
                    lessons_group_id = "Группа";
                    lesson_status = "Статус";
                    lessons_start_date = "Дата";
                    lessons_start_time = "Время";
                    status_canceled  = "Отменен";
                    status_completed = "Завершен";
                    status_procesing = "В процесе";
                    status_watiting  = "Ожидание";
                    student_redact = "Изменить";
                    student_add = "Добавить";
                    old_password = "Старый пароль";
                    new_password = "Новый пароль";
                    student_password = "*Пароль";
                    student_password_confirm = "*Потверждение пароля";
                    students_student_id = "Студент Айди:\n";
                    by = "с"; // Не в сети С 22:22
                    groups_group_id = "Групп Айди:\n";



                    button_groups.Text = "Группы";
                    label_groups_managment.Text = "Управление группами";
                    button_group_managment_add_menu_add.Text = "Добавить";
                    button_group_managment_add_menu_cancel.Text = "Отмена";
                    button_groups_remove.Text = "Удалить";
                    button_groups_add.Text = "Добавить";
                    label_group_managment_add_menu_name.Text = "Название";
                    label_group_managment_add_menu_error.Text = "";

                    button_lessons.Text = "Занятия";
                    label_lessons_managment.Text = "Управление занятиями";
                    button_lessons_add.Text = "Добавить";
                    button_lessons_remove.Text = "Удалить";
                    label_lessons_managment_add_menu_theme.Text = "Тема";
                    label_lessons_managment_add_menu_data.Text = "Дата";
                    label_lessons_add_menu_start.Text = "Начало";
                    label_lessons_add_menu_end.Text = "Конец";
                    label_lessons_add_menu_group.Text = "Группа";
                    label_lessons_add_menu_error.Text = "";
                    button_lessons_managment_add_menu_add.Text = "Добавить";
                    button_lessons_managment_add_menu_cancel.Text = "Отмена";

                    button_students.Text = "Студенты";
                    label_student_add_menu_first_name.Text = "Имя";
                    label_student_add_menu_last_name.Text = "Фамилия";
                    label_student_add_menu_middle_name.Text = "Отчество";
                    label_student_add_menu_group.Text = "Группа";
                    label_student_add_menu_username.Text = "*Логин";
                    label_student_add_menu_password.Text = student_password;
                    label_student_add_menu_password_confirm.Text = student_password_confirm;
                    label_student_manangment.Text = "Управление студентами";
                    label_student_add_menu_email.Text = "*Почта";
                    button_students_add.Text = "Добавить";
                    button_student_managment_add_menu_add.Text = "Добавить";
                    button_students_remove.Text = "Удалить";
                    button_student_managment_add_menu_cancel.Text = "Отмена";
                    break;
            }

            dataGridView_students.Columns.Add("id", "id");
            dataGridView_students.Columns.Add("student_fullname", fullname_student);
            dataGridView_students.Columns.Add("student_login", login_student);
            dataGridView_students.Columns.Add("student_status", status_student);
            dataGridView_students.Columns.Add("student_group", group_student);
            dataGridView_students.Columns[0].Visible = false;
            dataGridView_students.Columns[1].Width = 257;
            dataGridView_students.Columns[2].Width = 110;
            dataGridView_students.Columns[3].Width = 100;
            dataGridView_students.Columns[4].Width = 90;

            dataGridView_groups.Columns.Add("id", "id");
            dataGridView_groups.Columns.Add("group_name", group_student);
            dataGridView_groups.Columns.Add("group_population", group_students_count);

            dataGridView_lessons.Columns.Add("id", "id");
            dataGridView_lessons.Columns.Add("lessons_theme", lessons_theme);
            dataGridView_lessons.Columns.Add("lessons_group_id", lessons_group_id);
            dataGridView_lessons.Columns.Add("lesson_status", lesson_status);
            dataGridView_lessons.Columns.Add("lessons_start_date", lessons_start_date);
            dataGridView_lessons.Columns.Add("lessons_start_time", lessons_start_time);
            dataGridView_lessons.Columns[0].Visible = false;
            dataGridView_lessons.Columns[1].Width = 177;
            dataGridView_lessons.Columns[2].Width = 60;
            dataGridView_lessons.Columns[3].Width = 80;
            dataGridView_lessons.Columns[4].Width = 70;
            dataGridView_lessons.Columns[5].Width = 70;

            
        }

        private void initInOutGrop(DataGridView gridView)
        {
            gridView.Columns.Add("JUST", "JUST");
            gridView.Columns[0].Visible = false;
            gridView.Columns.Add("id", "id");
            gridView.Columns[1].Visible = false;
            gridView.Columns.Add("group_id", "group_id");
            gridView.Columns[2].Visible = false;
            gridView.Columns.Add("fullname_student", fullname_student);
            gridView.Columns.Add("student_login", login_student);

            gridView.Columns[4].Width = 80;
        }

        private void hideAllPanels()
        {
            panel_students.Visible=false;
            panel_students.Location = new System.Drawing.Point(-368, 33);

            panel_groups.Visible = false;
            panel_groups.Location = new System.Drawing.Point(-368, 33);

            panel_lessons.Visible = false;
            panel_lessons.Location = new System.Drawing.Point(-368, 33);
        }

        private void disableAllButtons()
        {
            disableButton(button_groups);
            disableButton(button_students);
            disableButton(button_lessons);
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
                thisStud[3] = thisStud[3].Replace("ONLINE", online_student);
                thisStud[3] = thisStud[3].Replace("OFFLINE", offline_student);
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
            label_students_student_id.Visible = false;
            if(!isUserRedacting)
            {
                label_student_managment_add_menu_satus_time.Visible = false;
            }
        }

        private void button_student_managment_add_menu_cancel_Click(object sender, EventArgs e)
        {
            stop_user_redacting();
            panel_students_add_menu.Visible = false;

            textBox_student_add_menu_username.Text = "";
            textBox_student_add_menu_first_name.Text = "";
            textBox_student_add_menu_last_name.Text = "";
            textBox_student_add_menu_middle_name.Text = "";
            textBox_student_add_menu_email.Text = "";
        }

        private async void button_student_managment_add_menu_add_Click(object sender, EventArgs e)
        {
            label_student_add_menu_error.Text = "";
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
                string answer;
                if (isUserRedacting)
                {
                    string id = label_students_student_id.Text.Split('\n')[1];
                    answer = await Program.client.SendAsync($"REDACT_USER|{id}|{username}|{password}|{password_confirm}|{email}|{firstname}|{lastname}|{middlename}|{group}");
                }
                else
                {
                    answer = await Program.client.SendAsync($"REGISTER|{username}|{password}|{email}|{firstname}|{lastname}|{middlename}|{group}");
                }
                switch (answer)
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
                        textBox_student_add_menu_email.Text = "";
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
                        label_student_add_menu_error.Text = unexpected_error;
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
            if ((!isUserRedacting && password.Length < 2) || username.Length < 2)
            {
                label_student_add_menu_error.Text = moreThan;
                return false;
            }
            if ((!isUserRedacting && string.IsNullOrEmpty(password)) || string.IsNullOrEmpty(username))
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
                string username = dataGridView_students.Rows[selectedIndex].Cells[2].Value.ToString();
                string answer = await Program.client.SendAsync($"REMOVE|{username}");
                switch(answer)
                {
                    case "SUCCESS":
                        loadStudents();
                        showMessage(successRemoving, language);
                        break;
                    case "UNEXPECTED_ERROR":
                        showMessage(unexpected_error, language);
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
                string username = dataGridView_groups.Rows[selectedIndex].Cells[1].Value.ToString();
                string answer = await Program.client.SendAsync($"REMOVE_GROUP|{username}");
                switch (answer)
                {
                    case "SUCCESS":
                        loadGroups();
                        showMessage(successRemoving, language);
                        break;
                    case "UNEXPECTED_ERROR":
                        showMessage(unexpected_error, language);
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
            if(textBox_group_managment_add_menu_name.Text.Length < 2)
            {
                label_group_managment_add_menu_error.Text = moreThan;
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

        private void button_lessons_managment_add_menu_cancel_Click(object sender, EventArgs e)
        {
            panel_lessons_managment_add_menu.Visible = false;
        }

        private void button_lessons_add_Click(object sender, EventArgs e)
        {
            panel_lessons_managment_add_menu.Visible = true;
        }

        private async void button_lessons_Click(object sender, EventArgs e)
        {
            loadLessons();
            hideAllPanels();
            disableAllButtons();
            show_panel(panel_lessons);
            enableButton(button_lessons);
        }

        private string init_status_name(string status)
        {
            switch (status)
            {
                case "CANCELED":
                    return status_canceled;
                case "COMPLETED":
                    return status_completed;
                case "PROCESING":
                    return status_procesing;
                case "WAITING":
                    return status_watiting;
                default:
                    return status;
            }
        }

        private async void loadLessons()
        {
            try
            {
                string answer = await Program.client.SendAsync($"GET_LESSONS");
                dataGridView_lessons.Rows.Clear();
                string[] lessons = answer.Split('|');
                foreach (string this_lesson in lessons)
                {
                    string[] thisStud = this_lesson.Split('/');
                    try
                    {
                        thisStud[3] = init_status_name(thisStud[3]);
                        dataGridView_lessons.Rows.Add(thisStud);
                    }
                    catch { }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private async void comboBox_lessons_add_menu_group_Click(object sender, EventArgs e)
        {
            string answer = await Program.client.SendAsync($"GET_GROUPS_NAME");
            comboBox_lessons_add_menu_group.Items.Clear();
            foreach (string group_name in answer.Split('|'))
            {
                comboBox_lessons_add_menu_group.Items.Add(group_name);
            }
        }

        private async void button_lessons_managment_add_menu_add_Click(object sender, EventArgs e)
        {
            DateTime baseTime = dateTimePicker_lessons_managment_add_menu_date.Value;
            DateTime startTime = datetimepicker_lessons_managment_add_menu_start.Value;
            DateTime endTime = datetimepicker_lessons_managment_add_menu_end.Value;
            if(startTime > endTime)
            {
                DateTime temp = startTime;
                startTime = endTime;
                endTime = temp;
            }
            string theme = textBox_lessons_managment_add_menu_theme.Text;
            theme = theme.Replace("|","");
            string group = comboBox_lessons_add_menu_group.Text;

            string request = $"REGISTER_LESSON|{baseTime.Day}|{baseTime.Month}|{baseTime.Year}|{startTime.Hour}|{startTime.Minute}|{endTime.Hour}|{endTime.Minute}|{theme}|{group}";
            string answer = await Program.client.SendAsync(request);
            switch(answer)
            {
                case "SUCCESS":
                    showMessage(successAdding, language);
                    panel_lessons_managment_add_menu.Visible = false;
                    textBox_lessons_managment_add_menu_theme.Text = "";
                    comboBox_lessons_add_menu_group.Text = "";
                    datetimepicker_lessons_managment_add_menu_start.Text = DateTime.Now.ToString();
                    datetimepicker_lessons_managment_add_menu_end.Text = DateTime.Now.ToString();
                    dateTimePicker_lessons_managment_add_menu_date.Text = DateTime.Now.ToString();
                    break;
                case "UNEXPECTED_ERROR":
                    showMessage(unexpected_error, language);
                    break;
            }
        }

        private async void button_lessons_remove_Click(object sender, EventArgs e)
        {
            if (dataGridView_lessons.SelectedCells.Count > 0)
            {
                int selectedIndex = dataGridView_lessons.SelectedCells[0].RowIndex;
                string index = dataGridView_lessons.Rows[selectedIndex].Cells[0].Value.ToString();
                string answer = await Program.client.SendAsync($"REMOVE_LESSON|{index}");
                switch (answer)
                {
                    case "SUCCESS":
                        showMessage(successRemoving, language);
                        loadLessons();
                        break;
                    case "UNEXPECTED_ERROR":
                        showMessage(unexpected_error, language);
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

        private async void stop_user_redacting()
        {
            isUserRedacting = false;
            button_student_managment_add_menu_add.Text = student_add;
            label_students_student_id.Text = students_student_id;
            label_student_managment_add_menu_satus_time.Visible = false;
            label_students_student_id.Visible = false;
            button_student_managment_add_menu_add.Text = student_add;
            label_student_add_menu_password.Text = student_password;
            label_student_add_menu_password_confirm.Text = student_password_confirm;
        }

        private async void redact_student(DataGridViewRow row)
        {
            string username = row.Cells[2].Value.ToString();
            string answer = await Program.client.SendAsync($"GET_FULL_USER|{username}");
            switch (answer)
            {
                case "UNEXPECTED_ERROR":
                    showMessage(unexpected_error, language);
                    break;
                case "NOT_FOUND":
                    showMessage(not_found, language);
                    break;
                default:
                    button_student_managment_add_menu_add.Text = student_redact;
                    panel_students_add_menu.Visible = true;
                    label_students_student_id.Visible = true;
                    label_student_managment_add_menu_satus_time.Visible=true;
                    label_students_student_id.Text = students_student_id + row.Cells[0].Value.ToString();
                    isUserRedacting = true;
                    panel_students_add_menu.Visible = true;
                    string[] userdata = answer.Split('|');
                    textBox_student_add_menu_username.Text = userdata[1];
                    textBox_student_add_menu_first_name.Text = userdata[2];
                    textBox_student_add_menu_last_name.Text = userdata[3];
                    textBox_student_add_menu_middle_name.Text = userdata[4];
                    textBox_student_add_menu_email.Text = userdata[5];
                    label_student_managment_add_menu_satus_time.Text = $"{userdata[7].Replace("True",online_student).Replace("False",offline_student)} {by} {userdata[8]}";

                    textBox_student_add_menu_password.Text = "";
                    textBox_student_add_menu_password_confirm.Text = "";

                    label_student_add_menu_password.Text = old_password;
                    label_student_add_menu_password_confirm.Text = new_password;
                    isUserRedacting = true;
                    break;
            }
        }

        private void panel_students_add_menu_VisibleChanged(object sender, EventArgs e)
        {
            if(panel_students_add_menu.Visible)
            {
                label_student_add_menu_error.Text = "";
            }
        }

        private void dataGridView_students_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_students.SelectedCells.Count > 0)
            {
                redact_student(dataGridView_students.Rows[dataGridView_students.SelectedCells[0].RowIndex]);
            }
        }

        private void panel_students_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView_students.SelectedCells.Count > 0)
            {
                dataGridView_students.SelectedCells[0].Selected = false;
            }
        }

        private async void load_users_by_id(string[] users_id, int group_id, DataGridView inGroup, DataGridView outGroup)
        {
            inGroup.Rows.Clear();
            outGroup.Rows.Clear();
            foreach(string user_id in users_id)
            {
                string[] answer = (await Program.client.SendAsync($"GET_USER_FIO_USERNAME_GROUPID|{user_id}")).Split('|');
                if (Convert.ToInt32(answer[2]) == group_id)
                {
                    inGroup.Rows.Add(answer);
                }
                else
                {
                    outGroup.Rows.Add(answer);
                }
            }

        }

        private async void redact_group(DataGridViewRow row)
        {
            string answer = await Program.client.SendAsync($"GET_USERS_ID");
            if(answer.StartsWith("UNEXPECTED_ERROR"))
            {
                showMessage(unexpected_error, language);
                return;
            }
            else
            {
                panel_groups_redact_group.Visible = true;
                int id = Convert.ToInt32(row.Cells[0].Value.ToString());
                load_users_by_id(answer.Split('|')[1].Split('/'), id, dataGridView_groups_redact_group_in_group, dataGridView_groups_redact_group_not_in_group);
                string group_name = row.Cells[1].Value.ToString();
                textBox_groups_redact_group.Text = group_name;
                label_groups_redact_group_id.Text = groups_group_id + id;
            }
        }

        private void dataGridView_groups_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_groups.SelectedCells.Count > 0)
            {
                redact_group(dataGridView_groups.Rows[dataGridView_groups.SelectedCells[0].RowIndex]);
            }
        }

        private void stop_group_redact()
        {
            panel_groups_redact_group.Visible=false;
        }

        private void button_groups_redact_group_Click(object sender, EventArgs e)
        {
            stop_group_redact();
            idToReverse = new string[0];
        }

        private async void button_groups_redact_group_add_Click(object sender, EventArgs e)
        {
            if(textBox_groups_redact_group.Text.Length < 3)
            {
                showMessage(moreThan, language);
                return;
            }
            string answer = await Program.client.SendAsync($"REVERSE_GROUP_ID|{label_groups_redact_group_id.Text.Split('\n')[1]}|1|{string.Join("/",idToReverse)}");
            showMessage(answer, language);   
        }

        private void toReverse(string id)
        {
            if(idToReverse.Contains(id))
            {
                idToReverse = idToReverse.Where(x => x != id).ToArray();
            }
            else
            {
                idToReverse = idToReverse.Append(id).ToArray();
            }
        }

        private void dataGridView_groups_redact_group_in_group_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_groups_redact_group_in_group.SelectedCells.Count > 0)
            {
                DataGridViewRow row = dataGridView_groups_redact_group_in_group.Rows[e.RowIndex];
                dataGridView_groups_redact_group_in_group.Rows.Remove(row);
                dataGridView_groups_redact_group_not_in_group.Rows.Add(row);
                toReverse(row.Cells[1].Value.ToString());
            }
        }

        private void dataGridView_groups_redact_group_not_in_group_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_groups_redact_group_not_in_group.SelectedCells.Count > 0)
            {
                DataGridViewRow row = dataGridView_groups_redact_group_not_in_group.Rows[e.RowIndex];
                dataGridView_groups_redact_group_not_in_group.Rows.Remove(row);
                dataGridView_groups_redact_group_in_group.Rows.Add(row);
                toReverse(row.Cells[1].Value.ToString());
            }
        }

        private void redact_lesson(DataGridViewRow row)
        {
            panel_lessons_managment_add_menu.Visible = true;
            isLessonRedacting = true;
        }

        private void dataGridView_lessons_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(!(dataGridView_lessons.SelectedCells.Count > 0))
            {
                return;
            }
            redact_lesson(dataGridView_lessons.Rows[e.RowIndex]);
        }
    }
}
