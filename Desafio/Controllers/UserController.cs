using AutoMapper;
using Desafio.Filters;
using Desafio.Models;
using Desafio.Repository;
using Desafio.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly AccessRepository _accessRepository;
        private readonly UserCompanyRepository _userCompanyRepository;
        private readonly IMapper _mapper; 

        public UserController(UserRepository userRepository, 
                              AccessRepository accessRepository,
                              UserCompanyRepository userCompanyRepository,
                              IMapper mapper)
        {
            _userRepository = userRepository;
            _accessRepository = accessRepository;
            _userCompanyRepository = userCompanyRepository;
            _mapper = mapper; 
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

        [HttpGet("represent-company/{companyId}")]
        [Authorization]
        public IActionResult RepresentCompany(int companyId)
        {
            var access = _accessRepository.GetByToken(HttpContext.Request.Headers["Authorization"]);

            var userCompany = _userCompanyRepository.Add(new UserCompany 
            {
                UserId = access.UserId, 
                CompanyId = companyId 
            });

            if(userCompany == null)
            {
                BadRequest();
            }

            return Ok();
        }
    }
}