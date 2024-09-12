using CsvHelper.TypeConversion;
using CsvHelper;
using System.Net.Sockets;
using System.Net;

namespace ContactManager.API.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (HeaderValidationException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("CSV file has invalid headers. Please ensure the CSV contains the correct fields.");
            }
            catch (TypeConverterException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("CSV file contains invalid data types. Please ensure the data is properly formatted.");
            }
            catch (ReaderException re) when (re.InnerException is FormatException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Birth Date is in invalid format.");
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"Something went wrong");
            }
        }
    }
}
