using System;

public class MonthlyStatementViewModel
{
    public int MonthlyStatementId { get; set; }

    public int Year { get; set; }
    public int Month { get; set; }

    public decimal TotalReservations { get; set; }
    public decimal TotalWalkIns { get; set; }
    public decimal TotalPenalties { get; set; }
    public decimal GrandTotal { get; set; }

    public string PaymentStatus { get; set; }

    public DateTime DueDate { get; set; }
}