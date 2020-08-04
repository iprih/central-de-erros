﻿using AutoMapper;
using IdentityModel.Client;
using CentralErros.Models;
using CentralErros.DTO;
using CentralErros.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace CentralErros.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class EnvironmentController : ControllerBase
    {
        private readonly IEnvironmentService _envService;
        private readonly IMapper _mapper;
        private readonly CentralErroContexto _context;

        public EnvironmentController(IEnvironmentService envService, IMapper mapper, CentralErroContexto context)
        {
            _envService = envService;
            _mapper = mapper;
            _context = context;
        }

        // GETALL: api/environment/
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Environment>> FindAll()
        {
            var env = _envService.FindAll();
            if (env != null)
            {

                return Ok(env.Select(x => _mapper.Map<Environment>(x)).ToList());
            }
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Environment> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Environment env = _envService.FindById(id);

            if (env != null)
            {
                _context.Environments.Remove(env);
                var retorno = _envService.SaveOrUpdate(env);

                return Ok(retorno);
            }
            else
            {
                object res = null;
                NotFoundObjectResult notfound = new NotFoundObjectResult(res);
                notfound.StatusCode = 404;

                notfound.Value = "O Environment " + id + " não foi encontrado!";
                return NotFound(notfound);
            }
        }


    }
}