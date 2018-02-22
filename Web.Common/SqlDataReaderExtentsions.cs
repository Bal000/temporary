using System;
using System.Data.SqlClient;

namespace Web.Common
{
    public static class SqlDataReaderExtentsions
    {
        public static Int32 GetInt32Value(this SqlDataReader reader, string columnName)
        {
            return (int)reader[columnName];
        }

        public static Int32? GetNullableInt32Value(this SqlDataReader reader, string columnName)
        {
            if (reader.IsDBNull(reader.GetOrdinal(columnName)))
                return new int?();

            return (int)reader[columnName];
        }

        public static bool GetBooleanValue(this SqlDataReader reader, string columnName)
        {
            return (bool)reader[columnName];
        }

        public static bool? GetNullableBooleanValue(this SqlDataReader reader, string columnName)
        {
            if (reader.IsDBNull(reader.GetOrdinal(columnName)))
                return new bool?();

            return (bool)reader[columnName];
        }

        public static DateTime GetDateTimeValue(this SqlDataReader reader, string columnName)
        {
            return (DateTime)reader[columnName];
        }

        public static DateTime? GetNullableDateTimeValue(this SqlDataReader reader, string columnName)
        {
            if (reader.IsDBNull(reader.GetOrdinal(columnName)))
                return new DateTime?();

            return (DateTime)reader[columnName];
        }

        public static string GetStringValue(this SqlDataReader reader, string columnName)
        {
            return reader[columnName].ToString();
        }

        public static char GetCharValue(this SqlDataReader reader, string columnName)
        {
            return (char)reader[columnName];
        }

        public static char? GetNullableCharValue(this SqlDataReader reader, string columnName)
        {
            if (reader.IsDBNull(reader.GetOrdinal(columnName)))
                return new char?();

            return Convert.ToChar(reader[columnName]);
        }

        public static decimal GetDecimalValue(this SqlDataReader reader, string columnName)
        {
            return (decimal)reader[columnName];
        }

        public static decimal? GetNullableDecimalValue(this SqlDataReader reader, string columnName)
        {
            if (reader.IsDBNull(reader.GetOrdinal(columnName)))
                return new decimal?();

            return (decimal)reader[columnName];
        }

        public static Guid GetGuidValue(this SqlDataReader reader, string columnName)
        {
            return (Guid)reader[columnName];
        }
    }
}
