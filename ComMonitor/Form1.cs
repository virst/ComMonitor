using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComMonitor
{
    public partial class Form1 : Form
    {
        bool lascActionGet = false;

        public Form1()
        {
            InitializeComponent();

            List<EncodingAdapter> enl = new List<EncodingAdapter>();

            // enl.Add(new EncodingAdapter(Encoding.UTF7, "UTF7"));
            enl.Add(new EncodingAdapter(Encoding.BigEndianUnicode, "BigEndianUnicode"));
            enl.Add(new EncodingAdapter(Encoding.Unicode, "Unicode"));
            enl.Add(new EncodingAdapter(Encoding.ASCII, "ASCII"));
            enl.Add(new EncodingAdapter(Encoding.UTF32, "UTF32"));
            enl.Add(new EncodingAdapter(Encoding.UTF8, "UTF8"));

            comboBox2.DataSource = enl;

            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            comboBox2.SelectedIndex = comboBox2.Items.Count - 1;
          
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        void AddText(string s, bool newLine = true)
        {
            if (newLine)
                richTextBox1.AppendText(s + "\n");
            else
                richTextBox1.AppendText(s);
            if (checkBox1.Checked)
            {
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comDataSet1.UpdatePorts();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                AddText(serialPort1.PortName + "-Close");
                serialPort1.Close();
            }
            else
            {
                serialPort1.PortName = comboBox1.SelectedValue.ToString();
                serialPort1.Encoding = (comboBox2.SelectedItem as EncodingAdapter).Encoding;
                serialPort1.Open();
                AddText(serialPort1.PortName + "-Open");
            }

            timer1_Tick(sender, e);
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

            this.Invoke(new EventHandler((object p, EventArgs et) => { DataReceived(); }), null, null);
        }

        void DataReceived()
        {
            string s = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            s = ">> " + s + " ";           
            s = s + serialPort1.ReadLine();
            AddText(s);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.pictureBox1.Image = serialPort1.IsOpen ? Properties.Resources.link : Properties.Resources.broken_link;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine(textBox1.Text);
                string s = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                s = "<< " + s + " ";
                AddText(s + textBox1.Text);
                textBox1.Text = "";
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button3_Click(sender, null);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.Encoding = (comboBox2.SelectedItem as EncodingAdapter).Encoding;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
                serialPort1.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
