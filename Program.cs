using XGraphql.Schema.Mutations;
using XGraphql.Schema.Queries;
using XGraphql.Schema.Subscriptions;
using XGraphql.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();

builder.Services.AddPooledDbContextFactory<SchoolDbContext>(o => o.UseSqlite(builder.Configuration["ConnectionStrings:Sqlite"]));


var app = builder.Build();

// Автоматическое применение миграций.
using(var scope = app.Services.CreateScope())
{
    var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SchoolDbContext>>();

    using(var context = contextFactory.CreateDbContext())
    {
        context.Database.Migrate();
    }
}

app.UseRouting();

// Нужно для subscriptions.
app.UseWebSockets();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.MapGet("/", () => "Hello World!");

app.Run();

// Школа - курсы, студенты и преподаватели.