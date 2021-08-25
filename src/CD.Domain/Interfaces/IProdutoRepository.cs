using CD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.Domain.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterTodos(Guid categoriaId, Guid userId);
        Task<Produto> ObterProduto(Guid produtoId, Guid userId);
        Task<int> ObterProximaPosicao(Guid categoriaId, Guid userId);
    }
}
