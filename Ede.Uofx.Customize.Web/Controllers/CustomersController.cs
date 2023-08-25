using Ede.Uofx.Customize.Web.Models;
using Ede.Uofx.Customize.Web.Service;
using Ede.Uofx.FormSchema.UofxFormSchema;
using Ede.Uofx.PubApi.Sdk.NetStd.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace Ede.Uofx.Customize.Web.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class CustomersController : ControllerBase
    {
        [HttpGet("CheckedPrice")]
        public string CheckedPrice(string? price)
        {

            if (price == null)
                return "";

            if (Convert.ToDecimal(price) > 10)
            {
                return "金額不可超過10";
            }

            return "";
        }

        [HttpPost("CheckedOrderPrice")]
        public string CheckedOrderPrice(CheckedOrderPriceModel model)
        {

            if (model.price == null || model.OrderID == null)
                return "";

            if (Convert.ToDouble(model.price) > Convert.ToDouble(model.ProductPrice) * 0.8)
            {
                return "實際金額不可大於產品金額8成";
            }

            return "";
        }

        [HttpPost("CreateFormData")]
        public string CreateFormData(FormData model)
        {

            SqlTransaction trans = Connection.GetTransaction();
            try
            {
                Connection.OpenConnection(trans);
                return CustomersService.CreateOrder(trans, model.FORM_NBR, model.CATEGORY_ID, model.PRODUCT_ID,
                    model.APPLICANT,model.UNIT_PRICE.ToString(), model.FORM_RESULT);
            }
            finally
            {
                Connection.CloseConnection(trans);
            }

            return "";
        }

        [HttpPost("UpdateFormData")]
        public string UpdateFormData(FormStatus model)
        {

            SqlTransaction trans = Connection.GetTransaction();
            try
            {
                Connection.OpenConnection(trans);
                return CustomersService.UpdateOrder(trans, model.FORM_NBR, model.FORM_RESULT);
            }
            finally
            {
                Connection.CloseConnection(trans);
            }

            return "";
        }



        [HttpGet("GetCategories")]
        public List<Categories> GetCategories()
        {
            SqlTransaction trans = Connection.GetTransaction();
            try
            {
                Connection.OpenConnection(trans);
                return CustomersService.GetCategories(trans);
            }
            finally
            {
                Connection.CloseConnection(trans);
            }
        }

        [HttpGet("GetProducts")]
        public List<Products> Products(string? categoryID)
        {
            if (string.IsNullOrEmpty(categoryID))
            {
                return new List<Products>();
            }
            SqlTransaction trans = Connection.GetTransaction();
            try
            {
                Connection.OpenConnection(trans);
                return CustomersService.GetProducts(trans, categoryID);
            }
            finally
            {
                Connection.CloseConnection(trans);
            }
        }

        [HttpGet("GetProductPrice")]
        public List<Products> GetProductPrice(string? productID)
        {
            if (string.IsNullOrEmpty(productID))
            {
                return new List<Products>();
            }
            SqlTransaction trans = Connection.GetTransaction();
            try
            {
                Connection.OpenConnection(trans);
                return CustomersService.GetProductPrice(trans, productID);
            }
            finally
            {
                Connection.CloseConnection(trans);
            }
        }

        [HttpGet("GetCustomers")]
        public List<Customers> GetCustomers()
        {
            SqlTransaction trans = Connection.GetTransaction();
            try
            {
                Connection.OpenConnection(trans);
                return CustomersService.GetCustomers(trans);
            }
            finally
            {
                Connection.CloseConnection(trans);
            }
        }

        [HttpGet("GetOrders")]
        public List<Order> GetOrders(string? customerID)
        {

            if (string.IsNullOrEmpty(customerID))
            {
                return new List<Order>();
            }
            SqlTransaction trans = Connection.GetTransaction();
            try
            {
                Connection.OpenConnection(trans);
                return CustomersService.GetOrders(trans, customerID);
            }
            finally
            {
                Connection.CloseConnection(trans);
            }
        }

        [HttpPost("CheckedOrderEstimatedPrice")]
        public string CheckedOrderEstimatedPrice(CheckedOrderPriceModel model)
        {

            if (model.estimatedPrice == null)
                return "";

            SqlTransaction trans = Connection.GetTransaction();
            try
            {
                Connection.OpenConnection(trans);

                List<OrderDetailTotal> list = CustomersService.GetOrderTotal(trans, model.OrderID);
                if (Convert.ToDouble(model.estimatedPrice) > Convert.ToDouble(list.First().OrderSum))
                {
                    return $"您輸入的採購金額大於原始訂單訂價{list.First().OrderSum}";
                }
            }
            finally
            {
                Connection.CloseConnection(trans);
            }


            return "";
        }

        [HttpPost("StartOrderComfirmForm")]
        public string StartOrderComfirmForm(OrderComfirmForm model)
        {

            if (model.estimatedPrice == null || string.IsNullOrEmpty(model.EmployeeID) || string.IsNullOrEmpty(model.OrderID))
                return "";
            try
            {
                var traceId = RunTask(model);

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                string errorMsg = $"It is error.";

                if (UofxService.Error.IsUnknownException(ex, out var message))
                {
                    errorMsg += "\r\n" + "Unknown-Exception:";
                    errorMsg += "\r\n" + DeserializeObject(message);
                }
                else if (UofxService.Error.TryGetErrorCodes(ex, out var errorCodes))
                {
                    errorMsg += "\r\n" + "ErrorCodes:";
                    errorMsg += "\r\n" + DeserializeObject(errorCodes);
                }
                else
                {
                    errorMsg += "\r\n" + "Exception:";
                    errorMsg += "\r\n" + DeserializeObject(ex);
                }

                return errorMsg;
            }


            return "";
        }



        async System.Threading.Tasks.Task<string> RunTask(OrderComfirmForm model)
        {
            //   UofxService.Key = "PFJTQUtleVZhbHVlPjxNb2R1bHVzPno2NEZPNkM1TXRJUnVwczdrWG43clRjeFZPS2ZHYkh3RDhjN3ZsYUFRUWJWWXVac2tvVHRFWlFlWWVDdHlxK2RSQXgxeXZjbmc4RGlXOGtVclNVbDJsc1AwblNHYjNId0ROK0hJNUFPbXpnMG5WaEp5czg0OSthUmJpV3R4QkxudUtBY0JLWk8yb1A1S2ZZcE1zdEhLbDNsV3FzNjh5U0VXY0pPRTUrc1dEcz08L01vZHVsdXM\\+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjwvUlNBS2V5VmFsdWU\\+";
            // 在串接服務 => API服務管理中設定key
            UofxService.Key = "eyJDbGllbnRJZCI6ImFjNmE0NzAzLTVmNDgtNDE2NC05ZmZkLTA4ZGI5YTRhZDlkYiIsIlByaXZhdGVLZXkiOiJQRkpUUVV0bGVWWmhiSFZsUGp4TmIyUjFiSFZ6UG5aSFVURTFSa3BhTDBoTEwyd3lVR1YzYVdoVVpVSkZiMVZsZVVjMlpGWlhlREl5YUVsTFUweHFkMjAzVDB0cE0ybE9PVXR2YmpFMFpFdFNZakJ4VjB4SE1uQkJRVVpFTVN0ek9IQXdaM2RKY1c1R1QxWnpNWFJWVUdOYWFXdFNjRVpQVjNoUFF5OVNMMFJSVFhSaVdFUmFVRFJ5ZEVScUwzcHVZMDA0YldWbmIzQkRhV2RHVFhKYVYydHZMMnRrYzFWSkszcDNTek5KU1ZOV1pEbDNURWgwVjBKaE1FbDBMMGR5VFQwOEwwMXZaSFZzZFhNXHUwMDJCUEVWNGNHOXVaVzUwUGtGUlFVSThMMFY0Y0c5dVpXNTBQanhRUGpkeFlUVlZWbkFyTVVKMGNXb3JiVTVCWW10WlprVk9jQ3QxTml0elNpOU5hemd6ZWxSSWNYSXpiWEF4VFRFMllrOTRlaXROV0U1bFN6bFdaRXR3VkV4c2FFVkVNMGxrYjFKeFpHZHJOV2xUZFVOcFVuQlJQVDA4TDFBXHUwMDJCUEZFXHUwMDJCZVdoWmIwUlJhMjE0ZVZWRVdUTTJObGhFZUd4a1oyeHZhQzlwTUd4Q05sbDRPREUyUkdsVGVUWk5VMVpOUTJnNFpDdG5kMFJzU2xjNGMyVTNNV0pWU0RocVMwOVBhRWgyVEdoRVdFOTVaMkYzTTNOaVpIYzlQVHd2VVQ0OFJGQVx1MDAyQlkwWjRMMFpDWVZCalJEUlpVbVp0YjAxbk9UZDJPRmhrZFVFclFVdEthVlZOVjB4bFJXcDNVR1kwUTFBdlkyVnNVRzltUVVKdWFGTTVhMEpYZUZJeVdsaFBXbFk0ZUdGMGRXUmFSVmRxYlZSdGNrVXZTVkU5UFR3dlJGQVx1MDAyQlBFUlJQbTFUYlRONU1FWXZXVmh2WkVkdFEyNVZSbUkyVFhvNGIwOWlNekkxZW1oV1dsVklTREl3V2xoVWRrbElaa2wxUXpSelNscEJaRWhKVlhCUGRFOXRhazVxTDFKbVRsbFlORTlJY2s1R1pXNXpaSFpKYzA5M1BUMDhMMFJSUGp4SmJuWmxjbk5sVVQ1RVRDc3haV2RQYjI5aWQwZHlORUpJT0ZBNFIwUnhaazVKVWxNME55dElNMnB2UjJwNWNGTkRPVFZPVmpVck5HVTRSR1pITlUxamJWQTVhMlpITUVNMWN6WjBlR0p0UkdOS2MwRXZTV3h6WnpsdFVGbFBaejA5UEM5SmJuWmxjbk5sVVQ0OFJENTJRamhLV0dKVlMxaE1aWFJOUW5sMldGbDRkRkpXUmtWNFVVZ3JLM1JPY0dNek1qWjZNa1JuV2pOYVpFOVZabTg1YTIxaVQzTlZNa2R4ZG1oalYybFVhakF5TjBkV1Z5OVBOMUJSTTBGdVpWRlhNR2gyYmpSS1lsaGhPVGc1WTJwWVFYQkhOR2QwSzA1aGJYTldkRVEzY0hSaU1rZDZSMmwzWjBwclVGY3lVelJXV0hoeU4yd3dWRGxOU21KUk0wOVdibTVyVjJOWlJVb3JTMnRZTTNVMVdtcGxWMEp1YzFsTU9HczlQQzlFUGp3dlVsTkJTMlY1Vm1Gc2RXVVx1MDAyQiIsIlB1YmxpY0tleSI6IlBGSlRRVXRsZVZaaGJIVmxQanhOYjJSMWJIVnpQblpIVVRFMVJrcGFMMGhMTDJ3eVVHVjNhV2hVWlVKRmIxVmxlVWMyWkZaWGVESXlhRWxMVTB4cWQyMDNUMHRwTTJsT09VdHZiakUwWkV0U1lqQnhWMHhITW5CQlFVWkVNU3R6T0hBd1ozZEpjVzVHVDFaek1YUlZVR05hYVd0U2NFWlBWM2hQUXk5U0wwUlJUWFJpV0VSYVVEUnlkRVJxTDNwdVkwMDRiV1ZuYjNCRGFXZEdUWEphVjJ0dkwydGtjMVZKSzNwM1N6TkpTVk5XWkRsM1RFaDBWMEpoTUVsMEwwZHlUVDA4TDAxdlpIVnNkWE1cdTAwMkJQRVY0Y0c5dVpXNTBQa0ZSUVVJOEwwVjRjRzl1Wlc1MFBqd3ZVbE5CUzJWNVZtRnNkV1VcdTAwMkIiLCJQcml2YXRlS2V5UGVtIjoiLS0tLS1CRUdJTiBSU0EgUFJJVkFURSBLRVktLS0tLVxuTUlJQ1hRSUJBQUtCZ1FDOFpEWGtVbG44Y3JcdTAwMkJYWTk3Q0tGTjRFU2hSN0licDFWYkhiYUVncEl1UENiczRxTGVJXG4zMHFpZlhoMHBGdlNwWXNiYWtBQVVQWDZ6eW5TREFpcWNVNVd6VzFROXhtS1JHa1U1YkU0TDlIOE5BeTF0Y05rXG4vaXUwT1AvT2R3enlaNkNpa0tLQVV5dGxhU2pcdTAwMkJSMnhRajdQQXJjZ2hKVjMzQXNlMVlGclFpMzhhc3dJREFRQUJcbkFvR0JBTHdmQ1YyMUNseTNyVEFjcjEyTWJVVlJSTVVCL3ZyVGFYTjl1czlnNEdkMlhUbEg2UFpKbXpyRk5ocXJcbjRYRm9rNDlOdXhsVnZ6dXowTndKM2tGdEliNVx1MDAyQkNXMTJ2ZlBYSTF3S1J1SUxmaldwckZiUVx1MDAyQjZiVzloc3hvc0lDXG5aRDF0a3VGVjhhXHUwMDJCNWRFL1RDVzBOemxaNTVGbkdCQ2ZpcEY5N3VXWTNsZ1o3R0MvSkFrRUE3cWE1VVZwXHUwMDJCMUJ0cVxualx1MDAyQm1OQWJrWWZFTnBcdTAwMkJ1Nlx1MDAyQnNKL01rODN6VEhxcjNtcDFNMTZiT3h6XHUwMDJCTVhOZUs5VmRLcFRMbGhFRDNJZG9ScWRnXG5rNWlTdUNpUnBRSkJBTW9XS0EwSkpzY2xBMk5cdTAwMkJ1bHc4WlhZSmFJZjR0SlFlbU1mTmVnNGtzdWpFbFRBb2ZIZm9cbk1BNVNWdkxIdTlXMUIvSXlqam9SN3k0UTF6c29Hc043RzNjQ1FIQmNmeFFXajNBXHUwMDJCR0VYNXFESVBlNy9GM2JnUFxuZ0NpWWxERmkzaEk4RDNcdTAwMkJBai8zSHBUNkh3QVo0VXZaQVZzVWRtVnptVmZNV3JibldSRm81azVxeFB5RUNRUUNaXG5LYmZMUVg5aGVoMGFZS2RRVnZvelB5ZzV2ZmJuT0ZWbFFjZmJSbGRPOGdkOGk0TGl3bGtCMGNoU2s2MDZhTTJQXG45RjgxaGZnNGVzMFY2ZXgyOGl3N0FrQU12N1Y2QTZpaHZBYXZnRWZ3L3dZT3A4MGhGTGp2NGZlT2dhUEtsSUwzXG5rMVhuN2g3d044Ymt4eVkvMlI4YlFMbXpxM0Z1WU53bXdEOGlXeUQyWTlnNlxuLS0tLS1FTkQgUlNBIFBSSVZBVEUgS0VZLS0tLS0iLCJQdWJsaWNLZXlQZW0iOiItLS0tLUJFR0lOIFBVQkxJQyBLRVktLS0tLVxuTUlHZk1BMEdDU3FHU0liM0RRRUJBUVVBQTRHTkFEQ0JpUUtCZ1FDOFpEWGtVbG44Y3JcdTAwMkJYWTk3Q0tGTjRFU2hSXG43SWJwMVZiSGJhRWdwSXVQQ2JzNHFMZUkzMHFpZlhoMHBGdlNwWXNiYWtBQVVQWDZ6eW5TREFpcWNVNVd6VzFRXG45eG1LUkdrVTViRTRMOUg4TkF5MXRjTmsvaXUwT1AvT2R3enlaNkNpa0tLQVV5dGxhU2pcdTAwMkJSMnhRajdQQXJjZ2hcbkpWMzNBc2UxWUZyUWkzOGFzd0lEQVFBQlxuLS0tLS1FTkQgUFVCTElDIEtFWS0tLS0tIn0=";
            UofxService.UofxServerUrl = "http://172.16.3.16/";

            var traceid = await UofxService.BPM.
                ApplyForm(new UofxFormSchema()
                {

                    Account = model.Account,
                    DeptCode = model.DeptCode,
                    Fields =
                    { C002 = model.EmployeeID, C003 = model.OrderID, C004 = int.Parse(model.estimatedPrice.ToString()) }
                });

            return traceid;

        }


        private static object DeserializeObject(Exception e)
        {
            try
            {
                return JsonConvert.DeserializeObject(e.Message);
            }
            catch
            {
                return e.Message;
            }
        }
        private static object DeserializeObject(object obj)
        {
            var str = JsonConvert.SerializeObject(obj);
            try
            {
                return JsonConvert.DeserializeObject(str);
            }
            catch
            {
                return str;
            }
        }


    }
}
