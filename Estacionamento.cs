using System;
using System.Collections.Generic;

namespace SistemaEstacionamento;

public class Estacionamento
{
    public TimeSpan HoraDeAbertura { get; set; } = new TimeSpan(8, 0, 0);
    public TimeSpan HoraFechamento { get; set; } = new TimeSpan(22, 0, 0);
    public double ValorPrimeiraHora { get; set; } = 15.00;
    public double ValorFracao { get; set; } = 3.00;
    public double TetoDiaria { get; set; } = 50.00;
    public int TempoTolerancia { get; set; } = 15;

    public Dictionary<string, SetorVaga> Setores { get; set; } =
        new()
        {
            { "normal", new SetorVaga("Normal", 40) },
            { "idoso", new SetorVaga("Idoso", 5) },
            { "pcd", new SetorVaga("PcD", 5) },
        };

    public Dictionary<string, Veiculo> VeiculosEstacionados { get; set; } = new();

    public bool EstaAberto(TimeSpan horaAtual)
    {
        return horaAtual >= HoraDeAbertura && horaAtual <= HoraFechamento;
    }

    public double CalcularValor(double minutosTotais)
    {
        if (minutosTotais <= TempoTolerancia)
            return 0.00;

        if (minutosTotais <= 60)
            return ValorPrimeiraHora;

        double minutosAdicionais = minutosTotais - 60;
        double fracoes = Math.Ceiling(minutosAdicionais / 15);
        double valorCalculado = ValorPrimeiraHora + (fracoes * ValorFracao);

        return Math.Min(valorCalculado, TetoDiaria);
    }

    public bool ExecutarEntrada(string placa, string tipoVaga)
    {
        if (!EstaAberto(DateTime.Now.TimeOfDay))
            return false;

        placa = placa.Trim().ToUpper();

        if (VeiculosEstacionados.ContainsKey(placa))
            return false;

        if (!Setores.TryGetValue(tipoVaga, out SetorVaga? setor) || setor.EstaLotado)
            return false;

        setor.Ocupadas++;
        VeiculosEstacionados.Add(placa, new Veiculo(placa, tipoVaga, DateTime.Now));
        return true;
    }

    public double ExecutarSaida(string placa, out double minutosPermanencia)
    {
        minutosPermanencia = 0;

        placa = placa.Trim().ToUpper();

        if (!VeiculosEstacionados.TryGetValue(placa, out Veiculo? veiculo))
            return -1;

        minutosPermanencia = (DateTime.Now - veiculo.HorarioEntrada).TotalMinutes;
        double valorFinal = CalcularValor(minutosPermanencia);

        if (Setores.TryGetValue(veiculo.TipoVaga, out SetorVaga? setor))
        {
            setor.Ocupadas--;
        }

        VeiculosEstacionados.Remove(placa);
        return valorFinal;
    }
}
