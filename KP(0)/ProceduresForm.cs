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


           
        }




        private string CalculateFinalPrice(int index)
        {
            using (SqlCommand command = new SqlCommand("CalculateFinalPrice", MainForm.sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter inputParameter = new SqlParameter("@productId", SqlDbType.Int, 50);
                inputParameter.Direction = ParameterDirection.Input;
                SqlParameter outputParameter = new SqlParameter("@finalPrice", SqlDbType.Decimal, 50);
                outputParameter.Direction = ParameterDirection.Output;

                command.Parameters.AddWithValue("@productId", Convert.ToInt32(index));
                command.Parameters.Add(outputParameter);
                command.ExecuteNonQuery();

                return (outputParameter.Value).ToString();
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlCommand command = new SqlCommand("CalculateFinalPrice", MainForm.sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;


                SqlParameter inputParameter = new SqlParameter("@productId", SqlDbType.Int, 50);
                inputParameter.Direction = ParameterDirection.Input;
                SqlParameter outputParameter = new SqlParameter("@finalPrice", SqlDbType.Decimal, 50);
                outputParameter.Direction = ParameterDirection.Output;






                command.Parameters.AddWithValue("@productId", Convert.ToInt32(textBox1.Text)); // Замените на свои значения параметров
                command.Parameters.Add(outputParameter);

                // Выполняем команду
                command.ExecuteNonQuery();

                // Получаем значение выходного параметра
                string outputValue = outputParameter.Value.ToString();


                textBox2.Text = outputValue;




            //    sqlCommand1.Parameters["@productId"].Value =
            //Convert.ToInt32(textBox1.Text);

            //    sqlConnection1.Open();
            //    sqlCommand1.ExecuteNonQuery();
            //    sqlConnection1.Close();

            //    var temp = sqlCommand1.Parameters["@finalPrice"].Value;

            //    textBox2.Text = temp.ToString();

            }
        }

    }
}
