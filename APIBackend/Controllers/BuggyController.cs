using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIBackend.Error;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace APIBackend.Controllers
{
    public class BuggyController : BaseAPIController
    {
        public StoreContext StoreContext { get; }
        public BuggyController(StoreContext storeContext)
        {
            this.StoreContext = storeContext;
        }
        [HttpGet("NotFound")]
        public ActionResult GetNotFoundRequest(){
            var thing=StoreContext.products.Find(45);
            if(thing==null)
            {
                return NotFound(new APIResponce(404));
            }
            return Ok();
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError(){
            var thing=StoreContext.products.Find(405);
            var thingtoreturn=thing.ToString();
            return Ok();
        }
         [HttpGet("badrequest")]
        public ActionResult getBadRequest(){
            return BadRequest(new APIResponce(400));
        }
         [HttpGet("badrequest/{id}")]
        public ActionResult getBadreqeust(int id){
            return Ok();
        }

    }
}