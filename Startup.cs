using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace ResponseCompression
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // 封包壓縮的服務
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<CustomCompressionProvider>();
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    "image/png"
                });
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            // 封包壓縮的 Middleware
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
