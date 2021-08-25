using CD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.Domain.Interfaces
{
    public interface ICategoriaService : IDisposable
    {
        Task<Categoria> Adicionar(Categoria categoria);
        Task<bool> Remover(Guid id);

        // Produtos
        Task<Produto> AdicionarProduto(Produto produto);
        Task<Produto> AtualizarProduto(Produto produto);
        Task<bool> RemoverProduto(Guid produtoId);
    }
}
