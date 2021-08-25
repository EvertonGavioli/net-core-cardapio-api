using CD.Domain.Interfaces;
using CD.Domain.Models;
using CD.Domain.Models.Validacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.Domain.Services
{
    public class CategoriaService : BaseService, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProdutoRepository _produtoRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository,
                                IProdutoRepository produtoRepository,
                                INotificador notificador,
                                IUser aspNetUser) : base(notificador, aspNetUser)
        {
            _categoriaRepository = categoriaRepository;
            _produtoRepository = produtoRepository;
        }

        // Categorias
        public async Task<Categoria> Adicionar(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidation(), categoria))
            {
                return null;
            }

            categoria.Id = Guid.NewGuid();
            categoria.UsuarioId = UsuarioId;
            categoria.Posicao = await _categoriaRepository.ObterProximaPosicao(categoria.EstabelecimentoId, UsuarioId);

            await _categoriaRepository.Adicionar(categoria);
            return categoria;
        }

        public async Task<bool> Remover(Guid id)
        {
            await _categoriaRepository.Remover(id);
            return true;
        }


        // Produtos
        public async Task<Produto> AdicionarProduto(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto))
            {
                return null;
            }

            produto.Id = Guid.NewGuid();
            produto.UsuarioId = UsuarioId;
            produto.Posicao = await _produtoRepository.ObterProximaPosicao(produto.CategoriaId, UsuarioId);

            await _produtoRepository.Adicionar(produto);
            return produto;
        }

        public async Task<Produto> AtualizarProduto(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto))
            {
                return null;
            }
            
            produto.UsuarioId = UsuarioId;

            await _produtoRepository.Atualizar(produto);
            return produto;
        }

        public async Task<bool> RemoverProduto(Guid produtoId)
        {
            await _produtoRepository.Remover(produtoId);
            return true;
        }

        public void Dispose()
        {
            _categoriaRepository?.Dispose();
        }
    }
}
