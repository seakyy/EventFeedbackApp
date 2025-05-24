# EventFeedbackApp
EventFeedbackApp is a web-based platform that enables event organizers to create interactive feedback sessions. Participants can join via QR code or session ID, answer questions in real-time, and view live results through dynamic charts.

| Make-or-Break Feature                    | Description                                                                                     | Proof of Concept (45′)                                             |
|------------------------------------------|-------------------------------------------------------------------------------------------------|--------------------------------------------------------------------|
| **Dynamic Question Engine**              | Allows admin to create and manage various question types (Multiple-Choice, Rating, Text).        | Implement simple CRUD form in Razor Pages.                         |
| **Mobile UX & Session Onboarding**       | Enables participants to join via QR code or session ID and view questions on mobile devices.    | Build static HTML page with QR-code input and dummy questions.     |
| **Real-time Dashboard & Data Pipeline**  | Live updates of responses in bar and pie charts in the admin dashboard.                         | Integrate Chart.js with static data and simple polling.            |


# Overview Diagram

![diagram](https://github.com/user-attachments/assets/73975b94-94fc-4198-beda-c22589275e70)


## 🌐 Accessible Endpoints

-  **`/Admin/CreateSession`**
-  **`/Feedback/JoinSession`**
-  **`/Feedback/Questions/1`**
-  **`/Admin/Results/1`**


## Project Structure (full)

```plaintext
/src
├── Data/
│   └── AppDbContext.cs            → Database context for the application
│
├── Hubs/
│   └── FeedbackHub.cs             → SignalR hub for real-time feedback
│
├── Migrations/                    → (Unable to retrieve details at this time)
│
├── Models/
│   ├── Option.cs                  → Represents an option for a question
│   ├── Question.cs                → Represents a question entity
│   ├── Response.cs                → Represents a response to a question
│   └── Session.cs                 → Represents a feedback session
│
├── Pages/
│   ├── Admin/                     → (Directory for admin-related pages)
│   │   ├── CreateQuestion.cshtml          → View to create a new question
│   │   ├── CreateQuestion.cshtml.cs       → Code-behind for CreateQuestion view
│   │   ├── CreateSession.cshtml           → View to create a new session
│   │   ├── CreateSession.cshtml.cs        → Code-behind for CreateSession view
│   │   ├── Results.cshtml                 → View to display results
│   │   ├── Results.cshtml.cs              → Code-behind for Results view
│   │   ├── ResultsPolling.cshtml          → Results view with polling functionality
│   │   ├── ResultsPolling.cshtml.cs       → Code-behind for ResultsPolling view
│   │   ├── ResultsSignalR.cshtml          → Results view with SignalR integration
│   │   └── ResultsSignalR.cshtml.cs       → Code-behind for ResultsSignalR view
│   │   
│   ├── Feedback/                  → (Directory for feedback-related pages)
│   │   ├── JoinSession.cshtml             → View to join a feedback session
│   │   ├── JoinSession.cshtml.cs          → Code-behind for JoinSession view
│   │   ├── Qrtest.cshtml                  → View for QR code testing
│   │   ├── Qrtest.cshtml.cs               → Code-behind for Qrtest view
│   │   ├── Questions.cshtml               → View to display questions in a session
│   │   └── Questions.cshtml.cs            → Code-behind for Questions view
│   │   
│   ├── Shared/                    → (Directory for shared views)
│   │  ├── _Layout.cshtml                 → Layout file for shared views
│   │  ├── _Layout.cshtml.css             → CSS file associated with the layout
│   │  └── _ValidationScriptsPartial.cshtml → Partial view for validation scripts
│   │   
│   ├── Error.cshtml               → Error page view
│   ├── Error.cshtml.cs            → Code-behind for the error page
│   ├── Index.cshtml               → Main landing page view
│   ├── Index.cshtml.cs            → Code-behind for the main landing page
│   ├── Privacy.cshtml             → Privacy policy page view
│   ├── Privacy.cshtml.cs          → Code-behind for the privacy policy page
│   ├── _ViewImports.cshtml        → View imports for Razor views
│   └── _ViewStart.cshtml          → Startup logic for Razor views
│
├── Properties/
│   └── launchSettings.json        → Launch settings for the application
│
├── wwwroot/
│   ├── css/                       → Directory for CSS files
│   ├── favicon.ico                → Favicon for the application
│   ├── js/                        → Directory for JavaScript files
│   └── lib/                       → Directory for third-party libraries
│
├── EventFeedbackApp.csproj        → Project configuration file
├── EventFeedbackApp.csproj.user   → User-specific project settings
├── EventFeedbackApp.sln           → Solution file
├── Program.cs                     → Main entry point of the application
├── appsettings.json               → Application settings
├── appsettings.Development.json   → Development-specific settings
└── feedback.db                    → SQLite or similar database file
```

