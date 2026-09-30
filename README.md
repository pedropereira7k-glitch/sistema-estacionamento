# Sistema de Estacionamento

Projeto em C# para controle de entrada, saída e ocupação de vagas em um estacionamento.

## Funcionalidades

- Registrar entrada de veículos
- Registrar saída de veículos
- Exibir painel de vagas
- Calcular valor da permanência
- Controlar vagas por setor:
  - Normal
  - Idoso
  - PcD

## Regras do sistema

- Horário de funcionamento: 08:00 às 22:00
- Tolerância gratuita: 15 minutos
- Primeira hora: R$ 15,00
- Fração de 15 minutos: R$ 3,00
- Teto diário: R$ 50,00

## Estrutura do projeto

- `Program.cs` → menu principal e interação com o usuário
- `Estacionamento.cs` → regras do estacionamento
- `Veiculo.cs` → dados do veículo
- `SetorVaga.cs` → controle dos setores de vagas

## Como executar

1. Clone o repositório: git clone https://github.com/pedropereira7k-glitch/sistema-estacionamento.git
2. Abra o projeto no Visual Studio ou VS Code
3. Compile e execute a aplicação com: dotnet run

## Exemplo de uso

### Entrada
- Digite a placa do veículo
- Escolha o tipo de vaga:
  - 1 = Normal
  - 2 = Idoso
  - 3 = PcD

### Saída
- Digite a placa do veículo
- O sistema calcula automaticamente o valor a pagar

## Tecnologias utilizadas

- C#
- .NET

## Autor

Pedro Henrique Pereira Leão da Silva
