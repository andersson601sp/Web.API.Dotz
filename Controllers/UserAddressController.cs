using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Web.API.Dotz.Services;
using Web.API.Dotz.Entities;
using Web.API.Dotz.Models;
using AutoMapper;
using System.Threading.Tasks;
using Web.API.Dotz.Dtos;
using Web.API.Dotz.Data.RepoUser;
using Web.API.Dotz.Helpers;
using System.Collections.Generic;
using Web.API.Dotz.Data.RepoUserAddress;

namespace Web.API.Dotz.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserAddressController : ControllerBase
    {
       private readonly IMapper _mapper;

        public readonly IRepositoryUserAddress _repo;
        public readonly IRepositoryUser _repoUsr;

        public UserAddressController(IMapper mapper, IRepositoryUserAddress repo, IRepositoryUser repoUsr)
        {
            _mapper = mapper;
            _repo = repo;
            _repoUsr = repoUsr;
        }

        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            var userAddress = await _repo.GetAllUsersAddressAsync(pageParams);

            var userAddressResult = _mapper.Map<IEnumerable<UserAddressDto>>(userAddress);

            Response.AddPagination(userAddress.CurrentPage, userAddress.PageSize, userAddress.TotalCount, userAddress.TotalPages);

            return Ok(userAddressResult);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            var userAddress = _repo.GetUserById(id);
            var userAddressResult = _mapper.Map<UserAddressDto>(userAddress);
            return Ok(userAddressResult);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] UserAddress model)
        {
             var user = _repoUsr.GetUserById(model.userId);

             if(user == null)  return BadRequest("Usuário não cadastrado");

            UserAddress userAddress = _mapper.Map<UserAddress>(model);

            _repo.Add(userAddress);
            if (_repo.SaveChanges())
            {
                return Created($"/useraddress/{model.Id}", _mapper.Map<UserAddressDto>(userAddress));
            }

            return BadRequest("Endereço não cadastrado");
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDto model)
        {
             var user = _repoUsr.GetUserById(model.Id);

             if(user == null)  return BadRequest("Usuário não cadastrado");

            var userAddress = _repoUsr.GetUserById(id);
            if (userAddress == null) return BadRequest("Endereço não encontrado");

            _mapper.Map(model, userAddress);

            _repo.Update(userAddress);
            if (_repo.SaveChanges())
            {
                return Created($"/useraddress/{model.Id}", _mapper.Map<UserAddressDto>(userAddress));
            }

            return BadRequest("Usuario não Atualizado");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userAddress = _repoUsr.GetUserById(id);
            if (userAddress == null) return BadRequest("Endereço não encontrado");

            _repo.Delete(userAddress);
            if (_repo.SaveChanges())
            {
                return Ok("Endereço deletado");
            }

            return BadRequest("Endereço não deletado");
        }

    }
}
