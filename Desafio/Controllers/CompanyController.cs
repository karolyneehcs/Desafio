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
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyController(CompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_companyRepository.GetAll());

        }

        [HttpGet("{id}")]
        public ActionResult Get(int Id)
        {
            return Ok(_companyRepository.GetById(Id));
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(_mapper.Map<CompanyViewModel>(_companyRepository.GetById(Id)));
        }

        [HttpGet]
        public ActionResult GetAll_()
        {
            return Ok(_mapper.Map<IEnumerable<CompanyViewModel>>(_companyRepository.GetAll()));
        }

        [HttpPut]
        public ActionResult Update(CompanyViewModel company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(a => a.Errors).Select(a => a.ErrorMessage));
            }

            try
            {
                return Ok(_companyRepository.Update(_mapper.Map<Company>(company)));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}