namespace RazorPagesMovie.Domain.Entities;

public class Movie
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public string Genre { get; private set; }
    public decimal Price { get; private set; } 

    // Construtor para criar um novo filme
    public Movie(string title, DateTime releaseDate, string genre, decimal price)
    {
        // Validações de domínio (invariantes de criação)
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("O título não pode ser nulo ou vazio.", nameof(title));
        if (string.IsNullOrWhiteSpace(genre))
            throw new ArgumentException("O gênero não pode ser nulo ou vazio.", nameof(genre));
        if (price <= 0)
            throw new ArgumentOutOfRangeException(nameof(price), "O preço deve ser maior que zero.");
        if (releaseDate == default)
            throw new ArgumentException("A data de lançamento não pode ser o valor padrão.", nameof(releaseDate)); 

        Title = title;
        ReleaseDate = releaseDate;
        Genre = genre;
        Price = price; 
    }

    // Construtor privado para o Entity Framework Core
    private Movie()
    {
        Title = string.Empty;
        Genre = string.Empty; 
    }

    // --- Comportamentos (Lógica de Negócio) ---

    // Método para atualizar os detalhes de um filme
    public void UpdateDetails(string newTitle, DateTime newReleaseDate, string newGenre, decimal newPrice)
    {
        // Validações de domínio para a atualização
        if (string.IsNullOrWhiteSpace(newTitle))
            throw new ArgumentException("O novo título não pode ser nulo ou vazio.", nameof(newTitle));
        if (string.IsNullOrWhiteSpace(newGenre))
            throw new ArgumentException("O novo gênero não pode ser nulo ou vazio.", nameof(newGenre));
        if (newPrice <= 0)
            throw new ArgumentOutOfRangeException(nameof(newPrice), "O novo preço deve ser maior que zero."); 

        Title = newTitle;
        ReleaseDate = newReleaseDate;
        Genre = newGenre;
        Price = newPrice; 
    }

    // Método para aplicar um desconto ao preço do filme
    //public void ApplyDiscount(decimal percentage)
    //{
    //    if (percentage <= 0 || percentage > 100)
    //        throw new ArgumentOutOfRangeException(nameof(percentage), "A porcentagem de desconto deve estar entre 0 e 100.");

    //    Price = Price * (1 - (percentage / 100));
    //} 
}

/*✅ S — Single Responsibility Principle (SRP)
Responsabilidade única: representar um filme com suas regras de negócio básicas (validação, alteração de estado).
Nada de persistência, DTO, UI, nem lógica de aplicação. SRP totalmente respeitado.

✅ O — Open/Closed Principle (OCP)
Você pode estender os comportamentos (ex: métodos como ApplyDiscount, ChangeGenre, SetPromoPrice) sem alterar o que já está validado e funcionando. A lógica de validação está centralizada, pronta pra evoluir. Ponto pro OCP.

✅ L — Liskov Substitution Principle (LSP)
Como é uma classe concreta e não herda, o LSP não se aplica diretamente.
Porém, como Movie segue o contrato de entidade imutável (via encapsulamento com setters privados e atualização via métodos), ela não quebra comportamentos esperados. LSP: Ok.

✅ I — Interface Segregation Principle (ISP)
Não força ninguém a lidar com dependências desnecessárias. É uma entidade pura, focada em domínio. Nada a reclamar. ISP ok.

✅ D — Dependency Inversion Principle (DIP)
Não depende de infraestrutura, serviços externos, nem classes concretas — apenas tipos nativos do .NET (como DateTime, string, decimal). DIP 100% aderente.*/