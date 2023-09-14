using AutoMapper;
using EEBlog.Mvc.Filters;
using EEBlog.Services.AutoMapper.Profiles;
using System.Text.Json.Serialization;

namespace EEBlog.Mvc
{
    public class Program
    {
        public Program(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddControllersWithViews(options =>
            {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "This area must not be empty.");
                options.Filters.Add<MvcExceptionFilter>();
            }).AddRazorRuntimeCompilation().AddJsonOptions(OptionsBuilderConfigurationExtensions =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
        }
    }
}
























// var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    app.UseStatusCodePages();

//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}


//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

////app.MapControllerRoute(
////    name: "default",
////    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapAreaControllerRoute(
//        name: "Admin",
//        areaName: "Admin",
//        pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
//        );

//    endpoints.MapControllerRoute(
//        name: "post",
//        pattern: "{title}/{postId}",
//        defaults: new { controller = "Post", action = "Detail" }
//        );
//    endpoints.MapDefaultControllerRoute();
//});

//app.Run();
