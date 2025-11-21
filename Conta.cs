using static System.Console;

public class Conta
{
    public int Numero { get; set; }
    public string Cliente { get; set; }
    public string Cpf { get; set; }
    public string Senha { get; set; }
    public decimal Saldo { get; set; }
    public decimal Limite { get; set; }

    public decimal SaldoDisponível => Saldo + Limite;

    public Conta(int numero, string cliente, string cpf, string senha, decimal limite = 0)
    {
        Numero = numero;
        Cliente = cliente;
        Cpf = cpf;
        Senha = senha;
        Saldo = 0;
        Limite = limite;
    }

    public void Depositar(decimal valor)
    {
        Saldo += valor;
    }

    public void sacar(decimal valor)
    {
        if(valor <= 0)
        {
            WriteLine("\nvalor de saque inválido.");
            return;
        }
        if(valor > Saldo + Limite)
        {
            WriteLine("\nSaldo insuficiente para saque.");
            return;
        }

        Saldo -= valor;
    }

    public void aumentarLimite(decimal valor)
    {
        Limite += valor;
    }

    public void diminuirLimite(decimal valor)
    {
        Limite -= valor;
    }
}
