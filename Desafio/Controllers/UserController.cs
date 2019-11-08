using AutoMapper;
using Desafio.Context;
using Desafio.Filters;
using Desafio.Models;
using Desafio.Repository;
using Desafio.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper; 

        public UserController(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper; 
        }

        [HttpGet]
        [Authorization]
        public IActionResult Get()
        {
            var user = HttpContext.Request.Headers["Authorization"];
            return Ok();
        }

        [HttpGet("test")]
        public IActionResult GetTest()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(_mapper.Map<UserViewModel>(_userRepository.GetById(Id)));
        }

        [HttpPost]
        public ActionResult Create(UserViewModel user)
        {
            var usuario = _userRepository.Add(_mapper.Map<User>(user)); 

            if(usuario == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<UserViewModel>(usuario)); 
        }
    }
}