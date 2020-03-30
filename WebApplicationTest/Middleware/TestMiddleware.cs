using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplicationTest.Middleware
{
    internal class TestMiddleware
    {
        private readonly RequestDelegate next;

        public TestMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var claimsIdentity = (ClaimsIdentity)context.User.Identity;
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, "IamNotTony"));
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, "Tony"));
            context.User = new ClaimsPrincipal(claimsIdentity);

            return next(context);
        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static void UseTestMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<TestMiddleware>();
        }
    }
}
