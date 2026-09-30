namespace SistemaEstacionamento;

public class SetorVaga
{
    public string Nome { get; set; }
    public int Limite { get; set; }
    public int Ocupadas { get; set; }

    public int Disponivel => Limite - Ocupadas;
    public bool EstaLotado => Ocupadas >= Limite;

    public SetorVaga(string nome, int limite)
    {
        Nome = nome;
        Limite = limite;
        Ocupadas = 0;
    }
}
