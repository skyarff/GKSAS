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
        internal static string connectionString = "Data Source=DESKTOP-KOFNV9U\\FIRSTSERVER;Initial Catalog=GKSAS;Integrated Security=True";

        internal static Dictionary<string, SqlDataAdapter> sqlDataAdapters = new Dictionary<string, SqlDataAdapter>()
        {
            ["Appeal"] = new SqlDataAdapter("SELECT * FROM Appeal", MainForm.connectionString),
            ["Client"] = new SqlDataAdapter("SELECT * FROM Client", MainForm.connectionString),
            ["Delivery"] = new SqlDataAdapter("SELECT * FROM Delivery", MainForm.connectionString),
            ["DigitalProduct"] = new SqlDataAdapter("SELECT * FROM DigitalProduct", MainForm.connectionString),
            ["Image"] = new SqlDataAdapter("SELECT * FROM Image", MainForm.connectionString),
            ["KeyForSale"] = new SqlDataAdapter("SELECT * FROM KeyForSale", MainForm.connectionString),
            ["LinkKeyPurchase"] = new SqlDataAdapter("SELECT * FROM LinkKeyPurchase", MainForm.connectionString),
            ["PlatformOfKeys"] = new SqlDataAdapter("SELECT * FROM PlatformOfKeys", MainForm.connectionString),
            ["Provider"] = new SqlDataAdapter("SELECT * FROM Provider", MainForm.connectionString),
            ["Purchase"] = new SqlDataAdapter("SELECT * FROM Purchase", MainForm.connectionString),
            ["Staff"] = new SqlDataAdapter("SELECT * FROM Staff", MainForm.connectionString),
        };
        internal static Dictionary<string, BindingSource> bindingSources = new Dictionary<string, BindingSource>()
        {
            ["Appeal"] = new BindingSource(),
            ["Client"] = new BindingSource(),
            ["Delivery"] = new BindingSource(),
            ["DigitalProduct"] = new BindingSource(),
            ["Image"] = new BindingSource(),
            ["KeyForSale"] = new BindingSource(),
            ["LinkKeyPurchase"] = new BindingSource(),
            ["PlatformOfKeys"] = new BindingSource(),
            ["Provider"] = new BindingSource(),
            ["Purchase"] = new BindingSource(),
            ["Staff"] = new BindingSource(),
        };
        internal static DataSet dataSet = new DataSet();



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



        }

        private void таблицыToolStripMenuItem_Click(object sender, EventArgs e)
        {

            (new Thread(() =>
            {
                TablesForm tablesForm = new TablesForm();
                tablesForm.ShowDialog();
            })).Start();
 
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
