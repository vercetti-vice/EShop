using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Core.Interfaces;
using Product = DAL.Models.Product;

namespace DAL
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }


    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountManager _accountManager;
        private readonly ILogger _logger;

        public DatabaseInitializer(ApplicationDbContext context, IAccountManager accountManager,
            ILogger<DatabaseInitializer> logger)
        {
            _accountManager = accountManager;
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);

            if (!await _context.Users.AnyAsync())
            {
                _logger.LogInformation("Generating inbuilt accounts");

                const string adminRoleName = "administrator";
                const string userRoleName = "user";

                await EnsureRoleAsync(adminRoleName, "Default administrator",
                    ApplicationPermissions.GetAllPermissionValues());
                await EnsureRoleAsync(userRoleName, "Default user", new string[] { });

                await CreateUserAsync("admin", "tempP@ss123", "Administrator", "admin@mail.com", "+1 (123) 000-0000",
                    new string[] {adminRoleName});
                await CreateUserAsync("user", "tempP@ss123", "Standard User", "user@mail.com", "+1 (123) 000-0001",
                    new string[] {userRoleName});

                _logger.LogInformation("Inbuilt account generation completed");
            }


            if (!await _context.Customers.AnyAsync())
            {
                _logger.LogInformation("Seeding initial data");

                Customer cust_1 = new Customer
                {
                    Name = "Ebenezer Monney",
                    Email = "contact@ebenmonney.com",
                    Gender = Gender.Male,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                Customer cust_2 = new Customer
                {
                    Name = "Itachi Uchiha",
                    Email = "uchiha@narutoverse.com",
                    PhoneNumber = "+81123456789",
                    Address = "Some fictional Address, Street 123, Konoha",
                    City = "Konoha",
                    Gender = Gender.Male,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                Customer cust_3 = new Customer
                {
                    Name = "John Doe",
                    Email = "johndoe@anonymous.com",
                    PhoneNumber = "+18585858",
                    Address = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio.
                    Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet",
                    City = "Lorem Ipsum",
                    Gender = Gender.Male,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                Customer cust_4 = new Customer
                {
                    Name = "Jane Doe",
                    Email = "Janedoe@anonymous.com",
                    PhoneNumber = "+18585858",
                    Address = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio.
                    Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet",
                    City = "Lorem Ipsum",
                    Gender = Gender.Male,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                Brand brand_1 = new Brand("Adidas", "Немецкое качество", "http://4erwge7if1e2ap8yq43nh9h1.wpengine.netdna-cdn.com/wp-content/uploads/2018/05/logo-adidas-300x300.png");
                Brand brand_2 = new Brand("Microsoft", "Американский Государственный долг", "https://az801952.vo.msecnd.net/migration/partner-16.jpg");
                Brand brand_3 = new Brand("Redmond", "Лучшие мультиварки на планете", "https://shopogolikam.ru/images/redmond.jpg");
                Brand brand_4 = new Brand("Xiaomi", "Мы слоамли Китайскую стену", "https://fitnessbit.ru/wp-content/uploads/2017/11/xiaomi-mi-fit.png");
                Brand brand_5 = new Brand("Hitachi", "Раньше мы делали что-то, а теперь нет", "https://pts-trading.de/media/image/48/93/16/hitachi.png");
                Brand brand_6 = new Brand("Apple", "Самая крупная американская компания", "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fa/Apple_logo_black.svg/300px-Apple_logo_black.svg.png");
                Brand brand_7 = new Brand("Canon", "Фотоаппараты? Это к ним", "http://logotypes101.com/logos/556/FE9C737C8A5B988A52B4F3ACE1967CCA/Canon195.png");

                Category category_1 = new Category("Портативная техника", null);
                Category category_2 = new Category("Смартфоны", 1);
                Category category_3 = new Category("Плееры", 1);
                Category category_4 = new Category("Наушники", 1);
                Category category_5 = new Category("Фото-видео техника", null);
                Category category_6 = new Category("Фотоаппараты", 5);
                Category category_7 = new Category("Видеокамеры", 5);
                Category category_8 = new Category("Экшн-камеры", 5);

                Product product_1 = new Product("iPhone X", 2, 6, 20000,
                    "Самый инновационный смартфон на данный момент", "https://mcdn01.gittigidiyor.net/39006/tn30/390063604_tn30_0.jpg");
                Product product_2 = new Product("Canon 50D", 6, 7, 25000,
                    "Надёжный фотоаппарат на века", "http://www.lasuitenumerique.com/wp-content/uploads/2015/05/550d-eos-canon-1-300x300.jpg");

                _context.Customers.Add(cust_1);
                _context.Customers.Add(cust_2);
                _context.Customers.Add(cust_3);
                _context.Customers.Add(cust_4);

                _context.Brands.Add(brand_1);
                _context.Brands.Add(brand_2);
                _context.Brands.Add(brand_3);
                _context.Brands.Add(brand_4);
                _context.Brands.Add(brand_5);
                _context.Brands.Add(brand_6);
                _context.Brands.Add(brand_7);

                _context.Categories.Add(category_1);
                _context.Categories.Add(category_2);
                _context.Categories.Add(category_3);
                _context.Categories.Add(category_4);
                _context.Categories.Add(category_5);
                _context.Categories.Add(category_6);
                _context.Categories.Add(category_7);
                _context.Categories.Add(category_8);

                _context.Products.Add(product_1);
                _context.Products.Add(product_2);

                await _context.SaveChangesAsync();

                _logger.LogInformation("Seeding initial data completed");
            }
        }


        private async Task EnsureRoleAsync(string roleName, string description, string[] claims)
        {
            if ((await _accountManager.GetRoleByNameAsync(roleName)) == null)
            {
                ApplicationRole applicationRole = new ApplicationRole(roleName, description);

                var result = await this._accountManager.CreateRoleAsync(applicationRole, claims);

                if (!result.Item1)
                    throw new Exception(
                        $"Seeding \"{description}\" role failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");
            }
        }

        private async Task<ApplicationUser> CreateUserAsync(string userName, string password, string fullName,
            string email, string phoneNumber, string[] roles)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = userName,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsEnabled = true
            };

            var result = await _accountManager.CreateUserAsync(applicationUser, roles, password);

            if (!result.Item1)
                throw new Exception(
                    $"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");


            return applicationUser;
        }
    }
}