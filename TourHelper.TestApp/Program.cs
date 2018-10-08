using Npgsql;
using System;
using System.Data;
using TourHelper.Base.Model.Entity;
using TourHelper.Manager.Calculators;
using TourHelper.Repository;

namespace TourHelper.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //try
            //{
            //    DataSet ds = new DataSet();
            //    DataTable dt = new DataTable();

            //    // PostgeSQL-style connection string
            //    string connstring = String.Format("Server={0};Port={1};" +
            //                "User Id={2};Password={3};Database={4};",
            //                "85.255.7.165", "50050", "tourhelper",
            //                "!P@ssw0rd", "TourHelper");
            //    // Making connection with Npgsql provider
            //    NpgsqlConnection conn = new NpgsqlConnection(connstring);
            //    conn.Open();
            //    // quite complex sql statement
            //    string sql = "SELECT * FROM public.\"User\"";
            //    // data adapter making request from our connection
            //    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            //    // i always reset DataSet before i do
            //    // something with it.... i don't know why :-)
            //    ds.Reset();
            //    // filling DataSet with result from NpgsqlDataAdapter
            //    da.Fill(ds);
            //    // since it C# DataSet can handle multiple tables, we will select first
            //    dt = ds.Tables[0];
            //    // connect grid to DataTable
            //    // since we only showing the result we don't need connection anymore

            //    var x = dt.Columns["CreatedOn"].Ordinal;
            //    var y = dt.Rows[0][x];
            //    conn.Close();
            //}
            //catch (Exception msg)
            //{
            //    // something went wrong, and you wanna know why
            //    throw;
            //}



            var userRepository = new UserRepository();
            var user = userRepository.GetByLogin("cyc");

            int a = 1;
            double[] output;
            TMConverter conv = new TMConverter();
            Coordinate c = new Coordinate();
            c.Latitude = 52.657570f;
            c.Longitude = 1.717922f;

            output = conv.ConvertCoordinates(c);

            System.Console.WriteLine("X:" + output[0].ToString() + "\nY:" + output[1].ToString() + "\nZ:");
            System.Console.ReadKey();
        }
    }
}
