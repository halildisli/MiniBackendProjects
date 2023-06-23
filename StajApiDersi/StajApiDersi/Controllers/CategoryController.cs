using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StajApiDersi.Models.Concrete;
using StajApiDersi.Repositories.Abstract;
using System.Net;

namespace StajApiDersi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryRepository _cr;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _cr = categoryRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_cr.GetAll());
            //return Ok(_cr.GetAll());
        }
        [HttpPost]
        public IActionResult Post(Category item)
        {
            bool result=_cr.Add(item);
            return Ok(_cr.GetAll());
        }
        [HttpPut]
        public IActionResult Put(Category item)
        {
            bool result=_cr.Edit(item);
            if (result == true)
            {
                item.ModifiedDate = DateTime.Now;
                return Ok(_cr.GetById(item.ID));
            }
            else
            {
                return BadRequest(_cr.GetById(item.ID));
            }
        }
        [HttpDelete]
        public IActionResult Delete(Category item)
        {
            _cr.Remove(item);
            return Ok();
        }
    }
}
