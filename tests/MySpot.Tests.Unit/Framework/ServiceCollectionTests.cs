using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MySpot.Tests.Unit.Framework;

public class ServiceCollectionTests
{
    [Fact]
    public void test()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IUser, Admin>();
        serviceCollection.AddSingleton<IUser, Employee>();
        serviceCollection.AddSingleton<IUser, Manager>();

        serviceCollection.AddScoped(typeof(IMessenger<>), typeof(Messenger<>));
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var user = serviceProvider.GetServices<IUser>();
        // using var scope = serviceProvider.CreateScope();
        // var messenger1 = scope.ServiceProvider.GetService<IMessenger>();
        // var messenger2 = scope.ServiceProvider.GetService<IMessenger>();
        // using var scope2 = serviceProvider.CreateScope();
        // var messenger3 = scope2.ServiceProvider.GetService<IMessenger>();
        // var messenger4 = scope2.ServiceProvider.GetService<IMessenger>();
        // Assert.Equal(messenger1, messenger2);
    }

    public interface IMessenger<T>
    {
        void Send(T message);
    }

    public class Messenger<T> : IMessenger<T>
    {
        private Guid _id = Guid.NewGuid();

        public void Send(T message)
        {
            Console.WriteLine($"Sending a message... [Id { _id }]");
        }
    }

    public interface IUser
    {

    }

    public class Admin : IUser
    {
        private readonly IMessenger<string> _messenger;
        public Admin(IMessenger<string> messenger)
        {
            _messenger = messenger;
        }
    }

    public class Employee : IUser
    {

    }

    public class Manager : IUser
    {

    }
}