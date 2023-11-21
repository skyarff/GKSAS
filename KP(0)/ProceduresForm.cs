using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KP_0_
{
    public partial class ProceduresForm : Form
    {
        public ProceduresForm()
        {
            InitializeComponent();
        }

        internal static Dictionary<string, DataGridView> dataGridViews;
        internal static Dictionary<string, BindingNavigator> bindingNavigators;


        private void ProceduresForm_Load(object sender, EventArgs e)
        {

            for (int i = 0; i < MainForm.tableNames.Count - 1; i++)
            {
                comboBox2.Items.Add(MainForm.tableNames[i]);
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;




        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("CalculateFinalPrice", MainForm.sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter inputParameter = new SqlParameter("@productId", SqlDbType.Int, 50);
                    inputParameter.Direction = ParameterDirection.Input;
                    SqlParameter outputParameter = new SqlParameter("@finalPrice", SqlDbType.Decimal, 50);
                    outputParameter.Direction = ParameterDirection.Output;


                    command.Parameters.AddWithValue("@productId", Convert.ToInt32(textBox1.Text));
                    command.Parameters.Add(outputParameter);

                    command.ExecuteNonQuery();
                    string outputValue = outputParameter.Value.ToString();
                    textBox2.Text = outputValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("AddUser", MainForm.sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    SqlParameter inputParameter1 = new SqlParameter("@login", SqlDbType.NVarChar, 50);
                    inputParameter1.Direction = ParameterDirection.Input;


                    SqlParameter inputParameter2 = new SqlParameter("@password", SqlDbType.NVarChar, 50);
                    inputParameter2.Direction = ParameterDirection.Input;



                    command.Parameters.AddWithValue("@login", textBox3.Text);
                    command.Parameters.AddWithValue("@password", textBox4.Text);


                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try //GrantUser @command REVOKE SELECT ON [схема].[таблица] FROM [имя_пользователя];
            {
                using (SqlCommand command = new SqlCommand("GrantUser", MainForm.sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter inputParameter1 = new SqlParameter("@command", SqlDbType.NVarChar, 250);
                    inputParameter1.Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@command", $"GRANT {comboBox1.Text} ON {comboBox2.Text} TO {textBox5.Text}");

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("DeleteSchemaFromDatabase", MainForm.sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter inputParameter1 = new SqlParameter("@usernameToDelete", SqlDbType.NVarChar, 250);
                    inputParameter1.Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@usernameToDelete", textBox3.Text);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("DeleteUserFromDatabase", MainForm.sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter inputParameter1 = new SqlParameter("@usernameToDelete", SqlDbType.NVarChar, 250);
                    inputParameter1.Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@usernameToDelete", textBox3.Text);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try //GrantUser @command REVOKE SELECT ON [схема].[таблица] FROM [имя_пользователя];
            {
                using (SqlCommand command = new SqlCommand("GrantUser", MainForm.sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter inputParameter1 = new SqlParameter("@command", SqlDbType.NVarChar, 250);
                    inputParameter1.Direction = ParameterDirection.Input;

                    command.Parameters.AddWithValue("@command", $"REVOKE {comboBox1.Text} ON {comboBox2.Text} TO {textBox5.Text}");

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
