Real-Time Notification System🚀
-------------------------------
This is a real-time notification system built using ASP.NET Core, SignalR, and JWT Authentication. The system allows users to receive notifications in real-time, and it also stores notification history in an SQL Server database. The system includes a RESTful API for sending and retrieving notifications.

⭐Features
-----------------
• Real-time notifications: Notifications are pushed to all connected clients in real-time using SignalR.

• Notification history: Notifications are stored in an SQL Server database and can be retrieved through the API.

• Authentication: JWT authentication is used to secure the endpoints and SignalR connections.

• Client-side interaction: Users can login, send notifications, and view received notifications in real-time.

🎯Project Structure
-----------------
• SignalR Hub: The NotificationHub handles the real-time communication and sends notifications to connected clients.

• API Endpoints:

----📌POST /api/auth/login: Authenticates a user and provides a JWT token.

----📌POST /api/notifications: Sends a new notification and saves it in the database.

----📌GET /api/notifications: Retrieves the notification history from the database.

• Database: SQL Server database is used to store notifications. The Notification model includes Id, Message, and SentDate.

🖥️Setup Instructions
-----------------
Prerequisites

• .NET Core 5.0 or higher

• SQL Server (LocalDB or a remote instance)

•Visual Studio or any other .NET-supported IDE

Steps to Run the Project

1️⃣ Clone the repository:
```bash
git clone https://github.com/GizemG6/StudyCase.git
```

2️⃣ Configure Database:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=NotificationDb;Trusted_Connection=True;"
}
```

• Open the appsettings.json file and update the connection string to your SQL Server instance.

3️⃣ Run the application:

•Open the solution in Visual Studio or your preferred IDE.

•Build and run the project.

4️⃣ Access the Web Pages:

• The first page opened is set to notifications.html but since the user is not logged in, it sends the user to index.hmtl

• Once logged in, you will be redirected to notifications.html where you can see and send real-time notifications.

🚩API Endpoints
-----------------
🕵🏽Authentication

POST /api/auth/login

Logs in the user and returns a JWT token.

Request:
```json
{
  "email": "admin@example.com",
  "password": "123456"
}
```

Response:
```json
{
  "token": "your-jwt-token-here"
}
```

🔔Notifications

POST /api/notifications

Sends a notification and saves it to the database.

Request:
```json
{
  "message": "Your notification message"
}
```

Response:
```json
{
  "message": "Notification sent successfully!"
}
```

🔔Notifications

GET /api/notifications

Retrieves the notification history.

Response:
```json
[
  {
    "id": 1,
    "message": "Your notification message",
    "sentDate": "2025-03-19T12:00:00Z"
  },
  ...
]
```

💻Client-Side
-----------------
🔎Login Page (index.html)

• Users can log in by providing their email and password.

• Upon successful login, a JWT token is saved to localStorage and the user is redirected to the notifications page.

🔔Notifications Page (notifications.html)

• Displays a list of real-time notifications.

• Users can send a notification by clicking the Send Notification button.

• Notifications are displayed using SweetAlert2 and added to a list dynamically.

• Notifications are received in real-time via SignalR.

💡SignalR Connection
-----------------
The SignalR connection is established using the JWT token for authentication. Once connected, the client can receive notifications in real-time.

🌐Sending Notifications
-----------------
Notifications can be sent via the Send Notification button, which opens a SweetAlert prompt for the user to enter a message. The message is then sent to the server, which pushes it to all connected clients.

🛠️Technologies Used
-----------------
• ASP.NET Core: Framework for building the RESTful API and SignalR Hub.

• SignalR: Real-time communication library used for pushing notifications.

• Entity Framework Core: ORM for interacting with SQL Server.

• JWT Authentication: Secures the API and SignalR Hub connections.

• SweetAlert2: Used for displaying notifications in the UI.

• SQL Server: Database for storing notification history.

📊SQL ScreenShot (some trial notifications)
-----------------
![Ekran görüntüsü 2025-03-20 001025](https://github.com/user-attachments/assets/41141150-23c1-4ac9-a0bb-e9d08445f4b8)

▶️Screenshots of the project in running
-----------------
![Ekran görüntüsü 2025-03-20 001615](https://github.com/user-attachments/assets/33b96988-abde-490d-b991-8ca6eec57ede)

![Ekran görüntüsü 2025-03-20 001631](https://github.com/user-attachments/assets/c6e0c899-67d7-43e6-b912-651c4d81cd1c)

📍Also, notifications from SQL is visible.

![Ekran görüntüsü 2025-03-20 003626](https://github.com/user-attachments/assets/6f34fbf5-f84a-4185-81ff-27ba3435b986)



