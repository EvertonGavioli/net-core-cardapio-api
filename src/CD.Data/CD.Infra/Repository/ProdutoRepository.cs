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
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Produto>> ObterTodos(Guid categoriaId, Guid userId)
        {
            return await Db.Produtos
                .AsNoTracking()
                .Where(c => c.CategoriaId == categoriaId && c.UsuarioId == userId)
                .ToListAsync();
        }

        public async Task<Produto> ObterProduto(Guid produtoId, Guid userId)
        {
            return await Db.Produtos
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == produtoId && c.UsuarioId == userId);
        }

        public async Task<int> ObterProximaPosicao(Guid categoriaId, Guid userId)
        {
            var produto = await Db.Produtos
                .AsNoTracking()
                .Where(f => f.CategoriaId == categoriaId && f.UsuarioId == userId)
                .OrderByDescending(f => f.Posicao)
                .FirstOrDefaultAsync();

            return produto != null ? produto.Posicao + 1 : 0;
        }
    }
}
