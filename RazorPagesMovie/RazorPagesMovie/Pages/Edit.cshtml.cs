using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Application.DTOs;
using RazorPagesMovie.Application.Interfaces;
using RazorPagesMovie.Domain.Exceptions;

namespace RazorPagesMovie.Web.Pages;

public class EditModel(IMovieService movieService) : PageModel
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            bool updateSuccessful = await movieService.UpdateMovieAsync(Movie);

            if (!updateSuccessful)
            {
                TempData["ErrorMessage"] = "Filme não encontrado. Pode ter sido removido por outro usuário.";
                return RedirectToPage("/Movies/Erro");
            }

            return RedirectToPage("./Index");
        }
        catch (ConcurrencyConflictException ex)
        {
            TempData["ErrorMessage"] = $"Conflito de concorrência: {ex.Message}";
            return RedirectToPage("/Movies/Erro");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Erro inesperado ao atualizar o filme.";
            return RedirectToPage("/Movies/Erro");
        }
    }
}