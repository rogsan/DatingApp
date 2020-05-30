using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
   [Authorize]
   [Route("api/[controller]")]
   [ApiController]
   public class UsersController : ControllerBase
   {
      private readonly IDatingRepository _repository;
      private readonly IMapper _mapper;

      public UsersController(IDatingRepository repository, IMapper mapper)
      {
         _repository = repository;
         _mapper = mapper;
      }

      [HttpGet]
      public async Task<ActionResult> GetUsers()
      {
         var users = await _repository.GetUsers();
         var usersToReturn = _mapper.Map<IEnumerable<UsersForListDTO>>(users);

         return Ok(usersToReturn);
      }

      [HttpGet("{id}")]
      public async Task<IActionResult> GetUser(int id)
      {
         var user = await _repository.GetUser(id);
         var userToReturn = _mapper.Map<UserForDetailDTO>(user);

         return Ok(userToReturn);
      }
   }
}