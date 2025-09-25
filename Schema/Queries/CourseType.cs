using XGraphql.Models;

namespace XGraphql.Schema.Queries;

public class CourseType
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Subject Subject { get; set; }
    [GraphQLNonNullType]
    public InstructorType Instructor { get; set; }
    public IEnumerable<Student> Students { get; set; }
}
