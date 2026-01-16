using MaratonValto.Models;
using MaratonValto.Models.DTOs;
using MaratonValto.Services.Library;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("{id}/results")]
        public async Task<ActionResult> GetRunnerResults(int id)
        {
            try
            {
                var requestResult = await _futok.GetRunnerResults(id);
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
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRunner(int id, FutoDTO futo)
        {
            try
            {
                var requestResult = await _futok.UpdateRunner(id, futo);
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