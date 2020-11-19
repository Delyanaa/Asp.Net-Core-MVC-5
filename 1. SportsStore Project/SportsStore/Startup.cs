using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using Microsoft.AspNetCore.Http;

namespace SportsStore
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //used to set up shared objects that can be used throughout the
        //application through the dependency injection feature
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                    Configuration["Data:SportStoreProducts:ConnectionString"]
                    )
            );

            services.AddControllersWithViews();

            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();

            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                // => product/list/Soccer/Page2
                endpoints.MapControllerRoute(
                        name: "product/list/category/page",
                        pattern: "{category}/Page{productPage}",
                        defaults: new { controller = "Product", action = "List" }
                );

                // => product/list/Page2
                endpoints.MapControllerRoute(
                        name: "product/list/page",
                        pattern: "Page{productPage:int}",
                        defaults: new { controller = "Product", action = "List", productPage = 1 }
                );

                // => product/list/Soccer
                endpoints.MapControllerRoute(
                        name: "product/list/category",
                        pattern: "{category}",
                        defaults: new { controller = "Product", action = "List", productPage = 1 }
                );


                // => product/list/
                endpoints.MapControllerRoute(
                       name: "product",
                       pattern: "",
                       defaults: "{controller=product}/{action=list}/productPage{productPage}"
                   );

                // => 
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=product}/{action=list}/{id?}");
            });

            SeedData.EnsurePopulated(app); //populates the database
        }
    }
}