using System.Collections.Generic;

public interface IBillingService
{
    List<MonthlyStatement> GetByCustomerId(int customerId);
    MonthlyStatement GetById(int id);
    void Create(MonthlyStatement  monthlyStatement);
}