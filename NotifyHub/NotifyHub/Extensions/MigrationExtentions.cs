using Microsoft.EntityFrameworkCore;
using NotifyHub.Infrastructure.Persistence;

namespace NotifyHub.Api.Extensions;

public static class MigrationExtentions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ApplicationDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
}
