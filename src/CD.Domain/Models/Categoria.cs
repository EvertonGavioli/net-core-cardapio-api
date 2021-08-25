using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.Domain.Models
{
    public class Categoria : Entity
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public int Posicao { get; set; }

        // Relação EF
        public Guid EstabelecimentoId { get; set; }
        public Estabelecimento Estabelecimento { get; set; }
        public IEnumerable<Produto> Produtos { get; set; }
    }
}
