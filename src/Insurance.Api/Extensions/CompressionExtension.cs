﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;

namespace Insurance.Api.Extensions
{
    public static class CompressionExtension
    {
        public static IServiceCollection AddCompression(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(
            options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            return services;
        }
    }
}
