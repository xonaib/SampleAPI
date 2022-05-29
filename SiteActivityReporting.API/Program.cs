using SiteActivityReporting.API.BackgroundService;
using SiteActivityReporting.API.DAL;
using SiteActivityReporting.API.Services;
using SiteActivityReporting.Model.DTO;
using SiteActivityReporting.Model.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ActivityCleaner cleaner = new ActivityCleaner();
//IStore<Activity> store = new InMemoryActivity(cleaner);

//builder.Services.Add(new ServiceDescriptor(typeof(IRepository<ActivityDTO>), new ActivityRepository(store)));
//builder.Services.Add(new ServiceDescriptor(typeof(IStore<ActivityDTO>), store));
//builder.Services.Add(new ServiceDescriptor(typeof(ActivityCleaner), cleaner));

builder.Services.AddSingleton<IRepository<ActivityDTO>, ActivityRepository>();
builder.Services.AddSingleton<IStore<Activity>, ActivityStore>();
//builder.Services.AddSingleton<IObservable<Activity>, ActivityCleaner>();
//builder.Services.AddSingleton<IActivityEventBus, ActivityCleaner>();
builder.Services.AddSingleton<ActivitySceduler, ActivitySceduler>();
//builder.Services.AddScoped()

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
