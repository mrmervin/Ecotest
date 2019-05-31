using ECO3_Testing.CustomException;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ECO3_Testing.FileReader
{
    public class CsvFileReader
    {
        List<ErrorFileColumns> allData = new List<ErrorFileColumns>();
        private List<string> dataInRows(string downloadedCsvPath)
        {
         return File.ReadAllLines(downloadedCsvPath).ToList();
        }

        public int RetrieveDownloadedCsvData(string filePath)
        {
            foreach (var row in dataInRows(filePath))
            {
                string[] entries = row.Split(',');
                ErrorFileColumns columns = new ErrorFileColumns();

                columns.Id = entries[0];
                columns.DataFileType = entries[1];
                columns.UploadedBy = entries[2];
                columns.UploadedAt = entries[3];
                columns.DocumentId = entries[4];
                columns.Version = entries[5];
                columns.Data = entries[6];
                columns.Status = entries[7];
                columns.ValidationErrorCode = entries[8];
                columns.ValidationError = entries[9];
                columns.LineNo = entries[10];
                allData.Add(columns);
            }
           return allData.Count;
        }

        public string GetColumnData(string columnName, int rowNumber)
        {
            //Row number 0 is header
            if (rowNumber <= 0)
            {
                rowNumber = 1;
            }
            switch (columnName.ToString().ToUpper())
            {
                case "ID":
                    return allData[rowNumber].Id;
                case "DATAFILETYPE":
                    return allData[rowNumber].DataFileType;
                case "UPLOADEDBY":
                    return allData[rowNumber].UploadedBy;
                case "UPLOADEDAT":
                    return allData[rowNumber].UploadedAt;
                case "DOCUMENTID":
                    return allData[rowNumber].DocumentId;
                case "VERSION":
                    return allData[rowNumber].Version;
                case "DATA":
                    return allData[rowNumber].Data;
                case "STATUS":
                    return allData[rowNumber].Status;
                case "VALIDATIONERRORCODE":
                    return allData[rowNumber].ValidationErrorCode;
                case "VALIDATIONERROR":
                    return allData[rowNumber].ValidationError;
                case "LINENO":
                    return allData[rowNumber].LineNo;
                default :
                    throw new ECO3TestException("Column name " + columnName.ToString() + " does not exist, please add colmuns from ErrorFileColumns class");
            }
         }
    }
}
