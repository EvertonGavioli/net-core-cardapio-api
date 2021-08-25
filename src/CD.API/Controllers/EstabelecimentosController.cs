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
    [Route("api/estabelecimentos")]
    public class EstabelecimentosController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IEstabelecimentoRepository _estabelecimentoRepository;
        private readonly IEstabelecimentoService _estabelecimentoService;

        public EstabelecimentosController(IMapper mapper,
                                          IUser user,
                                          INotificador notificador,
                                          IEstabelecimentoRepository estabelecimentoRepository,
                                          IEstabelecimentoService estabelecimentoService) : base(notificador, user)
        {
            _mapper = mapper;
            _estabelecimentoRepository = estabelecimentoRepository;
            _estabelecimentoService = estabelecimentoService;
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<EstabelecimentoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<EstabelecimentoViewModel>>(await _estabelecimentoRepository.ObterTodosPorUsuario(UsuarioId));
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EstabelecimentoViewModel>> ObterPorId(Guid id)
        {
            var estabelecimento = await _estabelecimentoRepository.ObterEstabelecimentoEndereco(id);

            if (estabelecimento == null) return NotFound();

            return _mapper.Map<EstabelecimentoViewModel>(estabelecimento);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<EstabelecimentoViewModel>> Adicionar(EstabelecimentoViewModel estabelecimentoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _estabelecimentoService.Adicionar(_mapper.Map<Estabelecimento>(estabelecimentoViewModel));

            return CustomResponse(nameof(Adicionar), _mapper.Map<EstabelecimentoViewModel>(result));
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<EstabelecimentoViewModel>> Atualizar(Guid id, EstabelecimentoViewModel estabelecimentoViewModel)
        {
            if (id != estabelecimentoViewModel.Id)
            {
                AdicionarErro("O id informado não é o mesmo que foi informado na query.");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _estabelecimentoService.Atualizar(_mapper.Map<Estabelecimento>(estabelecimentoViewModel));

            return CustomResponse(_mapper.Map<EstabelecimentoViewModel>(result));
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<EstabelecimentoViewModel>> Excluir(Guid id)
        {
            var estabelecimento = await _estabelecimentoRepository.ObterEstabelecimentoEndereco(id);

            if (estabelecimento == null) return NotFound();

            await _estabelecimentoService.Remover(id);

            return CustomResponse(_mapper.Map<EstabelecimentoViewModel>(estabelecimento));
        }
    }
}
