using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static DataTable CloneSelection(this DataTable dataTableToClone, string selectFilter)
        {
            DataTable clonedDataTable = dataTableToClone.Clone();
            DataRow[] selectedDataRows = dataTableToClone.Select(selectFilter);
            foreach (DataRow d in selectedDataRows)
            {
                clonedDataTable.ImportRow(d);
            }
            return clonedDataTable;
        }
    }
}
