using MaratonValto.Models;
using MaratonValto.Models.DTOs;
using MaratonValto.Services.Library;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace MaratonValto.Services
{
    public class FutokServices(MaratonvaltoContext context, ResponseDTO responseDto) : IFutok
    {
        private readonly MaratonvaltoContext _context = context;
        private readonly ResponseDTO _responseDto = responseDto;
        public async Task<object> GetAllRunners()
        {
            try
            {
                var futok = await _context.Futoks.ToListAsync();
                _responseDto.Message = "Sikeres lekérdezés!";
                _responseDto.Result = futok;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.Result = null;
                return _responseDto;
            }
        }
    }
}
