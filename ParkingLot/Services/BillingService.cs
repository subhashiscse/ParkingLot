using ParkingLot.Data;

public class BillingService : IBillingService
{
    private readonly ParkingLotDbContext _context;

    public BillingService(ParkingLotDbContext context)
    {
        _context = context;
    }

    // =========================
    // GET STATEMENTS FOR USER
    // =========================
    public List<MonthlyStatement> GetByCustomerId(int customerId)
    {
        return _context.MonthlyStatements
            .Where(s => s.CustomerId == customerId)
            .ToList();
    }

    // =========================
    // GET SINGLE STATEMENT
    // =========================
    public MonthlyStatement GetById(int id)
    {
        return _context.MonthlyStatements
            .FirstOrDefault(s => s.MonthlyStatementId == id);
    }

    public void Create(MonthlyStatement monthlyStatement)
    {
        var statement = _context.MonthlyStatements.Where(s=>s.CustomerId == monthlyStatement.CustomerId && s.ReservationId == monthlyStatement.ReservationId).FirstOrDefault();
        if (statement == null)
        {
            _context.MonthlyStatements.Add(monthlyStatement);
            _context.SaveChanges();
        }
    }
}