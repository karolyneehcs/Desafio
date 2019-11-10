using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Desafio.Models;
using Desafio.Repository;
using Desafio.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly AccessRepository _accessRepository;
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper; 

        public AccessController(AccessRepository accessRepository, 
                                UserRepository userRepository,
                                IMapper mapper)
        {
            _accessRepository = accessRepository;
            _userRepository = userRepository;
            _mapper = mapper; 
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            var user = _userRepository.CheckPassword(login.Email, login.Password);

            if(user == null)
            {
                return BadRequest(new[] { "Email or password invalid" });
            }

            return Ok(_accessRepository.Add(new Access 
            { 
                Token = Guid.NewGuid().ToString(), 
                UserId = user.Id 
            }));
        }
    }
}