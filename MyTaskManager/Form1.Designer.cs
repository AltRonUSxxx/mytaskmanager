namespace MyTaskManager
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listView_main = new System.Windows.Forms.ListView();
            this.button_refresh = new System.Windows.Forms.Button();
            this.button_kill = new System.Windows.Forms.Button();
            this.button_runCalculator = new System.Windows.Forms.Button();
            this.button_browser = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label_status = new System.Windows.Forms.Label();
            this.textBox_whatFind = new System.Windows.Forms.TextBox();
            this.button_find = new System.Windows.Forms.Button();
            this.label_clearFinder = new System.Windows.Forms.Label();
            this.textBox_custom = new System.Windows.Forms.TextBox();
            this.button_remember = new System.Windows.Forms.Button();
            this.timer_refrsh = new System.Windows.Forms.Timer(this.components);
            this.checkBox_isRefresh = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // listView_main
            // 
            this.listView_main.HideSelection = false;
            this.listView_main.Location = new System.Drawing.Point(12, 41);
            this.listView_main.Name = "listView_main";
            this.listView_main.Size = new System.Drawing.Size(765, 157);
            this.listView_main.TabIndex = 0;
            this.listView_main.UseCompatibleStateImageBehavior = false;
            this.listView_main.DoubleClick += new System.EventHandler(this.listView_main_DoubleClick);
            this.listView_main.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_main_MouseClick);
            // 
            // button_refresh
            // 
            this.button_refresh.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_refresh.Location = new System.Drawing.Point(12, 204);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(149, 30);
            this.button_refresh.TabIndex = 1;
            this.button_refresh.Text = "Refresh";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // button_kill
            // 
            this.button_kill.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold);
            this.button_kill.Location = new System.Drawing.Point(628, 204);
            this.button_kill.Name = "button_kill";
            this.button_kill.Size = new System.Drawing.Size(149, 30);
            this.button_kill.TabIndex = 2;
            this.button_kill.Text = "Kill";
            this.button_kill.UseVisualStyleBackColor = true;
            this.button_kill.Click += new System.EventHandler(this.button_kill_Click);
            // 
            // button_runCalculator
            // 
            this.button_runCalculator.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold);
            this.button_runCalculator.Location = new System.Drawing.Point(302, 237);
            this.button_runCalculator.Name = "button_runCalculator";
            this.button_runCalculator.Size = new System.Drawing.Size(149, 30);
            this.button_runCalculator.TabIndex = 3;
            this.button_runCalculator.Text = "Calculator";
            this.button_runCalculator.UseVisualStyleBackColor = true;
            this.button_runCalculator.Click += new System.EventHandler(this.button_runCalculator_Click);
            // 
            // button_browser
            // 
            this.button_browser.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold);
            this.button_browser.Location = new System.Drawing.Point(302, 273);
            this.button_browser.Name = "button_browser";
            this.button_browser.Size = new System.Drawing.Size(149, 30);
            this.button_browser.TabIndex = 4;
            this.button_browser.Text = "Browser";
            this.button_browser.UseVisualStyleBackColor = true;
            this.button_browser.Click += new System.EventHandler(this.button_browser_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold);
            this.button5.Location = new System.Drawing.Point(302, 377);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(149, 30);
            this.button5.TabIndex = 5;
            this.button5.Text = "custom";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold);
            this.button6.Location = new System.Drawing.Point(302, 309);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(149, 30);
            this.button6.TabIndex = 6;
            this.button6.Text = "notePad";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_status.Location = new System.Drawing.Point(251, 414);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(73, 27);
            this.label_status.TabIndex = 7;
            this.label_status.Text = "Status";
            // 
            // textBox_whatFind
            // 
            this.textBox_whatFind.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_whatFind.Location = new System.Drawing.Point(12, 5);
            this.textBox_whatFind.Name = "textBox_whatFind";
            this.textBox_whatFind.Size = new System.Drawing.Size(201, 30);
            this.textBox_whatFind.TabIndex = 8;
            // 
            // button_find
            // 
            this.button_find.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_find.Location = new System.Drawing.Point(248, 6);
            this.button_find.Name = "button_find";
            this.button_find.Size = new System.Drawing.Size(76, 30);
            this.button_find.TabIndex = 9;
            this.button_find.Text = "find";
            this.button_find.UseVisualStyleBackColor = true;
            this.button_find.Click += new System.EventHandler(this.button_find_Click);
            // 
            // label_clearFinder
            // 
            this.label_clearFinder.AutoSize = true;
            this.label_clearFinder.BackColor = System.Drawing.Color.Maroon;
            this.label_clearFinder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_clearFinder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_clearFinder.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_clearFinder.ForeColor = System.Drawing.Color.Red;
            this.label_clearFinder.Location = new System.Drawing.Point(214, 6);
            this.label_clearFinder.Name = "label_clearFinder";
            this.label_clearFinder.Size = new System.Drawing.Size(28, 29);
            this.label_clearFinder.TabIndex = 10;
            this.label_clearFinder.Text = "X";
            this.label_clearFinder.Click += new System.EventHandler(this.label_clearFinder_Click);
            // 
            // textBox_custom
            // 
            this.textBox_custom.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_custom.Location = new System.Drawing.Point(302, 345);
            this.textBox_custom.Name = "textBox_custom";
            this.textBox_custom.Size = new System.Drawing.Size(149, 26);
            this.textBox_custom.TabIndex = 11;
            // 
            // button_remember
            // 
            this.button_remember.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold);
            this.button_remember.Location = new System.Drawing.Point(12, 240);
            this.button_remember.Name = "button_remember";
            this.button_remember.Size = new System.Drawing.Size(149, 30);
            this.button_remember.TabIndex = 12;
            this.button_remember.Text = "remember";
            this.button_remember.UseVisualStyleBackColor = true;
            // 
            // timer_refrsh
            // 
            this.timer_refrsh.Enabled = true;
            this.timer_refrsh.Interval = 5000;
            this.timer_refrsh.Tick += new System.EventHandler(this.timer_refrsh_Tick);
            // 
            // checkBox_isRefresh
            // 
            this.checkBox_isRefresh.AutoSize = true;
            this.checkBox_isRefresh.Checked = true;
            this.checkBox_isRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_isRefresh.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_isRefresh.Location = new System.Drawing.Point(72, 391);
            this.checkBox_isRefresh.Name = "checkBox_isRefresh";
            this.checkBox_isRefresh.Size = new System.Drawing.Size(151, 23);
            this.checkBox_isRefresh.TabIndex = 13;
            this.checkBox_isRefresh.Text = "Refrsh every 5scnd";
            this.checkBox_isRefresh.UseVisualStyleBackColor = true;
            this.checkBox_isRefresh.CheckedChanged += new System.EventHandler(this.checkBox_isRefresh_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkBox_isRefresh);
            this.Controls.Add(this.button_remember);
            this.Controls.Add(this.textBox_custom);
            this.Controls.Add(this.label_clearFinder);
            this.Controls.Add(this.button_find);
            this.Controls.Add(this.textBox_whatFind);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button_browser);
            this.Controls.Add(this.button_runCalculator);
            this.Controls.Add(this.button_kill);
            this.Controls.Add(this.button_refresh);
            this.Controls.Add(this.listView_main);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_main;
        private System.Windows.Forms.Button button_refresh;
        private System.Windows.Forms.Button button_kill;
        private System.Windows.Forms.Button button_runCalculator;
        private System.Windows.Forms.Button button_browser;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.TextBox textBox_whatFind;
        private System.Windows.Forms.Button button_find;
        private System.Windows.Forms.Label label_clearFinder;
        private System.Windows.Forms.TextBox textBox_custom;
        private System.Windows.Forms.Button button_remember;
        private System.Windows.Forms.Timer timer_refrsh;
        private System.Windows.Forms.CheckBox checkBox_isRefresh;
    }
}

