using AutoMapper;
using ManagerApi4.Attributes;
using ManagerApi4.Context;
using ManagerApi4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagerApi4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ManagerApi4.Controllers
{
    [Route("[controller]s/[action]")]
    [ApiController]
    
    public class TeamController : ControllerBase
    {
        private readonly ManagerDbContext _db;
        private readonly IMapper _mapper;
        private readonly TeamService service;


        public TeamController(ManagerDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            service = new TeamService(db, mapper);
        }


        // GET: api/<TeamController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var list = service.Get();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var model = service.GetById(id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET api/<TeamController>?<id>=2
        [HttpGet]
        public IActionResult GetQ([FromQuery] int id)
        {
            try
            {
                var model = service.GetById(id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<TeamController>
        [HttpPost]
        public IActionResult Post([FromBody] TeamViewModel model)
        {
            try
            {
                var attr = typeof(TeamViewModel).GetCustomAttributes(typeof(UserCheckAttribute), true).Cast<UserCheckAttribute>().FirstOrDefault();
                if (attr.UserName == model.UserName && attr.Password == model.Password)
                {
                    service.Add(model);
                    return StatusCode(201, new { message = $"{model.Name} oluşturuldu!" });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<TeamController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TeamViewModel model)
        {
            try
            {
                model.Id = id;
                service.Update(model);
              
                return Ok(new { message = "İşlem Onaylandı!" });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT api/<TeamController>?<id>=2
        [HttpPut]
        public IActionResult PutQ([FromQuery] int id, [FromBody] TeamViewModel model)
        {
            try
            {
                model.Id = id;
                service.Update(model);
               
                return Ok(new { message = "İşlem Onaylandı!" });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                service.Delete(id);
              
                return Ok(new { message = "Takım silindi" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
