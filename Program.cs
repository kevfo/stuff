var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
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
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{customer_name?}"
    // defaults: new {controller = "ChangeCustomerController", action = "GetCustomerInfo" }
);
app.MapControllerRoute(
    name: "post_orders",
    pattern: "{controller}/{action}/{customer_id?}/{item?}");
    // defaults: new { controller = "Blog", action = "Article" });
app.Run();