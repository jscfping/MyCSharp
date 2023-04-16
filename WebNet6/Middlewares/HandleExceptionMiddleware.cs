using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Core31.Library.Exceptions;
using Core31.Library.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace WebNet6.Middlewares
{
    public class HandleExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HandleExceptionMiddleware(RequestDelegate next, IWebHostEnvironment webHostEnvironment)
        {
            _next = next;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException ex)
            {
                context.Response.StatusCode = ex.HttpCode;
                context.Response.ContentType = "application/json";

                if (_webHostEnvironment.IsDevelopment())
                {
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new DevErrorResponse(ex.Message, ex.HttpCode, ex.StackTrace)));
                }
                else
                {
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse(ex.Message)));
                }
            }
            catch (Exception ex)
            {
                int httpCode = (int)HttpStatusCode.InternalServerError;
                var error = _webHostEnvironment.IsDevelopment()
                    ? new DevErrorResponse(ex.Message, httpCode, ex.StackTrace)
                    : new ErrorResponse("error occurs!");

                context.Response.StatusCode = httpCode;
                context.Response.ContentType = "application/json";
                
                if (_webHostEnvironment.IsDevelopment())
                {
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new DevErrorResponse(ex.Message, httpCode, ex.StackTrace)));
                }
                else
                {
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse("error occurs!")));
                }
            }
        }
    }
}