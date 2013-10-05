using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using ExtensionMethods;

namespace ExtensionMethodsTests
{
    [TestClass]
    public class MyExtensionsTests
    {
        [TestMethod]
        public void CloneFullTable()
        {
            DataTable originalDataTable = TestHelpers.CreateTestTable();
            DataTable clonedTable = originalDataTable.CloneSelection("");
            Assert.AreEqual(originalDataTable.Rows.Count, clonedTable.Rows.Count);
        }

        [TestMethod]
        public void CloneNoRows()
        {
            DataTable originalDataTable = TestHelpers.CreateTestTable();
            DataTable clonedTable = originalDataTable.CloneSelection("false");
            Assert.AreEqual(0, clonedTable.Rows.Count);
        }

        [TestMethod]
        public void CloneOnlyFirstRow()
        {
            DataTable originalDataTable = TestHelpers.CreateTestTable();
            DataTable clonedTable = originalDataTable.CloneSelection("[Column1] like '*Row1*'");
            Assert.AreEqual(1, clonedTable.Rows.Count);
            Assert.AreEqual("Column1-Row1", clonedTable.Rows[0]["Column1"].ToString());
        }
    }

}
