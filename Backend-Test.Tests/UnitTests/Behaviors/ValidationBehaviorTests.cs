using BackendTest.Application.Behaviors;
using FluentAssertions;
using FluentValidation;
using MediatR;

namespace BackendTest.Test.UnitTests.Behaviors;

public class ValidationBehaviorTests
{
    [Fact]
    public async Task Handle_WithNoValidators_ShouldInvokeNext()
    {
        var behavior = new ValidationBehavior<TestRequest, string>(Enumerable.Empty<IValidator<TestRequest>>());
        var wasNextCalled = false;

        var response = await behavior.Handle(
            new TestRequest("ok"),
            () =>
            {
                wasNextCalled = true;
                return Task.FromResult("done");
            },
            CancellationToken.None);

        response.Should().Be("done");
        wasNextCalled.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WithValidRequest_ShouldInvokeNext()
    {
        var validators = new IValidator<TestRequest>[] { new TestRequestValidator() };
        var behavior = new ValidationBehavior<TestRequest, string>(validators);
        var wasNextCalled = false;

        var response = await behavior.Handle(
            new TestRequest("valid"),
            () =>
            {
                wasNextCalled = true;
                return Task.FromResult("done");
            },
            CancellationToken.None);

        response.Should().Be("done");
        wasNextCalled.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ShouldThrowValidationException()
    {
        var validators = new IValidator<TestRequest>[] { new TestRequestValidator() };
        var behavior = new ValidationBehavior<TestRequest, string>(validators);
        var wasNextCalled = false;

        Func<Task> act = async () => await behavior.Handle(
            new TestRequest(string.Empty),
            () =>
            {
                wasNextCalled = true;
                return Task.FromResult("done");
            },
            CancellationToken.None);

        var exception = await act.Should().ThrowAsync<ValidationException>();

        exception.Which.Errors.Should().Contain(error => error.PropertyName == nameof(TestRequest.Name));
        wasNextCalled.Should().BeFalse();
    }

    private sealed record TestRequest(string Name) : IRequest<string>;

    private sealed class TestRequestValidator : AbstractValidator<TestRequest>
    {
        public TestRequestValidator()
        {
            RuleFor(request => request.Name).NotEmpty();
        }
    }
}
