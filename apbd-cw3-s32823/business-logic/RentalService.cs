using apbd_cw3_s32823.Classes;

namespace apbd_cw3_s32823.business_logic;

using System;

public class RentalService
{
    private readonly List<Equipment> _equipments = new List<Equipment>();
    private readonly List<User> _users = new List<User>();
    private readonly List<Rental> _rentals = new List<Rental>();

    private readonly Dictionary<string, int> _rentalLimits = new Dictionary<string, int>
    {
        {"Student", 2},
        {"Employee", 5}
    };
    
    public void AddUser(User user) =>  _users.Add(user);
    public void AddEquipment(Equipment equipment) =>  _equipments.Add(equipment);
    public IReadOnlyList<Equipment> GetAllEquipment() => _equipments.AsReadOnly();
    public IReadOnlyList<Equipment> GetAvailableEquipment() => _equipments.Where(e => e.IsAvailable).ToList().AsReadOnly();
    
    public Rental RentItem(Guid userId, Guid equipmentId, TimeSpan duration)
    {
        var user = _users.FirstOrDefault(u => u.ID == userId);
        var equipment = _equipments.FirstOrDefault(e => e.Id == equipmentId);
        if (user == null || equipment == null)
        {
            throw new Exception($"User or Equipment does not exist");
        }

        if (!equipment.IsAvailable)
        {
            throw new Exception($"Equipment {equipment.Name} is not available");
        }
        
        int activeRentalsCount = _rentals.Count(r => r.Renter.ID == user.ID && r.IsActive);
        int maxAllowed = _rentalLimits.ContainsKey(user.UserType) ? _rentalLimits[user.UserType] : 0;
        if (activeRentalsCount >= maxAllowed)
        {
            throw new Exception($"User {user.FirstName} is already exceeded limit {maxAllowed} rentals");
        }
        
        var rental = new Rental(user, equipment, DateTime.Now, duration);
        _rentals.Add(rental);

        return rental;
    }

    public void ReturnItem(Guid rentalId, DateTime? customReturnDate = null)
    {
        var rental = _rentals.FirstOrDefault(r => r.Id == rentalId);
        if (rental == null)
        {
            throw new Exception("Rental not found");
        }

        if (!rental.IsActive)
        {
            throw new Exception("Rental is already finished");
        }

        var returnDate = customReturnDate ?? DateTime.Now;
        decimal penalty = 0;

        if (returnDate > rental.DueDate)
        {
            var overDueDays = (int)Math.Ceiling((returnDate - rental.DueDate).TotalDays);
            if (overDueDays > 0)
            {
                penalty = overDueDays * 50;
            }

        }

        rental.CompleteRent(returnDate, penalty);
    }

    public void SetEquipmentUnavailable(Guid equipmentId)
    {
        var equipment = _equipments.FirstOrDefault(e => e.Id == equipmentId);
        if (equipment == null)
        {
            throw new Exception("Equipment not found");
        }
        equipment.SetAvailable(false);
    }

    public IReadOnlyList<Rental> DisplayRentalsForUser(Guid userId)
    {
        return _rentals.
            Where(u => u.Id == userId && u.IsActive).
            ToList().
            AsReadOnly();
    }

    public IReadOnlyList<Rental> DisplayOverdueRentals()
    {
        var currentTime = DateTime.Now;
        return _rentals.
            Where(r => r.IsActive &&  r.DueDate < currentTime).
            ToList().
            AsReadOnly();
    }

    public string GenerateReport()
    {
        int equipmentCount = _equipments.Count;
        int availableEquipmentCount = _equipments.Count(e => e.IsAvailable);
        int activeRentalsCount = _rentals.Count(r => r.IsActive);
        int overdueRentalsCount = DisplayOverdueRentals().Count;

        return $"\n------ SUMMARY REPORT ------\n" +
               $"Total equipment: {equipmentCount}\n" +
               $"Total available equipment: {availableEquipmentCount}\n" +
               $"Total active rentals: {activeRentalsCount}\n" +
               $"Total overdue rentals: {overdueRentalsCount}\n" +
               $"----------------------------";
    }
}