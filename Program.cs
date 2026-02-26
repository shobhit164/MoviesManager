using Microsoft.EntityFrameworkCore;
using MoviesManager.Data;
using MoviesManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add MVC controllers with views
builder.Services.AddControllersWithViews();

// EF Core + SQLite
builder.Services.AddDbContext<MovieDbContext>(options =>
    options.UseSqlite("Data Source=movies.db")); // store movies in SQLite

// API & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed initial movies
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
    db.Database.EnsureCreated(); // create DB if missing

    if (!db.Movies.Any())
    {
        db.Movies.AddRange(
            new Movie { Title = "3 Idiots", Director = "Rajkumar Hirani", Genre = "Comedy/Drama", Year = 2009, Rating = 8.4 },
            new Movie { Title = "RRR", Director = "S. S. Rajamouli", Genre = "Action/Drama", Year = 2022, Rating = 7.8 },
            new Movie { Title = "Fight Club", Director = "David Fincher", Genre = "Drama/Thriller", Year = 1999, Rating = 8.8 },
            new Movie { Title = "Uri: The Surgical Strike", Director = "Aditya Dhar", Genre = "Action/War", Year = 2019, Rating = 8.0 },
            new Movie { Title = "Roundhay Garden Scene", Director = "Louis Le Prince", Genre = "Documentary / real-life", Year = 1888, Rating = 0 }
        );
        db.SaveChanges();
    }
}

// Configure HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // force HTTPS
app.UseStaticFiles();      // serve CSS, JS, images
app.UseRouting();          // enable routing
app.UseAuthorization();    // enforce authorization

// MVC routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

// Map API controllers
app.MapControllers();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.Run();