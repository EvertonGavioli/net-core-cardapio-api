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
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Categoria>> ObterTodas(Guid estabelecimentoId, Guid userId)
        {
            return await DbSet.AsNoTracking()
                .Where(f => f.EstabelecimentoId == estabelecimentoId && f.UsuarioId == userId)
                .ToListAsync();
        }

        public async Task<Categoria> ObterCategoria(Guid categoriaId, Guid userId)
        {
            return await Db.Categorias.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == categoriaId && c.UsuarioId == userId);
        }

        public async Task<Categoria> ObterCategoriaProdutos(Guid categoriaId, Guid userId)
        {
            return await Db.Categorias.AsNoTracking()
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(c => c.Id == categoriaId && c.UsuarioId == userId);
        }

        public async Task<int> ObterProximaPosicao(Guid estabelecimentoId, Guid userId)
        {
            var categoria = await Db.Categorias
                .AsNoTracking()
                .Where(f => f.EstabelecimentoId == estabelecimentoId && f.UsuarioId == userId)
                .OrderByDescending(f => f.Posicao)
                .FirstOrDefaultAsync();

            return categoria != null ? categoria.Posicao + 1 : 0;
        }
    }
}
