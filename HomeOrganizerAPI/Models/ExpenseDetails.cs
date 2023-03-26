namespace HomeOrganizerAPI.Models;

public partial record ExpenseDetails : Model
{
    public byte[] ExpenseUuid { get; set; }
    public decimal Value { get; set; }
    public byte[] PayerUuid { get; set; }
    public byte[] RecipientUuid { get; set; }

    public virtual Expenses Expense { get; set; }
    public virtual User Payer { get; set; }
    public virtual User Recipient { get; set; }
}
