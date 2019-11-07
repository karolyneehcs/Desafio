using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Desafio.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Controllers
{
    [Route("api/page")]
    [ApiController]
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
    }
}