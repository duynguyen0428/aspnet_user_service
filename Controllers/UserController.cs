using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controllers
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            
        }
    }
}