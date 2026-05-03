using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data.Repository
{
    public class StudentRepository : CollegeRepository<Student>,  IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;
        public StudentRepository(CollegeDBContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public Task<List<Student>> GetStudentsByFeeStatusAsync(int feeStatus)
        {
            //write your logic here to return the status fee
            return null;
        }



        /*public async Task<int> CreateAsync(Student student)
        {

             _dbContext.Add(student);
            await _dbContext.SaveChangesAsync();
            return student.Id;
        }
        public async Task<bool> DeleteAsync(Student student)
        {

            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            var student = await _dbContext.Students.AsNoTracking().ToListAsync();
            return student;
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _dbContext.Students.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Student> GetByNameAsync(string name)
        {
            return await _dbContext.Students.AsNoTracking().SingleOrDefaultAsync(s => s.StudentName.ToLower().Equals(name.ToLower()));
        }

        public async Task<int> UpdateAsync(Student student)
        {
            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync();
            return student.Id;
        }*/
    }
}
