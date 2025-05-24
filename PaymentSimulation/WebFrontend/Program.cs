using Serilog;
using Serilog.Sinks.PostgreSQL;
using WebFrontend.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.ConfigureServices();

var columnWriters = new Dictionary<string, ColumnWriterBase>
{
    {"message", new RenderedMessageColumnWriter()},
    {"message_template", new MessageTemplateColumnWriter()},
    {"level", new LevelColumnWriter()},
    {"timestamp", new TimestampColumnWriter()},
    {"exception", new ExceptionColumnWriter()},
    {"log_event", new LogEventSerializedColumnWriter()}
};

Log.Logger = new LoggerConfiguration()
    .WriteTo.PostgreSQL(
        connectionString: Environment.GetEnvironmentVariable("DATABASE_URL"),
        tableName: "log_frontend",
        needAutoCreateTable: true,
        columnOptions: columnWriters
    )
    .CreateLogger();


builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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