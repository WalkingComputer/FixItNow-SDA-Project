# ??? FixItNow Application - Complete Architecture Analysis
## **For Viva Examination**

---

## ?? OVERALL ARCHITECTURE DIAGRAM

```
???????????????????????????????????????????????????????????????
?              PRESENTATION LAYER (UI)                         ?
?  - Console Menus (MainMenu, AdminMenu, ResidentMenu, etc)   ?
?  - User Interaction                                          ?
???????????????????????????????????????????????????????????????
                           ?
                           ?
???????????????????????????????????????????????????????????????
?          APPLICATION LAYER (Business Logic)                 ?
?  - Services (TicketService, UserService, etc)              ?
?  - DTOs (Data Transfer Objects)                            ?
?  - Business Rules                                           ?
???????????????????????????????????????????????????????????????
                           ?
                           ?
???????????????????????????????????????????????????????????????
?         INFRASTRUCTURE LAYER (Data Access)                  ?
?  - Repositories (UserRepository, TicketRepository)         ?
?  - Unit of Work Pattern                                    ?
?  - MongoDB Context                                         ?
???????????????????????????????????????????????????????????????
                           ?
                           ?
???????????????????????????????????????????????????????????????
?           DOMAIN LAYER (Core Business)                      ?
?  - Entities (User, Ticket, Role, etc)                      ?
?  - Interfaces (Repository Contracts)                       ?
?  - Enums (TicketStatusEnum)                               ?
???????????????????????????????????????????????????????????????
                           ?
                           ?
???????????????????????????????????????????????????????????????
?              DATABASE (MongoDB Atlas)                        ?
?  - Collections (Users, Tickets, Roles, etc)               ?
???????????????????????????????????????????????????????????????
```

---

## ??? **ARCHITECTURAL PATTERNS APPLIED**

### **1. LAYERED ARCHITECTURE (N-Tier Architecture)**

**Definition:** Application is divided into distinct layers, each with specific responsibilities.

**Layers in FixItNow:**

```
Layer 1: Presentation Layer
??? MainMenu.cs
??? AdminMenu.cs
??? ResidentMenu.cs
??? TechnicianMenu.cs
??? ConsoleHelper.cs
??? Program.cs (Main entry point)

Layer 2: Application Layer
??? Services/
?   ??? TicketService.cs
?   ??? UserService.cs
?   ??? AuthenticationService.cs
??? Interfaces/
?   ??? ITicketService.cs
?   ??? IUserService.cs
?   ??? IAuthenticationService.cs
??? DTOs/
    ??? CreateTicketRequest.cs
    ??? RegisterUserRequest.cs

Layer 3: Infrastructure Layer
??? Repositories/
?   ??? UserRepository.cs
?   ??? TicketRepository.cs
?   ??? TicketAssignmentRepository.cs
?   ??? AuditLogRepository.cs
?   ??? NotificationRepository.cs
??? UnitOfWork/
?   ??? UnitOfWork.cs
??? Data/
    ??? MongoDbContext.cs
    ??? MongoDbSettings.cs
    ??? SeedData.cs

Layer 4: Domain Layer
??? Entities/
?   ??? User.cs
?   ??? Ticket.cs
?   ??? TicketAssignment.cs
?   ??? Role.cs
?   ??? Category.cs
?   ??? Priority.cs
?   ??? TicketStatus.cs
?   ??? Location.cs
?   ??? Notification.cs
?   ??? AuditLog.cs
?   ??? Comment.cs
?   ??? Attachment.cs
?   ??? Feedback.cs
??? Interfaces/
?   ??? IUserRepository.cs
?   ??? ITicketRepository.cs
?   ??? ITicketAssignmentRepository.cs
?   ??? IAuditLogRepository.cs
?   ??? INotificationRepository.cs
?   ??? IUnitOfWork.cs
??? Enums/
    ??? TicketStatusEnum.cs
```

**Benefits:**
- ? Clear separation of concerns
- ? Easy to test each layer independently
- ? Changes in one layer don't affect others
- ? Code is more maintainable

---

### **2. REPOSITORY PATTERN**

**Definition:** Abstracts data access logic and provides a collection-like interface for accessing data.

**Implementation in FixItNow:**

```csharp
// Domain Layer - Interface (Contract)
public interface IUserRepository
{
    Task<User> GetByIdAsync(int userId);
    Task<User> GetByUsernameAsync(string username);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task<List<User>> GetAllAsync();
}

// Infrastructure Layer - Implementation
public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(MongoDbContext context)
    {
        _users = context.Users;
    }

    public async Task<User> GetByIdAsync(int userId)
    {
        var filter = Builders<User>.Filter.Eq(u => u.UserId, userId);
        return await _users.Find(filter).FirstOrDefaultAsync();
    }
    
    // ... other methods
}
```

