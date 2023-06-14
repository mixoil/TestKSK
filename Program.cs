using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestKSK;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectString = builder.Configuration.GetConnectionString("DefaultConnection");
SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder(connectString);
sqlBuilder.AttachDBFilename = @"D:\Misha\Coding\OtherProjects\TestKSK\App_Data\data.mdf";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(sqlBuilder.ConnectionString));

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
