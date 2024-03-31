namespace BankApp.Models
{
    public class RaiffeisenRsd
    {
        public int Id { get; set; }
        public string TransactionDate { get; set; }
        public string Reference { get; set; }
        public string Recipient { get; set; }
        public string TransactionDescription { get; set; }
        public decimal Debit { get; set; }
        public string FileName { get; set; }
    }
}
