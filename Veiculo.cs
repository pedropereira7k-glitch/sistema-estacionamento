using System;

namespace SistemaEstacionamento;

public class Veiculo
{
    public string Placa { get; set; }
    public string TipoVaga { get; set; }
    public DateTime HorarioEntrada { get; set; }

    public Veiculo(string placa, string tipoVaga, DateTime horarioEntrada)
    {
        Placa = placa;
        TipoVaga = tipoVaga;
        HorarioEntrada = horarioEntrada;
    }
}
