namespace CollegeApp.Model.Repositories
{
    public static class CollegeRespository
    {
        private static readonly List<Student> students = new List<Student>()
        {
            new Student() { Id = 1, StudentName = "Adekunle", Email = "studentemail@gmail.com", Address = " Hyd INDIA"},
            new Student() { Id = 2, StudentName = "Obatala", Email = "studentemail2@gmail.com", Address = "Banglore, INDIA"}
        };
        public static List<Student> GetAllStudents()
        {
            return students;
        }
        public static Student? GetStudent(int id)
        {
            return students.FirstOrDefault(x => x.Id == id);
        }
        public static Student? GetStudentByName(string name)
        {
            return students.FirstOrDefault(x => x.StudentName == name);
        }
        public static void AddStudent(Student student)
        {
            student.Id = students.Max(x => x.Id) + 1;
            students.Add(student);
        }
        public static bool DeleteStudent(int id)
        {
            var stdt = students.FirstOrDefault(x => x.Id == id);
            if (stdt != null)
            {
                students.Remove(stdt);
                return true;
            }
            else return false;         
        }
        public static Student? UpdateStudent(int id)
        {
             return students.FirstOrDefault(x => x.Id == id);
        }
    }
}
