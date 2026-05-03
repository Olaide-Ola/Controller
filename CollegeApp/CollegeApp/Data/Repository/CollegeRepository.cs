using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CollegeApp.Data.Repository
{
    public class CollegeRepository<T> : ICollegeRepository<T> where T : class //this is the generic repository
    {
        private readonly CollegeDBContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public CollegeRepository(CollegeDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<T> CreateAsync(T dbRecord)
        {
            
            await _dbSet.AddAsync(dbRecord);
            await _dbContext.SaveChangesAsync();
            return dbRecord;
        }
        public async Task<bool> DeleteAsync(T dbRecord)
        {
            _dbSet.Remove(dbRecord);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<T>> GetAllAsync()
        {           
            return await _dbSet.AsNoTracking().ToListAsync();        
        }
        public async Task<T> GetAsync(Expression<Func<T,bool>> filter) //check this signature
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync(filter);
        }
        public async Task<List<T>> GetAllByFilterAsync(Expression<Func<T,bool>> filter) //This bring out list of object. Just verified it shaa
        {
            return await _dbSet.AsNoTracking().Where(filter).ToListAsync();
        }
       
        //public async Task<T> GetByNameAsync(Expression<Func<T,bool>> filter)
        //{
        //    return await _dbSet.AsNoTracking().SingleOrDefaultAsync(filter);
        //}
        public async Task<T> UpdateAsync(T dbRecord)
        {
            
            _dbSet.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
            return dbRecord;
        }
    }
}
