using Bogus;
using XGraphql.Models;

namespace XGraphql.Schema.Queries;

public class Query
{
    private readonly string[] _courseNames = new[]
        {
            "Introduction to Programming",
            "Advanced Mathematics",
            "Modern Physics I",
            "Chemistry Fundamentals",
            "Biology for Beginners",
            "World History",
            "Economics Principles",
            "Computer Science 101",
            "Data Structures and Algorithms",
            "Web Development"
        };

    private readonly Faker<Student> _studentFaker;
    private readonly Faker<InstructorType> _instructorFaker;
    private readonly Faker<CourseType> _courseFaker;

    public Query()
    {
        _studentFaker = new Faker<Student>()
            .RuleFor(s => s.Id, f => Guid.NewGuid())
            .RuleFor(s => s.FitstName, f => f.Name.FirstName())
            .RuleFor(s => s.LastName, f => f.Name.LastName())
            .RuleFor(s => s.GPA, f => Math.Round(f.Random.Double(2.0, 4.0), 2));

        _instructorFaker = new Faker<InstructorType>()
            .RuleFor(i => i.Id, f => Guid.NewGuid())
            .RuleFor(i => i.FirstName, f => f.Name.FirstName())
            .RuleFor(i => i.LastName, f => f.Name.LastName())
            .RuleFor(i => i.Salary, f => Math.Round(f.Random.Double(40000, 100000), 2));

        _courseFaker = new Faker<CourseType>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Name, f => f.PickRandom(_courseNames))
            .RuleFor(c => c.Subject, f => f.PickRandom<Subject>())
            .RuleFor(c => c.Instructor, f => _instructorFaker.Generate())
            .RuleFor(c => c.Students, f => _studentFaker.Generate(f.Random.Int(2, 4)));
    }

    public IEnumerable<CourseType> GetCourses()
    {
        return _courseFaker.Generate(5);
    }

    public async Task<CourseType> GetCourseByIdAsync(Guid id)
    {
        await Task.Delay(2000);

        var course = _courseFaker.Generate();
        course.Id = id;
        return course;
    }

    public string instructions => "My string!";
}
