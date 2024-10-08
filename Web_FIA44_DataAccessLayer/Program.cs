namespace Web_FIA44_DataAccessLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews().AddMvcOptions
            (o => o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((input, field) => $"{input} ungültig für {field}"));
            var app = builder.Build();

            app.MapControllerRoute(name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseStaticFiles();

            app.UseRouting();
            app.Run();
        }
    }
}
