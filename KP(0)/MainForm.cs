using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace KP_0_
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        internal static SqlConnection sqlConnection = null;
        internal static List<string> tableNames = new List<string>()
        {
            "Appeal",
            "Client",
            "Delivery",
            "DigitalProduct",
            "Image",
            "KeyForSale",
            "LinkKeyPurchase",
            "Purchase",
            "ViewLinkKeyProduct"
        };
        internal static Dictionary<string, SqlDataAdapter> sqlDataAdapters = new Dictionary<string, SqlDataAdapter>();
        internal static Dictionary<string, BindingSource> bindingSources = new Dictionary<string, BindingSource>();
        internal static DataSet dataSet;



        private void Form1_Load(object sender, EventArgs e)
        {

            for (int i = 0; i < Tools.configValues.Length; i++) Tools.ConfigRead(i.ToString());


            textBox1.Text = Tools.configValues[0];
            textBox2.ReadOnly = true;


            try
            {
                sqlConnection = new SqlConnection(Tools.configValues[0]);
                sqlConnection.Close();
                sqlConnection.Open();

                if (sqlConnection.State == ConnectionState.Open)
                {
                    textBox2.Text = "Соединение было успешно установлено.";
                }
            }
            catch
            {
                menuStrip1.Enabled = false;
                textBox2.Text = "Ошибка, не удалось установить соединение.";
            }



            if (menuStrip1.Enabled)
            {
                for (int i = 0; i < tableNames.Count; i++)
                {
                    sqlDataAdapters.Add(tableNames[i],
                        new SqlDataAdapter($"SELECT * FROM {tableNames[i]}", Tools.configValues[0]));

                    bindingSources.Add(tableNames[i],
                        new BindingSource());
                }


                MainForm.dataSet = new DataSet();
                for (int i = 0; i < tableNames.Count; i++)
                {
                    MainForm.sqlDataAdapters[tableNames[i]].Fill(MainForm.dataSet, tableNames[i]);
                    MainForm.bindingSources[tableNames[i]].DataSource = MainForm.dataSet;
                    MainForm.bindingSources[tableNames[i]].DataMember = tableNames[i];
                }
            }






            






           



        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            Environment.Exit(0);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Tools.ConfigAdd("0", textBox1.Text);
        }

        private void процедурыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Thread(() =>
            {
                ProceduresForm proceduresForm = new ProceduresForm();
                proceduresForm.ShowDialog();
            })).Start();
        }


        private void таблицыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Thread(() =>
            {
                TablesForm tablesForm = new TablesForm();
                tablesForm.ShowDialog();
            })).Start();

        }
        private void запросыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new Thread(() =>
            {
                QueryForm queryForm = new QueryForm();
                queryForm.ShowDialog();
            })).Start();
        }
    }
}
