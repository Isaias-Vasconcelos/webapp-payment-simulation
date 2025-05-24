using MassTransit;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using StatusPayment.Consumer;
using StatusPayment.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

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
        tableName: "log_service_status_payment",
        needAutoCreateTable: true,
        columnOptions: columnWriters
    )
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins(Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "http://localhost:5000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddMassTransit(c =>
{
    c.AddConsumer<PaymentProcessedConsumer>();

    c.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host("rabbitmq", "/",h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        configurator.ReceiveEndpoint("payment-processed-status-queue", c =>
        {
            c.ConfigureConsumer<PaymentProcessedConsumer>(context);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<HubMessages>("/notifications");

app.Run();