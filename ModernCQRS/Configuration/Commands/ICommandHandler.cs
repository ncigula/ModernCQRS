using CSharpFunctionalExtensions;
using MediatR;

namespace ModernCQRS.Configuration.Commands;

/// <summary>
/// Base interface, for all command handlers.
/// </summary>
/// <typeparam name="TCommand">Type of the command, that will be handled.</typeparam>
internal interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : ICommand
{
}

/// <summary>
/// <inheritdoc cref="ICommandHandler{TCommand}"/>
/// </summary>
/// <typeparam name="TCommand"><inheritdoc cref="ICommandHandler{TCommand}" path="/typeparam"/></typeparam>
/// <typeparam name="TResult">Type of the object, that will be returned as command execution result.</typeparam>
internal interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>>
    where TCommand : ICommand<TResult>
{
}