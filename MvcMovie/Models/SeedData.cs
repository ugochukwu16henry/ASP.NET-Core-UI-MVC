using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>());

        var favoriteMovies = new List<Movie>
        {
            new Movie
            {
                Title = "Iron Man 3",
                ReleaseDate = DateTime.Parse("2013-05-03"),
                Genre = "Action",
                Rating = "PG-13",
                Price = 12.99M
            },
            new Movie
            {
                Title = "Avengers: Endgame",
                ReleaseDate = DateTime.Parse("2019-04-26"),
                Genre = "Action",
                Rating = "PG-13",
                Price = 14.99M
            },
            new Movie
            {
                Title = "Spider-Man: No Way Home",
                ReleaseDate = DateTime.Parse("2021-12-17"),
                Genre = "Action",
                Rating = "PG-13",
                Price = 13.99M
            },
            new Movie
            {
                Title = "When Harry Met Sally",
                ReleaseDate = DateTime.Parse("1989-2-12"),
                Genre = "Romantic Comedy",
                Rating = "R",
                Price = 7.99M
            },
            new Movie
            {
                Title = "Ghostbusters",
                ReleaseDate = DateTime.Parse("1984-3-13"),
                Genre = "Comedy",
                Rating = "PG",
                Price = 8.99M
            },
            new Movie
            {
                Title = "Ghostbusters 2",
                ReleaseDate = DateTime.Parse("1989-06-16"),
                Genre = "Comedy",
                Rating = "PG",
                Price = 9.99M
            }
        };

        var existingTitles = context.Movie
            .Select(m => m.Title)
            .Where(t => t != null)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var moviesToAdd = favoriteMovies
            .Where(movie => movie.Title != null && !existingTitles.Contains(movie.Title))
            .ToList();

        if (moviesToAdd.Count > 0)
        {
            context.Movie.AddRange(moviesToAdd);
            context.SaveChanges();
        }
    }
}
