using Operations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddOperationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapAreaControllerRoute(
    name: "CrawlerApiArea",
    areaName: "CrawlerApi",
    pattern: "crawlerapi/{controller=CrawlerApi}/{action=CPH}/{url}"
);

app.UseAuthorization();

app.MapRazorPages();

app.Run();
