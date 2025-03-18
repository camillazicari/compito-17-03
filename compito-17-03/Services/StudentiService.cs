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
            catch (Exception ex)
            {
                return false;
            }
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

        public async Task<bool> EditStudenteAsync(EditViewModel editViewModel)
        {
            try
            {
                var studente = await _context.Studenti.FindAsync(editViewModel.Id);

                if (studente == null)
                {
                    return false;
                }

                studente.Name = editViewModel.Name;
                studente.Cognome = editViewModel.Cognome;
                studente.DataNascita = editViewModel.DataNascita;
                studente.Email = editViewModel.Email;

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteStudenteByIdAsync(Guid id)
        {
            try
            {
                var product = await _context.Studenti.FindAsync(id);

                if (product == null)
                {
                    return false;
                }

                _context.Studenti.Remove(product);

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
