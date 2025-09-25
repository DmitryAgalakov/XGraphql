using HotChocolate.Subscriptions;
using XGraphql.Schema.Queries;
using XGraphql.Schema.Subscriptions;

namespace XGraphql.Schema.Mutations;

public class Mutation
{
    private readonly List<CourseResult> _cources;

    public Mutation()
    {
        _cources = new();
    }

    public async Task<CourseResult> CreateCourse(CourseInputType input, [Service] ITopicEventSender sender)
    {
        var course = new CourseResult
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            Subject = input.Subject,
            InstructorId = input.InstructorId,
        };

        _cources.Add(course);
        await sender.SendAsync(nameof(Subscription.CourseCreated), course);

        return course;
    }


    public async Task<CourseResult> UpdateCourse(Guid id, CourseInputType input, [Service] ITopicEventSender sender)
    {
        var course = _cources.FirstOrDefault((c) => c.Id == id) ?? throw new Exception("Course not found.");

        course.Name = input.Name;
        course.Subject = input.Subject;
        course.InstructorId = input.InstructorId;

        string topic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
        await sender.SendAsync(topic, course);

        return course;
    }

    public bool DeleteCourse(Guid id)
    {
        return _cources.RemoveAll(c => c.Id == id) >= 1;
    }


}
