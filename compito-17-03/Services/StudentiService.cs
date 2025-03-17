using compito_17_03.Data;
using compito_17_03.Models;
using compito_17_03.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace compito_17_03.Services
{
    public class StudentiService
    {
        private readonly Compito1703DbContext _context;

        public StudentiService(Compito1703DbContext context)
        {
            _context = context;
        }

        public async Task<StudenteViewModel> GetAllStudentiAsync()
        {
            var studenti = new StudenteViewModel();
            try
            {
                studenti.Studenti = await _context.Studenti.ToListAsync();
            }
            catch
            {
                studenti.Studenti = null;
            }

            return studenti;
        }

        public async Task<Studente> GetStudenteByIdAsync(Guid id)
        {
            var studente = await _context.Studenti.FindAsync(id);

            if (studente == null)
            {
                return null;
            }

            return studente;
        }
    }
}
