using System;
using apbd_cw3_s32823.Classes;
using apbd_cw3_s32823.business_logic;
class Program
{
    static void Main(string[] args)
        { 
            var service = new RentalService();

            Console.WriteLine("--- Adding equipment and users ---");
            var laptop = new Laptop("Dell XPS 15", 16, "Intel i7");
            var projector = new Projector("Epson 4K", "3840x2160", 3500);
            var camera = new Camera("Sony Alpha", 24.2, true);
            var laptop2 = new Laptop("MacBook Air", 8, "M1");

            service.AddEquipment(laptop);
            service.AddEquipment(projector);
            service.AddEquipment(camera);
            service.AddEquipment(laptop2);

            var student = new Student("John", "Doe");
            var employee = new Employee("Jane", "Smith");

            service.AddUser(student);
            service.AddUser(employee);
            Console.WriteLine("Data loaded successfully!\n");

            Console.WriteLine("--- Example of correct rental operation ---");
            var rental1 = service.RentItem(student.ID, laptop.Id, TimeSpan.FromDays(5));
            Console.WriteLine($"Student {student.FirstName} successfully rented {laptop.Name}.\n");

            Console.WriteLine("--- Attempt invalid operations ---");
            try
            {
                service.RentItem(student.ID, projector.Id, TimeSpan.FromDays(2));
                Console.WriteLine($"Student {student.FirstName} rented {projector.Name}. (Active rentals: 2)");
                
                Console.WriteLine("Attempting to rent a 3rd item....");
                service.RentItem(student.ID, camera.Id, TimeSpan.FromDays(1));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.ResetColor();
            }

            try
            {
                Console.WriteLine("\nAttempt to rent an unavailable laptop....");
                service.RentItem(employee.ID, laptop.Id, TimeSpan.FromDays(1));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: {ex.Message}\n");
                Console.ResetColor();
            }

            Console.WriteLine("--- Example of return completed on time ---");
            service.ReturnItem(rental1.Id);
            Console.WriteLine($"Equipment {laptop.Name} returned. Penalty: {rental1.PenaltyFee}\n");

            Console.WriteLine("--- Example of delayed return ---");
            var rentalDelayed = service.RentItem(employee.ID, camera.Id, TimeSpan.FromDays(2));
            
            service.ReturnItem(rentalDelayed.Id, DateTime.Now.AddDays(5));
            Console.WriteLine($"Equipment {camera.Name} returned. Penalty: {rentalDelayed.PenaltyFee}\n");

            Console.WriteLine("--- Example of generating report ---");
            Console.WriteLine(service.GenerateReport());
            
            Console.ReadLine(); 
    }
}