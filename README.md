# ChainStrategy

![build-status](https://github.com/mjbradvica/ChainStrategy/workflows/main/badge.svg) ![downloads](https://img.shields.io/nuget/dt/ChainStrategy) ![downloads](https://img.shields.io/nuget/v/ChainStrategy) ![activity](https://img.shields.io/github/last-commit/mjbradvica/ChainStrategy/master)

## Table of Contents

* [Overview](#overview)
* [Installation](#installation)
* [Setup](#setup)
* [Quick Start](#quick-start)
* [Detailed Chain of Responsibility](#chain-of-responsibility)
* [Detailed Strategy](#strategy)
* [FAQ](#faq)

## Overview

An implementation of the Chain of Responsibility and Strategy patterns for .NET.

The advantages of ChainStrategy are:

* Ready to go with minimal boilerplate
* Easy unit testing
* Build with dependency injection in mind
* Small footprint
* Easy-to-learn, minimal API

## Installation

The easiest way is [install with NuGet](https://www.nuget.org/).

Install where you need with:

```bash
Install-Package ChainStrategy
```

## Setup

ChainStrategy provides a built-in method for easy DependencyInjection with any DI container that is Microsoft compatible.

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

The method also accepts a params of Assemblies to register from if you need to add handlers and profiles from multiple assemblies.

```csharp
builder.Services.AddChainStrategy(Assembly.Load("FirstProject"), Assembly.Load("SecondProject"));
```

### Quick Start

#### Quick Chain of Responsibility

Create a request object that inherits from the ChainRequest base class.

```csharp
public class MyChainRequest : ChainRequest
{
    public int Value { get; set; }

    // potentially lots of properties here.
}
```

Create handlers that inherit from the ChainHandler of T, where T is the type of the your request object.

Implement the DoWork method for each handler.

```csharp
public class MyChainHandler : ChainHandler<MyChainRequest>
{
    public MyChainHandler(IChainHandler<MyChainRequest>? handler)
        : base(handler)
    {
    }

    public override Task<MyChainRequest> DoWork(MyChainRequest request)
    {
        request.Value += 10;

        return Task.FromResult(request);
    }
}
```

Create a profile for a chain that inherits the ChainProfile class. Add steps in the constructor.

```csharp
public class MyProfile : ChainProfile<MyChainRequest>
{
    public MyProfile()
    {
        AddStep<MyChainHandler>()
        .AddStep<NextStep>()
        .AddStep<FinalStep>();
    }
}
```

Start a chain by injecting an IChainFactory of type T into a service. Call the Execute method and pass a request object.

```csharp
public class IMyService
{
    private readonly IChainFactory<MyRequest> _chainFactory;

    public IMyService(IChainFactory<MyRequest> chainFactory)
    {
        _chainFactory = chainFactory;
    }

    public async Task Handle()
    {
        var result = await _chainFactory.Execute(new MyRequest());
    }
}
```

#### Quick Strategy

Create a request and response object for a strategy. The request object must inherit from the IStrategyRequest object of type T, where T is the type of the response object.

```csharp
public class MyResponse
{
    public int MyResult { get; set; }
}

public class MyRequest : IStrategyRequest<MyResponse>
{
    // properties in here
}
```

Create any handlers required by inheriting from the IStrategyHandler of T and K. Where T is the type of the request object and K is the type of the response object.

```csharp
public class MyStrategyHandler : IStrategyHandler<MyRequest, MyResponse>
{
    public async Task<MyResponse> Handle(MyRequest request)
    {
        // implement and return response
    }
}
```

Create a profile by inheriting from the StrategyProfile of type T and K. Where T is the type of the request object and K is the type of the response object.

Add predicate conditions for each handler. Use the AddDefault for a default.

```csharp
public class MyStrategyProfile : StrategyProfile<MyRequest, MyResponse>
{
    public MyStrategyProfile()
    {
        AddStrategy<MyFirstHandler>(request => request.Value > 10);
        AddStrategy<MySecondHandler>(request => request.Value == 0);
        AddDefault<MyDefaultHandler>();
    }
}
```

Start a strategy by injecting an IStrategyFactory of type T and K. Where T is the type of the request object and K is the type of the response object.

Call the Execute method and pass a request object.

```csharp
public class MyService
{
    private readonly IStrategyFactory<MyRequest, MyResponse> _strategyFactory;

    public MyService(IStrategyFactory<MyRequest, MyResponse> strategyFactory)
    {
        _strategyFactory = strategyFactory;
    }

    public async Task Handle()
    {
        var result = await _strategyFactory.Execute(new MyRequest());
    }
}
```

### Chain of Responsibility

#### Chain Request

All Chains revolve around a common request object that is used for both input and output. Any state you need to store for the duration of the chain should be contained in the object.

```csharp
public class MyChainRequest : ChainRequest
{
    public int Value { get; set; }

    // potentially lots of properties here.
}
```

> All chain request objects must inherit from the IChainRequest interface. You may implement your own. However it is commonly recommended to use the ChainRequest base object most of the time.

All Chain handlers follow a similar method. Create a class and inherit from ChainHandler of type T where T is your request type.

You must implement the "DoWork" method for each handler.

```csharp
public class MyChainHandler : ChainHandler<MyChainRequest>
{
    public MyChainHandler(IChainHandler<MyChainRequest>? handler)
        : base(handler)
    {
    }

    public override Task<MyChainRequest> DoWork(MyChainRequest request)
    {
        request.Value += 10;

        return Task.FromResult(request);
    }
}
```

> A public constructor that accepts a sibling chain handler is required.

#### Accepting Dependencies

ChainStrategy is built with dependency injection in mind. You may inject any dependency you need into the constructor.

```csharp
public class MyChainHandler : ChainHandler<MyChainRequest>
{
    private readonly IMyDataSource _data;

    public MyChainHandler(IChainHandler<MyChainRequest>? handler, IMyDataSource data)
    : base(handler)
    {
        _data = data;
    }

    public override async Task<MyChainRequest> DoWork(MyChainRequest request)
    {
        var myData = await _data.GetData();

        request.Value = myData;

        return request;
    }
}
```

#### Aborting A Chain

There may be conditions where your chain faults or must return early. There is a built-in way of returning a request back to the originator to avoid finishing the entire chain.

```csharp
public class MyChainHandler : ChainHandler<MyChainRequest>
{
    private readonly IMyDataSource _data;

    public MyChainHandler(IChainHandler<MyChainRequest>? handler, IMyDataSource data)
    : base(handler)
    {
        _data = data;
    }

    public override async Task<MyChainRequest> DoWork(MyChainRequest request)
    {
        try
        {
            var myData = await _data.GetData();

            request.Value = myData;
        }
        catch
        {
            request.Faulted();
        }

        return request;
    }
}
```

You may also pass an exception to the Faulted method if you'd like to log it.

```csharp
    catch (Exception exception)
    {
        request.Faulted(exception);
    }
```

#### Using A Base Handler

If you find yourself repeating yourself in multiple handlers you may create your own base handler to accomplish common tasks.

The example shows an abstract handler that will override the Middleware method. Middleware just calls DoWork under the hood.

```csharp
public abstract class SampleLoggingHandler<T> : ChainHandler<T>
    where T : ChainRequest
{
    protected SampleLoggingHandler(IChainHandler<T>? handler)
        : base(handler)
    {
    }

    public override Task<T> Middleware(T request)
    {
        try
        {
            return base.Middleware(request);
        }
        catch (Exception exception)
        {
            request.Faulted(exception);

            return Task.FromResult(request);
        }
    }
}
```

Your handlers that need to use this can simply inherit from this class instead.

```csharp
public class MyChainHandler : SampleLoggingHandler<MyChainRequest>
{
    // everything ele is the same as above.
}
```

#### Building A Profile

ChainStrategy uses Profiles to define what steps you want to use and in what order to use them.

```csharp
public class MyProfile : ChainProfile<MyChainRequest>
{
    public MyProfile()
    {
        AddStep<MyChainHandler>()
        .AddStep<NextStep>()
        .AddStep<FinalStep>();
    }
}
```

The library will execute each step in the order you define them.

> Do not put conditional logic in a profile. That kind of logic belongs in handlers.

#### Usage

Simply inject a IChainFactory of type T where T is your request when needed. Call the Execute method on the factory object to initiate your chain.

```csharp
public class IMyService
{
    private readonly IChainFactory<MyRequest> _chainFactory;

    public IMyService(IChainFactory<MyRequest> chainFactory)
    {
        _chainFactory = chainFactory;
    }

    public async Task Handle()
    {
        var result = await _chainFactory.Execute(new MyRequest());
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

        var result = await handler.Handle(new MyRequest());

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public async Task WithDependency()
    {
        var mock = new Mock<IMyDependency>();
        mock.Setup(x => x.MyMethod()).ReturnsAsync(new MyExpectedReturn());

        var handler = new MyHandler(null, mock.Object);

        var result = await handler.Handle(new MyRequest());

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public async Task ServiceTestForFactory()
    {
        var mock = new Mock<IChainFactory<MyRequest>>();
        mock.Setup(x => x.Execute(It.IsAny<MyRequest>())).ReturnsAsync(new MyHandler(null));

        var service = new MyService(mock.Object);

        var serviceResult = await service.DoSomething();

        Assert.AreEqual(expected, serviceResult);
    }
}
```

### Strategy

#### Request and Response

Unlike chains, a strategy uses both a request and response object.

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

#### Implementing A Handler

Implement a handler by inheriting from the IStrategyHandler of type TRequest, TResponse where TRequest is your request type, and TResponse is your response type.

Implement the Handle method as required.

```csharp
public class MyStrategyHandler : IStrategyHandler<MyRequest, MyResponse>
{
    public async Task<MyResponse> Handle(MyRequest request)
    {
        // implement and return response
    }
}
```

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

    public async Task<MyResponse> Handle(MyRequest request)
    {
        // implement and return response
    }
}
```

#### Strategy Profiles

Profiles are very similar to chains except you are defining conditions instead of steps.

You define a strategy by giving it a predicate based on your request object properties.

> Note: These are executed in order so put your most constrained definitions first.

You may add a default strategy if no conditions are met. The default does not accept a predicate.

```csharp
public class MyStrategyProfile : StrategyProfile<MyRequest, MyResponse>
{
    public MyStrategyProfile()
    {
        AddStrategy<MyFirstHandler>(request => request.Value > 10);
        AddStrategy<MySecondHandler>(request => request.Value == 0);
        AddDefault<MyDefaultHandler>();
    }
}
```

#### Testing Strategy Handlers

Testing any Strategy handlers is no different than a chain handler class.

#### Strategy Usage

Strategies follow the same pattern as chains, inject the factory into the class you want to use it in. Call the ExecuteStrategy method when required.

```csharp
public class MyService
{
    private readonly IStrategyFactory<MyRequest, MyResponse> _strategyFactory;

    public MyService(IStrategyFactory<MyRequest, MyResponse> strategyFactory)
    {
        _strategyFactory = strategyFactory;
    }

    public async Task Handle()
    {
        var result = await _strategyFactory.Execute(new MyRequest());
    }
}
```

## FAQ

### Do I need a Chain of Responsibility?

Do you have a complex process that can be broken up in to multiple steps to enable easier development and testing?

### Do I need a Strategy?

Do you have a common input/output interface that may use different implementations depending on a condition?

It is best to think of a Strategy as a complex switch statement where each switch case may be a long-lived, complex process.

(A common example is having to process credit cards with different payment providers.)

### How is either different from a Mediator?

A Mediator is a 1:1 relationship between a request and reply with a single handler per request.

A Chain of Responsibility is 1:M relationship with multiple handlers per request in a specific order.

A Strategy is 1:M relationship with a single handler chosen depending on a predicate.

### Can I use them together?

Yes! You can use any or all three in conjunction. None of them are mutually exclusive.

### How often can I use a Chain of Responsibility or Strategy?

A Chain of Responsibility is a **medium usage** pattern. It is best used when you need to break a problem down into smaller easier-to-test chunks.

A Strategy is a **low usage** pattern. It is best used when you need to have multiple implementations of a algorithm that uses the same interface.
