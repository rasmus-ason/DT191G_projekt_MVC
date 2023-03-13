using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DT191G_projekt.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//Connection strings
builder.Services.AddDbContext<ProductContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("ProductDbString"))
);

builder.Services.AddDbContext<ProductBrandContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("ProductBrandDbString"))
);

builder.Services.AddDbContext<ProductCategoryContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("ProductCategoryDbString"))
);

builder.Services.AddDbContext<CustomerOrderContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("CustomerOrderDbString"))
);

builder.Services.AddDbContext<DetailedOrderContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DetailedOrderDbString"))
);

builder.Services.AddDbContext<RecipeContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("RecipeDbString"))
);

builder.Services.AddDbContext<AboutUsContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("AboutUsDbString"))
);


//https://www.youtube.com/watch?v=XTQo2s3LDW0
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build => {
    build.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
