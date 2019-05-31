using ECO3_Testing.CustomException;
using System.Data.SqlClient;

namespace ECO3_Testing.DataSource
{
    public class DatabaseResult
    {
        public string firstColumn { get; set; }
        public string secondColumn { get; set; }
        public string thirdColumn { get; set; }
        public string forthColumn { get; set; }
        public string fifthColumn { get; set; }
        public string sixthColumn { get; set; }
        public string seventhColumn { get; set; }
        public string eigthColumn { get; set; }

        public void GetDataOfSpecifiedColumns(SqlDataReader dataReader)
        {
            int numberOfColumns = dataReader.FieldCount;
            switch (numberOfColumns)
            {
                case 1:
                    firstColumn = dataReader.GetValue(0).ToString();
                    break;
                case 2:
                    firstColumn = dataReader.GetValue(0).ToString();
                    secondColumn = dataReader.GetValue(1).ToString();
                    break;
                case 3:
                    firstColumn = dataReader.GetValue(0).ToString();
                    secondColumn = dataReader.GetValue(1).ToString();
                    thirdColumn = dataReader.GetValue(2).ToString();
                    break;
                case 4:
                    firstColumn = dataReader.GetValue(0).ToString();
                    secondColumn = dataReader.GetValue(1).ToString();
                    thirdColumn = dataReader.GetValue(2).ToString();
                    forthColumn = dataReader.GetValue(3).ToString();
                    break;
                case 5:
                    firstColumn = dataReader.GetValue(0).ToString();
                    secondColumn = dataReader.GetValue(1).ToString();
                    thirdColumn = dataReader.GetValue(2).ToString();
                    forthColumn = dataReader.GetValue(3).ToString();
                    fifthColumn = dataReader.GetValue(4).ToString();
                    break;
                case 6:
                    firstColumn = dataReader.GetValue(0).ToString();
                    secondColumn = dataReader.GetValue(1).ToString();
                    thirdColumn = dataReader.GetValue(2).ToString();
                    forthColumn = dataReader.GetValue(3).ToString();
                    fifthColumn = dataReader.GetValue(4).ToString();
                    sixthColumn = dataReader.GetValue(5).ToString();
                    break;
                case 7:
                    firstColumn = dataReader.GetValue(0).ToString();
                    secondColumn = dataReader.GetValue(1).ToString();
                    thirdColumn = dataReader.GetValue(2).ToString();
                    forthColumn = dataReader.GetValue(3).ToString();
                    fifthColumn = dataReader.GetValue(4).ToString();
                    sixthColumn = dataReader.GetValue(5).ToString();
                    seventhColumn = dataReader.GetValue(6).ToString();
                    break;
                case 8:
                    firstColumn = dataReader.GetValue(0).ToString();
                    secondColumn = dataReader.GetValue(1).ToString();
                    thirdColumn = dataReader.GetValue(2).ToString();
                    forthColumn = dataReader.GetValue(3).ToString();
                    fifthColumn = dataReader.GetValue(4).ToString();
                    sixthColumn = dataReader.GetValue(5).ToString();
                    seventhColumn = dataReader.GetValue(6).ToString();
                    eigthColumn = dataReader.GetValue(7).ToString();
                    break;
                default:
                    throw new ECO3TestException("Number of ("+numberOfColumns +") specified columns in SQL Query is more than can be processed, specify between 1 to 8 columns in sql query");
            }

        }
    }
}
