using MediatR;

namespace TodoApp.Application.Commons
{
    public interface IBroker
    {
        public Task CommandAsync(ICommand command);
        public Task CommandAsync(ICommand command, CancellationToken cancellationToken);
        public Task<TCommandResult> CommandAsync<TCommandResult>(ICommand<TCommandResult> command);
        public Task<TCommandResult> CommandAsync<TCommandResult>(ICommand<TCommandResult> command, CancellationToken cancellationToken);
        public Task QueryAsync(IQuery command);
        public Task QueryAsync(IQuery command, CancellationToken cancellationToken);
        public Task<TQueryResult> QueryAsync<TQueryResult>(IQuery<TQueryResult> command);
        public Task<TQueryResult> QueryAsync<TQueryResult>(IQuery<TQueryResult> command, CancellationToken cancellationToken);
        public Task PublishAsync(INotification notification);
    }

    public class Broker : IBroker
    {
        private readonly IMediator _mediator;
        public Broker(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task CommandAsync(ICommand command)
        {
            return _mediator.Send(command);
        }
        public Task CommandAsync(ICommand command, CancellationToken cancellationToken)
        {
            return _mediator.Send(command, cancellationToken);
        }
        public Task<TCommandResult> CommandAsync<TCommandResult>(ICommand<TCommandResult> command)
        {
            return _mediator.Send(command);
        }
        public Task<TCommandResult> CommandAsync<TCommandResult>(ICommand<TCommandResult> command, CancellationToken cancellationToken)
        {
            return _mediator.Send(command, cancellationToken);
        }
        public Task QueryAsync(IQuery command)
        {
            return _mediator.Send(command);
        }
        public Task QueryAsync(IQuery command, CancellationToken cancellationToken)
        {
            return _mediator.Send(command, cancellationToken);
        }
        public Task<TQueryResult> QueryAsync<TQueryResult>(IQuery<TQueryResult> command)
        {
            return _mediator.Send(command);
        }
        public Task<TQueryResult> QueryAsync<TQueryResult>(IQuery<TQueryResult> command, CancellationToken cancellationToken)
        {
            return _mediator.Send(command, cancellationToken);
        }
        public Task PublishAsync(INotification notification)
        {
            return _mediator.Publish(notification);
        }
    }
}
