using System;

namespace CD.Domain.Models
{
    public class Produto : Entity
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string ImagemUrl { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Posicao { get; set; }
        public bool Ativo { get; set; }

        // Relação EF
        public Guid CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
