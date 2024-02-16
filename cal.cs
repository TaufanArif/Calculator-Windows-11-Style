using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator_Windows_11_Style
{
    public partial class cal : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse
        );
        public cal()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 14, 14));
            
            
        }

        private void btnclsclick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnmaxclick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnminclick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void formmove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        
        private void btnnumclick(object sender, EventArgs e)
        {
            string num = (sender as Button).Text;
            if (txtbx3.Text == "0")
            {
                txtbx3.Text = num;
                
            }
            else
            {
                txtbx3.Text += num;
                
            }
        }
        string tmp;
        string pre;
        
        private void btnopclick(object sender, EventArgs e)
        {
            string op = (sender as Button).Text;
            if (txtbx3.Text != "0" && !txtbx3.Text.EndsWith(".") && button7.Enabled == true)
            {
                pre = txtbx3.Text;
                txtbx1.Text += txtbx3.Text + " " + op + " ";
                if (op != "×" && op != "÷")
                {
                    tmp += txtbx3.Text + op;
                    txtbx3.Text = "0";

                }
                else
                {
                    if (op == "×")
                    {
                        tmp += txtbx3.Text + "*";
                        txtbx3.Text = "0";

                    }
                    if (op == "÷")
                    {
                        tmp += txtbx3.Text + "/";
                        txtbx3.Text = "0";

                    }

                }
            }
            else if (button7.Enabled == false)
            {
                txtbx1.Text += txtbx3.Text + op;
                tmp += op;
                txtbx3.Text = "0";
            }
        }

        private void cmaclick(object sender, EventArgs e)
        {
            if (!txtbx3.Text.Contains("."))
            {
                txtbx3.Text += ".";
            }
        }

        private void pmclick(object sender, EventArgs e)
        {
            if (!txtbx3.Text.Contains("-"))
            {
                txtbx3.Text = txtbx3.Text.Insert(0, "-");
            }
            else
            {
                txtbx3.Text = txtbx3.Text.Remove(0, 1);
            }
        }
        public static double Evaluate(string expression)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("expression", string.Empty.GetType(), expression);
            System.Data.DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }
        private void txt3change(object sender, EventArgs e)
        {
            if(tmp != string.Empty && txtbx1.Text != string.Empty && !txtbx3.Text.Contains("%"))
            {
                txtbx2.Text = Convert.ToString(Evaluate(tmp + txtbx3.Text));
            }
            
        }

        private void eqclick(object sender, EventArgs e)
        {
            txtbx3.Text = txtbx2.Text;
            txtbx1.Text = "";
            txtbx2.Text = "";
            tmp = string.Empty;
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        //CE button
        private void button8_Click(object sender, EventArgs e)
        {
            txtbx3.Text = "0";
        }


        //C button
        private void button9_Click(object sender, EventArgs e)
        {
            txtbx1.Text = "";
            txtbx2.Text = "";
            txtbx3.Text = "0";
            tmp = string.Empty;
        }


        // delete button
        private void button10_Click(object sender, EventArgs e)
        {
            if (txtbx3.Text.Length != 1)
            {
                txtbx3.Text = txtbx3.Text.Remove(txtbx3.Text.Length - 1, 1);
            }
            else
            {
                txtbx3.Text = "0";
                
            }

        }

        string per;
        //percen button
        private void button7_Click(object sender, EventArgs e)
        {
            per += "(" + txtbx3.Text + "/100*" + pre + ")";
            tmp += per;
            button7.Enabled = false;
            txtbx3.Text += "%";
        }
        // 1/x button
        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("under build!");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("under build!");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("under build!");
        }

       

        private void HistoryC(object sender, EventArgs e)
        {
            
        }
    }
}
