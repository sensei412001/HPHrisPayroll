using System.IO;
using HPHrisPayroll.API.Helper;
using Microsoft.AspNetCore.Mvc;

namespace HPHrisPayroll.API.Controllers
{
    public class FallbackController: ControllerBase
    {
        public IActionResult Index()
        {            
            CurrentDirectoryHelpers.SetCurrentDirectory(); // call it here
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot","index.html"), "text/HTML");
        }
    }
}