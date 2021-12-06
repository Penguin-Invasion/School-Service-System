namespace SchoolServiceSystem.DTOs.School
{
    public class CreateUserDTO
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}