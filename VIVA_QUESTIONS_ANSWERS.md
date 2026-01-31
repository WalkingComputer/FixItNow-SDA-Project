# ?? VIVA EXAMINATION - QUICK REFERENCE GUIDE
## FixItNow Application Architecture

---

## ? **COMMON VIVA QUESTIONS & ANSWERS**

### **Q1: What is the architecture used in your project?**

**Answer:**
```
Layered Architecture (N-Tier Architecture) with 4 layers:

1. PRESENTATION LAYER - Console menus and user interface
2. APPLICATION LAYER - Business logic and services
3. INFRASTRUCTURE LAYER - Data access and repositories
4. DOMAIN LAYER - Core entities and interfaces

Database: MongoDB Atlas (NoSQL)
Framework: .NET Framework 4.7.2
Language: C# 7.3
```

---

### **Q2: Explain the Repository Pattern in your project?**

**Answer:**
```
REPOSITORY PATTERN:
?? Definition: Abstracts data access logic and provides 
?  a collection-like interface
?
?? How it's used:
?  • Interface IUserRepository (contract in Domain)
?  • Implementation UserRepository (in Infrastructure)
?  • Abstracts MongoDB operations
?
?? Benefits:
?  ? Decouples business logic from data access
?  ? Easy to swap database (MongoDB ? SQL)
?  ? Testability (mock repositories)
?  ? Single responsibility
?
?? Repositories in project:
   • IUserRepository / UserRepository
   • ITicketRepository / TicketRepository
   • ITicketAssignmentRepository / TicketAssignmentRepository
   • IAuditLogRepository / AuditLogRepository
   • INotificationRepository / NotificationRepository
```

---

### **Q3: What is Dependency Injection? How is it used?**

**Answer:**
```
DEPENDENCY INJECTION:
?? Definition: Objects receive their dependencies from 
?  external sources (IoC Container)
?
?? Problem it solves:
?  ? Without DI: Services create own dependencies (tight coupling)
?  ? With DI: Container manages dependencies (loose coupling)
?
?? Implementation in FixItNow:
?  • Container: Microsoft.Extensions.DependencyInjection
?  • Setup in Program.cs ? ConfigureServices()
?  • Lifetimes:
?    - Singleton: MongoDbSettings, MongoDbContext
?    - Scoped: Repositories, Services
?    - Transient: Menu classes
?
?? Example:
   ServiceCollection services = new ServiceCollection();
   services.AddScoped<ITicketService, TicketService>();
   services.AddScoped<IUserRepository, UserRepository>();
   ServiceProvider provider = services.BuildServiceProvider();
   var ticketService = provider.GetService<ITicketService>();

?? Benefits:
   ? Loose coupling
   ? Easy testing (inject mocks)
   ? Centralized configuration
```

---

### **Q4: Explain the Unit of Work Pattern?**

**Answer:**
```
UNIT OF WORK PATTERN:
?? Definition: Manages a collection of repositories and
?  coordinates database operations
?
?? Structure:
?  public class UnitOfWork : IUnitOfWork
?  {
?      public IUserRepository Users { get; }
?      public ITicketRepository Tickets { get; }
?      public ITicketAssignmentRepository TicketAssignments { get; }
?      public IAuditLogRepository AuditLogs { get; }
?      public INotificationRepository Notifications { get; }
?  }
?
?? Purpose:
?  • Centralizes repository access
?  • Coordinates multiple operations
?  • Manages transactions
?
?? Benefits:
   ? Single point for all repository access
   ? Better transaction management
   ? Cleaner service code
```

---

### **Q5: What is the Service Layer? Why do we need it?**

**Answer:**
```
SERVICE LAYER:
?? Definition: Contains business logic that coordinates
?  repositories and applies business rules
?
?? Services in FixItNow:
?  • TicketService - Manages ticket operations
?  • UserService - Manages user operations
?  • AuthenticationService - Handles login/logout
?
?? Example - TicketService:
   public class TicketService : ITicketService
   {
       public async Task<Ticket> CreateTicketAsync(CreateTicketRequest req)
       {
           // 1. Validate input
           // 2. Generate ticket ID
           // 3. Create ticket entity
           // 4. Save to repository
           // 5. Create audit log
           // 6. Send notification
           return ticket;
       }
   }

?? Why we need it:
   ? Centralizes business logic
   ? Reusable across multiple UI components
   ? Easy to test
   ? Single responsibility
   
?? Flow:
   Menu ? Service ? Repository ? Database
```

