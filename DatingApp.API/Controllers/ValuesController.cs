﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatingApp.API.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ValuesController : ControllerBase
   {
      private readonly ILogger<ValuesController> _logger;
      private readonly DataContext _context;

      public ValuesController(ILogger<ValuesController> logger, DataContext context)
      {
         _context = context;
         _logger = logger;
      }

      [HttpGet]
      public async Task<IActionResult> Get()
      {
         var values = await _context.Values.ToListAsync();
         return Ok(values);
      }

      [HttpGet("{id}")]
      public async Task<IActionResult> Get(int id)
      {
         var value = await _context.Values.FirstOrDefaultAsync(v => v.Id == id);
         return Ok(value);
      }

      [HttpPost]
      public void Post([FromBody] string value)
      {

      }

      [HttpPut]
      public void Put(int id, [FromBody] string value)
      {

      }

      [HttpDelete("{id}")]
      public void Delete(int id)
      {

      }
   }
}
