using  WebApplication7;
var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseErrorLoggingMiddleware();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Form}/{action=Index}/{id?}");



app.Run();  