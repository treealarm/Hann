using Hahn.ApplicatonProcess.February2021.Data;
using Hahn.ApplicatonProcess.February2021.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using FluentValidation.AspNetCore;

namespace Hahn.ApplicatonProcess.February2021.Web
{
    namespace swag_examples
    {
        public class SingleAsset : IExamplesProvider<Asset>
        {
            public Asset GetExamples()
            {
                return new Asset
                {
                    AssetName = "Billy",
                    Department = Asset.EN_DEPARTMENT.MaintenanceStation,
                    CountryOfDepartment = "US",
                    EMailAdressOfDepartment = "bill@microsoft.com",
                    PurchaseDate = DateTime.UtcNow,
                    broken = false
                };
            }
        }

        public class MultiAsset : IExamplesProvider<IEnumerable<Asset>>
        {
            public IEnumerable<Asset> GetExamples()
            {
                return new List<Asset>()
            {
                new Asset
                {
                    AssetName = "Angela",
                    Department = Asset.EN_DEPARTMENT.Store1,
                    CountryOfDepartment = "DE",
                    EMailAdressOfDepartment = "angela@microsoft.com",
                    PurchaseDate = DateTime.UtcNow,
                    broken = false
                },
                new Asset
                {
                    AssetName = "Gerhard",
                    Department = Asset.EN_DEPARTMENT.Store2,
                    CountryOfDepartment = "DE",
                    EMailAdressOfDepartment = "gerhard@microsoft.com",
                    PurchaseDate = DateTime.UtcNow,
                    broken = false
                }
            };
            }
        }
    }
    
    
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddScoped<IUnitOfWork<Asset>, UnitOfWork>();
            services.AddScoped<IRepository<Asset>, AssetRepositoryImp>();
            services.AddDbContext<AssetContext>();

            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Asset>());

            services.AddSwaggerGen(setUpAction =>
            {
                setUpAction.SwaggerDoc("APISpecification", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "AssetApi",
                    Version = "1.0"
                });
                setUpAction.ExampleFilters();

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //setUpAction.IncludeXmlComments(xmlCommentsFile);
                //setUpAction.AddFluentValidationRules();
            });
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(setUpAction =>
            {
                setUpAction.SwaggerEndpoint("/swagger/APISpecification/swagger.json", "Applicant API");
                setUpAction.RoutePrefix = "";

            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
