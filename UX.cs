using System.ComponentModel;
using static System.Console;

public class UX
{
    private readonly Banco _banco;
    private readonly string _titulo;

    public UX(string titulo, Banco banco)
    {
        _titulo = titulo;
        _banco = banco;
    }

    public void Executar()
    {
        CriarTitulo(_titulo);
        WriteLine(" [1] Criar Conta");
        WriteLine(" [2] Listar Contas");
        WriteLine(" [3] Efetuar Saque");
        WriteLine(" [4] Efetuar Depósito");
        WriteLine(" [5] Aumentar Limite");
        WriteLine(" [6] Diminuir Limte");
        ForegroundColor = ConsoleColor.Red;
        WriteLine("\n [9] Sair");
        ForegroundColor = ConsoleColor.White;
        CriarLinha();
        ForegroundColor = ConsoleColor.Yellow;
        Write(" Digite a opção desejada: ");
        var opcao = ReadLine() ?? "";
        ForegroundColor = ConsoleColor.White;
        switch (opcao)
        {
            case "1": CriarConta(); break;
            case "2": MenuListarContas(); break;
            case "3": EfetuarSaque(); break;
            case "4": Depositar(); break;
            case "5": AumentarLimite(); break;
            case "6": DiminuirLimite(); break;
        }
        if (opcao != "9")
        {
            Executar();
        }
        _banco.SaveContas();
    }

    private void CriarConta()
    {
        CriarTitulo(_titulo + " - Criar Conta");
        Write(" Numero:  ");
        var numero = Convert.ToInt32(ReadLine());
        Write(" Cliente: ");
        var cliente = ReadLine() ?? "";
        Write(" CPF:     ");
        var cpf = ReadLine() ?? "";
        Write(" Senha:   ");
        var senha = ReadLine() ?? "";
        Write(" Limite:  ");
        var limite = Convert.ToDecimal(ReadLine());

        var conta = new Conta(numero, cliente, cpf, senha, limite);
        _banco.Contas.Add(conta);

        CriarRodape("Conta criada com sucesso!");
    }

    private void MenuListarContas()
    {
        CriarTitulo(_titulo + " - Listar Contas");
        foreach (var conta in _banco.Contas)
        {
            WriteLine($" Conta: {conta.Numero} - {conta.Cliente}");
            WriteLine($" Saldo: {conta.Saldo:C} | Limite: {conta.Limite:C}");
            WriteLine($" Saldo Disponível: {conta.SaldoDisponível:C}\n");
        }
        CriarRodape();
    }

    private void EfetuarSaque()
    {
        CriarTitulo(_titulo + " - Efetuar Saque");
        Write(" Digite o numero da conta para saque: ");
        if(!int.TryParse(ReadLine(), out int numeroConta))
        {
            CriarRodape("Numero invalido!");
            return;
        }

        var contaAlvo = _banco.Contas.FirstOrDefault(c => c.Numero == numeroConta);
        if(contaAlvo == null)
        {
            CriarRodape("Conta não encontrada.");
            return;
        }

        Write($"\n Conta Selecionada: {contaAlvo.Cliente}");
        Write($" Saldo Disponível: {contaAlvo.SaldoDisponível:c}");
        Write("\n Valor a sacar: ");
        var saque = Convert.ToDecimal(ReadLine());

        contaAlvo.sacar(saque);

        CriarRodape($"Saque realizado. Novo saldo: {contaAlvo.SaldoDisponível:C}");
    }

    private void Depositar()
    {
        CriarTitulo(_titulo + " - Efetuar Deposito");
        Write(" Digite o numero da conta para depositar: ");
        if(!int.TryParse(ReadLine(), out int numeroConta))
        {
            CriarRodape("Numero invalido!");
            return;
        }

        var contaAlvo = _banco.Contas.FirstOrDefault(c => c.Numero == numeroConta);
        if(contaAlvo == null)
        {
            CriarRodape("Conta não encontrada.");
            return;
        }

        Write($"\n Conta Selecionada: {contaAlvo.Cliente}");
        Write($" Saldo Disponível: {contaAlvo.SaldoDisponível:c}");
        Write("\n Valor a depositar: ");
        var deposito = Convert.ToDecimal(ReadLine());

        contaAlvo.Depositar(deposito);

        CriarRodape($"Deposito realizado. Novo saldo: {contaAlvo.SaldoDisponível:C}");
    }

    private void AumentarLimite()
    {
        CriarRodape(_titulo + " - Aumentar Limite");
        Write(" Digite o numero da conta para aumentar o limite: ");
        if(!int.TryParse(ReadLine(), out int numeroConta))
        {
            CriarRodape("Numero invalido");
            return;
        }

        var contaAlvo = _banco.Contas.FirstOrDefault(c => c.Numero == numeroConta);
        if(contaAlvo == null)
        {
            CriarRodape("Conta não encontrada");
            return;
        }

        Write($"\n Conta Selecionada: {contaAlvo.Cliente}");
        Write($" Saldo Disponível: {contaAlvo.Limite}");
        Write("\n Valor a aumentar: ");
        var limite = Convert.ToDecimal(ReadLine());

        contaAlvo.aumentarLimite(limite);

        CriarRodape($" Limite aumentado. Novo limite: {contaAlvo.Limite}");
    }

    private void DiminuirLimite()
    {
        CriarRodape(_titulo + " - Diminuir Limite");
        Write(" Digite o numero da conta para diminuir o limite: ");
        if(!int.TryParse(ReadLine(), out int numeroConta))
        {
            CriarRodape("Numero invalido");
            return;
        }

        var contaAlvo = _banco.Contas.FirstOrDefault(c => c.Numero == numeroConta);
        if(contaAlvo == null)
        {
            CriarRodape("Conta não encontrada");
            return;
        }

        Write($"\n Conta Selecionada: {contaAlvo.Cliente}");
        Write($" Saldo Disponível: {contaAlvo.Limite}");
        Write("\n Valor a diminuir: ");
        var limite = Convert.ToDecimal(ReadLine());

        contaAlvo.diminuirLimite(limite);

        CriarRodape($"Limite diminuido. Novo limite: {contaAlvo.Limite}");
    }

    private void CriarLinha()
    {
        WriteLine("-------------------------------------------------");
    }

    private void CriarTitulo(string titulo)
    {
        Clear();
        ForegroundColor = ConsoleColor.White;
        CriarLinha();
        ForegroundColor = ConsoleColor.Yellow;
        WriteLine(" " + titulo);
        ForegroundColor = ConsoleColor.White;
        CriarLinha();
    }

    private void CriarRodape(string? mensagem = null)
    {
        CriarLinha();
        ForegroundColor = ConsoleColor.Green;
        if (mensagem != null)
            WriteLine(" " + mensagem);
        Write(" ENTER para continuar");
        ForegroundColor = ConsoleColor.White;
        ReadLine();
    }

}
