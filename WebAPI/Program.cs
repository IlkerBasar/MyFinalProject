
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.Identity.Client;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Autofac entegrasonu
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterModule(new AutofacBusinessModule());
            });


            //Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject --> IoC Container
            //AOP
            builder.Services.AddControllers();


            // �leri seviye konu.
            // Dependency Injection. -> YouTube'dan video izle.
            // IProductService �a��r�cam. GetAll methodnu �a��rd�m. ProductManager art�k busun.
            // Singleton : Uygulama boyunca yaln�zca bir kez olu�turulur ve her seferinde ayn� �rnek kullan�l�r.
            // Transient : Servis her kullan�ld���nda yeni bir �rnek olu�turulur. K�sa �m�rl� servisler i�in uygundur.
            // Scoped : Her istek ba��na bir �rnek olu�turulur. Ayn� HTTP iste�i boyunca ayn� servis kullan�l�r.
            builder.Services.AddSingleton<IProductService, ProductManager>();
            //builder.Services.AddTransient<IProductService, ProductManager>();
            //builder.Services.AddScoped<IProductService, ProductManager>();
            //builder.Services.AddSingleton<IProductDal, EfProductDal>();



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        //Autofac entegrasonu
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        //    .ConfigureContainer<ContainerBuilder>(builder =>
        //    {
        //        builder.RegisterModule(new AutofacBusinessModule());
        //    })
        //    .ConfigureWebHostDefaults(webBuilder =>
        //    {
        //        webBuilder.UseStartup<StartUp>();
        //    });
    }
}