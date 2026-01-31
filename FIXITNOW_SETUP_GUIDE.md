# ?? FixItNow Application - Complete Setup & Testing Guide

## ? Build Status: **SUCCESSFUL**

Your entire application compiles without any errors!

---

## ?? PROJECT STRUCTURE

```
FixItNow (Solution)
??? FixItNow.Domain/          (Core entities & interfaces)
?   ??? Entities/             (User, Ticket, Role, etc.)
?   ??? Enums/                (TicketStatusEnum, etc.)
?   ??? Interfaces/           (Repository contracts)
?
??? FixItNow.Application/     (Business logic)
?   ??? Services/             (TicketService, UserService, AuthenticationService)
?   ??? DTOs/                 (RegisterUserRequest, CreateTicketRequest)
?   ??? Interfaces/           (ITicketService, IUserService, etc.)
?
??? FixItNow.Infrastructure/  (Data access & MongoDB)
?   ??? Data/                 (MongoDbContext, SeedData, MongoDbSettings)
?   ??? Repositories/         (UserRepository, TicketRepository, etc.)
?   ??? UnitOfWork/           (UnitOfWork pattern)
?
??? FixItNow.Presentation/    (Console UI)
    ??? Menus/                (MainMenu, AdminMenu, ResidentMenu, TechnicianMenu)
    ??? Helpers/              (ConsoleHelper)
    ??? Program.cs            (Main entry point with DI setup)
    ??? appsettings.json      (Configuration)
```

---

## ?? Default Test Credentials

After the app runs and seeds the database, you can login with:

### **Admin Account**
- **Username:** `admin`
- **Password:** `admin123`

### **Technician Account**
- **Username:** `tech1`
- **Password:** `tech123`

### **Resident Account**
- **Username:** `resident1`
- **Password:** `resident123`

---

## ??? Database Seeding

When you first run the app, it automatically seeds:

### **Roles** (3 roles)
- 1 = Resident
- 2 = Admin
- 3 = Technician

### **Categories** (5 categories)
- Plumbing
- Electric
- WiFi
- Furniture
- Other

### **Priorities** (4 levels)
- Low
- Medium
- High
- Urgent

### **Ticket Statuses** (6 statuses)
- Open
- Assigned
- InProgress
- Resolved
- Closed
- Reopened

### **Locations** (4 locations)
- Boys Hostel A - Block 1, Ground, Room 101
- Boys Hostel A - Block 1, First Floor, Room 201
- Girls Hostel B - Block 2, Ground, Room 102
- Common Area - Main, Ground, Cafeteria

### **Default Users** (3 users - see credentials above)

---

## ??? HOW TO RUN

### **Step 1: Set Startup Project**
1. Right-click on **`FixItNow.Presentation`** in Solution Explorer
2. Select **"Set as Startup Project"**

### **Step 2: Ensure MongoDB Atlas Whitelist**
1. Go to [MongoDB Atlas](https://www.mongodb.com/cloud/atlas)
2. Log in with your account
3. Go to **Network Access**
4. Ensure your IP is whitelisted (or add 0.0.0.0/0 for development)

### **Step 3: Update Connection String (if needed)**
Edit `FixItNow.Presentation\appsettings.json`:
```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb+srv://em1804683:<db_password>@cluster0.9jmdcfb.mongodb.net/?appName=Cluster0",
    "DatabaseName": "FixItNowDB"
  }
}
```

?? **Replace `<db_password>` with your actual MongoDB password**

If your password contains special characters, URL-encode them:
- `@` ? `%40`
- `#` ? `%23`
- `:` ? `%3A`

### **Step 4: Run the Application**
- **Press `F5`** to start debugging
- **Or press `Ctrl+F5`** to run without debugging

---

## ?? APPLICATION FLOW

### **Main Menu ? Login**
```
???????????????????????????????
?    FixItNow System          ?
? Hostel Maintenance Ticket   ?
?        Manager              ?
???????????????????????????????
? 1. Login                    ?
? 2. Exit                     ?
???????????????????????????????
```

### **After Login - Role-Based Routing**

#### **?? Resident Menu**
- View My Tickets
- Create New Ticket
- Logout

#### **?? Technician Menu**
- View My Assigned Tickets
- Update Ticket Status
- Logout

#### **????? Admin Menu**
- View All Tickets
- Assign Tickets to Technicians
- View All Users
- Logout

---

## ?? SERVICES OVERVIEW

### **AuthenticationService**
- `LoginAsync(username, password)` - Authenticate user
- `LogoutAsync()` - Clear current user session
- `GetCurrentUser()` - Get logged-in user

### **TicketService**
- `CreateTicketAsync()` - Create new ticket
- `AssignTicketAsync()` - Assign to technician
- `ChangeStatusAsync()` - Update ticket status
- `GetAllTicketsAsync()` - List all tickets
- `GetMyTicketsAsync(userId)` - User's tickets
- `GetAssignedTicketsAsync(technicianId)` - Technician's tickets

### **UserService**
- `RegisterUserAsync()` - Register new user
- `GetUserByIdAsync()` - Get user details
- `GetAllUsersAsync()` - List all users
- `GetTechniciansAsync()` - List all technicians
- `DeactivateUserAsync()` - Deactivate user account

---

## ??? REPOSITORIES

All repositories are MongoDB-based:
- `IUserRepository` ? `UserRepository`
- `ITicketRepository` ? `TicketRepository`
- `ITicketAssignmentRepository` ? `TicketAssignmentRepository`
- `IAuditLogRepository` ? `AuditLogRepository`
- `INotificationRepository` ? `NotificationRepository`

---

## ?? COMMON ISSUES & SOLUTIONS

### **? "Connection Refused"**
**Solution:** Check that MongoDB Atlas is running and your IP is whitelisted

### **? "appsettings.json not found"**
**Solution:** Ensure `appsettings.json` is in the `FixItNow.Presentation` folder

### **? "Login failed: User not found"**
**Solution:** Use default credentials: `admin` / `admin123`

### **? "Type or namespace name does not exist"**
**Solution:** All project references should be fixed now. Rebuild if needed.

---

## ?? FEATURES IMPLEMENTED

? **User Management**
- User registration
- User authentication
- Role-based access (Resident, Admin, Technician)
- User deactivation

? **Ticket Management**
- Create tickets
- Assign to technicians
- Update status
- Track ticket lifecycle

? **Audit Logging**
- Track all user actions
- Timestamp all operations

? **Notifications**
- Notify admins of new tickets
- Notify technicians of assignments
- Notify residents of status changes

? **Data Seeding**
- Auto-seed roles, categories, priorities, statuses, locations, and default users

---

## ?? NEXT STEPS

1. **Run the application** (`F5` or `Ctrl+F5`)
2. **Login with test credentials** (see above)
3. **Create a ticket** as Resident
4. **Assign ticket** as Admin
5. **Update status** as Technician
6. **Monitor audit logs** for all operations

---

## ?? NOTES

- **C# Version:** 7.3
- **.NET Framework:** 4.7.2
- **Database:** MongoDB Atlas
- **DI Container:** Microsoft.Extensions.DependencyInjection
- **Async/Await:** Fully supported throughout

---

**Your application is ready to run! Enjoy FixItNow!** ??
