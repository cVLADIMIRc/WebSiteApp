namespace WebSiteApp
{
	public class User
	{
		public string login { get; set; }
		public string password { get; set; }
	}
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var app = builder.Build();

			app.UseStaticFiles();
			app.Use(async (context, next) =>
			{
				if (context.Request.Path.Equals("/Auth"))
				{
					var TestUser = context.Request.ReadFromJsonAsync<User>();
					if (TestUser.Result.login.Equals("admin") && TestUser.Result.password.Equals("admin"))
					{
						context.Response.Redirect("/");
					}
					else
					{
						context.Response.Redirect("/html/index.html");
					}
				}
				next?.Invoke();
			});
			app.Map("/", () => "hello world!");
			app.Run();
		}
	}
}