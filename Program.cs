using EfCoreRelationshipDemo.Data;
using EfCoreRelationshipDemo.Models.OneToOne;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, configuration) =>
    {
        configuration.AddUserSecrets<Program>(optional: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var retries = 10;
    for (var i = 0; i < retries; i++)
    {
        try
        {
            await db.Database.MigrateAsync();
            logger.LogInformation("Database migration applied successfully.");
            break;
        }
        catch (Exception ex) when (i < retries - 1)
        {
            logger.LogWarning("Database not ready (attempt {Attempt}/{Retries}): {Message}. Retrying in 5s...",
                i + 1, retries, ex.Message);
            await Task.Delay(5000);
        }
    }

    var person = new Person("Mehrdad", null);

    var passport = new Passport("0014701871", person);

    person.SetPassport(passport);

    db.Persons.Add(person);
    await db.SaveChangesAsync();
}
