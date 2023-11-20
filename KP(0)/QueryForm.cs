using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace KP_0_
{
    public partial class QueryForm : Form
    {
        public QueryForm()
        {
            InitializeComponent();
        }

        internal static LINQmethods lINQmethods = new LINQmethods(); //ин!!!
        internal static Dictionary<string, DataGridView> dataGridViews;

        internal static new Dictionary<string, List<object>> linkData;


        private void QueryForm_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = Tools.tabPageIndexOfQueries;

            linkData = new Dictionary<string, List<object>>()
            {
                ["0"] = new List<object>
                {
                    "Delivery", textBox4, textBox6
                },
                ["1"] = new List<object>
                {
                    "DigitalProduct", textBox14, new TextBox()
                },
                ["2"] = new List<object>
                {
                    "Client", textBox21, new TextBox()
                },
                ["3"] = new List<object>
                {
                    "Purchase", textBox25, textBox23
                },
                ["4"] = new List<object>
                {
                    "Appeal", textBox1, new TextBox()
                },
            };

            dataGridViews = new Dictionary<string, DataGridView>
            {
                ["Delivery"] = dataGridView1,
                ["DigitalProduct"] = dataGridView2,
                ["Client"] = dataGridView3,
                ["Purchase"] = dataGridView4,
                ["Appeal"] = dataGridView5,

            };





            //----------------
            //BindingSource newBindingSource = new BindingSource();
            //newBindingSource.DataSource = MainForm.bindingSources["Delivery"].DataSource;
            //newBindingSource.DataMember = MainForm.bindingSources["Delivery"].DataMember;


        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tools.tabPageIndexOfQueries = tabControl1.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QueryForm queryForm = new QueryForm();
            this.Close();

            (new Thread(() => queryForm.ShowDialog())).Start();
        }


        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                var temp = linkData[Tools.tabPageIndexOfQueries.ToString()];

                dataGridViews[temp[0].ToString()].DataSource =
                    lINQmethods.linqMethods[temp[0].ToString()](MainForm.bindingSources[temp[0].ToString()],
                    temp[1],
                    temp[2]);
                dataGridViews[temp[0].ToString()].AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch { }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
