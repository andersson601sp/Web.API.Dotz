using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Web.API.Dotz.Services;
using Web.API.Dotz.Entities;
using Web.API.Dotz.Models;
using AutoMapper;
using System.Threading.Tasks;
using Web.API.Dotz.Dtos;
using Web.API.Dotz.Data.RepoUserDotz;
using Web.API.Dotz.Helpers;
using System.Collections.Generic;

namespace Web.API.Dotz.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserDotzController : ControllerBase
    {
        private readonly IMapper _mapper;

        public readonly IRepositoryUserDotz _repo;

        public UserDotzController(IMapper mapper, IRepositoryUserDotz repo)
        {
            _mapper = mapper;
             _repo = repo;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            var userDotz = await _repo.GetAllUserDotzAsync(pageParams);

            var userDotzResult = _mapper.Map<IEnumerable<UserDotzModel>>(userDotz);

            Response.AddPagination(userDotz.CurrentPage, userDotz.PageSize, userDotz.TotalCount, userDotz.TotalPages);

            return Ok(userDotzResult);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            var userDotz = _repo.GetUserDotzById(id);
            var userDotzResult = _mapper.Map<UserDotzModel>(userDotz);
            return Ok(userDotzResult);
        }

    }
}
