using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Application.DTOs;
using RazorPagesMovie.Infrastructure.Data.DbContexts;

namespace RazorPagesMovie.Web.Pages;

public class IndexModel(RazorPagesMovieContext context) : PageModel
{
    private readonly RazorPagesMovieContext _context = context; 

    public IList<MovieDTO> Movie { get;set; } = default!;

    public async Task OnGetAsync()
    {
        var movieEntities = await _context.Movie.ToListAsync();
        Movie = movieEntities.Select(m => new MovieDTO
        {
            Id = m.Id,
            Title = m.Title,
            ReleaseDate = m.ReleaseDate,
            Genre = m.Genre,
            Price = m.Price 
        }).ToList();
    }
}
