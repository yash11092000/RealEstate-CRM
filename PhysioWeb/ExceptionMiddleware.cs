using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using PdfSharp.Fonts;
using PhysioWeb.Data;
using PhysioWeb.Helpers;
using PhysioWeb.Models;
using PhysioWeb.Repository;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // next middleware
        }
        catch (Exception ex)
        {
            // Log error (Serilog / DB / File)
            Console.WriteLine(ex.Message);

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var result = new
            {
                message = "Something went wrong. Please try again later."
            };

            await context.Response.WriteAsJsonAsync(result);
        }
    }
}