using System;
using System.Drawing;
using System.Windows.Forms;

namespace lip
{
    public partial class Form1 : Form
    {
        private Hotkey hotkey;
        private IpGet ipGet = new IpGet();
        public NotifyIcon notifyIcon = new NotifyIcon();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            hotkey = new Hotkey(this.Handle);
            hotkey.RegisterHotKeys();
            ReCheck();
        }
        protected override void WndProc(ref Message keyPressed)
        {
            base.WndProc(ref keyPressed);
            if (keyPressed.Msg == 0x0312)
            {
                Show();
                ReCheck();
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
                notifyIcon1.Visible = false;
                Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ipGet.CheckPort(textBox3.Text) == true)
            {
                label3.BackColor = Color.Green;
            }
            else
            {
                label3.BackColor = Color.Red;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            ReCheck();
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            Focus();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipText = "Press CTRL+M to reopen Ip\'N\'Dip";
                notifyIcon1.BalloonTipTitle = "Ip\'N\'Dip";
                notifyIcon1.ShowBalloonTip(5000);
                Hide();
            }
        }

        private void ReCheck()
        {
            ipGet.CheckIPs();
            textBox1.Text = ipGet.LocalIP;
            textBox2.Text = ipGet.ExternalIP;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Updating...";
            textBox2.Text = "Updating...";
            ReCheck();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            hotkey.UnRegisterHotKeys();
        }
    }
}