using Warehouse.Api.Application.Commands.StorageSlots;

namespace Warehouse.Api.Application.Commands;

public class CreateStorageSlotCommandHandler : IRequestHandler<CreateStorageSlotCommand, bool>
{
    private readonly IStorageSlotRepository _storageSlotRepository;

    public CreateStorageSlotCommandHandler(IStorageSlotRepository storageSlotRepository)
    {
        _storageSlotRepository = storageSlotRepository;
    }

    public async Task<bool> Handle(CreateStorageSlotCommand request, CancellationToken cancellationToken)
    {
        var storageSlot = new StorageSlot(request.StorageSlotId);

        _storageSlotRepository.Add(storageSlot);

        var result = await _storageSlotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return result;
    }
}
