// Startup.cs
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

                
        services.AddControllers();
    }

    

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

       app.UseCors("AllowAnyOrigin");

       

        // Configure routing
        app.UseEndpoints(endpoints =>
        {
            //Sets the endpoint to Create on a post request
            endpoints.MapControllerRoute(
                name: "CustomerOrderCreate",
                pattern: "/CustomerOrder/Create",
                defaults: new { controller = "CustomerOrder", action = "Create" }
            );

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        
    }
}
