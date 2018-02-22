using System.Collections.Generic;
using System.Data.SqlClient;
using MyWebApp.Models;
using Web.Common;
using Configuration;
using System;

namespace MyWebApp.Resources
{
    public class AdventureWorksRepository : IAdventureWorksRepository
    {
        public List<ProductViewModel> GetPaginatedProducts(int page, int pageSize)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            using (var conn = new SqlConnection(DataAccess.AdventureWorksDB))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[usp_PaginatedProducts]";
                    cmd.Parameters.AddWithValue("@Page", page);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            products.Add(new ProductViewModel()
                            {
                                ProductID = reader.GetInt32Value("ProductID"),
                                Name = reader.GetStringValue("Name"),
                                ProductNumber = reader.GetStringValue("ProductNumber"),
                                Color = reader.GetStringValue("Color"),
                                StandardCost = reader.GetDecimalValue("StandardCost"),
                                ListPrice = reader.GetDecimalValue("ListPrice"),
                                Size = reader.GetStringValue("Size"),
                                Weight = reader.GetNullableDecimalValue("Weight"),
                                Class = reader.GetNullableCharValue("Class"),
                                Style = reader.GetNullableCharValue("Style"),
                                SellStartDate = reader.GetDateTimeValue("SellStartDate"),
                                SellEndDate = reader.GetNullableDateTimeValue("SellEndDate"),
                                RowGuid = reader.GetGuidValue("rowguid"),
                                ModifiedDate = reader.GetDateTimeValue("ModifiedDate"),
                                RowCount = reader.GetInt32Value("RowCount")

                            });
                        }
                        conn.Close();
                    }
                }
            }

            return products;
        }
    }
}