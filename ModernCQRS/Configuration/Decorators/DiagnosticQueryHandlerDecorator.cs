﻿using CSharpFunctionalExtensions;
using MediatR;
using ModernCQRS.Configuration.Queries;
using System.Diagnostics;

namespace ModernCQRS.Configuration.Decorators;

internal sealed class DiagnosticQueryHandlerDecorator<TRequest, TResult> : IQueryHandler<TRequest, TResult>
    where TRequest : IQuery<TResult>
{
    private readonly IRequestHandler<TRequest, Result<TResult>> _decorated;

    public DiagnosticQueryHandlerDecorator(IRequestHandler<TRequest, Result<TResult>> decorated)
    {
        _decorated = decorated;
    }

    public async Task<Result<TResult>> Handle(TRequest query, CancellationToken cancellationToken)
    {
        Console.ForegroundColor = ConsoleColor.Green;

        Stopwatch stopWatch = Stopwatch.StartNew();

        Console.WriteLine("DiagnosticQueryHandlerDecorator: Started");

        var result = await _decorated.Handle(query, cancellationToken);

        Console.ForegroundColor = ConsoleColor.Green;

        stopWatch.Stop();

        Console.WriteLine($"DiagnosticQueryHandlerDecorator: Operation completed in {stopWatch.ElapsedTicks}ms");

        return result;
    }
}