using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDEProdutos.models
{
    public class Produto
    {
        public string Codigo { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }
        public float PrecoVendas { get; set; }
        public float PrecoCusto { get; set; }

        public DateTime DataCadastro { get; set; }

        public float Estoque { get; set; }

        public string Imagem { get; set; }

        public int AlturaCentimetros { get; set; }

        public int LarguraCentimetros { get; set; }

        public int ProfundidadeCentimetros { get; set; }

        public Categoria Categoria { get; set; }

        public string CategoriaProdutos { get; set; }

        public int IDCategoria { get; set; }

        public bool Ativo { get; set; }

        public int EstoqueAtual { get; set; }
        public int EstoqueMinimo { get; set; }



    }
}
