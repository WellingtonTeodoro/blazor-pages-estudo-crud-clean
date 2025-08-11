namespace RazorPagesMovie.Application.Interfaces;

public interface ISeedService
{
    Task SeedMoviesAsync();
    // Outros métodos de seed podem ficar aqui, se tiver outras entidades
    // Task SeedUsersAsync();
}


/*✅ S — Single Responsibility Principle (SRP)
A interface tem uma responsabilidade clara: definir contratos para operações de seed (popular o banco com dados iniciais).
Ela não mistura com lógica de domínio, persistência ou controle de fluxo.
Inclusive, há espaço para expandir com novos métodos (SeedUsersAsync(), etc.), mantendo a coesão. Respeita o SRP.

✅ O — Open/Closed Principle (OCP)
A interface está aberta para extensão (novos métodos de seed podem ser adicionados), sem necessidade de alterar a lógica já existente.
Não tem implementação concreta acoplada aqui. Extensível sem quebra.

✅ L — Liskov Substitution Principle (LSP)
Qualquer classe que implemente ISeedService pode ser usada no lugar da interface — sem quebrar o comportamento esperado.
Nada fora do contrato. LSP mantido.

✅ I — Interface Segregation Principle (ISP)
A interface é enxuta, define apenas o necessário. Não obriga ninguém a implementar métodos que não use (e ainda deixa comentado que pode crescer com sentido). Segregada e limpa.

✅ D — Dependency Inversion Principle (DIP)
Sendo uma interface, ela já é uma abstração por natureza. A camada que a consome (AppDatabaseSeedExtensions, por exemplo) não depende da implementação concreta (MovieSeedService), apenas da abstração. Alinhado com o DIP.*/