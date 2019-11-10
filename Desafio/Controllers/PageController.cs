using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Desafio.Filters;
using Desafio.Models;
using Desafio.Repository;
using Desafio.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Controllers
{
    [Route("api/page")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly PageRepository _pageRepository;
        private readonly AccessRepository _accessRepository;
        private readonly IMapper _mapper;

        public PageController(PageRepository pageRepository, 
                              AccessRepository accessRepository, 
                              IMapper mapper)
        {
            _pageRepository = pageRepository;
            _accessRepository = accessRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(_mapper.Map<PageViewModel>(_pageRepository.GetById(Id)));
        }

        [HttpGet]
        public ActionResult GetAll_()
        {
            return Ok(_mapper.Map<IEnumerable<PageViewModel>>(_pageRepository.GetAll()));
        }

        [HttpPost("createpage")]
        [Authorization]
        public ActionResult Create(PageViewModel page)
        {
            var access = _accessRepository.GetByToken(HttpContext.Request.Headers["Authorization"]);
            page.OwnerId = access.UserId;
            var pagina = _pageRepository.Add(_mapper.Map<Page>(page));

            if (pagina == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<UserViewModel>(pagina));
        }

        [HttpPut]
        public ActionResult Update(PageViewModel page)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(a => a.Errors).Select(a => a.ErrorMessage));
            }

            try
            {
                return Ok(_pageRepository.Update(_mapper.Map<Page>(page)));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}