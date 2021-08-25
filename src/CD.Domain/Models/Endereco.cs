using System;

namespace CD.Domain.Models
{
    public class Endereco : Entity
    {
        public string CEP { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }

        // Relação EF
        public Guid EstabelecimentoId { get; set; }
        public Estabelecimento Estabelecimento { get; set; }
    }
}
