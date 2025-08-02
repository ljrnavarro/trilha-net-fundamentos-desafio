namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<VeiculoEstacionado> veiculos = new List<VeiculoEstacionado>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            var placaDigitada = Convert.ToString(Console.ReadLine());

            if (!veiculos.Any(x => x.Placa.ToUpper() == placaDigitada.ToUpper()))
            {
                veiculos.Add(new VeiculoEstacionado(placaDigitada));
                Console.WriteLine($"O veículo {placaDigitada} foi adicionado com sucesso!");
            }
            else
            {
                Console.WriteLine("Placa inválida ou veículo já está estacionado.");
            }
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");
            var placaDigitada = Convert.ToString(Console.ReadLine());

            // Verifica se o veículo existe  
            var veiculoSelecionado = veiculos.FirstOrDefault(x => x.Placa.ToUpper() == placaDigitada.ToUpper());

            if (veiculoSelecionado != null)
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
                int hras;
                if (int.TryParse(Console.ReadLine(), out hras))
                {
                    decimal valorTotal = precoInicial + (precoPorHora * hras);
                    veiculoSelecionado.DataHoraSaida = veiculoSelecionado.DataHoraEntrada.AddHours(hras);
                    veiculoSelecionado.ValorTotal = valorTotal;
                    Console.WriteLine($"O veículo {placaDigitada} foi removido e o preço total foi de: R$ {valorTotal}");
                }
                else
                {
                    Console.WriteLine("Quantidade de horas inválida. O veículo não será removido.");
                }
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos(EstadoVeiculoEstacionado estadoVeiculoEstacionado)
        {
            List<VeiculoEstacionado> listaFiltro = new List<VeiculoEstacionado>();

            switch (estadoVeiculoEstacionado)
            {
                case EstadoVeiculoEstacionado.Estacionado:
                    listaFiltro = veiculos.Where(x => x.ValorTotal <= 0).ToList();
                    break;
                case EstadoVeiculoEstacionado.NaoEstacionado:
                    listaFiltro = veiculos.Where(x => x.DataHoraSaida != null).ToList();
                    break;
                default:
                    listaFiltro = veiculos;
                    break;
            }

            // Verifica se há veículos no estacionamento  
            if (listaFiltro.Any())
            {
                Console.WriteLine(" ################|  Os veículos estacionados são: |####################");
                Console.WriteLine($"                    ¤ preço inicial: R$ {precoInicial.ToString("F2")}");
                Console.WriteLine($"                    ¤ preço por hora: R$ {precoPorHora.ToString("F2")}");
                Console.WriteLine();
                int cont = 0;

                foreach (var veiculo in listaFiltro)
                {
                    Console.WriteLine($"  Veículo {cont + 1}");
                    Console.WriteLine($"  Veículo de placa:{veiculo.Placa.ToString()}");
                    Console.WriteLine($"  data/horário de entrada: {veiculo.DataHoraEntrada.ToString("dd/MM/yyyy hh:mm")}");
                    if (veiculo.DataHoraSaida.HasValue) Console.WriteLine($"  data/horário de saída: {veiculo.DataHoraSaida.Value.ToString("dd/MM/yyyy hh:mm")}");
                    if (veiculo.ValorTotal > 0) Console.WriteLine($"  Valor total: R$ {veiculo.ValorTotal.ToString("F2")}");

                    if (cont < veiculos.Count())
                    {
                        Console.WriteLine($"");
                        Console.WriteLine($"-------------------------------------");
                        Console.WriteLine($"");
                    }

                    cont++;
                }

                Console.WriteLine(" ");
                Console.WriteLine(" ################|  Resumo : |####################");
                Console.WriteLine($"    ¤  Quantidade de veículos: {listaFiltro.Count}");
                if (!estadoVeiculoEstacionado.Equals(EstadoVeiculoEstacionado.Estacionado)) Console.WriteLine($"    ¤  Valor total acumulado: R$ {listaFiltro.Sum(x => x.ValorTotal).ToString("F2")}");
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }

    public enum EstadoVeiculoEstacionado
    {
        Estacionado,
        NaoEstacionado,
        Todos
    }
}
