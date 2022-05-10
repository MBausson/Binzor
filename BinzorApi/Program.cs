string corsOptionName = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o =>
{
    o.AddPolicy(corsOptionName, policy =>
    {
        policy.WithOrigins("*");
    });
});

var app = builder.Build();

Console.WriteLine(app.Environment);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(corsOptionName);
app.UseAuthorization();
app.MapControllers();
app.Run();
