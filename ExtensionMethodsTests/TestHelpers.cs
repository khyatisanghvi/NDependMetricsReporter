using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExtensionMethodsTests
{
    static class TestHelpers
    {
        public static DataTable CreateTestTable()
        {
            DataTable table = new DataTable("TestTAble");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = typeof(System.String);
            column.ColumnName = "Column1";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(System.String);
            column.ColumnName = "Column2";
            table.Columns.Add(column);

            for (int i = 0; i <= 2; i++)
            {
                row = table.NewRow();
                row["Column1"] = "Column1-Row" + i;
                row["Column2"] = "Column2-Row " + i;
                table.Rows.Add(row);
            }

            return table;
        }


    }
}
