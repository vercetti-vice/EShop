using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Core.Entities;
using EShop.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Helpers
{
  public class DbInitializer
  {
    private readonly IServiceProvider _serviceProvider;

    public DbInitializer(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    public void Initialize()
    {
      InitalizeContext();
    }

    private void InitalizeContext()
    {
      using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

        var brand1 = new Brand("Samsung", "Крупнейший корейский производитель техники", "http://asdasd.com") {Id = 1};
        var brand2 = new Brand("Apple", "Самая дорогая американская компания", "http://qweqwe.com") {Id = 2};
        var brand3 = new Brand("Honor", "Странная китайская компания", "http://fghfgh.com") {Id = 3};
        var brand4 = new Brand("Xiaomi", "Странная китайская компания", "http://fghfgsdfe.com") { Id = 4 };
        var brand5 = new Brand("Microsoft", "Очередная американская компания", "http://microsoft.com") { Id = 5 };

        context.Brands.Add(brand1);
        context.Brands.Add(brand2);
        context.Brands.Add(brand3);
        context.Brands.Add(brand4);
        context.Brands.Add(brand5);

        var city1 = new City("Казань") {Id = 1};
        var city2 = new City("Москва") {Id = 2};
        var city3 = new City("Санкт-Петербург") {Id = 3};

        context.Cities.Add(city1);
        context.Cities.Add(city2);
        context.Cities.Add(city3);


        var category1 = new Category("Техника", null) {Id = 1};
        var category2 = new Category("Ноутбуки", parentCategoryId: 1) { Id = 2 };
        var category3 = new Category("Смартфоны", parentCategoryId: 1) { Id = 3 };
        var category4 = new Category("Умные часы", parentCategoryId: 1) { Id = 4 };
        var category5 = new Category("Apple", parentCategoryId: 3) { Id = 5 };

        context.Categories.Add(category1);
        context.Categories.Add(category2);
        context.Categories.Add(category3);
        context.Categories.Add(category4);

        var product1 = new Product("iPhone X", 3, 2, 80000, "The most powerful smartphone ever", "http://iphone.com") { Id = 1 };
        var product2 = new Product("Samsung S9", 3, 1, 30000, "The worst smartphone ever", "http://samsungs9.com") { Id = 2 };
        var product3 = new Product("Honor X10", 3, 3, 40000, "The most chinese smartphone ever", "http://honorx10.com") { Id = 3 };

        context.Products.Add(product1);
        context.Products.Add(product2);
        context.Products.Add(product3);

        context.SaveChanges();
      }
    }

  }
}
