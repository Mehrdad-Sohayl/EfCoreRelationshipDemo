

using EfCoreRelationshipDemo.Data;
using EfCoreRelationshipDemo.Models.OneToOne;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, configuration) =>
    {
        configuration.AddJsonFile("appsettings.json", optional: false,
            reloadOnChange: true);

    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var person = new Person("Mehrdad", null);

    var passport = new Passport("0014701871", person);

    person.SetPassport(passport);

    db.Persons.Add(person);
    await db.SaveChangesAsync();
}