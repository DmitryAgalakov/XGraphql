namespace XGraphql.Schema.Queries
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FitstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [GraphQLName("gpa")]
        public double GPA { get; set; }
    }
}
