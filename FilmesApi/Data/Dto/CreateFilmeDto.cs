using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dto;

public class CreateFilmeDto
{
    [Required(ErrorMessage = "O campo título é obrigatório")]
    [StringLength(50, ErrorMessage = "O campo título não pode exceder 50 caracteres")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O campo gênero é obrigatório")]

    public string Genero { get; set; }

    [Required(ErrorMessage = "O campo duração é obrigatório")]
    [Range(70, 240, ErrorMessage = "O filme deve ter entre 1h10 e 4h de duração")]
    public int Duracao { get; set; }
}
