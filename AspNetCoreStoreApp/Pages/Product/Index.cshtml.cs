using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AspNetCoreStoreApp.Pages.Client
{
    public class IndexModel : PageModel
    {
        public List<ProductInfo> listvalue = new List<ProductInfo>();
        public void OnGet()
        {
            try
            {
                 string ConnectionString = @"data source=DESKTOP-4H46A0N\MSSQLSERVER01; database=Books; integrated security=SSPI";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    // Creating SqlCommand objcet   
                    SqlCommand cm = new SqlCommand("select * from products", connection);
                    // Opening Connection  
                    connection.Open();
                    // Executing the SQL query  
                    SqlDataReader sdr = cm.ExecuteReader();
                    while (sdr.Read())
                    {
                        ProductInfo productInfo = new ProductInfo();
                        productInfo.ProductID = sdr.GetInt32(0).ToString();
                        productInfo.ProductName = sdr.GetString(1);
                        productInfo.Price =  sdr.GetDecimal(2).ToString();
                        productInfo.LastModified = sdr.GetDateTime(3).ToString();

                        listvalue.Add(productInfo);

                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class ProductInfo
    {
        public string ProductID;
        public string ProductName;
        public string Price;
        public string LastModified;
    }
}
