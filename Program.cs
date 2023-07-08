using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestKSK;
using TestKSK.Auth;
using TestKSK.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices()
    .AddControllersWithViews();
builder.Services.AddAuthentication("Basic")
        .AddScheme<CustomAuthenticationOptions, CustomAuthenticationHandler>("Basic", null);
await builder.Services.AddCustomizedDataStore(builder.Configuration);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=VendingMachine}/{action=Index}/{id?}");

app.Run();
