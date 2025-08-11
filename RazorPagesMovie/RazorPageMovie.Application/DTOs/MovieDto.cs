using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Application.DTOs;

public class MovieDTO
{
    public int Id { get; set; }

    [Display(Name = "Título")]
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(100, ErrorMessage = "O título não pode exceder 100 caracteres.")]
    public string Title { get; set; } = string.Empty;
     
    [DataType(DataType.Date)]
    [Display(Name = "Data de Lançamento")]
    [Required(ErrorMessage = "A data de lançamento é obrigatória.")]
    public DateTime ReleaseDate { get; set; }

    [Display(Name = "Gênero")]
    [Required(ErrorMessage = "O gênero é obrigatório.")]
    public string Genre { get; set; } = string.Empty;

    [Display(Name = "Preço")]
    [Range(0.01, 1000000.00, ErrorMessage = "O preço deve estar entre 0.01 e 1.000.000,00")]
    [Column(TypeName ="decimal(18,2)")]
    [Required(ErrorMessage = "O preço é obrigatório.")]
    public decimal Price { get; set; }
}

/*✅ S — Single Responsibility Principle (SRP)
MovieDTO tem responsabilidade única: transportar dados entre camadas (Controller ↔ Service).
Sem lógica de negócio, sem dependência de infraestrutura. Enxuto, específico e direto ao ponto. 

✅ O — Open/Closed Principle (OCP)
É facilmente extensível com novos campos (ex: Director, Rating) sem alterar sua estrutura principal.
As validações por Data Annotations também são isoladas, e não interferem nas regras de negócio.
Aberto para extensão, fechado para modificação. 

✅ L — Liskov Substitution Principle (LSP)
É um DTO puro (POCO), e pode ser substituído ou herdado (ex: MovieCreateDTO, MovieUpdateDTO) sem quebrar nada.
Compatível com LSP. 

✅ I — Interface Segregation Principle (ISP)
Não se aplica diretamente, pois DTO não implementa interface. Mas:
⚠️ Boa prática futura: separar MovieDTO, MovieCreateDTO, MovieUpdateDTO se quiser evitar campos desnecessários em cada cenário.
Ainda assim, está coeso.

✅ D — Dependency Inversion Principle (DIP)
Não depende de nenhuma implementação concreta, nem de serviços ou repositórios.
Usado por serviços e controllers como contrato de entrada e saída. Cumpre DIP perfeitamente. */