using CSharpFunctionalExtensions;
using MediatR;
using ModernCQRS.Configuration.Requests;

namespace ModernCQRS.Configuration.Decorators;

internal sealed class LoggingRequestHandlerDecorator<TRequest> : IRequestHandler<TRequest, Result>
        where TRequest : IIdentifiableRequest
{
    private readonly IRequestHandler<TRequest, Result> _decorated;

    public LoggingRequestHandlerDecorator(IRequestHandler<TRequest, Result> decorated)
    {
        _decorated = decorated;
    }

    public async Task<Result> Handle(TRequest request, CancellationToken cancellationToken)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine($"LoggingRequestHandlerDecorator: Request {request.RequestId} started.");

        await _decorated.Handle(request, cancellationToken);

        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine($"LoggingRequestHandlerDecorator: Request {request.RequestId} ended.");

        return Result.Success();
    }
}

internal sealed class LoggingRequestHandlerDecorator<TRequest, TResult> : IRequestHandler<TRequest, Result<TResult>>
        where TRequest : IIdentifiableRequest<TResult>
{
    private readonly IRequestHandler<TRequest, Result<TResult>> _decorated;

    public LoggingRequestHandlerDecorator(IRequestHandler<TRequest, Result<TResult>> decorated)
    {
        _decorated = decorated;
    }

    public async Task<Result<TResult>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine($"LoggingRequestHandlerDecorator: Request {request.RequestId} started.");

        var result = await _decorated.Handle(request, cancellationToken);

        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.WriteLine($"LoggingRequestHandlerDecorator: Request {request.RequestId} ended.");

        return result;
    }
}