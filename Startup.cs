// Startup.cs


public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {    
        services.AddControllers();  
        
    }

    

    

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {


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
