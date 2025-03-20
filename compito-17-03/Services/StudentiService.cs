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

        private async Task<bool> SaveAsync()
        {
            try
            {
                var rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public async Task<StudenteViewModel> GetAllStudentiAsync()
        {
            var studenti = new StudenteViewModel();
            try
            {
                studenti.Studenti = await _context.ApplicationUsers.ToListAsync();

            }
            catch
            {
                studenti.Studenti = null;
            }

            return studenti;
        }

        public async Task<ApplicationUser> GetStudenteByIdAsync(string id)
        {
            var studente = await _context.ApplicationUsers.FindAsync(id);

            if (studente == null)
            {
                return null;
            }

            return studente;
        }

        public async Task<bool> EditStudenteAsync(EditViewModel editViewModel)
        {
            try
            {
                var studente = await _context.ApplicationUsers.FindAsync(editViewModel.Id);

                if (studente == null)
                {
                    return false;
                }

                studente.Nome = editViewModel.Name;
                studente.Cognome = editViewModel.Cognome;
                studente.DataNascita = editViewModel.DataNascita;
                studente.Email = editViewModel.Email;

                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteStudenteByIdAsync(string id)
        {
            try
            {
                var user = await _context.ApplicationUsers.FindAsync(id);

                if (user == null)
                {
                    return false;
                }

                _context.ApplicationUsers.Remove(user);

                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}
