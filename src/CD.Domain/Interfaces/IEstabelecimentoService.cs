using CD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.Domain.Interfaces
{
    public interface IEstabelecimentoService : IDisposable
    {
        Task<Estabelecimento> Adicionar(Estabelecimento estabelecimento);
        Task<Estabelecimento> Atualizar(Estabelecimento estabelecimento);
        Task<bool> Remover(Guid id);
    }
}
