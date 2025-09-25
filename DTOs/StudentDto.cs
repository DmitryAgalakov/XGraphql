using XGraphql.Models;

namespace XGraphql.DTOs;

public class StudentDto
{
    public Guid Id { get; set; }
    public string FitstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public double GPA { get; set; }
    public IEnumerable<CourseDto> Courses { get; set; }
}
