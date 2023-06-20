using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bidder.UserService.Infastructure.Context
{
    public class UserDbContextDesignFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        private readonly IConfiguration configuration;

        public UserDbContextDesignFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public UserDbContext CreateDbContext(string[] args)
        {
            var constr = configuration.GetConnectionString("UserConnectionString");

            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>()
                .UseSqlServer(constr);

            return new UserDbContext(optionsBuilder.Options, new NoMediator());
        }

    }
    class NoMediator : IMediator
    {
        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<object> CreateStream(object request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(default(TResponse));
        }

        public Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(default(object));
        }

        public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
        {
            return Task.FromResult<object>(default(TRequest));
        } 
    }

}
