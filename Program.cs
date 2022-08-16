global using FirstProject.Data;
global using Microsoft.EntityFrameworkCore;
using FirstProject.Data.Services;
using FirstProject.Hubs;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IActorsService, ActorsService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<IDirectorsService, DirectorsService>();
builder.Services.AddSignalR();

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
app.MapHub<NotificationHub>("/NotificationHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Actors}/{action=Index}/{id?}");

app.Run();
