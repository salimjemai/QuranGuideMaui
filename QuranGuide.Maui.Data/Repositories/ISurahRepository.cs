using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuranGuide.Maui.Core.Models;

namespace QuranGuide.Maui.Infrastructure.Repositories
{
    public interface ISurahRepository : IRepository<Surah>
    {
        Task<Surah> GetByNumberAsync(int number);
        Task<IEnumerable<Surah>> GetByRevelationTypeAsync(string revelationType);
    }

    public class SurahRepository : Repository<Surah>, ISurahRepository
    {
        public SurahRepository(QuranDbContext context) : base(context)
        {
        }

        public async Task<Surah> GetByNumberAsync(int number)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.Number == number);
        }

        public async Task<IEnumerable<Surah>> GetByRevelationTypeAsync(string revelationType)
        {
            return await _dbSet.Where(s => s.RevelationType == revelationType).ToListAsync();
        }
    }
}