---

### **Q6: Explain the role-based access control in your system?**

**Answer:**
```
ROLE-BASED ACCESS CONTROL (RBAC):

RESIDENT (RoleId = 1):
?? Can create new tickets
?? Can view own tickets
?? Cannot assign tickets
?? Cannot resolve tickets

ADMIN (RoleId = 2):
?? Can view all tickets
?? Can assign tickets to technicians
?? Can view all users
?? Can manage the system

TECHNICIAN (RoleId = 3):
?? Can view assigned tickets
?? Can update ticket status
?? Cannot create tickets
?? Can only work on assigned tickets

Implementation:
• User.RoleId field stores role
• After login, check RoleId
• Route to appropriate menu:
  
  switch (user.RoleId)
  {
      case 1: await _residentMenu.ShowAsync(); break;
      case 2: await _adminMenu.ShowAsync(); break;
      case 3: await _technicianMenu.ShowAsync(); break;
  }
```

---

### **Q7: What is MongoDB and why use NoSQL for this project?**

**Answer:**
```
MONGODB:
?? Definition: NoSQL document-based database
?
?? Document Structure:
   {
     "_id": ObjectId,
     "userId": 1,
     "username": "admin",
     "email": "admin@example.com",
     "createdAt": Date
   }

?? Why NoSQL for FixItNow:
   ? Flexible schema (can add fields easily)
   ? Horizontal scalability
   ? Fast reads/writes
   ? Good for hierarchical data
   ? Cloud-based Atlas available

?? MongoDB Atlas:
   • Cloud-hosted MongoDB
   • Connection: mongodb+srv://username:password@cluster.mongodb.net
   • Automatic backups
   • Scalable infrastructure

?? Collections in FixItNow:
   • Users
   • Tickets
   • Roles
   • Categories
   • Priorities
   • TicketStatuses
   • Locations
   • Notifications
   • AuditLogs
```

---

### **Q8: Explain Async/Await in your project?**

**Answer:**
```
ASYNC/AWAIT:
?? Definition: Non-blocking asynchronous programming pattern
?
?? Why it matters:
   ? Synchronous: Thread blocks until operation completes
   ? Asynchronous: Thread continues while operation runs
?
?? Usage in FixItNow:
   
   // Repository
   public async Task<User> GetByIdAsync(int userId)
   {
       return await _users.Find(filter).FirstOrDefaultAsync();
   }
   
   // Service
   public async Task<Ticket> CreateTicketAsync(...)
   {
       await _ticketRepository.AddAsync(ticket);
       await CreateNotificationAsync(...);
       await LogAuditAsync(...);
   }
   
   // Menu
   public async Task ShowAsync()
   {
       var user = await _authService.LoginAsync(username, password);
   }
?
?? Benefits:
   ? Non-blocking database calls
   ? Better resource utilization
   ? Responsive application
   ? Scalable to many concurrent users
?
?? Thread states:
   Thread waiting for I/O ? Can handle other requests
   Thread not blocked ? Better scalability
```

---

### **Q9: What is the flow when a Resident creates a ticket?**

**Answer:**
```
TICKET CREATION FLOW:

1. PRESENTATION LAYER
   ?? ResidentMenu.ShowAsync()
   ?? Gets user input (title, description, category, etc)

2. APPLICATION LAYER
   ?? TicketService.CreateTicketAsync(CreateTicketRequest)
   ?? Generate next TicketId
   ?? Create Ticket entity
   ?? Call repository

3. INFRASTRUCTURE LAYER
   ?? TicketRepository.AddAsync(ticket)
   ?? Convert to MongoDB document
   ?? Insert into Tickets collection

4. DATABASE (MongoDB)
   ?? Store in FixItNowDB.Tickets collection
   ?? Assign ObjectId

5. SERVICE CONTINUES
   ?? Notification.CreateNotificationAsync()
   ?  ?? Notifies all admins of new ticket
   ?? AuditLog.LogAuditAsync()
   ?  ?? Records "CREATE" action
   ?? Return ticket to UI

6. BACK TO UI
   ?? Show success message

Code Flow:
  ResidentMenu 
    ? (await)
  TicketService.CreateTicketAsync()
    ? (await)
  TicketRepository.AddAsync()
    ? (await)
  MongoDB.InsertOne()
    ?
  Success ?
```

