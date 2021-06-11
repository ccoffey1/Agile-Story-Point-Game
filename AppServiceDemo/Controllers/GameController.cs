using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppServiceDemo.Controllers
{
    public class GameController : Controller
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create(string playerName, string teamName)
        {
            throw new NotImplementedException();
        }
    }
}
