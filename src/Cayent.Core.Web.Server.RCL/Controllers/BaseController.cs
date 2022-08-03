using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cayent.Core.Web.Server.RCL.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected string StoreId
        {
            get
            {
                var storeId = string.Empty;

                var cookie = Request.Cookies["StoreId"];

                if (!string.IsNullOrWhiteSpace(cookie))
                    storeId = cookie;

                return storeId;
            }
        }

        protected string UserId
        {
            get
            {
                var item = User.FindFirstValue(ClaimTypes.NameIdentifier);

                return item;
            }
        }

        protected string TenantIdFromClaims
        {
            get
            {
                var item = User.FindFirstValue("TenantId");

                return item;
            }
        }

        protected string TenantIdFromRouteValues
        {
            get
            {
                var item = "test";// Request.RouteValues["tenant"]?.ToString();

                return item;
            }
        }

        protected Guid Guid()
        {
            byte[] guidArray = System.Guid.NewGuid().ToByteArray();

            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;

            // Get the days and milliseconds which will be used to build the byte string 
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;

            // Convert to a byte array 
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            // Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }

        protected string GuidStr()
        {
            return Guid().ToString().ToLower();
        }

        protected string GenerateRandomCode(int count = 8)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random(DateTime.Now.Second);
            var result = new string(
                Enumerable.Repeat(chars, count)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }
    }
}
