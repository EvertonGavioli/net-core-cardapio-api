using CD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.Domain.Interfaces
{
    public interface IEstabelecimentoRepository : IRepository<Estabelecimento>
    {
        Task<IEnumerable<Estabelecimento>> ObterTodosPorUsuario(Guid UserId);
        Task<Estabelecimento> ObterEstabelecimentoEndereco(Guid id);
    }
}