**Repositories Used:**
- ? UserRepository
- ? TicketRepository
- ? TicketAssignmentRepository
- ? AuditLogRepository
- ? NotificationRepository

**Benefits:**
- ? Decouples business logic from data access
- ? Easy to swap database (MongoDB ? SQL)
- ? Testability (mock repositories)
- ? Single responsibility

---

### **3. DEPENDENCY INJECTION (DI) PATTERN**

**Definition:** Objects receive their dependencies from external sources rather than creating them.

**Implementation in FixItNow:**

```csharp
// Program.cs - Dependency Container Setup
private static ServiceProvider ConfigureServices(IConfiguration configuration)
{
    var services = new ServiceCollection();

    // Register singleton services
    services.AddSingleton<MongoDbContext>();
    services.AddSingleton(mongoSettings);

    // Register scoped services
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<ITicketRepository, TicketRepository>();
    services.AddScoped<IAuthenticationService, AuthenticationService>();
    services.AddScoped<ITicketService, TicketService>();

    // Register transient services
    services.AddTransient<MainMenu>();
    services.AddTransient<ResidentMenu>();
    services.AddTransient<AdminMenu>();
    services.AddTransient<TechnicianMenu>();

    return services.BuildServiceProvider();
}
```

**Lifetimes Used:**
1. **Singleton** - `MongoDbSettings` (one instance for app lifetime)
2. **Scoped** - Repositories and Services (one per request/operation)
3. **Transient** - Menus (new instance each time)

**Benefits:**
- ? Loose coupling between classes
- ? Easy testing (inject mocks)
- ? Centralized configuration
- ? Reduced complexity

---

### **4. UNIT OF WORK PATTERN**

**Definition:** Manages a collection of repositories and coordinates database operations.

**Implementation:**

```csharp
public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    ITicketRepository Tickets { get; }
    ITicketAssignmentRepository TicketAssignments { get; }
    IAuditLogRepository AuditLogs { get; }
    INotificationRepository Notifications { get; }
    
    Task<int> CompleteAsync();
}

public class UnitOfWork : IUnitOfWork
{
    public IUserRepository Users { get; private set; }
    public ITicketRepository Tickets { get; private set; }
    public ITicketAssignmentRepository TicketAssignments { get; private set; }
    public IAuditLogRepository AuditLogs { get; private set; }
    public INotificationRepository Notifications { get; private set; }

    public UnitOfWork(MongoDbContext context)
    {
        Users = new UserRepository(context);
        Tickets = new TicketRepository(context);
        TicketAssignments = new TicketAssignmentRepository(context);
        AuditLogs = new AuditLogRepository(context);
        Notifications = new NotificationRepository(context);
    }
}
```

**Benefits:**
- ? Centralized repository access
- ? Coordinates multiple operations
- ? Better transaction management
- ? Cleaner service layer code

---

### **5. SERVICE LAYER PATTERN**

**Definition:** Business logic encapsulated in service classes that coordinate repositories and apply rules.

**Example - TicketService:**

```csharp
public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ITicketAssignmentRepository _assignmentRepository;
    private readonly IAuditLogRepository _auditRepository;
    private readonly INotificationRepository _notificationRepository;

    public async Task<Ticket> CreateTicketAsync(CreateTicketRequest request)
    {
        // Business Logic
        var allTickets = await _ticketRepository.GetAllAsync();
        var nextTicketId = allTickets.Any() ? allTickets.Max(t => t.TicketId) + 1 : 1;

        var ticket = new Ticket
        {
            TicketId = nextTicketId,
            TicketCode = $"TKT-{nextTicketId:D5}",
            Title = request.Title,
            Description = request.Description,
            // ... more properties
        };

        await _ticketRepository.AddAsync(ticket);
        await CreateNotificationAsync(...);
        await LogAuditAsync(...);

        return ticket;
    }
}
```

**Services Implemented:**
- ? TicketService - Ticket management
- ? UserService - User management
- ? AuthenticationService - Authentication

**Benefits:**
- ? Centralized business logic
- ? Reusable across layers
- ? Easy to test
- ? Follows Single Responsibility

---

### **6. DATA TRANSFER OBJECT (DTO) PATTERN**

**Definition:** Simplifies data transfer between layers without exposing domain entities directly.

**Example DTOs:**

```csharp
// CreateTicketRequest.cs
public class CreateTicketRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public int LocationId { get; set; }
    public int PriorityId { get; set; }
    public int CreatedByUserId { get; set; }
}

// RegisterUserRequest.cs
public class RegisterUserRequest
{
    public int RoleId { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
}
```