---

### **Q10: What SOLID principles are applied?**

**Answer:**
```
SOLID PRINCIPLES:

S - SINGLE RESPONSIBILITY:
? TicketService handles only ticket operations
? UserRepository handles only user data access
? Each class has one reason to change

O - OPEN/CLOSED:
? Repository interface allows adding new implementations
? Can add new repositories without modifying existing code
? Services depend on abstractions, not concrete classes

L - LISKOV SUBSTITUTION:
? UserRepository implements IUserRepository
? Can substitute UserRepository with any IUserRepository
? All repositories follow same contract

I - INTERFACE SEGREGATION:
? IUserRepository has only user-related methods
? ITicketRepository has only ticket-related methods
? Clients depend only on methods they need

D - DEPENDENCY INVERSION:
? Services depend on IRepository (abstraction)
? Not on concrete UserRepository class
? DI container injects correct implementation
```

---

### **Q11: How does authentication work?**

**Answer:**
```
AUTHENTICATION FLOW:

1. User enters username and password in MainMenu

2. LoginAsync(username, password):
   ?? Query UserRepository for username
   ?? Check if user exists
   ?? Check if user is active
   ?? Verify password matches (TODO: implement hashing)
   ?? Set _currentUser in AuthenticationService

3. If successful:
   ?? Return User object
   ?? Route to appropriate menu (based on RoleId)

4. If failed:
   ?? Throw exception
   ?? Show error to user
   ?? Return to login screen

5. LogoutAsync():
   ?? Clear _currentUser
   ?? Return to MainMenu

Security Note:
• Currently: Plain text (for development)
• Production: Use bcrypt/PBKDF2 hashing
  - Hash password during registration
  - Hash input during login
  - Compare hashes instead of plain text
```

---

### **Q12: Explain the Data Flow in your architecture?**

**Answer:**
```
COMPLETE DATA FLOW:

USER ? PRESENTATION LAYER (Menu)
  ?
  ??? TicketService.CreateTicketAsync(request)
       ?
       ??? TicketRepository.AddAsync(ticket)
       ?   ??? MongoDB Driver
       ?       ??? MONGODB ATLAS
       ?
       ??? NotificationRepository.AddAsync(notification)
       ?   ??? MONGODB ATLAS
       ?
       ??? AuditLogRepository.AddAsync(auditLog)
           ??? MONGODB ATLAS

Response flows back:
MONGODB ? Repository ? Service ? Menu ? User

Example:
????????????????????
?   ResidentMenu   ? (UI)
????????????????????
         ?
         ?
????????????????????
? TicketService    ? (Business Logic)
????????????????????
         ?
         ??????? TicketRepository
         ??????? NotificationRepository
         ??????? AuditLogRepository
                  ?
                  ?
         ????????????????????
         ? MongoDB Atlas    ? (Database)
         ????????????????????
```

---

### **Q13: Why separate Entities and DTOs?**

**Answer:**
```
ENTITIES vs DTOs:

ENTITY (Domain Layer):
?? Represents database structure
?? Contains all properties
?? Example: Ticket with 15+ properties
?? Used for: Database operations

DTO (Application Layer):
?? Represents input/output structure
?? Contains only needed properties
?? Example: CreateTicketRequest (title, description, categoryId)
?? Used for: API communication

Benefits of Separation:
? Security: Don't expose all properties
? Flexibility: Can change Entity without breaking API
? Validation: DTOs can have validation
? Performance: Send only needed fields
? Decoupling: Entity changes don't break client code

Example:
Ticket Entity has: TicketId, TicketCode, Title, Description, 
                   CategoryId, LocationId, PriorityId, StatusId,
                   CreatedByUserId, CurrentTechnicianId, 
                   CreatedAt, UpdatedAt, ResolvedAt, ClosedAt,
                   ReopenedAt

CreateTicketRequest DTO has: Title, Description, CategoryId,
                             LocationId, PriorityId, 
                             CreatedByUserId
```

