# ChainStrategy

An implementation of the Chain of Responsibility and Strategy patterns for the dotnet platform.

![TempIcon](https://i.imgur.com/LMX4jJf.png)

![build-status](https://github.com/mjbradvica/ChainStrategy/workflows/main/badge.svg) ![downloads](https://img.shields.io/nuget/dt/ChainStrategy) ![downloads](https://img.shields.io/nuget/v/ChainStrategy) ![activity](https://img.shields.io/github/last-commit/mjbradvica/ChainStrategy/master)

## Overview

The advantages of ChainStrategy are:

- :page_with_curl: Ready to go with minimal boilerplate
- :heavy_check_mark: Easy unit testing
- :arrow_down: Built with dependency injection in mind
- :seedling: Small footprint
- :books: Easy-to-learn API
- :purse: Cancellation Token support

## Table of Contents

- [ChainStrategy](#chainstrategy)
  - [Overview](#overview)
  - [Table of Contents](#table-of-contents)
  - [Dependencies](#dependencies)
  - [Installation](#installation)
  - [Setup](#setup)
  - [Chain of Responsibility](#chain-of-responsibility)
    - [Quick Start for Chain of Responsibility](#quick-start-for-chain-of-responsibility)
    - [Detailed Usage for Chain of Responsibility](#detailed-usage-for-chain-of-responsibility)
      - [Custom Payload Objects](#custom-payload-objects)
      - [Accepting Dependencies](#accepting-dependencies)
      - [Aborting A Chain](#aborting-a-chain)
      - [Using A Base Handler](#using-a-base-handler)
      - [Handler Constraints](#handler-constraints)
      - [Testing](#testing)
  - [Strategy](#strategy)
    - [Quick Start for Strategy](#quick-start-for-strategy)
    - [Detailed Usage for Strategies](#detailed-usage-for-strategies)
      - [Default Profiles](#default-profiles)
      - [Accepting Strategy Dependencies](#accepting-strategy-dependencies)
      - [Base Strategy Handlers](#base-strategy-handlers)
      - [Testing Strategy Handlers](#testing-strategy-handlers)
  - [FAQ](#faq)
    - [Do I need a Chain of Responsibility?](#do-i-need-a-chain-of-responsibility)
    - [Do I need a Strategy?](#do-i-need-a-strategy)
    - [How is either different from a Mediator?](#how-is-either-different-from-a-mediator)
    - [Can I use them together?](#can-i-use-them-together)
    - [How often can I use a Chain of Responsibility or Strategy?](#how-often-can-i-use-a-chain-of-responsibility-or-strategy)

## Dependencies

ChainStrategy has one dependency on a single [Microsoft package](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection.Abstractions) that allows for integration into the universal dependency injection container.

## Installation

The easiest way to get started is to: [Install with NuGet](https://www.nuget.org/packages/ChainStrategy/).

Install where you need with:

```bash
Install-Package ChainStrategy
```

## Setup

ChainStrategy provides a built-in method for easy Dependency Injection with any DI container that is Microsoft compatible.

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddChainStrategy(Assembly.GetExecutingAssembly());

        // Continue setup below
    }
}
```

The method also accepts params of Assemblies to register from if you need to add handlers and profiles from multiple assemblies.

```csharp
builder.Services.AddChainStrategy(Assembly.Load("FirstProject"), Assembly.Load("SecondProject"));
```

## Chain of Responsibility

### Quick Start for Chain of Responsibility

Create a payload object that inherits from the ChainPayload base class.

```csharp
public class MyChainPayload : ChainPayload
{
    public int InitialValue { get; set; }

    public int FinalValue { get; set; }
}
```

> Your payload object should contain all data necessary for a chain, including initial, temporary, and final values.

Create handlers that inherit from the ChainHandler of T, where T is the type of your payload object.

Implement the DoWork method for each handler.

```csharp
public class MyChainHandler : ChainHandler<MyChainPayload>
{
    public MyChainHandler(IChainHandler<MyChainPayload>? handler)
        : base(handler)
    {
    }

    public override Task<MyChainPayload> DoWork(MyChainPayload payload, CancellationToken cancellationToken)
    {
        payload.Value += 10;

        return Task.FromResult(payload);
    }
}
```

Create a profile for a chain that inherits from the ChainProfile of type T where T is your payload object class. Add steps in the constructor.

> These steps are executed in the order. Make sure you double check your order of operations.

```csharp
public class MyProfile : ChainProfile<MyChainPayload>
{
    public MyProfile()
    {
        AddStep<MyChainHandler>()
        .AddStep<NextStep>()
        .AddStep<FinalStep>();
    }
}
```

Start a chain by injecting an IChainFactory into a service. Call the Execute method and pass a payload object.

```csharp
public class IMyService
{
    private readonly IChainFactory _chainFactory;

    public IMyService(IChainFactory chainFactory)
    {
        _chainFactory = chainFactory;
    }

    public async Task Handle()
    {
        var result = await _chainFactory.Execute(new MyPayload());
    }
}
```

### Detailed Usage for Chain of Responsibility

#### Custom Payload Objects

You may create a custom implementation of the IChainPayload interface if you like. It only has one property that must be implemented. This property is checked by each handler before it executes. If the value is true, the chain is aborted and returned to the caller.

```csharp
public interface IChainPayload
{
    bool IsFaulted { get; }
}
```

The base ChainPayload class has two virtual methods that may be overridden, the most common use case would be if you wanted to enrich faults with more metadata.

```csharp
public abstract class MyCustomPayload : ChainPayload
{
    public DateTime FaultedAt { get; private set; }

    public override void Faulted(Exception exception)
    {
        FaultedAt = DateTime.UtcNow;
        base.Faulted(exception);
    }
}
```

All of your payloads would now inherit from your new base payload class.

#### Accepting Dependencies

ChainStrategy is built for dependency injection. You may inject any dependency you need into the constructor.

```csharp
public class MyChainHandler : ChainHandler<MyChainPayload>
{
    private readonly IMyDataSource _data;

    public MyChainHandler(IChainHandler<MyChainPayload>? handler, IMyDataSource data)
        : base(handler)
    {
        _data = data;
    }

    public override async Task<MyChainPayload> DoWork(MyChainPayload payload, CancellationToken cancellationToken)
    {
        var myData = await _data.GetData();

        payload.Value = myData;

        return payload;
    }
}
```

You may start another chain or strategy from inside a chain handler. Inject the appropriate factory and execute a payload or request.

```csharp
public class MyChainHandler : ChainHandler<MyChainPayload>
{
    private readonly IStrategyFactory _strategyFactory;

    public MyChainHandler(IChainHandler<MyChainPayload>? handler, IStrategyFactory strategyFactory)
        : base(handler)
    {
        _strategyFactory = strategyFactory;
    }

    public override async Task<MyChainPayload> DoWork(MyChainPayload payload, CancellationToken cancellationToken)
    {
        var strategyResult = await _strategyFactory.Execute(new StrategyRequest(payload));

        payload.Value = strategyResult;

        return payload;
    }
}
```

#### Aborting A Chain

There may be conditions where your chain faults or must return early. There is a built-in way of returning a payload to the originator to avoid finishing the entire chain.

```csharp
public class MyChainHandler : ChainHandler<MyChainPayload>
{
    private readonly IMyDataSource _data;

    public MyChainHandler(IChainHandler<MyChainPayload>? handler, IMyDataSource data)
        : base(handler)
    {
        _data = data;
    }

    public override async Task<MyChainPayload> DoWork(MyChainPayload payload, CancellationToken cancellationToken)
    {
        try
        {
            var myData = await _data.GetData();

            payload.Value = myData;
        }
        catch
        {
            payload.Faulted();
        }

        return payload;
    }
}
```

You may also pass an exception to the Faulted method if you'd like to log the object.

```csharp
    catch (Exception exception)
    {
        payload.Faulted(exception);
    }
```

#### Using A Base Handler

If you find yourself repeating yourself in multiple handlers you may create a base handler to accomplish common tasks.

The example shows an abstract handler that will override the Middleware method. Middleware just calls DoWork under the hood.

```csharp
public abstract class SampleLoggingHandler<T> : ChainHandler<T>
    where T : ChainPayload
{
    protected SampleLoggingHandler(IChainHandler<T>? handler)
        : base(handler)
    {
    }

    public override async Task<T> Middleware(T payload, CancellationToken cancellationToken)
    {
        try
        {
            return await base.Middleware(payload, cancellationToken);
        }
        catch (Exception exception)
        {
            payload.Faulted(exception);

            return payload;
        }
    }
}
```

Your handlers that need to use this can simply inherit from this class instead.

```csharp
public class MyChainHandler : SampleLoggingHandler<MyChainPayload>
{
    public MyChainHandler(IChainHandler<MyChainPayload>? handler)
        : base(handler)
        {
        }

    public override async Task<MyChainPayload> DoWork(MyChainPayload payload, CancellationToken cancellationToken)
    {
        // implement and return payload.
    }
}
```

> When adding steps in your profile, make sure you are using the correct handler. Accidentally adding an abstract base handler will throw an exception.

#### Handler Constraints

You may reuse a handler in multiple chains by constraining the payload type via an interface.

> The interface needs to inherit from the "IChainPayload" interface even if you rely on the default implementation.

```csharp
public interface IData : IChainPayload
{
    Guid Id { get; }

    void UpdateData(MyData data);
}
```

```csharp
public class MyChainPayload : ChainPayload, IData
{
    // implement properties and methods
}
```

Add the constraint handler and implement the interface accordingly.

> Constrained handlers need to be abstract classes which utilize the generic constraint.

```csharp
public abstract class MyConstrainedHandler<T> : ChainHandler<T>
    where T : IData
{
    protected MyConstrainedHandler(IChainHandler<T>? successor)
        : base(successor)
        {
        }

    public override Task<T> DoWork(T payload, CancellationToken cancellationToken)
    {
        if (payload.id == Guid.Empty)
        {
            payload.UpdateId(id);
        }

        return Task.FromResult(payload);
    }
}
```

Your concrete handler only needs to derive from the constrained base.

```csharp
public class MyHandler : MyConstrainedHandler<MyChainPayload>
{
    public MyHandler(IChainHandler<MyPayload>? handler)
        : base(handler)
        {
        }
}
```

#### Testing

Testing a chain handler is no different than unit testing any other class or method.

```csharp
[TestClass]
public class MyHandlerTests
{
    [TestMethod]
    public async Task MyHandleWorks()
    {
        var handler = new MyHandler(null);

        var result = await handler.Handle(new MyPayload(), CancellationToken.None);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public async Task WithDependency()
    {
        var mock = new Mock<IMyDependency>();
        mock.Setup(x => x.MyMethod()).ReturnsAsync(new MyExpectedReturn());

        var handler = new MyHandler(null, mock.Object);

        var result = await handler.Handle(new MyPayload(), CancellationToken.None);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public async Task ServiceTestForFactory()
    {
        var mock = new Mock<IChainFactory<MyPayload>>();
        mock.Setup(x => x.Execute(It.IsAny<MyPayload>(), CancellationToken.None))
            .ReturnsAsync(new MyPayload());

        var service = new MyService(mock.Object);

        var serviceResult = await service.DoSomething();

        Assert.AreEqual(expected, serviceResult);
    }
}
```

## Strategy

### Quick Start for Strategy

Unlike chains, strategies use both a request and response object.

```csharp
public class MyResponse
{
    public int MyResult { get; set; }
}
```

Request objects will implement the IStrategyRequest interface of type T where T is your response type.

```csharp
public class MyRequest : IStrategyRequest<MyResponse>
{
    // properties in here
}
```

If your strategy has no return type, use the non-generic version of the IStrategyRequest interface.

```csharp
public class MyRequest : IStrategyRequest
{
}
```

Implement a handler by inheriting from the IStrategyHandler of type TRequest, TResponse where TRequest is your request type, and TResponse is your response type.

Implement the Handle method as required.

```csharp
public class MyStrategyHandler : IStrategyHandler<MyRequest, MyResponse>
{
    public async Task<MyResponse> Handle(MyRequest request, CancellationToken cancellationToken)
    {
        // implement and return response
    }
}
```

If your request object does not have a return type, the Nothing class will be used instead. Nothing as the name states, is a substitute for void.

```csharp
public class MyStrategyHandler : IStrategyHandler<MyRequest>
{
    public async Task<Nothing> Handle(MyRequest request, CancellationToken cancellationToken)
    {
        // implement and return response
    }
}
```

> All Strategy handlers must have a public or default constructor to be initialized properly.

Profiles are very similar to chains except you are defining conditions instead of steps.

You define a strategy by giving it a predicate based on your request object properties.

> Note: These are executed in order so put your most constrained definitions first.

```csharp
public class MyStrategyProfile : StrategyProfile<MyRequest, MyResponse>
{
    public MyStrategyProfile()
    {
        AddStrategy<MySecondHandler>(request => request.Value == 0);
        AddStrategy<MyFirstHandler>(request => request.Value > 10);
    }
}
```

Strategies follow the same pattern as chains, inject the factory into the class you want to use it in. Call the ExecuteStrategy method when required.

```csharp
public class MyService
{
    private readonly IStrategyFactory _strategyFactory;

    public MyService(IStrategyFactory strategyFactory)
    {
        _strategyFactory = strategyFactory;
    }

    public async Task Handle()
    {
        var result = await _strategyFactory.Execute(new MyRequest());
    }
}
```

### Detailed Usage for Strategies

#### Default Profiles

A profile may have a Default handler if no condition is satisfied.

```csharp
public class MyStrategyProfile : StrategyProfile<MyRequest, MyResponse>
{
    public MyStrategyProfile()
    {
        AddStrategy<MySecondHandler>(request => request.Value == 0);
        AddStrategy<MyFirstHandler>(request => request.Value > 10);
        AddDefault<MyFirstHandler>();
    }
}
```

> You may only have one default handler, calling the method twice will just overwrite the previous one.

#### Accepting Strategy Dependencies

You may use dependency injection for any other dependencies like normal.

```csharp
public class MyStrategyHandler : IStrategyHandler<MyRequest, MyResponse>
{
    private readonly IMyDependency _dependency;

    public MyStrategyHandler(IMyDependency dependency)
    {
        _dependency = dependency;
    }

    public async Task<MyResponse> Handle(MyRequest request, CancellationToken cancellationToken)
    {
        // implement and return response
    }
}
```

Similar to chains, you may start another chain or strategy inside of an existing handler.

```csharp
public class MyStrategyHandler : IStrategyHandler<MyRequest, MyResponse>
{
    private readonly IChainFactory _chainFactory;

    public MyStrategyHandler(IChainFactory chainFactory)
    {
        _chainFactory = chainFactory;
    }

    public async Task<MyResponse> Handle(MyRequest request, CancellationToken cancellationToken)
    {
        var chainResult = await _chainFactory.Execute(new MyChainPayload(request));

        return new MyResponse(chainResult);
    }
}
```

#### Base Strategy Handlers

Similar to chains, you may have a base handler to share common logic. This example shows wrapping logic in a try-catch.

```csharp
public abstract class SampleStrategyLoggingHandler<TRequest, TResponse> : IStrategyHandler<TRequest, TResponse>
    where TRequest : IStrategyRequest<TResponse>
    where TResponse : new()
{
    private readonly ILogger _logger;

    protected SampleStrategyLoggingHandler(ILogger logger)
    {
        _logger = logger;
    }

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await DoWork(request, cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.Error(exception, $"An exception occurred at {DateTime.UtcNow} in the {GetType().Name} handler.");
        }

        return new TResponse();
    }

    public abstract Task<TResponse> DoWork(TRequest request, CancellationToken cancellationToken);
}
```

Any handler would inherit from this and implement the DoWork function instead of Handle.

#### Testing Strategy Handlers

Testing strategy handlers is straightforward.

```csharp
[TestClass]
public class StrategyTests
{
    [TestMethod]
    public async Task Strategy_IsCorrect()
    {
        var strategy = new MyStrategyHandler();

        var result = await strategy.Execute(new MyStrategyRequest());

        Assert.Equal(expected, result);
    }

    [TestMethod]
    public async Task Strategy_WithDependency_IsCorrect()
    {
        var mock = new Mock<IDependency>();
        mock.Setup(x => x.Something()).ReturnsAsync(expectedObject);

        var strategy = new MyStrategyHandler(mock.Object);

        var result = await strategy.Execute(new MyStrategyRequest());

        Assert.Equal(expected, result);
    }

    [TestMethod]
    public async Task MockingFactory_FromService_IsCorrect()
    {
        var mock = new Mock<IStrategyFactory>();
        mock.Setup(x => x.Execute(It.IsAny<MyStrategyRequest>, CancellationToken.None))
            .ReturnsAsync(new MyStrategyResponse());

        var service = new ServiceWithFactory(mock.Object);

        var result = await service.HandleRequest(new MyRequest());

        Assert.Equal(expected, result);
    }
}
```

## FAQ

### Do I need a Chain of Responsibility?

Do you have a complex process that can be broken up into multiple steps to enable easier development and testing?

### Do I need a Strategy?

Do you have a common input/output interface that may use different implementations depending on a condition?

It is best to think of a Strategy as a complex switch statement where each switch case may be a long-lived, complex process.

(A common example is having to process credit cards with different payment providers.)

### How is either different from a Mediator?

A Mediator is a One-To-One relationship between a request and a response with a single handler per request.

A Chain of Responsibility is a One-To-Many relationship with multiple handlers per request in a specific order.

A Strategy is a One-To-Many relationship with a single handler chosen depending on a predicate.

### Can I use them together?

Yes! You can use any or all three in conjunction. None of them are mutually exclusive.

### How often can I use a Chain of Responsibility or Strategy?

A Chain of Responsibility is a **medium usage** pattern. It is best used when you need to break a problem down into smaller easier-to-test chunks.

A Strategy is a **low usage** pattern. It is best used when you need to have multiple implementations of an algorithm that uses the same interface.
