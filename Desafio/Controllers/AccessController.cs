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
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly AccessRepository _accessRepository;
        private readonly IMapper _mapper; 

        public AccessController(AccessRepository accessRepository, IMapper mapper)
        {
            _accessRepository = accessRepository;
            _mapper = mapper; 
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_accessRepository.GetAll());
          
        }

        [HttpGet("{id}")]
        public ActionResult Get(int Id)
        {
            return Ok(_accessRepository.GetById(Id));
        }


        [HttpGet("{id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(_mapper.Map<AccessRepository>(_accessRepository.GetById(Id)));
        }

    }
}