**Benefits:**
- ? Separates API contract from domain entities
- ? Input validation
- ? Flexible data mapping
- ? Security (doesn't expose sensitive fields)

---

### **7. DOMAIN-DRIVEN DESIGN (DDD) PRINCIPLES**

**Definition:** Focuses on the core domain and domain logic.

**Entities in Domain:**

```
User (Entity with identity)
??? UserId (Unique identifier)
??? RoleId (Foreign key to Role)
??? Username (Business attribute)
??? Email, Phone, FullName
??? IsActive (Business state)

Ticket (Entity with identity)
??? TicketId (Unique identifier)
??? TicketCode (Business code like TKT-00001)
??? Title, Description
??? CategoryId, LocationId, PriorityId, StatusId
??? CreatedAt, UpdatedAt, ResolvedAt, ClosedAt

Value Objects:
??? Role
??? Category
??? Priority
??? TicketStatus
??? Location
??? Notification
??? AuditLog
??? Feedback
```

**Benefits:**
- ? Models business domain accurately
- ? Clear relationships between entities
- ? Business logic closer to domain
- ? Maintainable codebase

---

### **8. CONFIGURATION PATTERN**

**Definition:** External configuration management for environment-specific settings.

**Implementation:**

```json
// appsettings.json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb+srv://...",
    "DatabaseName": "FixItNowDB"
  }
}
```

```csharp
// ConfigurationBuilder loads from appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Injected as dependency
var mongoSettings = new MongoDbSettings
{
    ConnectionString = configuration["MongoDbSettings:ConnectionString"],
    DatabaseName = configuration["MongoDbSettings:DatabaseName"]
};
```

**Benefits:**
- ? Environment-independent
- ? Easy configuration changes
- ? Secure credential management
- ? No hardcoded values

---

### **9. ASYNC/AWAIT PATTERN**

**Definition:** Asynchronous programming for non-blocking operations.

**Usage throughout FixItNow:**

```csharp
// Repository
public async Task<User> GetByIdAsync(int userId)
{
    var filter = Builders<User>.Filter.Eq(u => u.UserId, userId);
    return await _users.Find(filter).FirstOrDefaultAsync();
}

// Service
public async Task<Ticket> CreateTicketAsync(CreateTicketRequest request)
{
    await _ticketRepository.AddAsync(ticket);
    await CreateNotificationAsync(...);
    await LogAuditAsync(...);
    return ticket;
}

// Menu (UI)
public async Task ShowAsync()
{
    var user = await _authService.LoginAsync(username, password);
    await _ticketService.CreateTicketAsync(request);
}

// Main Entry
static async Task Main(string[] args)
{
    await seeder.SeedAllAsync();
    await mainMenu.ShowAsync();
}
```

**Benefits:**
- ? Non-blocking database operations
- ? Better resource utilization
- ? Responsive UI
- ? Scalable application

---

### **10. ENTITY-RELATIONSHIP PATTERN (NoSQL)**

**Definition:** MongoDB document relationships for data modeling.

**Relationships in FixItNow:**

```
User (Collection)
  ??? Has many Tickets (via CreatedByUserId)
  ??? Has many TicketAssignments (via TechnicianId)
  ??? Has one Role (via RoleId)

Ticket (Collection)
  ??? Belongs to User (via CreatedByUserId)
  ??? Belongs to Technician (via CurrentTechnicianId)
  ??? Has many TicketAssignments
  ??? Has many Notifications
  ??? Has many AuditLogs
  ??? Belongs to Category
  ??? Belongs to Priority
  ??? Belongs to TicketStatus
  ??? Belongs to Location

TicketAssignment (Collection)
  ??? Belongs to Ticket (via TicketId)
  ??? Belongs to Technician (via TechnicianId)
  ??? Assigned by Admin (via AssignedByAdminId)

AuditLog (Collection)
  ??? Tracks changes across all entities

Notification (Collection)
  ??? Notifies users of important events
```

---

### **11. ROLE-BASED ACCESS CONTROL (RBAC)**

**Definition:** Restricts system access based on user roles.

**Roles Implemented:**
```
1. RESIDENT (RoleId = 1)
   ??? Create Tickets
   ??? View My Tickets
   ??? Cannot assign or resolve tickets

2. ADMIN (RoleId = 2)
   ??? View All Tickets
   ??? Assign Tickets to Technicians
   ??? View All Users
   ??? Manage the system

3. TECHNICIAN (RoleId = 3)
   ??? View Assigned Tickets
   ??? Update Ticket Status
   ??? Work on assigned tickets only
```

**Implementation:**
```csharp
// MainMenu.cs - Route based on role
switch (user.RoleId)
{
    case 1: // Resident
        await _residentMenu.ShowAsync();
        break;
    case 2: // Admin
        await _adminMenu.ShowAsync();
        break;
    case 3: // Technician
        await _technicianMenu.ShowAsync();
        break;
}
```

---

### **12. AUDIT LOGGING PATTERN**

**Definition:** Records all system activities for accountability and debugging.

**AuditLog Tracking:**
```csharp
public class AuditLog
{
    public int AuditId { get; set; }
    public int? TicketId { get; set; }
    public string EntityType { get; set; }
    public int EntityId { get; set; }
    public string Action { get; set; }  // CREATE, UPDATE, DELETE, ASSIGN
    public string Details { get; set; }
    public int PerformedByUserId { get; set; }
    public DateTime PerformedAt { get; set; }
}
```

**Logged Actions:**
- ? User registration
- ? User deactivation
- ? Ticket creation
- ? Ticket assignment
- ? Status changes
- ? Comments added

---

### **13. NOTIFICATION PATTERN**

**Definition:** Informs users of important events asynchronously.

**Notification Types:**
```csharp
public class Notification
{
    public int NotificationId { get; set; }
    public int UserId { get; set; }
    public int TicketId { get; set; }
    public string Type { get; set; }  // NEW_TICKET, ASSIGNED, STATUS_CHANGE
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

**Notification Events:**
- ? New ticket created (notify admins)
- ? Ticket assigned (notify technician)
- ? Status changed (notify resident)

---

## ?? **DATA FLOW DIAGRAM**

```
USER INPUT (Console Menu)
        ?
        ?
PRESENTATION LAYER (MainMenu/AdminMenu/etc)
        ?
        ?
APPLICATION LAYER (Service)
        ??? Business Logic
        ??? Validation
        ??? Coordination
        ?
        ?
REPOSITORY LAYER
        ??? IUserRepository ? UserRepository
        ??? ITicketRepository ? TicketRepository
        ??? INotificationRepository ? NotificationRepository
        ?
        ?
MONGODB DRIVER
        ?
        ?
MONGODB ATLAS (Cloud Database)
        ?
        ?
RESPONSE BACK TO USER
```

---

## ?? **DESIGN PATTERNS SUMMARY TABLE**

| Pattern | Purpose | Status |
|---------|---------|--------|
| Layered Architecture | Separation of concerns | ? Implemented |
| Repository | Abstract data access | ? Implemented |
| Dependency Injection | Loose coupling | ? Implemented |
| Unit of Work | Coordinate operations | ? Implemented |
| Service Layer | Business logic | ? Implemented |
| DTO | Safe data transfer | ? Implemented |
| Domain-Driven Design | Model domain accurately | ? Implemented |
| Configuration Pattern | External settings | ? Implemented |
| Async/Await | Non-blocking operations | ? Implemented |
| RBAC | Role-based access | ? Implemented |
| Audit Logging | System accountability | ? Implemented |
| Notification | Event notification | ? Implemented |

---

## ?? **KEY ARCHITECTURAL BENEFITS**

### **1. Maintainability**
- ? Clear layer separation
- ? Each component has single responsibility
- ? Easy to locate and fix bugs

### **2. Scalability**
- ? Can add more repositories without touching service layer
- ? Easy to add new menus or features
- ? MongoDB supports horizontal scaling

### **3. Testability**
- ? Each layer can be tested independently
- ? Interfaces allow mocking
- ? Dependency injection facilitates unit testing

### **4. Reusability**
- ? Services used across different menu layers
- ? Repositories standardized for all entities
- ? DTOs reused across API endpoints

### **5. Flexibility**
- ? Easy to swap MongoDB with SQL database
- ? Can switch DI container
- ? Configuration externalized

---

## ?? **TECHNOLOGY STACK**

```
Backend Framework:     .NET Framework 4.7.2
Language:              C# 7.3
Database:              MongoDB Atlas
DI Container:          Microsoft.Extensions.DependencyInjection
Configuration:         Microsoft.Extensions.Configuration
Async Support:         System.Threading.Tasks
ODM:                   MongoDB.Driver
```

---

## ?? **PROJECT STATISTICS**

```
Total Layers:          4 (Domain, Application, Infrastructure, Presentation)
Total Entities:        13
Total Repositories:    5
Total Services:        3
Total Interfaces:      8
Total DTOs:            2
Total Menus:           4
Total Enums:           1 (TicketStatusEnum)
Total Roles:           3 (Resident, Admin, Technician)
```

---

## ? **CONCLUSION**

FixItNow demonstrates **professional enterprise architecture** with:

1. **Clear separation of concerns** through layered architecture
2. **Abstraction of data access** via repository pattern
3. **Loose coupling** through dependency injection
4. **Scalable design** supporting future enhancements
5. **Best practices** for .NET applications
6. **Comprehensive patterns** for production-grade software

This architecture is suitable for:
- ? Medium-scale applications
- ? Team development
- ? Long-term maintenance
- ? Easy testing and debugging
- ? Future scaling and expansion

---

**This application follows SOLID principles and industry best practices!** ?
