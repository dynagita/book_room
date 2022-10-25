namespace BookRoom.Readness.WebApi.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication AddSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Room");
            });
            return app;
        }
    }
}
