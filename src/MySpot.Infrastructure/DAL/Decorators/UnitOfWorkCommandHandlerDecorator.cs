using MySpot.Application.Abstractions;

namespace MySpot.Infrastructure.DAL.Decorators;

internal sealed class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public async Task HandleAsync(TCommand command)
    {
        await _unitOfWork.ExecuteAsync(() => _commandHandler.HandleAsync(command));
    }
}