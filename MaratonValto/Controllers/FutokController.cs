using MaratonValto.Models;
using MaratonValto.Services.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace MaratonValto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FutokController : ControllerBase
    {
    private readonly MaratonvaltoContext _context;
    private readonly IFutok _futok;

    public FutokController(MaratonvaltoContext context, IFutok futok)
    {
        _context = context;
        _futok = futok;
    }
    //Futok adatainak lekérérse
    [HttpGet]
        public async Task<ActionResult> GetAllRunner()
        {
            try
            {
                var requestResult = await _futok.GetAllRunners();
                return Ok(requestResult);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    message = ex.Message
                });
            }
        }
    }
}
