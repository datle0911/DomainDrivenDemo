namespace Warehouse.Infrastructure.Repositories;

public class GoodsIssueRepository : BaseRepository, IGoodsIssueRepository
{
    public GoodsIssueRepository(WarehouseDbContext context) : base(context)
    {
    }

    public GoodsIssue Add(GoodsIssue goodsIssue)
    {
        if (goodsIssue.IsTransient())
        {
            FixTracking(goodsIssue);

            _context.GoodsIssues.Add(goodsIssue);
            _context.Entry(goodsIssue.ShiftLeader).State = EntityState.Unchanged;
            foreach (var entry in goodsIssue.Entries)
            {
                _context.Entry(entry.Employee).State = EntityState.Unchanged;
                _context.Entry(entry.Product).State = EntityState.Unchanged;
            }

            return goodsIssue;
        }
        else
        {
            return goodsIssue;
        }
    }

    public async Task<GoodsIssue?> FindByIdAsync(string id)
    {
        var goodsIssue = await _context.GoodsIssues
            .Include(g => g.ShiftLeader)
            .Include(g => g.Entries)
            .ThenInclude(g => g.Product)
            .Include(g => g.Entries)
            .ThenInclude(g => g.Employee)
            .Include(g => g.Entries)
            .ThenInclude(g => g.Baskets)
            .Where(g => g.GoodsIssueId == id)
            .AsNoTracking()
            .SingleOrDefaultAsync();

        return goodsIssue;
    }

    public async Task<IEnumerable<GoodsIssue>> GetListAsync(DateTime startTime, DateTime endTime)
    {
        var queryable = _context.GoodsIssues
            .Include(g => g.ShiftLeader)
            .Include(g => g.Entries)
            .ThenInclude(g => g.Product)
            .Include(g => g.Entries)
            .ThenInclude(g => g.Employee)
            .Include(g => g.Entries)
            .ThenInclude(g => g.Baskets)
            .AsNoTracking();

        queryable = queryable.Where(g =>
                g.Timestamp.CompareTo(startTime) > 0 &&
                g.Timestamp.CompareTo(endTime) < 0);

        var goodsIssues = await queryable.ToListAsync();

        return goodsIssues;
    }

    public GoodsIssue Update(GoodsIssue goodsIssue)
    {
        FixTracking(goodsIssue);

        return _context.GoodsIssues
            .Update(goodsIssue)
            .Entity;
    }

    private GoodsIssue FixTracking(GoodsIssue goodsIssue)
    {
        foreach (var entry in goodsIssue.Entries)
        {
            if (entry.Employee == goodsIssue.ShiftLeader)
            {
                entry.SetEmployee(goodsIssue.ShiftLeader);
            }
            foreach (var comparedEntry in goodsIssue.Entries)
            {
                if (entry.Employee == comparedEntry.Employee)
                {
                    comparedEntry.SetEmployee(entry.Employee);
                }
            }
        }

        return goodsIssue;
    }
}
