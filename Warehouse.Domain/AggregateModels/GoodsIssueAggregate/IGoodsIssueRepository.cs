using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.AggregateModels.GoodsIssueAggregate;

public interface IGoodsIssueRepository : IRepository<GoodsIssue>
{
    GoodsIssue Add(GoodsIssue goodsIssue);
    GoodsIssue Update(GoodsIssue goodsIssue);
    Task<IEnumerable<GoodsIssue>> GetListAsync(DateTime startTime, DateTime endTime);
    Task<GoodsIssue?> FindByIdAsync(string id);
}
