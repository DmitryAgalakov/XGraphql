using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using XGraphql.Schema.Mutations;
using XGraphql.Schema.Queries;

namespace XGraphql.Schema.Subscriptions;

public class Subscription
{
    [Subscribe]
    public CourseResult CourseCreated([EventMessage] CourseResult course) => course;

    [SubscribeAndResolve]
    public ValueTask<ISourceStream<CourseResult>> CourseUpdated(Guid courseId, [Service] ITopicEventReceiver receiver)
    {
        string topic = $"{courseId}_{nameof(Subscription.CourseUpdated)}";
        return receiver.SubscribeAsync<CourseResult>(topic);
    }
}
