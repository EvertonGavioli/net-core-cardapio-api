using CD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.Domain.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> ObterTodas(Guid estabelecimentoId, Guid userId);
        Task<Categoria> ObterCategoria(Guid categoriaId, Guid userId);
        Task<Categoria> ObterCategoriaProdutos(Guid categoriaId, Guid userId);
        Task<int> ObterProximaPosicao(Guid estabelecimentoId, Guid userId);
    }
}