---

### **Q14: How is data seeding implemented?**

**Answer:**
```
DATA SEEDING:

Purpose:
?? Initialize database with default data
?? Create default users (admin, tech1, resident1)
?? Create lookup data (roles, categories, priorities)
?? Run once on first application start

Implementation:
?? SeedData.cs ??????????
? public async Task    ?
? SeedAllAsync()        ?
????????????????????????
?? SeedRolesAsync()    ? ? 3 roles
?? SeedCategoriesAsync()? ? 5 categories
?? SeedPrioritiesAsync()? ? 4 priorities
?? SeedTicketStatusesAsync() ? 6 statuses
?? SeedLocationsAsync() ? ? 4 locations
?? SeedDefaultUsersAsync() ? 3 users
   ???????????????????????

Flow in Program.cs:
await seeder.SeedAllAsync();
  ?? Check if collection empty
  ?? If empty ? Insert default data
  ?? If exists ? Skip (idempotent)

Default Users Seeded:
1. admin / admin123 (RoleId=2, Admin)
2. tech1 / tech123 (RoleId=3, Technician)
3. resident1 / resident123 (RoleId=1, Resident)
```

---

### **Q15: What are the benefits of this architecture?**

**Answer:**
```
ARCHITECTURAL BENEFITS:

1. MAINTAINABILITY:
   ? Clear layer separation
   ? Easy to locate and fix bugs
   ? Changes in one layer don't affect others

2. SCALABILITY:
   ? Add repositories without changing services
   ? Easy to add new features
   ? MongoDB supports horizontal scaling
   ? Async operations handle more concurrent users

3. TESTABILITY:
   ? Each layer tested independently
   ? Mock repositories for service testing
   ? No actual database needed for tests

4. REUSABILITY:
   ? Services used across menus
   ? Repositories standardized
   ? DTOs reused across application

5. FLEXIBILITY:
   ? Swap MongoDB with SQL database
   ? Change DI container
   ? Externalize configuration
   ? Easy to modify business rules

6. PROFESSIONAL PRACTICES:
   ? SOLID principles
   ? Industry best practices
   ? Production-ready design
   ? Team-friendly code structure
```

---

## ?? **QUICK REFERENCE TABLES**

### **Project Structure:**
```
FixItNow/
??? Domain/              (Core business, 13 entities)
??? Application/         (3 services, 8 interfaces)
??? Infrastructure/      (5 repositories, Unit of Work)
??? Presentation/        (4 menus, UI layer)
```

### **Lifetimes in DI:**
```
Singleton   ? Created once, shared across application
Scoped      ? Created once per request/operation
Transient   ? Created new each time requested
```

### **Entities & Collections:**
```
User, Ticket, Role, Category, Priority, TicketStatus,
Location, TicketAssignment, Notification, AuditLog,
Comment, Attachment, Feedback
```

### **Authentication Roles:**
```
Resident (1)    ? Create tickets, view own
Admin (2)       ? Manage all, assign tickets
Technician (3)  ? View assigned, update status
```

---

## ? **KEY POINTS TO REMEMBER**

1. **4 Layers:** Domain, Application, Infrastructure, Presentation
2. **Repository Pattern:** Abstracts data access
3. **Dependency Injection:** Loose coupling, testable code
4. **Unit of Work:** Coordinates repositories
5. **Service Layer:** Business logic coordination
6. **RBAC:** Role-based menu access
7. **MongoDB:** NoSQL, cloud-based
8. **Async/Await:** Non-blocking operations
9. **DTOs:** Safe data transfer
10. **SOLID:** Professional code design

---

**Good Luck with your Viva! ???**
