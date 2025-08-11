using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Application.DTOs;
using RazorPagesMovie.Application.Interfaces;

namespace RazorPagesMovie.Web.Pages;

public class DeleteModel(IMovieService movieService) : PageModel
{    
    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        await movieService.DeleteMovieAsync(id.Value);

        return RedirectToPage("./Index");
    }
}
