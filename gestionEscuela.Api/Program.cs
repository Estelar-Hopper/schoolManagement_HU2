using gestionEscuela.Domain.Repositories;
using gestionEscuela.Infrastructure.Extensions;
using gestionEscuela.Infrastructure.Repositories;
using gestionEscuela.Application.Services;
using gestionEscuela.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);
//----------------------------------------------------
// Dependency injection of db context:
builder.Services.AddInfrastructure(builder.Configuration);

// Other injections:
builder.Services.AddControllers();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<StudentService>();

builder.Services.AddScoped<IGenericRepository<Course>, CourseRepository>();
builder.Services.AddScoped<CourseService>();

builder.Services.AddScoped<IGenericRepository<Enrollment>, EnrollmentRepository>();
builder.Services.AddScoped<EnrollmentService>();

//----------------------------------------------------

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Mapping controllers
app.MapControllers();

app.Run();
