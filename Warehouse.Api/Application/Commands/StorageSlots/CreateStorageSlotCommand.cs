namespace Warehouse.Api.Application.Commands.StorageSlots;

[DataContract]
public class CreateStorageSlotCommand : IRequest<bool>
{
    [DataMember]
    public string StorageSlotId { get; private set; }

    public CreateStorageSlotCommand(string storageSlotId)
    {
        StorageSlotId = storageSlotId;
    }
}
