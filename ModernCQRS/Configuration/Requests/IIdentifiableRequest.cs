using CSharpFunctionalExtensions;
using MediatR;

namespace ModernCQRS.Configuration.Requests;

/// <summary>
/// Adds unique identifier for request.
/// </summary>
internal interface IIdentifiableRequest : IRequest<Result>
{
    /// <summary>
    /// Unique identifier of request.
    /// </summary>
    Guid RequestId { get; }
}

/// <summary>
/// Adds unique identifier for request.
/// </summary>
internal interface IIdentifiableRequest<TResult> : IRequest<Result<TResult>>
{
    /// <summary>
    /// Unique identifier of request.
    /// </summary>
    Guid RequestId { get; }
}