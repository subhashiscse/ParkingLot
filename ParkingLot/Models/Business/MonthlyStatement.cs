public class MonthlyStatement
{
    public int MonthlyStatementId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int ReservationId { get; set; }
    public decimal Amount { get; set; } 
    public DateTime PaymentDate { get; set; } = DateTime.Now;
    public string PaymentStatus { get; set; } = "Paid";
}