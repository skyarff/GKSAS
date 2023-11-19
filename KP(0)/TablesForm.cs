using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
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


            dataGridViews = new Dictionary<string, DataGridView>
            {
                ["Delivery"] = dataGridView1,
                ["DigitalProduct"] = dataGridView2,
                ["Client"] = dataGridView3,
                ["KeyForSale"] = dataGridView4,
                ["Purchase"] = dataGridView5,
                ["Appeal"] = dataGridView6,
                ["Image"] = dataGridView7,
                ["LinkKeyPurchase"] = dataGridView8,
                ["Staff"] = dataGridView9,
            };
            bindingNavigators = new Dictionary<string, BindingNavigator>
            {

                ["Delivery"] = bindingNavigator1,
                ["DigitalProduct"] = bindingNavigator2,
                ["Client"] = bindingNavigator3,
                ["KeyForSale"] = bindingNavigator4,
                ["Purchase"] = bindingNavigator5,
                ["Appeal"] = bindingNavigator6,
                ["Image"] = bindingNavigator7,
                ["LinkKeyPurchase"] = bindingNavigator8,
                ["Staff"] = bindingNavigator9,
            };



            foreach (var item in dataGridViews.Keys)
            {
                MainForm.sqlDataAdapters[item].Fill(MainForm.dataSet, item);


                MainForm.bindingSources[item].DataSource = MainForm.dataSet;
                MainForm.bindingSources[item].DataMember = item;


                dataGridViews[item].DataSource = MainForm.bindingSources[item];
                bindingNavigators[item].BindingSource = MainForm.bindingSources[item];


                dataGridViews[item].Columns[0].ReadOnly = true;

                dataGridViews[item].AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            //foreach (var item in dataGridViews.Keys)
            //{
            //    try
            //    {
            //        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlDataAdapters[item]);
            //        sqlDataAdapters[item].Update(dataSet, item);
            //    }
            //    catch { }
            //}


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


        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            OccurrenceSearch(textBox1.Text, "Delivery", "NameOfProvider", dataGridView1);
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            OccurrenceSearch(textBox1.Text, "Delivery", "ContactInformation", dataGridView1);
        }



        private void button2_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = AmongSearch(
                AmongSearch(
                    MainForm.dataSet.Tables[0], 
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




        private DataTable AmongSearch(DataTable dataTable, Type type, string table, string column, string p1 = "", string p2 = "")
        {

            bool f1 = p1.Length > 1 ? false : true;
            bool f2 = p2.Length > 1 ? false : true;


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

            DataTable dataTable = MainForm.dataSet.Tables[0];

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
    }
}
