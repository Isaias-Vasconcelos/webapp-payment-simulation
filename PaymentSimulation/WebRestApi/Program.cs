using Serilog;
using Serilog.Sinks.PostgreSQL;
using WebRestApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

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
        tableName: "log_api_web",
        needAutoCreateTable: true,
        columnOptions: columnWriters
    )
    .CreateLogger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureServices();
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();