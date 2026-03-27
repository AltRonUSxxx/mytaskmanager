using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MyTaskManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializationListViewMain();
            RefreshProcessesList();
            timer_refrsh.Interval = 5000;
        }

        private void RefreshProcessesList()
        {
            ListViewItem selected = listView_main.SelectedItems.Count > 0 ? listView_main.SelectedItems[0] : null;
            listView_main.Items.Clear();

            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                try
                {
                    ListViewItem item = new ListViewItem(process.ProcessName);
                    item.SubItems.Add(process.Id.ToString());
                    item.SubItems.Add((process.WorkingSet64 / 1024 / 1024).ToString());
                    item.SubItems.Add(string.IsNullOrEmpty(process.MainWindowTitle) ? "[Nothing]" : process.MainWindowTitle);
                    listView_main.Items.Add(item);
                }
                catch
                {
                    ListViewItem item = new ListViewItem(process.ProcessName + "[ system]");
                    item.SubItems.Add(process.Id.ToString());
                    item.SubItems.Add("[N'A]");
                    item.SubItems.Add("Protected");
                    listView_main.Items.Add(item);
                    
                }
            }

            label_status.Text = "Loaded: " + processes.Length.ToString() + " processes";

            if (selected != null)
            {
                foreach (ListViewItem item in listView_main.Items)
                {
                    if (item.Text == selected.Text && item.SubItems[1].Text == selected.SubItems[1].Text)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
        }

        private void RefreshProcessesList(string WhatFind)
        {
            ListViewItem selected = listView_main.SelectedItems.Count > 0 ? listView_main.SelectedItems[0] : null;
            listView_main.Items.Clear();

            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (!process.ProcessName.ToLower().Contains(WhatFind.ToLower()))
                {
                    continue;
                }
                try
                {
                    ListViewItem item = new ListViewItem(process.ProcessName);
                    item.SubItems.Add(process.Id.ToString());
                    item.SubItems.Add((process.WorkingSet64 / 1024 / 1024).ToString());
                    item.SubItems.Add(string.IsNullOrEmpty(process.MainWindowTitle) ? "[Nothing]" : process.MainWindowTitle);
                    listView_main.Items.Add(item);
                }
                catch
                {
                    ListViewItem item = new ListViewItem(process.ProcessName + "[ system]");
                    item.SubItems.Add(process.Id.ToString());
                    item.SubItems.Add("[N'A]");
                    item.SubItems.Add("Protected");
                    listView_main.Items.Add(item);

                }
            }

            label_status.Text = "Loaded: " + processes.Length.ToString() + " processes";
        }

        private void InitializationListViewMain()
        {
            listView_main.View = View.Details;
            listView_main.FullRowSelect = true;
            listView_main.GridLines = true;

            listView_main.Columns.Add("Task name", 150);
            listView_main.Columns.Add("Id", 75);
            listView_main.Columns.Add("MB", 100);
            listView_main.Columns.Add("Title", 205);


        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            RefreshProcessesList();
        }

        private void button_kill_Click(object sender, EventArgs e)
        {
            if (listView_main.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nothing selected");
                return;
            }

            ListViewItem selected = listView_main.SelectedItems[0];
            string processName = selected.Text;
            string processId = selected.SubItems[0].Text;

            DialogResult result = MessageBox.Show
                (
                "Do you agree to delete " + processName + "?\n" +
                "Id: " + processId.ToString(), "Ask", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                );

            if ( result == DialogResult.Yes )
            {
                try
                {
                    Process process = Process.GetProcessById(int.Parse(processId));
                    process.Kill();
                    process.WaitForExit(1000);
                    MessageBox.Show("Process " + processName + " succesfuly killed!");
                    RefreshProcessesList();
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private bool RunProgramm(string progName, string arg = "")
        {
            try
            {
                bool isAlreadyRunning = Process.GetProcesses().Any(p => p.ProcessName.ToLower().Contains(progName.ToLower()));

                if (isAlreadyRunning)
                {
                    DialogResult result = MessageBox.Show
                        (
                        "Programm already started", "Start one more?", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                        );
                    if (result == DialogResult.No)
                    {
                        return false;
                    }
                }

                ProcessStartInfo startInfo = new ProcessStartInfo(progName, arg);

                Process.Start(startInfo);
                RefreshProcessesList();
                label_status.Text = "succesfuly started " + progName;
                return true;
            }
            catch
            {
                MessageBox.Show("Error");
                return false;
            }
        }


        private void button_find_Click(object sender, EventArgs e)
        {
            RefreshProcessesList(textBox_whatFind.Text.Trim());
        }

        private void label_clearFinder_Click(object sender, EventArgs e)
        {
            textBox_whatFind.Text = "";
        }

        private void button_runCalculator_Click(object sender, EventArgs e)
        {
            RunProgramm("calc.exe");
        }

        private void button_browser_Click(object sender, EventArgs e)
        {
            RunProgramm("msedge.exe");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RunProgramm("notepad.exe");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RunProgramm(textBox_custom.Text + ".exe");
        }

        ListViewItem what_kill = null;
        private void listView_main_MouseClick(object sender, MouseEventArgs e)
        {
            if (listView_main.SelectedItems.Count > 0)
            {
                what_kill = listView_main.SelectedItems[0];
            }
        }

        private void listView_main_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (listView_main.SelectedItems.Count > 0)
                {
                    if (listView_main.SelectedItems[0] == what_kill || what_kill == null)
                    {
                        Process process = Process.GetProcessById(int.Parse(listView_main.SelectedItems[0].SubItems[1].Text));
                        process.Kill();
                    }
                }
                MessageBox.Show("Succsesful!");
            }
            catch
            {
                MessageBox.Show("Error");
            }
            
        }

        private void checkBox_isRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox_isRefresh.Checked == false)
            {
                timer_refrsh.Enabled = true;
            }
            else
            {
                timer_refrsh.Enabled = false;
            }
        }

        private void timer_refrsh_Tick(object sender, EventArgs e)
        {
            RefreshProcessesList();
        }
    }
}
