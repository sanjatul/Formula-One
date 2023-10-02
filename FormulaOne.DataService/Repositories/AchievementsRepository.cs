using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.DataService.Repositories
{
    public class AchievementsRepository : GenericRepository<Achievement>, IAchievementsRepository
    {
        public AchievementsRepository(ILogger logger, AppDbContext context) : base(logger, context)
        {
        }

        public async Task<Achievement?> GetAchievementAsync(Guid driverId)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(x => x.Id == driverId);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} GetAchievementAsync function error", typeof(AchievementsRepository));
                throw;
            }
        }


        public override async Task<IEnumerable<Achievement>> All()
        {
            try
            {
                return await _dbSet.Where(x => x.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.AddedDate)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} All function error", typeof(AchievementsRepository));
                throw;
            }
        }
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                //get entity
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
                if (result == null) return false;
                result.Status = 0;
                result.UpdatedDate = DateTime.Now;
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} Delete function error", typeof(AchievementsRepository));
                throw;
            }

        }

        public override async Task<bool> Update(Achievement achievement)
        {
            try
            {
                //get entity
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == achievement.Id);
                if (result == null) return false;
                result.UpdatedDate = DateTime.Now;
                result.FastestLab = achievement.FastestLab;
                result.PolePosition = achievement.PolePosition;
                result.RaceWins = achievement.RaceWins;
                result.WorldChaimpionship = achievement.WorldChaimpionship;
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} Update function error", typeof(AchievementsRepository));
                throw;
            }

        }


    }
}
