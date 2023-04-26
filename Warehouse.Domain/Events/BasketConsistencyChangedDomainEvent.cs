using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Events;

public class BasketConsistencyChangedDomainEvent: INotification
{
    public string BasketId { get; private set; }
    public bool IsConsistent { get; private set; }

    public BasketConsistencyChangedDomainEvent(string basketId, bool isConsistent)
    {
        BasketId = basketId;
        IsConsistent = isConsistent;
    }
}
