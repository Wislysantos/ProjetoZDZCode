using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiProjetoZDZCode.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Calorias { get; set; }
        public double Preco { get; set; }
        public decimal Quantidade { get; set; }

        public Produto() { }
    }
}