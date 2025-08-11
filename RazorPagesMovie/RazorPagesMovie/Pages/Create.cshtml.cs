using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Application.DTOs;
using RazorPagesMovie.Application.Interfaces;

namespace RazorPagesMovie.Web.Pages; 

public class CreateModel(IMovieService movieService) : PageModel
{
    [BindProperty]
    public MovieDTO Movie { get; set; } = default!;

    public IActionResult OnGet()
    {
        Movie = new MovieDTO { ReleaseDate = DateTime.Now };
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await movieService.CreateMovieAsync(Movie);  
        return RedirectToPage("./Index");
    }
}