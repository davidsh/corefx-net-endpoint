﻿using System;
using Microsoft.AspNetCore.Http;

namespace NetCoreServer
{
    public class StatusCodeHandler
    {
        public static void Invoke(HttpContext context)
        {
            string statusCodeString = context.Request.Query["statuscode"];
            string statusDescription = context.Request.Query["statusdescription"];
            try
            {
                int statusCode = int.Parse(statusCodeString);
                context.Response.StatusCode = statusCode;
                context.Response.SetStatusDescription(
                    string.IsNullOrWhiteSpace(statusDescription) ? " " : statusDescription);
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                context.Response.SetStatusDescription($"Error parsing statuscode: {statusCodeString}");
            }
        }
    }
}