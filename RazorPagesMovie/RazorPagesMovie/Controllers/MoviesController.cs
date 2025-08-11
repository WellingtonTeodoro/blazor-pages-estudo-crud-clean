using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RazorPagesMovie.Application.DTOs;
using RazorPagesMovie.Application.Interfaces;

namespace RazorPagesMovie.Web.Controllers;

[ApiController]
[Asp.Versioning.ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var movies = await _movieService.GetAllMoviesAsync();
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var movie = await _movieService.GetMovieByIdAsync(id);
        if (movie == null)
            return NotFound();

        return Ok(movie);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] MovieDTO movie,
        [FromRoute] Asp.Versioning.ApiVersion apiVersion)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var createdMovie = await _movieService.CreateMovieAsync(movie);
        var version = apiVersion?.ToString() ?? "1.0";
        return CreatedAtAction(nameof(GetById), new { id = createdMovie.Id, version }, createdMovie);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] MovieDTO movie)
    {
        if (id != movie.Id)
            return BadRequest("ID do corpo difere do ID da URL");

        var success = await _movieService.UpdateMovieAsync(movie);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _movieService.MovieExistsAsync(id))
            return NotFound();

        var success = await _movieService.DeleteMovieAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}

/*✅ S — Single Responsibility Principle (SRP)
O controller faz apenas a orquestração das requisições HTTP relacionadas a filmes (Movies).
A lógica fica encapsulada no IMovieService. Está bem separado, cumpre SRP.

✅ O — Open/Closed Principle (OCP)
Pode ser estendido com novos endpoints ou novas versões sem alterar os métodos existentes.
Versionamento via ApiVersion permite expansão sem quebra. Boa aderência ao OCP.

✅ L — Liskov Substitution Principle (LSP)
Não quebra contratos de ControllerBase. IMovieService pode ser substituído por qualquer implementação, inclusive mocks, mantendo comportamento esperado. Seguro no LSP.

✅ I — Interface Segregation Principle (ISP)
Depende só do IMovieService, que é focado no domínio de filmes. Não obriga a depender de métodos irrelevantes.
Respeita o ISP.

✅ D — Dependency Inversion Principle (DIP)
Depende da abstração (IMovieService), não da implementação concreta. Essa abstração é injetada via construtor, favorecendo testabilidade e modularidade.
Seguindo o DIP à risca.*/