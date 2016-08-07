using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace kFrameWork.Util
{
    public class ExcelUtil
    {
        private static DbDataAdapter GetDataAdapter() { return new System.Data.OleDb.OleDbDataAdapter(); }

        private static DbConnection GetConnection(string excelFileName)
        {
            return new System.Data.OleDb.OleDbConnection(
                "Provider=Microsoft.Jet.OLEDB.4.0;"
                + ";Data Source=" + excelFileName
                + ";Extended Properties=\"Excel 8.0;HDR=Yes;\""
                );
        }

        public static string[,] GetArray(int rows, int cols, string tableName, string excelFileName)
        {
            string[,] data = null;
            DbCommand cmd = GetConnection(excelFileName).CreateCommand();
            cmd.CommandText = "SELECT * FROM " + tableName;
            try
            {
                cmd.Connection.Open();
                DbDataReader reader = cmd.ExecuteReader();
                int rowCount = rows;
                int colCount = (cols < reader.FieldCount) ? cols : reader.FieldCount;
                data = new string[rowCount, colCount];
                int row = 0;
                while (reader.Read() && row < rowCount)
                {
                    for (int col = 0; col < colCount; col++)
                    {
                        if (reader.IsDBNull(col))
                        {
                            data[row, col] = "*NULL*";
                        }
                        else
                        {
                            data[row, col] = reader.GetValue(col).ToString();
                        }
                    }
                    row++;
                }
            }
            finally
            {
                cmd.Connection.Close();
            }
            return data;
        }

        public static DataSet GetDataSet(string tableName,string excelFileName)
        {
            DbCommand cmd = GetConnection(excelFileName).CreateCommand();
            cmd.CommandText = "SELECT * FROM " + tableName;
            DbDataAdapter da = GetDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        //static void DumpArray(string[,] data)
        //{
        //    int rows = data.GetLength(0);
        //    int cols = data.GetLength(1);
        //    for (int row = 0; row < rows; row++)
        //    {
        //        for (int col = 0; col < cols; col++)
        //        {
        //            if (col > 0) { Console.Write("\t"); }
        //            Console.Write(data[row, col]);
        //        }
        //        Console.WriteLine("");
        //    }
        //}

        // static void Main(string[] args) {

        //    Program pgm = new Program(@"C:\test.xls");

        //    DumpArray(pgm.GetArray(5, 3, "[Test$]"));

        //    Debug.WriteLine( pgm.GetDataSet("[Test$]").GetXml());

        //    //Debug.WriteLine(pgm.GetDataSet("[Sheet1$A1:B15]").GetXml());

        //}

    }
}
