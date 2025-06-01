
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


            // Ýleri seviye konu.
            // Dependency Injection. -> YouTube'dan video izle.
            // IProductService çaðýrýcam. GetAll methodnu çaðýrdým. ProductManager artýk busun.
            // Singleton : Uygulama boyunca yalnýzca bir kez oluþturulur ve her seferinde ayný örnek kullanýlýr.
            // Transient : Servis her kullanýldýðýnda yeni bir örnek oluþturulur. Kýsa ömürlü servisler için uygundur.
            // Scoped : Her istek baþýna bir örnek oluþturulur. Ayný HTTP isteði boyunca ayný servis kullanýlýr.
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