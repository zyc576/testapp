using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using ZTunnel.Pmms.Api.Middleware;
using ZTunnel.Pmms.Core.Log;
using ZTunnel.Pmms.Repository.IRepository;
using ZTunnel.Pmms.Repository.Repository;
using ZTunnel.Pmms.Service.IService;
using ZTunnel.Pmms.Service.Service;

namespace ZTunnel.Pmms.Api
{
    public class Startup
    {
        /// <summary>
        /// log4net 仓储库
        /// </summary>
        public static ILoggerRepository Repository { get; set; }
        public Startup(IConfiguration configuration,IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
            //log4net
            Repository = LogManager.CreateRepository("ZTunnel.Pmms.Api");
            //指定配置文件
            var contentPath = env.ContentRootPath;
            var log4Config = Path.Combine(contentPath, "log4net.config");
            XmlConfigurator.Configure(Repository, new FileInfo(log4Config));
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region swagger配置
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "养护平台Api", Version = "v1" });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "api.xml");
                c.IncludeXmlComments(xmlPath, true);
            });
            #endregion

            // log日志注入
            services.AddSingleton<ILoggerHelper, LogHelper>();

            string connectionString = Configuration.GetConnectionString("ConnectionString");
            services.AddTransient<IDbConnection>(s => new SqlConnection(connectionString));

            services.AddScoped<ITunnelInfoService, TunnelInfoService>();
            services.AddScoped<ITunnelInfoRepository, TunnelInfoRepository>();
            services.AddMvc(options => { options.Filters.Add<ExceptionFilter>(); })
                .AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseReuestResponseLog();//记录请求与返回数据 
            //else
            //{
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "养护平台Api");
                // c.InjectJavascript("zh_CN.js");

            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
