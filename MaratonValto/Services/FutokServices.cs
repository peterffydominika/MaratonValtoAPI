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
        public async Task<object> GetRunnerResults(int futoId)
        {
            try
            {
                var eredmenyek = await _context.Eredmenyeks
                    .Where(e => e.Futo == futoId)
                    .OrderBy(e => e.Kor)
                    .ToListAsync();

                _responseDto.Message = "Sikeres lekérdezés!";
                _responseDto.Result = eredmenyek;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.Result = null;
                return _responseDto;
            }
        }
        public async Task<object> UpdateRunner(int id, FutoDTO futo)
        {
            try
            {
                var existing = await _context.Futoks.FindAsync(id);
                if (existing == null)
                {
                    _responseDto.Message = "A futó nem található.";
                    _responseDto.Result = null;
                    return _responseDto;
                }
                existing.Fnev = futo.Fnev;
                existing.Szulev = futo.Szulev;
                existing.Szulho = futo.Szulho;
                existing.Csapat = futo.Csapat;
                existing.Ffi = futo.Ffi;

                await _context.SaveChangesAsync();

                _responseDto.Message = "Sikeres módosítás!";
                _responseDto.Result = existing;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.Result = null;
                return _responseDto;
            }
        }
        public async Task<object> DeleteRunner(int id)
        {
            try
            {
                var existing = await _context.Futoks.FindAsync(id);
                if (existing == null)
                {
                    _responseDto.Message = "A futó nem található.";
                    _responseDto.Result = null;
                    return _responseDto;
                }

                var relatedResults = await _context.Eredmenyeks
                    .Where(e => e.Futo == id)
                    .ToListAsync();

                if (relatedResults.Any())
                {
                    _context.Eredmenyeks.RemoveRange(relatedResults);
                }

                _context.Futoks.Remove(existing);
                await _context.SaveChangesAsync();

                _responseDto.Message = "Sikeres törlés!";
                _responseDto.Result = existing;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.Result = null;
                return _responseDto;
            }
        }
        public async Task<object> AddResult(int futoId, EredmenyDTO eredmeny)
        {
            try
            {
                var runner = await _context.Futoks.FindAsync(futoId);
                if (runner == null)
                {
                    _responseDto.Message = "A futó nem található.";
                    _responseDto.Result = null;
                    return _responseDto;
                }

                var newEredmeny = new Eredmenyek
                {
                    Futo = futoId,
                    Kor = eredmeny.Kor,
                    Ido = eredmeny.Ido
                };

                _context.Eredmenyeks.Add(newEredmeny);
                await _context.SaveChangesAsync();

                _responseDto.Message = "Eredmény sikeresen rögzítve!";
                _responseDto.Result = newEredmeny;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.Result = null;
                return _responseDto;
            }
        }
        public async Task<object> GetFemaleRunners()
        {
            try
            {
                var females = await _context.Futoks
                    .Where(f => f.Ffi == false)
                    .OrderBy(f => f.Fnev)
                    .Select(f => new { f.Fnev, f.Szulev })
                    .ToListAsync();

                _responseDto.Message = "Sikeres lekérdezés!";
                _responseDto.Result = females;
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