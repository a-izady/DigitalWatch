using static System.Net.Mime.MediaTypeNames;

namespace DigitalWatch
{
    public class Program
    {
        private static Timer _timer = null;
        public static string DateTimeTest { get; private set; }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Create a Timer object that knows to call our TimerCallback
            // method once every 2000 milliseconds.
            _timer = new Timer(TimerCallback, null, 0, 2000);
            builder.Services.Configure<MyOptions>(x => x.Test = "test2");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
        private static void TimerCallback(Object o)
        {
            // Display the date/time when this method got called.
            Console.WriteLine("In TimerCallback: " + DateTime.Now);
            DateTimeTest = DateTime.Now.ToString();
        }
    }

    public class MyOptions
    {
        public static int i = 0;
        public string Test { get; set; }
        public string MyAdd()
        {
            i++;
            return i.ToString();
        }
    }
  



}
