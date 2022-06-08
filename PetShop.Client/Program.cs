using PetShop.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using PetShop.Data.Repository;
using PetShop.Data.Model;
using PetShop.Service;
using PetShop.Service.Interfaces;
using PetShop.Service.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PetShopDataContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("PetShopConnection")));
builder.Services.AddDbContext<AuthenticationDataContext>(option =>
    option.UseSqlite("Data Source=c:\\temp\\user.db"));
builder.Services.AddDefaultIdentity<IdentityUser>()
        .AddEntityFrameworkStores<AuthenticationDataContext>();

builder.Services.AddScoped<IRepository<Animal>, AnimalRepository>();
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IRepository<Comment>, CommentRepository>();
builder.Services.AddScoped<IAnimalService<Animal>, AnimalService>();
builder.Services.AddScoped<ICategoryService<Category>, CategoryService>();
builder.Services.AddScoped<ICommentService<Comment>, CommentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
