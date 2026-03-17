namespace apbd_cw3_s32823.Classes;

public class Rental
{
    public Guid Id { get; private set; }
    public User Renter { get; private set; }
    public Equipment RentedItem { get; private set; }
    public DateTime RentDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? ActualReturnDate { get; private set; }
    public decimal PenaltyFee {get; private set; }
    
    public Rental(User renter, Equipment rentedItem, DateTime rentDate, TimeSpan rentDuration)
    {
        Id = Guid.NewGuid();
        Renter = renter;
        RentedItem = rentedItem;
        RentDate = rentDate;
        DueDate = rentDate.Add(rentDuration);
        PenaltyFee = 0;
        RentedItem.SetAvailable(false);
    }

    public void CompleteRent(DateTime completionDate, decimal penalty)
    {
        ActualReturnDate = completionDate;
        PenaltyFee = penalty;

        RentedItem.SetAvailable(true);
    }
    
    public bool IsActive => ActualReturnDate == null;
}