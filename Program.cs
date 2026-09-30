using System;

namespace SistemaEstacionamento
{
    public class Program
    {
        static void Main(string[] args)
        {
            Estacionamento sistema = new Estacionamento();
            bool rodarMenu = true;

            while (rodarMenu)
            {
                Console.WriteLine("\n=== SISTEMA DE ESTACIONAMENTO ===");
                Console.WriteLine("1. Registrar Entrada");
                Console.WriteLine("2. Registrar Saída");
                Console.WriteLine("3. Exibir Painel de Vagas");
                Console.WriteLine("4. Sair");
                Console.Write("Opção (1-4): ");

                int opcao;
                while (!int.TryParse(Console.ReadLine(), out opcao) || opcao < 1 || opcao > 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Opção inválida. Digite uma opção entre 1 e 4: ");
                    Console.ResetColor();
                }

                switch (opcao)
                {
                    case 1:
                        MenuRegistrarEntrada(sistema);
                        break;
                    case 2:
                        MenuRegistrarSaida(sistema);
                        break;
                    case 3:
                        MenuExibirPainel(sistema);
                        break;
                    case 4:
                        Console.WriteLine("Encerrando programa...");
                        rodarMenu = false;
                        break;
                }
            }
        }

        static void MenuRegistrarEntrada(Estacionamento sistema)
        {
            Console.WriteLine("\n--- Check-in de Veículo ---");
            Console.Write("Digite a placa: ");
            string placa = (Console.ReadLine() ?? "").Trim().ToUpper();

            while (string.IsNullOrWhiteSpace(placa))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("A placa não pode ser vazia. Digite novamente: ");
                Console.ResetColor();
                placa = (Console.ReadLine() ?? "").Trim().ToUpper();
            }

            Console.Write("Tipo de vaga (1. Normal, 2. Idoso, 3. PcD): ");
            string opcaoVaga = Console.ReadLine() ?? "";

            while (opcaoVaga != "1" && opcaoVaga != "2" && opcaoVaga != "3")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Opção inválida. Selecione apenas 1, 2 ou 3: ");
                Console.ResetColor();
                opcaoVaga = (Console.ReadLine() ?? "").Trim();
            }

            string tipo = opcaoVaga switch
            {
                "2" => "idoso",
                "3" => "pcd",
                _ => "normal",
            };

            bool sucesso = sistema.ExecutarEntrada(placa, tipo);

            if (sucesso)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nENTRADA AUTORIZADA COM SUCESSO!");
                Console.WriteLine(
                    $"Veículo: {placa} | Setor: {tipo.ToUpper()} | Horário: {DateTime.Now:HH:mm}"
                );
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERRO NA OPERAÇÃO: Entrada recusada.");
                Console.WriteLine(
                    "Motivo: pátio lotado, veículo já estacionado ou estacionamento fechado."
                );
                Console.ResetColor();
            }
        }

        static void MenuRegistrarSaida(Estacionamento sistema)
        {
            Console.WriteLine("\n--- Check-out e Pagamento ---");
            Console.Write("Digite a placa para saída: ");
            string placaSaida = (Console.ReadLine() ?? "").Trim().ToUpper();

            double valorTotal = sistema.ExecutarSaida(placaSaida, out double minutos);

            if (valorTotal >= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSaída processada para: {placaSaida}");
                Console.WriteLine($"Tempo de permanência: {Math.Round(minutos)} minutos.");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"TOTAL A PAGAR: R$ {valorTotal:F2}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro: veículo não encontrado no pátio.");
                Console.ResetColor();
            }
        }

        static void MenuExibirPainel(Estacionamento sistema)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- PAINEL DE OCUPAÇÃO ---");
            Console.ResetColor();

            foreach (var s in sistema.Setores.Values)
            {
                if (s.EstaLotado)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(
                        $"{s.Nome, -7} -> [LOTADO] Livres: {s.Disponivel}/{s.Limite} (Ocupadas: {s.Ocupadas})"
                    );
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(
                        $"{s.Nome, -7} -> Livres: {s.Disponivel}/{s.Limite} (Ocupadas: {s.Ocupadas})"
                    );
                }
            }
        }
    }
}
