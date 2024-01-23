namespace FilmesApi.Controllers;

using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dto;
using FilmesApi.Modelos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{

    public FilmeContext _context { get; set; }
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme1 = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme1);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmePorId), new {id = filme1.Id}, filme1);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateFilme(int id, [FromBody] UpdateFilmeDto updateDto)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
        if (filme == null) return NotFound();

        _mapper.Map(updateDto, filme);
        _context.SaveChanges();

        return NoContent();    
    }

    [HttpGet]
    public IEnumerable<Filme> RecuperarFilmes([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _context.Filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
        if (filme == null) return NotFound();
        return Ok(filme);
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(
            filme => filme.Id == id);
        if (filme == null) return NotFound();

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();

    }


}
