﻿using Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using MediatR.Pipeline;
using ModernCQRS.Commands.ProcessPayment;
using ModernCQRS.Commands.UpdatePrice;
using ModernCQRS.Configuration.Decorators;
using ModernCQRS.Configuration.Pipelines;
using ModernCQRS.Queries;

var container = ConfigureDIContainer();
using var scope = container.BeginLifetimeScope();

var mediator = scope.Resolve<IMediator>();

var commandResult = await mediator.Send(new ProcessPaymentCommand(Guid.NewGuid(), "Credit payment"));

Console.WriteLine();

await mediator.Send(new UpdatePriceCommand(30));

Console.WriteLine();

var queryResult = await mediator.Send(new GetProductsCountQuery("MyQuery"));

static IContainer ConfigureDIContainer()
{
    var builder = new ContainerBuilder();

    var configuration = MediatRConfigurationBuilder
            .Create(typeof(ProcessPaymentCommand).Assembly)
            .WithAllOpenGenericHandlerTypesRegistered()
            .Build();

    builder.RegisterMediatR(configuration);

    builder.RegisterGeneric(typeof(ValidationRequestPreProcessor<>)).As(typeof(IRequestPreProcessor<>));
    builder.RegisterGeneric(typeof(MetricsRequestPostProcessor<,>)).As(typeof(IRequestPostProcessor<,>));

    builder.RegisterGenericDecorator(typeof(UnitOfWorkCommandHandlerDecorator<>), typeof(IRequestHandler<,>));
    builder.RegisterGenericDecorator(typeof(LoggingRequestHandlerDecorator<>), typeof(IRequestHandler<,>));

    builder.RegisterGenericDecorator(typeof(UnitOfWorkCommandHandlerDecorator<,>), typeof(IRequestHandler<,>));
    builder.RegisterGenericDecorator(typeof(DiagnosticQueryHandlerDecorator<,>), typeof(IRequestHandler<,>));
    builder.RegisterGenericDecorator(typeof(LoggingRequestHandlerDecorator<,>), typeof(IRequestHandler<,>));

    return builder.Build();
}