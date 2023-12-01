using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static KP_0_.DataClasses;

namespace KP_0_
{

    internal class LINQmethods
    {
        internal delegate List<object> LINQ(BindingSource bindingSource, params object[] args);
        internal Dictionary<string, LINQ> linqMethods = new Dictionary<string, LINQ>();


        public LINQmethods()
        {
            linqMethods.Add("Delivery" ,(bindingSource, args) =>
            {
                decimal temp1 = (((TextBox)args[0]).Text).Length > 0 ? 
                Convert.ToDecimal(((TextBox)args[0]).Text) : 0;

                decimal temp2 = (((TextBox)args[1]).Text).Length > 0 ?
                Convert.ToDecimal(((TextBox)args[1]).Text) : 999999;


                return bindingSource.Cast<DataRowView>()
                                    .Where(row => (decimal)row["Price"] >= temp1
                                                && (decimal)row["Price"] <= temp2)
                                    .Select(row => (object)(new Delivery
                                    {
                                        Id = (int)row["Id"],
                                        Date = (DateTime)row["Date"],
                                        Price = (decimal)row["Price"],
                                        NameOfProvider = (string)row["NameOfProvider"],
                                        ContactInformation = (string)row["ContactInformation"],
                                    }))
                                    .ToList();
            });

            linqMethods.Add("DigitalProduct", (bindingSource, args) =>
            {
                string regexPattern = $".*{((TextBox)args[0]).Text}.*";
                return bindingSource.Cast<DataRowView>()
                                    
                                    .Where(row => Regex.IsMatch(row["Name"].ToString(), regexPattern, RegexOptions.IgnoreCase))
                                    .Select(row => (object)(new DigitalProduct
                                    {
                                        Id = (int)row["Id"],
                                        Name = (string)row["Name"],
                                        NameOfPlatformOfKeys = (string)row["NameOfPlatformOfKeys"],
                                        Description = (string)row["Description"],
                                        Price = (decimal)row["Price"],
                                        Discount = (decimal)row["Discount"]
                                    }))
                                    .ToList();
            });

            linqMethods.Add("Client", (bindingSource, args) =>
            {
                string regexPattern = $".*{((TextBox)args[0]).Text}.*";
                return bindingSource.Cast<DataRowView>()

                                    .Where(row => Regex.IsMatch(row["Mail"].ToString(), regexPattern, RegexOptions.IgnoreCase))
                                    .Select(row => (object)(new Client
                                    {
                                        Mail = (string)row["Mail"],
                                        RegistrationDate = (DateTime)row["RegistrationDate"],
                                        Note = (string)row["Note"]
                                    }))
                                    .ToList();
            });

            linqMethods.Add("Purchase", (bindingSource, args) =>
            {
                DateTime temp1 = (((TextBox)args[0]).Text).Length > 0 ?
                Convert.ToDateTime(((TextBox)args[0]).Text) : DateTime.MinValue;

                DateTime temp2 = (((TextBox)args[1]).Text).Length > 0 ?
                Convert.ToDateTime(((TextBox)args[1]).Text) : DateTime.MaxValue;


                return bindingSource.Cast<DataRowView>()
                                    .Where(row => (DateTime)row["Date"] >= temp1
                                    && (DateTime)row["Date"] <= temp2)
                                    .Select(row => (object)(new Purchase
                                    {
                                        Id = (int)row["Id"],
                                        Mail = (string)row["Mail"],
                                        Date = (DateTime)row["Date"],
                                        
                                    }))
                                    .ToList();
            });

            linqMethods.Add("Appeal", (bindingSource, args) =>
            {
                string regexPattern = $".*{((TextBox)args[0]).Text}.*";
                return bindingSource.Cast<DataRowView>()

                                    .Where(row => Regex.IsMatch(row["StatusOfAppeal"].ToString(), regexPattern, RegexOptions.IgnoreCase))
                                    .Select(row => (object)(new Appeal
                                    {
                                        Id = (int)row["Id"],
                                        Mail = (string)row["Mail"],
                                        Date = (DateTime)row["Date"],
                                        TopicOfAppeal = (string)row["TopicOfAppeal"],
                                        TextOfAppeal = (string)row["TextOfAppeal"],
                                        StatusOfAppeal = (string)row["StatusOfAppeal"]
                                    }))
                                    .ToList();
            });
        }
    }
}
