using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Application.DTOs;
using RazorPagesMovie.Application.Interfaces;

namespace RazorPagesMovie.Web.Pages;

public class DetailsModel(IMovieService movieService) : PageModel
{  
    public MovieDTO Movie { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Movie = await movieService.GetMovieByIdAsync(id.Value);

        if (Movie == null)
        {
            return NotFound();
        } 

        return Page();
    }
}
