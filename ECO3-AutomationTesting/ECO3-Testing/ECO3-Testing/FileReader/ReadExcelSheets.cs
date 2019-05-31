using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ECO3_Testing.ExcelReader
{
    public class ReadExcelSheets
    {
        private static IDictionary<string, IExcelDataReader> cellData;
        private static FileStream stream;
        private static IExcelDataReader reader;

        static ReadExcelSheets()
        {
            cellData = new Dictionary<string, IExcelDataReader>();
        }

        public static object GetCellData(string filePath, string sheetName, int row, int column)
        {
            if (cellData.ContainsKey(sheetName))
            {
                reader = cellData[sheetName];
            }
            else
            {
                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                cellData.Add(sheetName, reader);
            }
            DataTable table = reader.AsDataSet().Tables[sheetName];
            return GetData(table.Rows[row][column].GetType(), table.Rows[row][column]);
        }

        private static IExcelDataReader GetExcelReader(string filePath, string sheetName)
        {
            FileStream fileStream = new FileStream(@"file location", FileMode.Open, FileAccess.Read);
            IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
            return reader;
        }
        public static int GetTotalRow(string filePath, string sheetName)
        {
            IExcelDataReader excelDataReader = GetExcelReader(filePath, sheetName);
            return excelDataReader.AsDataSet().Tables[sheetName].Rows.Count;
        }
        private static object GetData(Type type, object data)
        {
            switch (type.Name)
            {
                case "String":
                    return data.ToString();
                case "Double":
                    return Convert.ToDouble(data);
                case "DateTime":
                    return Convert.ToDateTime(data);
                default:
                    if (type.Name.Contains("Int"))
                    {
                        return Convert.ToInt64(data);
                    }
                    else
                    {
                        return null;
                    }

            }
        }
        public void ReadExcelSheet()
        {
            FileStream fileStream = new FileStream(@"file location", FileMode.Open, FileAccess.Read);
            IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
            var conf = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true
                }
            };
            DataTable resultSet = reader.AsDataSet(conf).Tables["SheetName"];

            for (int i = 0; i < resultSet.Rows.Count; i++)
            {
                var col = resultSet.Rows[i];
                for (int j = 0; j < col.ItemArray.Length; j++)
                {
                    var cellValue = col.ItemArray[j];
                }
            }
        }
    }
}
