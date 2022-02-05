using AutoMapper;
using ManagerApi4.Attributes;
using ManagerApi4.Context;
using ManagerApi4.Models;
using ManagerApi4.Services;
using ManagerApi4.Services.ExtensionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ManagerApi4.Controllers
{
    [Authorize]
    [Route("[controller]s")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ManagerDbContext _db;
        private readonly IMapper _mapper;
        private readonly PlayerValidator _validator;
        private readonly PlayerService _service;


        public PlayerController(ManagerDbContext db, IMapper mapper, PlayerService service)
        {
            _db = db;
            _mapper = mapper;
            _validator = new PlayerValidator();
            _service = service;

        }

        // GET: api/<PlayerController>
        [HttpGet("get")]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                var list = _service.Get();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET api/<PlayerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var model = _service.GetById(id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // GET api/<PlayerController>?<id>=2
        [HttpGet("getq")]
        public IActionResult GetQ([FromQuery] int id)
        {
            try
            {
                var model = _service.GetById(id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<PlayerController>
        [HttpPost]
        [UserCheck(UserName = "Bartu", Password = "123")]
        public IActionResult Post([FromBody] PlayerViewModel model)
        {
            try
            {
                var attr = typeof(PlayerViewModel).GetCustomAttributes(typeof(UserCheckAttribute), true).Cast<UserCheckAttribute>().FirstOrDefault();
                if (attr.UserName == model.UserName && attr.Password == model.Password)
                {

                    var validationResult = _validator.Validate(model);
                    if (!validationResult.IsValid)
                    {
                        foreach (var failure in validationResult.Errors)
                        {
                            return BadRequest(new { message = $"{failure.ErrorCode};{failure.ErrorMessage} " });
                        }
                    }
                    _service.Add(model);
                    return StatusCode(201, new { message = $"{model.Name} oluşturuldu!" });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PlayerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PlayerViewModel model)
        {
            try
            {
                model.Id = id;
                _service.Update(model);
                return Ok(new { message = "İşlem Onaylandı!" });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT api/<PlayerController>?<id>=2
        [HttpPut]
        public IActionResult PutQ([FromQuery] int id, [FromBody] PlayerViewModel model)
        {
            try
            {
                model.Id = id;
                _service.Update(model);
                return Ok(new { message = "İşlem Onaylandı!" });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/<PlayerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok(new { message = "Takım silindi" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
