using QRCoder;
using Slugify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.Domain.Models
{
    public class Estabelecimento : Entity
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string LogoUrl { get; set; }
        public string Segmento { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }
        public string Host { get; set; }
        public string QRCode { get; set; }
        public string CardapioUrl { get; private set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public int AcessosTotal { get; set; }
        public int AcessosUnicos { get; set; }
        public int Fonte { get; set; }
        public string CorHex { get; set; }
        public bool DarkMode { get; set; }
        public IEnumerable<Categoria> Categorias { get; set; }


        public void GerarDadosUrlQrCode()
        {
            GerarUrlEstabelecimento();
            GerarQrCode();
        }

        private void GerarUrlEstabelecimento()
        {
            SlugHelper helper = new SlugHelper();
            this.CardapioUrl = this.Host + "/" + helper.GenerateSlug(this.Nome) + "/" + this.Id;
        }

        private void GerarQrCode()
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(this.CardapioUrl, QRCodeGenerator.ECCLevel.Q);
            Base64QRCode qrCode = new Base64QRCode(qRCodeData);
            string qrCodeImageAsBase64 = qrCode.GetGraphic(20);

            this.QRCode = qrCodeImageAsBase64;
        }
    }
}
