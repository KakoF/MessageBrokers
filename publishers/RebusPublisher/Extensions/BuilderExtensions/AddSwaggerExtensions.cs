namespace RebusPublisher.Extensions.BuilderExtensions
{
    public static class AddSwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
