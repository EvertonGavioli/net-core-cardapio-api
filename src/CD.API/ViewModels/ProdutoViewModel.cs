using System;
using System.ComponentModel.DataAnnotations;

namespace CD.API.ViewModels
{
    public class ProdutoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string Nome { get; set; }

        public string ImagemUrl { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string Descricao { get; set; }

        public decimal Preco { get; set; }

        public int Posicao { get; set; }

        public bool Ativo { get; set; }

        public Guid CategoriaId { get; set; }
    }
}