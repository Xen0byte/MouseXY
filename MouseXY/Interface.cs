using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace MouseXY
{
    public partial class Interface : Form
    {
        #region Variables
        private IKeyboardMouseEvents m_GlobalHook;

        public static int XAxis { get; set; }
        public static int YAxis { get; set; }

        public static bool XLock = false;
        public static bool YLock = false;
        public static bool CLock = false;

        public static int CRad = 150;
        public static int CIncrement = 50;

        int tempX = 0;
        int tempY = 0;
        #endregion

        public Interface()
        {
            InitializeComponent();
            Subscribe();
            FormClosing += CleanUp;
        }

        #region  Rectangle Constructor
        // public Rectangle(int x, int y, int width, int height)
        //
        // Parameters:
        //
        // x:
        // Type: System.Int32
        // The x-coordinate of the upper-left corner of the rectangle.
        //
        // y:
        // Type: System.Int32
        // The y-coordinate of the upper-left corner of the rectangle.
        //
        // width:
        // Type: System.Int32
        // The width of the rectangle.
        //
        // height:
        // Type: System.Int32
        // The height of the rectangle.
        //
        // Screen Sizes:
        //
        // PresentationFramework.dll:
        // SystemParameters.VirtualScreenWidth
        // SystemParameters.VirtualScreenHeight
        //
        // System.Windows.Forms.dll:
        // SystemInformation.VirtualScreen.Width
        // SystemInformation.VirtualScreen.Height
        //
        // For console applications, add System.Windows to references for SystemParameters and System.Windows.Forms for SystemInformation.
        #endregion

        #region Control Events
        private void button1_Click(object sender, EventArgs e)
        {
            if (XLock == false)
            {
                XLock = true;
                YLock = false;
                CLock = false;

                button1.Text = "Toggle X-Axis Lock [ON]";
                button2.Text = "Toggle Y-Axis Lock [OFF]";
                button3.Text = "Toggle Circle Lock [OFF]";

                tempY = YAxis;
                Cursor.Clip = new Rectangle(0, tempY, SystemInformation.VirtualScreen.Width, 1);
            }
            else if (XLock == true)
            {
                XLock = false;
                button1.Text = "Toggle X-Axis Lock [OFF]";

                tempY = 0;
                Cursor.Clip = new Rectangle();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (YLock == false)
            {
                XLock = false;
                YLock = true;
                CLock = false;

                button1.Text = "Toggle X-Axis Lock [OFF]";
                button2.Text = "Toggle Y-Axis Lock [ON]";
                button3.Text = "Toggle Circle Lock [OFF]";

                tempX = XAxis;
                Cursor.Clip = new Rectangle(tempX, 0, 1, SystemInformation.VirtualScreen.Height);
            }
            else if (YLock == true)
            {
                YLock = false;
                button2.Text = "Toggle Y-Axis Lock [OFF]";

                tempX = 0;
                Cursor.Clip = new Rectangle();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CLock == false)
            {
                XLock = false;
                YLock = false;
                CLock = true;

                button1.Text = "Toggle X-Axis Lock [OFF]";
                button2.Text = "Toggle Y-Axis Lock [OFF]";
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
            button4.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CRad = CRad - CIncrement;
            button4.Text = "Radius = " + CRad + "\n\r" + "Increment = " + CIncrement;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CRad = CRad + CIncrement;
            button4.Text = "Radius = " + CRad + "\n\r" + "Increment = " + CIncrement;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CIncrement = Convert.ToInt32(textBox1.Text);
                button4.Text = "Radius = " + CRad + "\n\r" + "Increment = " + CIncrement;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show
                (
                "built by Xen0byte" + "\n\r" +
                "https://github.com/Xen0byte" + "\n\r\n\r\n\r" +
                "HOTKEYS:" + "\n\r\n\r" +
                "Toggle X-Axis Lock:   Alt+X" + "\n\r" +
                "Toggle Y-Axis Lock:   Alt+Y" + "\n\r" +
                "Toggle Circle Lock:   Alt+C" + "\n\r" +
                "Decrease Circle Radius:   Alt+Left" + "\n\r" +
                "Increase Circle Radius:   Alt+Right",
                "MouseXY"
                );
        }
        #endregion

        #region Control Focus Events
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4.Visible = true;
                label1.Focus();
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            button4.Visible = true;
            label1.Focus();
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            button4.Visible = true;
            label1.Focus();
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            button4.Visible = true;
            label1.Focus();
        }
        #endregion

        #region GlobalHooks
        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.MouseMove += HookManager_MouseMove;
            m_GlobalHook.KeyDown += OnKeyDown;
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseMove -= HookManager_MouseMove;
            m_GlobalHook.KeyDown -= OnKeyDown;
            m_GlobalHook.Dispose();
        }

        private void CleanUp(object sender, CancelEventArgs e)
        {
            Unsubscribe();
        }

        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = string.Format("    X={0:0000} | Y={1:0000}", e.X, e.Y);
            XAxis = e.X;
            YAxis = e.Y;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.X)
                button1.PerformClick();
            else if (e.Alt && e.KeyCode == Keys.Y)
                button2.PerformClick();
            else if (e.Alt && e.KeyCode == Keys.C)
                button3.PerformClick();
            else if (e.Alt && e.KeyCode == Keys.Left)
                button5.PerformClick();
            else if (e.Alt && e.KeyCode == Keys.Right)
                button6.PerformClick();
        }
        #endregion
    }
}