using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace KP_0_
{
    public partial class TablesForm : Form
    {
        public TablesForm()
        {
            InitializeComponent();
        }


        internal static Dictionary<string, DataGridView> dataGridViews;
        internal static Dictionary<string, BindingNavigator> bindingNavigators;


        private void TablesForm_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = Tools.tabPageIndexOfTables;


            dataGridViews = new Dictionary<string, DataGridView>
            {
                ["Delivery"] = dataGridView1,
                ["DigitalProduct"] = dataGridView2,
                ["Client"] = dataGridView3,
                ["KeyForSale"] = dataGridView4,
                ["Purchase"] = dataGridView5,
                ["Appeal"] = dataGridView6,
                ["LinkKeyPurchase"] = dataGridView8,
                ["ViewKeyProduct"] = dataGridView7,
            };
            bindingNavigators = new Dictionary<string, BindingNavigator>
            {

                ["Delivery"] = bindingNavigator1,
                ["DigitalProduct"] = bindingNavigator2,
                ["Client"] = bindingNavigator3,
                ["KeyForSale"] = bindingNavigator4,
                ["Purchase"] = bindingNavigator5,
                ["Appeal"] = bindingNavigator6,
                ["LinkKeyPurchase"] = bindingNavigator8,
            };


            foreach (var item in dataGridViews.Keys)
            {

                dataGridViews[item].DataError += YourDataGridView_DataError;
                dataGridViews[item].DataSource = MainForm.bindingSources[item];

                if (bindingNavigators.ContainsKey(item))
                    bindingNavigators[item].BindingSource = MainForm.bindingSources[item];

                if (item != "Client")
                    dataGridViews[item].Columns[0].ReadOnly = true;

                dataGridViews[item].AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

        }

        private void YourDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e) { }
        private DataTable AmongSearch(DataTable dataTable, Type type, string table, string column, string p1 = "", string p2 = "")
        {

            bool f1 = false;
            try { Convert.ChangeType(p1, type); }
            catch { f1 = true; }

            bool f2 = false;
            try { Convert.ChangeType(p2, type); }
            catch { f2 = true; }

            DataTable filteredDataTable = dataTable.Clone();

            if (type == typeof(DateTime))
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    var data = Convert.ToDateTime(row[column]);

                    if (f1 || (data >= Convert.ToDateTime(p1)) && (f2 || data <= Convert.ToDateTime(p2)))
                    {
                        filteredDataTable.ImportRow(row);
                    }
                }
            }
            else
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    var data = Convert.ToDecimal(row[column]);

                    if ((f1 || Convert.ToDecimal(data) >= Convert.ToDecimal(p1)) && (f2 || Convert.ToDecimal(data) <= Convert.ToDecimal(p2)))
                    {
                        filteredDataTable.ImportRow(row);
                    }
                }
            }

            return filteredDataTable;
        }
        private void OccurrenceSearch(string input, string table, string column, DataGridView dataGridView)
        {
            if (input.Length < 1)
            {
                dataGridView.DataSource = MainForm.bindingSources[table];
                return;
            }

            MainForm.dataSet = (DataSet)MainForm.bindingSources[table].DataSource;

            DataTable dataTable = MainForm.dataSet.Tables[table];

            string regexPattern = $".*{input}.*";

            DataTable filteredDataTable = dataTable.Clone();

            foreach (DataRow row in dataTable.Rows)
            {
                string productName = row[column].ToString();
                if (Regex.IsMatch(productName, regexPattern, RegexOptions.IgnoreCase))
                {
                    filteredDataTable.ImportRow(row);
                }
            }

            dataGridView.DataSource = filteredDataTable;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in dataGridViews.Keys)
            {
                try
                {
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(MainForm.sqlDataAdapters[item]);
                    MainForm.sqlDataAdapters[item].Update(MainForm.dataSet.Tables[item]);
                }
                catch { }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            TablesForm tablesForm = new TablesForm();
            this.Close();

            (new Thread(() => tablesForm.ShowDialog())).Start();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tools.tabPageIndexOfTables = tabControl1.SelectedIndex;
        }


        //Delivery
        #region
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            OccurrenceSearch(textBox1.Text, "Delivery", "NameOfProvider", dataGridView1);
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            OccurrenceSearch(textBox2.Text, "Delivery", "ContactInformation", dataGridView1);
        }
        private void button2_Click(object sender, EventArgs e)
        {


            dataGridView1.DataSource = AmongSearch(
                AmongSearch(
                    MainForm.dataSet.Tables["Delivery"],
                    typeof(decimal),
                    "Delivery",
                    "Price",
                    textBox5.Text,
                    textBox6.Text),
                typeof(DateTime),
                "Delivery",
                "Date",
                textBox3.Text,
                textBox4.Text);
        }
        #endregion


        //DigitalProduct
        #region
        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            OccurrenceSearch(textBox12.Text, "DigitalProduct", "Name", dataGridView2);
        }
        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            OccurrenceSearch(textBox11.Text, "DigitalProduct", "Description", dataGridView2);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = AmongSearch(
                AmongSearch(
                    MainForm.dataSet.Tables["DigitalProduct"],
                    typeof(decimal),
                    "DigitalProduct",
                    "Price",
                    textBox10.Text,
                    textBox9.Text),
                typeof(decimal),
                "DigitalProduct",
                "Discount",
                textBox8.Text,
                textBox7.Text);
        }
        #endregion 


        //Client
        #region
        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            OccurrenceSearch(textBox18.Text, "Client", "Mail", dataGridView3);
        }
        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            OccurrenceSearch(textBox17.Text, "Client", "Note", dataGridView3);
        }
        private void button4_Click(object sender, EventArgs e)
        {

            dataGridView3.DataSource = AmongSearch(
                    MainForm.dataSet.Tables["Client"],
                    typeof(DateTime),
                    "Client",
                    "RegistrationDate",
                    textBox16.Text,
                    textBox15.Text);

        }
        #endregion


        //KeyForSale
        #region
        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            OccurrenceSearch(textBox23.Text, "KeyForSale", "ValueOfKey", dataGridView4);
        }
        #endregion


        //Purchase
        #region
        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            OccurrenceSearch(textBox30.Text, "Purchase", "Mail", dataGridView5);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView5.DataSource = AmongSearch(
                    MainForm.dataSet.Tables["Purchase"],
                    typeof(DateTime),
                    "Purchase",
                    "Date",
                    textBox28.Text,
                    textBox27.Text);
        }
        #endregion


        //Appeal
        #region
        private void textBox36_TextChanged(object sender, EventArgs e)
        {
            OccurrenceSearch(textBox36.Text, "Appeal", "Mail", dataGridView6);
        }
        private void textBox35_TextChanged(object sender, EventArgs e)
        {
            OccurrenceSearch(textBox35.Text, "Appeal", "TopicOfAppeal", dataGridView6);
        }
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            OccurrenceSearch(textBox13.Text, "Appeal", "StatusOfAppeal", dataGridView6);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView6.DataSource = AmongSearch(
                    MainForm.dataSet.Tables["Appeal"],
                    typeof(DateTime),
                    "Appeal",
                    "Date",
                    textBox34.Text,
                    textBox33.Text);
        }
        #endregion


        //LinkKeyPurchase
        #region
        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            OccurrenceSearch(textBox14.Text, "LinkKeyPurchase", "IdOfPurchase", dataGridView8);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView8.DataSource = AmongSearch(
                    AmongSearch(
                        MainForm.dataSet.Tables["LinkKeyPurchase"],
                        typeof(decimal),
                        "LinkKeyPurchase",
                        "Discount",
                        textBox44.Text,
                        textBox43.Text),
                    typeof(decimal),
                    "LinkKeyPurchase",
                    "Price",
                    textBox46.Text,
                    textBox45.Text);
        }

        #endregion


        //ViewKeyProduct
        #region
        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            OccurrenceSearch(textBox19.Text, "ViewKeyProduct", "IdOfKey", dataGridView7);
        }
        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            OccurrenceSearch(textBox20.Text, "ViewKeyProduct", "IdOfDelivery", dataGridView7);
        }

        #endregion

    }
}
