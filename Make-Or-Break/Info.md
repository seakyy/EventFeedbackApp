# What is Make-Or-Break?

A **Make-Or-Break feature** is a core functionality that determines the overall success of the project. Without it, the application would fail to deliver its purpose. These features must be functional, testable, and demonstrably useful to the end user.

In this project (EventFeedbackApp), the following three features are defined as Make-Or-Break:

---

## 1. Dynamic Question Engine
**Description:**
Allows admins to create and manage different types of feedback questions (Single-Choice, Rating, Text).

**Proof of Concept:**
Implemented as a CRUD flow using Razor Pages. Questions are tied to sessions and saved in a SQLite database. UI allows dynamic addition of options.

---

## 2. JoinSession Flow & Mobile Usability
**Description:**
Participants can join feedback sessions using a session ID or QR code. The question interface is optimized for mobile devices.

**Proof of Concept:**
Tested manually with ngrok + mobile browser. URLs are session-specific and publicly accessible. QR codes link to live forms.

---

## 3. Real-time Dashboard with SignalR
**Description:**
Displays feedback results as live-updating charts in the admin view.

**Proof of Concept:**
Chart.js renders bar/pie charts. SignalR pushes updates instantly as participants submit responses. Admins can monitor results in real time.

---
