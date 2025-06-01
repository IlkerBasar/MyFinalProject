using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ProductTest();
            //CategoryTest();
            //GetDetailsDtoTest();
            //ProductTest2();
        }

        private static void GetDetailsDtoTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            foreach (var product in productManager.GetProductDetails().Data)
            {
                Console.WriteLine(product.ProductName + "/" + product.CategoryName);
            }
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            var result = productManager.GetAll().Data;

            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName);
            }
        }
        private static void ProductTest2()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            var result = productManager.GetProductDetails();

            if (result.Success == true)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + "/" + product.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            
        }
    }
}