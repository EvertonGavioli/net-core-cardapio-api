using CD.Domain.Interfaces;
using CD.Domain.Models;
using CD.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.Infra.Repository
{
    public class EstabelecimentoRepository : Repository<Estabelecimento>, IEstabelecimentoRepository
    {
        public EstabelecimentoRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<IEnumerable<Estabelecimento>> ObterTodosPorUsuario(Guid UserId)
        {
            return await DbSet
                .Include(f => f.Endereco)
                .Where(f => f.UsuarioId == UserId)
                .ToListAsync();
        }

        public async Task<Estabelecimento> ObterEstabelecimentoEndereco(Guid id)
        {
            return await Db.Estabelecimentos.AsNoTracking()
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
