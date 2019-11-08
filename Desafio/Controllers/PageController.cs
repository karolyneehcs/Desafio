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
    [Authorization]
    public class PageController : ControllerBase
    {
        private readonly PageRepository _pageRepository;
        private readonly IMapper _mapper;

        public PageController(PageRepository pageRepository, IMapper mapper)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_pageRepository.GetAll());

        }

        [HttpGet("{id}")]
        public ActionResult Get(int Id)
        {
            return Ok(_pageRepository.GetById(Id));
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

        [HttpPost]
        public ActionResult Create(PageViewModel page)
        {
            var pagina = _pageRepository.Add(_mapper.Map<Page>(page));

            if (pagina == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<PageViewModel>(pagina));
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