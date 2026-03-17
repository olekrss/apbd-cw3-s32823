
## Project Description
This is a console application designed to manage university equipment rentals. It allows adding users and equipment, processing rentals, handling returns, and calculating late penalties.

## Design Decisions and Architecture

### 1. File and Layer Organization
* classes folder: Contains pure business entities (Equipment(and its child classes), User(and its child classes), Rental). These classes only care about their own state and data. They do not know about the console or how saving/loading works.
* business-logic folder: Contains RentalService.cs. This separates the execution logic from the data models. The service acts as the central point for business rules (checking limits, calculating penalties), which prevents spreading this logic across multiple classes.

### 2. Class Responsibilities
I avoided putting limits (e.g. maximum 2 rentals for a Student) directly into the Student class. The Student class is only responsible for representing the user's data. The RentalService is responsible for enforcing the rental rules. This makes the system much easier to modify in the future if limits change.

### 3. Managing Coupling and Encapsulation
To prevent tight coupling and accidental data corruption from outside classes:
* private set: Properties in domain classes (like Equipment.IsAvailable) use private set. They can only be changed via specific methods (e.g. SetAvailability()), protecting the object's internal state.
* IReadOnlyList: When the RentalService returns lists of equipment or rentals to the console interface, it returns IReadOnlyList<T>. This ensures the UI layer cannot accidentally add or clear the internal database, strictly separating the interface from data management.

### 4. Inheritance vs. Composition
I used inheritance for Equipment (base abstract class) and concrete types (Laptop, Projector, Camera) because they all share a core identity (Id, Name, IsAvailable) but require specific fields. The same applies to User -> Student/Employee.