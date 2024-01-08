using Microsoft.EntityFrameworkCore;
using InternetShop.Repositories.Interfaces;
using InternetShop.Repositories;
using InternetShop.Services.Interfaces;
using InternetShop.Services;
using InternetShop.Models;

var builder = WebApplication.CreateBuilder(args);

// adding services
builder.Services.AddMvc();
builder.Services.AddSingleton<IUserService, UserService>(); //–еЇструЇ серв≥си €к одиночн≥ екземпл€ри (singleton)
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IProductService, ProductService>();

builder.Services.AddSingleton<AbstractAppDbContext, AppDbContext>();
builder.Services.AddSingleton<UserAuthentication, UserAuthentication>();
// adding repositories
builder.Services.AddSingleton<IOrdersRepository, OrdersRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IOrderItemRepository, OrderItemRepository>();


var app = builder.Build();

// mapping controllers with routes
app.MapControllers();

// ¬ключенн€ п≥дтримки статичних файл≥в
app.UseStaticFiles();
app.Run();
