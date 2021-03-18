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
using Web.API.Dotz.Data.RepoUserDotz;

namespace Web.API.Dotz.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly IMapper _mapper;

        public readonly IRepositoryUser _repo;
        public readonly IRepositoryUserDotz _repoDotz;

        public UsersController(IUserService userService, IMapper mapper, IRepositoryUser repo, IRepositoryUserDotz repoDotz)
        {
            _userService = userService;
            _mapper = mapper;
             _repo = repo;
             _repoDotz = repoDotz;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);
            if (user == null)
                return BadRequest(new { message = "Nome de usuário ou senha está incorreta." });

            var userResult = _mapper.Map<UserAuthDto>(user);

            return Ok(userResult);
        } 
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            var users = await _repo.GetAllUsersAsync(pageParams);

            var usersResult = _mapper.Map<IEnumerable<UserDto>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(usersResult);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // só permite que administradores acessem outros registros de usuário
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            var user = _repo.GetUserById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult Post([FromBody] UserDto model)
        {
            User user = _mapper.Map<User>(model);

            _repo.Add(user);
            if (_repo.SaveChanges())
            {
                return Created($"/user/{model.Id}", _mapper.Map<UserDto>(user));
            }

            return BadRequest("Usuario não cadastrado");
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDto model)
        {
            var user = _repo.GetUserById(id);
            if (user == null) return BadRequest("Usuario não encontrado");

            _mapper.Map(model, user);

            _repo.Update(user);
            if (_repo.SaveChanges())
            {
                return Created($"/user/{model.Id}", _mapper.Map<UserDto>(user));
            }

            return BadRequest("Usuario não Atualizado");
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] UserDto model)
        {
            var user = _repo.GetUserById(id);
            if (user == null) return BadRequest("Usuario não encontrado");

            _mapper.Map(model, user);

            _repo.Update(user);
            if (_repo.SaveChanges())
            {
                return Created($"/user/{model.Id}", _mapper.Map<UserDto>(user));
            }

            return BadRequest("Usuario não Atualizado");
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _repo.GetUserById(id);
            if (user == null) return BadRequest("Usuario não encontrado");

            _repo.Delete(user);
            if (_repo.SaveChanges())
            {
                return Ok("Usuario deletado");
            }

            return BadRequest("Usuario não deletado");
        }
    }
}
