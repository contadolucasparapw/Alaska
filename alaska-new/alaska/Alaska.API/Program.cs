using Alaska.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection String não encontrada."
    );

builder.Services.AddDbContext<AlaskaContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AlaskaContext>();

    // Garante que o banco e as tabelas estejam criados no SQL Server
    context.Database.Migrate();

    if (!context.Filmes.Any())
    {
        context.Filmes.AddRange(
            new Alaska.API.Models.Filme
            {
                Titulo = "Homem Aranha: Um Novo Dia",
                Duracao = 157,
                Classificacao = "12",
                DataLancamento = new DateTime(2026, 7, 29),
                PosterUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRwZ_7TwbKVrMG7hiGpo8WbeHo5qbApIXtjz-7M7LuCnIElp9y8V6kcQHE&s=10",
                Status = 0
            },
            new Alaska.API.Models.Filme
            {
                Titulo = "Resident Evil",
                Duracao = 97,
                Classificacao = "18",
                DataLancamento = new DateTime(2026, 9, 17),
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BNDk4ODY1MDEtMTA2Ny00NzQ2LTg2OWMtMDhhZDJjMDE2ZWJmXkEyXkFqcGc@._V1_.jpg",
                Status = 1
            },
            new Alaska.API.Models.Filme
            {
                Titulo = "A Odisseia",
                Duracao = 172,
                Classificacao = "14",
                DataLancamento = new DateTime(2026, 7, 17),
                PosterUrl = "https://m.media-amazon.com/images/S/pv-target-images/e8ba07ea3c18c0b9109484728d93610dcfa6f28d60f140e5464010637c6b72bf.jpg",
                Status = 2
            },
            new Alaska.API.Models.Filme
            {
                Titulo = "O Diabo Veste Prada 2",
                Duracao = 119,
                Classificacao = "12",
                DataLancamento = new DateTime(2026, 4, 30),
                PosterUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTOv_kQqzMjLcX2prt1IIbYxEG5pNdo4MKkIlDL10KItw&s",
                Status = 3
            },
            new Alaska.API.Models.Filme
            {
                Titulo = "Duna: Parte Dois",
                Duracao = 166,
                Classificacao = "14",
                DataLancamento = new DateTime(2026, 11, 1),
                PosterUrl = "https://image.tmdb.org/t/p/w500/8b8R8l88Qje9dn9OE8PY05Nxl1X.jpg",
                Status = 4
            },
            new Alaska.API.Models.Filme
            {
                Titulo = "Jogos Vorazes: Amanhecer da Colheita",
                Duracao = 158,
                Classificacao = "16",
                DataLancamento = new DateTime(2026, 11, 18),
                PosterUrl = "https://shoppingcidadedasflores.com.br/wp-content/uploads/2026/11/JOGOS-VORAZES-AMANHECER-NA-COLHEITA.jpg",
                Status = 5
            },
            new Alaska.API.Models.Filme
            {
                Titulo = "Vingadores: Doutor Destino",
                Duracao = 165,
                Classificacao = "12",
                DataLancamento = new DateTime(2026, 12, 17),
                PosterUrl = "https://ingresso-a.akamaihd.net/b2b/production/uploads/articles-content/0fc79e05-8ded-4bc0-9206-9b5274a7c24f.jpg",
                Status = 6
            },
            new Alaska.API.Models.Filme
            {
                Titulo = "Duna: Parte 3",
                Duracao = 151,
                Classificacao = "14",
                DataLancamento = new DateTime(2026, 12, 17),
                PosterUrl = "https://ingresso-a.akamaihd.net/prd/img/movie/duna-parte-3/af380a98-344c-4238-a39c-af1e110dcd10.webp",
                Status = 7
            },
            new Alaska.API.Models.Filme
            {
                Titulo = "Jumanji 3",
                Duracao = 126,
                Classificacao = "Livre",
                DataLancamento = new DateTime(2026, 12, 3),
                PosterUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRKO2DO9mUZnDvh9vu-bxqAAcaEPtC0afKBjWOIFacD7HMuVfmgklz2Al0&s=10",
                Status = 8
            },
            new Alaska.API.Models.Filme
            {
                Titulo = "O Nascimento de Cristo: Zero D.C.",
                Duracao = 115,
                Classificacao = "Livre",
                DataLancamento = new DateTime(2026, 12, 3),
                PosterUrl = "https://all.web.img.acsta.net/img/92/87/92879b6cd5d4ab3da295bcd732e6d296.jpeg",
                Status = 9
            }
        );

        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();
app.UseCors("Frontend");

app.UseAuthorization();

app.MapControllers();

app.Run();