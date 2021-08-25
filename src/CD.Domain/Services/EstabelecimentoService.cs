using CD.Domain.Interfaces;
using CD.Domain.Models;
using CD.Domain.Models.Validacoes;
using System;
using System.Threading.Tasks;

namespace CD.Domain.Services
{
    public class EstabelecimentoService : BaseService, IEstabelecimentoService
    {
        private readonly IEstabelecimentoRepository _estabelecimentoRepository;

        public EstabelecimentoService(IEstabelecimentoRepository estabelecimentoRepository,
                                      IUser user,
                                      INotificador notificador) : base(notificador, user)
        {
            _estabelecimentoRepository = estabelecimentoRepository;
        }

        public async Task<Estabelecimento> Adicionar(Estabelecimento estabelecimento)
        {
            if (!ExecutarValidacao(new EstabelecimentoValidation(), estabelecimento) ||
                !ExecutarValidacao(new EnderecoValidation(), estabelecimento.Endereco))
            {
                return null;
            }

            estabelecimento.Id = Guid.NewGuid();            
            estabelecimento.UsuarioId = UsuarioId;
            estabelecimento.Endereco.Id = Guid.NewGuid();

            estabelecimento.GerarDadosUrlQrCode();

            await _estabelecimentoRepository.Adicionar(estabelecimento);            
            return estabelecimento;
        }

        public async Task<Estabelecimento> Atualizar(Estabelecimento estabelecimento)
        {
            if (!ExecutarValidacao(new EstabelecimentoValidation(), estabelecimento) ||
                !ExecutarValidacao(new EnderecoValidation(), estabelecimento.Endereco))
            {
                return null;
            }
            
            estabelecimento.UsuarioId = UsuarioId;
            
            estabelecimento.GerarDadosUrlQrCode();

            await _estabelecimentoRepository.Atualizar(estabelecimento);
            return estabelecimento;
        }

        public async Task<bool> Remover(Guid id)
        {
            await _estabelecimentoRepository.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _estabelecimentoRepository?.Dispose();
        }
           
    }
}
