using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace MouseXY
{
    public partial class Interface : Form
    {
        private IKeyboardMouseEvents m_GlobalHook;

        public static int XAxis { get; set; }
        public static int YAxis { get; set; }
        public static bool XLock = false;
        public static bool YLock = false;
        public static bool CLock = false;
        public static int CRad = 150;

        public Interface()
        {
            InitializeComponent();
            Subscribe();
            FormClosing += CleanUp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (XLock == false)
            {
                XLock = true;
                button1.Text = "Toggle X-Axis Lock [ON]";
            }
            else if (XLock == true)
            {
                XLock = false;
                button1.Text = "Toggle X-Axis Lock [OFF]";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (YLock == false)
            {
                YLock = true;
                button2.Text = "Toggle Y-Axis Lock [ON]";
            }
            else if (YLock == true)
            {
                YLock = false;
                button2.Text = "Toggle Y-Axis Lock [OFF]";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CLock == false)
            {
                CLock = true;
                button3.Text = "Toggle Circle Lock [ON]";
            }
            else if (CLock == true)
            {
                CLock = false;
                button3.Text = "Toggle Circle Lock [OFF]";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        #region GlobalHooks
        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.MouseMove += HookManager_MouseMove;
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseMove -= HookManager_MouseMove;
            m_GlobalHook.Dispose();
        }

        private void CleanUp(object sender, CancelEventArgs e)
        {
            Unsubscribe();
        }

        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = string.Format("X={0:0000} | Y={1:0000}", e.X, e.Y);
            XAxis = e.X;
            YAxis = e.Y;
        }
        #endregion
    }
}