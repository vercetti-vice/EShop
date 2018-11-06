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


        var color1 = new Color("Чёрный", "#000000") {Id = 1};
        var color2 = new Color("Белый", "#ffffff") {Id = 2};
        var color3 = new Color("Красный", "#ff0000") {Id = 3};
        var color4 = new Color("Зелёный", "#008000") {Id = 4};
        var color5 = new Color("Синий", "#0000ff") {Id = 5};

        context.Colors.Add(color1);
        context.Colors.Add(color2);
        context.Colors.Add(color3);
        context.Colors.Add(color4);
        context.Colors.Add(color5);

        var brand1 = new Brand("Samsung", "Крупнейший корейский производитель техники", "http://asdasd.com") {Id = 1};
        var brand2 = new Brand("Apple", "Самая дорогая американская компания", "http://qweqwe.com") {Id = 2};
        var brand3 = new Brand("Honor", "Странная китайская компания", "http://fghfgh.com") {Id = 3};

        context.Brands.Add(brand1);
        context.Brands.Add(brand2);
        context.Brands.Add(brand3);

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

        context.Categories.Add(category1);
        context.Categories.Add(category2);
        context.Categories.Add(category3);
        context.Categories.Add(category4);

        context.SaveChanges();
      }
    }

  }
}
