using AutoMapper;
using CD.API.ViewModels;
using CD.Domain.Interfaces;
using CD.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CD.API.Controllers
{
    [Route("api")]
    public class CategoriaController : MainController
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ICategoriaService _categoriaService;
        private readonly IProdutoRepository _produtoRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository,
                                   ICategoriaService categoriaService,
                                   IProdutoRepository produtoRepository,
                                   IMapper mapper,
                                   INotificador notificador,
                                   IUser user) : base(notificador, user)
        {
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
            _categoriaService = categoriaService;
            _produtoRepository = produtoRepository;
        }

        [Authorize]
        [HttpGet("{estabelecimentoId:guid}/categorias")]
        public async Task<IEnumerable<CategoriaViewModel>> ObterTodas(Guid estabelecimentoId)
        {
            return _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodas(estabelecimentoId, UsuarioId));
        }

        [Authorize]
        [HttpGet("{estabelecimentoId:guid}/categorias/{categoriaId:guid}")]
        public async Task<ActionResult<CategoriaViewModel>> ObterPorId(Guid estabelecimentoId, Guid categoriaId)
        {
            var categoria = await _categoriaRepository.ObterCategoriaProdutos(categoriaId, UsuarioId);

            if (categoria == null) return NotFound();

            return _mapper.Map<CategoriaViewModel>(categoria);
        }

        [Authorize]
        [HttpPost("{estabelecimentoId:guid}/categorias")]
        public async Task<ActionResult<CategoriaViewModel>> Adicionar(Guid estabelecimentoId, CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (estabelecimentoId != categoriaViewModel.EstabelecimentoId)
            {
                AdicionarErro("O id do estabelecimento informado não é o mesmo que foi informado na query.");
                return CustomResponse();
            }

            var result = await _categoriaService.Adicionar(_mapper.Map<Categoria>(categoriaViewModel));

            return CustomResponse(nameof(Adicionar), _mapper.Map<CategoriaViewModel>(result));
        }

        [Authorize]
        [HttpDelete("{estabelecimentoId:guid}/categorias/{categoriaId:guid}")]
        public async Task<ActionResult<CategoriaViewModel>> Excluir(Guid estabelecimentoId, Guid categoriaId)
        {
            var categoria = await _categoriaRepository.ObterCategoria(categoriaId, UsuarioId);

            if (categoria == null) return NotFound();

            await _categoriaService.Remover(categoriaId);

            return CustomResponse(_mapper.Map<CategoriaViewModel>(categoria));
        }


        // Produtos
        [Authorize]
        [HttpPost("{estabelecimentoId:guid}/categorias/{categoriaId:guid}/produtos")]
        public async Task<ActionResult<ProdutoViewModel>> AdicionarProduto(Guid categoriaId, ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (categoriaId != produtoViewModel.CategoriaId)
            {
                AdicionarErro("O id do categoria informado no body não é o mesmo que foi informado na query.");
                return CustomResponse();
            }

            var result = await _categoriaService.AdicionarProduto(_mapper.Map<Produto>(produtoViewModel));

            return CustomResponse(nameof(AdicionarProduto), _mapper.Map<ProdutoViewModel>(result));
        }

        [Authorize]
        [HttpPut("{estabelecimentoId:guid}/categorias/{categoriaId:guid}/produtos/{produtoId:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Atualizar(Guid produtoId, ProdutoViewModel produtoViewModel)
        {
            if (produtoId != produtoViewModel.Id)
            {
                AdicionarErro("O id informado não é o mesmo que foi informado na query.");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _categoriaService.AtualizarProduto(_mapper.Map<Produto>(produtoViewModel));

            return CustomResponse(_mapper.Map<ProdutoViewModel>(result));
        }

        [Authorize]
        [HttpDelete("{estabelecimentoId:guid}/categorias/{categoriaId:guid}/produtos/{produtoId:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> ExcluirProduto(Guid produtoId)
        {
            var produto = await _produtoRepository.ObterProduto(produtoId, UsuarioId);

            if (produto == null) return NotFound();

            await _categoriaService.RemoverProduto(produtoId);

            return CustomResponse(_mapper.Map<ProdutoViewModel>(produto));
        }

    }
}
