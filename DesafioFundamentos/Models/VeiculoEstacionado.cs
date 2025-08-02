using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioFundamentos.Models
{
    public class VeiculoEstacionado
    {
        public string Placa { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime? DataHoraSaida { get; set; }
        public decimal ValorTotal { get; set; }
        public VeiculoEstacionado(string placa)
        {
            Placa = placa;
            DataHoraEntrada = DateTime.Now;
        }
               
    }
}
