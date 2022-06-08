using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ
{
    internal class RabbitMQPersistentConnection : IDisposable
    {

        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private readonly int _retryCount;
        private bool isDisposed;

        public RabbitMQPersistentConnection(IConnectionFactory connectionFactory, int retryCount = 5)
        {
            _connectionFactory = connectionFactory;
            _retryCount = retryCount;
        }

        public bool IsConnected => _connection != null && _connection.IsOpen;
        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("Bu işlemi yapabilmek için bağlantı kurulmalıdır.");
            }
            return _connection.CreateModel();
        }


        public void Dispose()
        {
            isDisposed = true;
            _connection.Dispose();
        }

        public bool TryConnect()
        {
            var policy = RetryPolicy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                   //log'a yazılabilir.
                });

            policy.Execute(() =>
            {
                _connection = _connectionFactory.CreateConnection();

            });

            if (IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.ConnectionBlocked += OnConnectionBlocked;
                _connection.CallbackException += _connection_CallbackException;
                return true;
            }

            return false;

        }

        private void _connection_CallbackException(object? sender, CallbackExceptionEventArgs e)
        {
            if (isDisposed) return;

                TryConnect();
        }

        private void OnConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
        {
            if (isDisposed) return;
            TryConnect();
        }

        private void OnConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            if (isDisposed) return;
            TryConnect();
        }
    }
}
