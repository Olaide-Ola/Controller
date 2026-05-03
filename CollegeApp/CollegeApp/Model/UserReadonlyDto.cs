namespace CollegeApp.Model
{
    public class UserReadonlyDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public string UserTypeId { get; set; }
    }
}
