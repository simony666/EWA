global using Demo;
global using Demo.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSqlServer<DB>($@"
    Data Source=(LocalDB)\MSSQLLocalDB;
    AttachDbFilename={builder.Environment.ContentRootPath}\DB.mdf;
");
builder.Services.AddAuthentication().AddCookie();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Helper>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRequestLocalization(options => options.SetDefaultCulture("en-MY"));
app.MapDefaultControllerRoute();
app.Run();
