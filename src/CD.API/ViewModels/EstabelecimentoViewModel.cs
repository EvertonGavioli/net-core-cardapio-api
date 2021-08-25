using System;
using System.ComponentModel.DataAnnotations;

namespace CD.API.ViewModels
{
    public class EstabelecimentoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string Nome { get; set; }
        public string LogoUrl { get; set; }
        public string LogoImagem { get; set; }
        public string Segmento { get; set; }
        public string Telefone { get; set; }
        public EnderecoViewModel Endereco { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 6)]
        public string Host { get; set; }
        public string QRCode { get; set; }
        public string CardapioUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public int AcessosTotal { get; set; }
        public int AcessosUnicos { get; set; }
    }
}
