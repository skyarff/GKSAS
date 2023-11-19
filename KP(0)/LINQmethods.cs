using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static KP_0_.DataClasses;

namespace KP_0_
{

    internal class LINQmethods
    {
        internal delegate List<object> LINQ(BindingSource bindingSource, params string[] args);


        internal Dictionary<string, LINQ> linqMethods = new Dictionary<string, LINQ>();


        public LINQmethods()
        {
            linqMethods.Add("Delivery" ,(bindingSource, args) =>
            {
                return bindingSource.Cast<DataRowView>()
                                    .Where(row => (decimal)row["Price"] >= Convert.ToDecimal(args[0])
                                                && (decimal)row["Price"] <= Convert.ToDecimal(args[1]))
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
        }





    }
}
