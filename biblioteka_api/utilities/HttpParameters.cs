using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace biblioteka_api.utilities
{
    public static class HttpParametersPagination
    {
        public async static Task paginationNumber<T>(this HttpContext httpContent,IQueryable<T> q)
        {
            if (httpContent == null)
            {
                throw new ArgumentNullException(nameof(httpContent));
            }
            
            int count = await q.CountAsync();
            httpContent.Response.Headers.Add("maxNumberOfUnits", count.ToString());

        }
    }
}
