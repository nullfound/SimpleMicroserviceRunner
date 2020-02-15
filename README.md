# SimpleMicroserviceRunner

![Build](https://github.com/nullfound/SimpleMicroserviceRunner/workflows/Build/badge.svg?branch=master)

The goal of *SimpleMicroserviceRunner* is to provide developers with a easy and flexible way spin up a microservice with all the support already built in. 

A service generally requires a lot of support with regards to DI, resiliency, logging. The idea behind this is to provide a ready framework so that developers can focus their effort on the service itself rather than spending time on creating the groundwork for a service.

This is currently WIP, however the basic service setup is live and can be used. A bit of an overkill at the moment, however will fit in nicely once the other supporting libraries are implemented.

The technologies planned to be used are:
* .Net Standard
* .Net Core
* SimpleInjector,
* Polly.Net
* Serilog
* xUnit

## Samples

### Basic
Shows a very basic console host that prints 'HelloWorld' until cancelled.

To run from src directory:
```
dotnet run -p .\Samples\SimpleMicroserviceRunner.Sample.Basic
```
