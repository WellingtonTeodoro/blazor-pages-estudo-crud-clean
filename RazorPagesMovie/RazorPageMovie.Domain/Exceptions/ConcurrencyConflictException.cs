namespace RazorPagesMovie.Domain.Exceptions;

public class ConcurrencyConflictException : Exception
{
    public ConcurrencyConflictException() { }
    public ConcurrencyConflictException(string message) : base(message) { }
    public ConcurrencyConflictException(string message, Exception innerException) : base(message, innerException) { }
}

/*✅ S — Single Responsibility Principle (SRP)
Tem uma única responsabilidade: representar uma exceção específica de conflito de concorrência.
100% alinhada com SRP.

✅ O — Open/Closed Principle (OCP)
Aberta para extensão (poderia, por exemplo, incluir propriedades adicionais ou mensagens padrão), mas fechada para modificação do comportamento básico de Exception. Boa aderência ao OCP.

✅ L — Liskov Substitution Principle (LSP)
Pode ser usada no lugar de qualquer Exception sem quebrar a lógica esperada por quem a consome (catch genérico, logging, etc).
Totalmente compatível com LSP.

✅ I — Interface Segregation Principle (ISP)
Não implementa interfaces, mas não obriga ninguém a lidar com métodos ou membros desnecessários. Não infringe ISP.

✅ D — Dependency Inversion Principle (DIP)
Não depende de nenhuma concretude externa. É uma entidade de domínio totalmente isolada e injetável se necessário (em mapeamentos, etc). DIP respeitado.*/