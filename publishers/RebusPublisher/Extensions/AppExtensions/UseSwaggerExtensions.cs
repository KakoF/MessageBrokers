namespace RebusPublisher.Extensions.AppExtensions
{
	public static class UseSwaggerExtensions
	{
		public static void ConfigureSwagger(this WebApplication app, IWebHostEnvironment environment)
		{
			if (environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
		}
	}
}
