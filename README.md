# Kindergarten Management System

Kindergarten Management System with secure login, password recovery, account maintenance (Admin, Tutor, Parent, Student), subject/class allocation, attendance tracking (QR/manual), and data reports (charts). Includes features like timetables, warning letters, Google Maps, and real-time chat

<hr>

# Brief User Manual
<div align="center"> 
  <b>Screenshot of User Layout</b> <br>
  <img src="https://github.com/user-attachments/assets/e98922c3-7e0a-4995-b0ae-cf551100e157" width="550" title="Screenshot of User Layout"/>
</div>

<div align="center"> 
  <b>Screenshot of Staff Layout</b> <br>
  <img src="https://github.com/user-attachments/assets/7efcc3a4-2627-4b2d-96dc-99a4a6ed7c74" width="550" title="Screenshot of Staff Layout"/>
</div>

<hr>

#### 1. Default Username and Password
<div align="center"> 
  <b>Table of Default Username and Password</b> <br>
  <img src="https://github.com/user-attachments/assets/414744ff-57a3-4199-a2d2-84b689dec467" width="550" title="Table of Default Username and Password"/>
</div>


#### 2. Access Admin Panel
<div align="center"> 
  <p>To Access Admin Panel, use direct link after login https://localhost:7023/accountMaintenance/</p> 
</div>

<hr> 

# Project Features
## 1. Security
### PIC: Wang Siew You
#### Client-side: Parent
#### Register
<div align="center"> 
  <b>Register an account</b> <br>
  <img src="https://github.com/user-attachments/assets/fe674958-37d7-4c52-8c30-01f44f5488f4" width="550" title="Parents account registration">
</div>

<div align="center"> 
  <b>Error Message will be displayed if the user inputs the wrong information</b> <br>
  <img src="https://github.com/user-attachments/assets/f234f259-c171-4b44-9034-17e0e0e9c70e" width="550" title="Inputs validation">
</div>
<div align="center"> 
  <b>Email Activation</b> <br>
  <img src="https://github.com/user-attachments/assets/a017bce4-cd4b-4f66-ad0d-4fb672236e73" width="350" title="Successful register an account">
  <img src="https://github.com/user-attachments/assets/19fcbd69-90db-4cd4-aa2b-a7f96cf003fa" width="350" title="Activation Email"/>
  <p align="justify">
    After a user registers an account, the account is not activated. The user will receive an activation email, and the user needs to click on it to activate the account. If a user registers the account but does not activate the account, a message showing the account is not activated will display when the user tries to log in.
  </p>
</div>

#### Login and Logout
<div align="center"> 
  <b>Google Recaptcha in Login page</b> <br>
  <img src="https://github.com/user-attachments/assets/3c908b87-819b-4312-ad0e-c396194f231d" width="350" title="Activation Email"/><br>
  <b>The error message will display where the email is not activated</b> <br>
  <img src="https://github.com/user-attachments/assets/23d9729b-c7d2-494e-842f-da1aaf544952" width="350" title="Email validation"/>
  <p align="justify">
    Users simply click a checkbox indicating they are human. This is followed by a challenge-response test if necessary. Automatically triggers verification when users click on an existing button on the site, with no need for an explicit checkbox.
  </p>
</div>

Password Hashing
Password Recovery (Email)


<hr> 

2. User Account Maintenance
PIC: Goh Qin Long
Admin Account Maintenance (CRUD)
Tutor Account Maintenance (CRUD)
Parent Account Maintenance (CRUD)
Student Account Maintenance (CRUD)
Searching, Filtering, Paging & Sort
Profile Photo Upload (Admin, Tutor, Parent and Student)
Profile Photo Resizing & Cropping
Send Account Password to User by Email

3. Subject & Class Allocation
PIC: Leong Zhi Yen
Subject Maintenance (CRUD)
Class Maintenance (CRUD)
Class-Subject Allocation
Tutor-Class Allocation
Student-Class Allocation
Searching, Filtering & Paging
Tutor and student timetable
Send timetable to tutor and student’s parent by Email

4. Attendance Tracking
PIC: Yong Choy Mun
Class Calendar (Daily/Weekly)
Class-Student Attendance List
Attendance Taking (Manual)
Attendance Taking (QR Code Generating)
Attendance Taking (QR Code Scanning)
Warning Letter (Email)

5. Data Report
PIC: Wang Siew You
User Roles Count (Pie Chart)
User Count By Role By Gender By Activity (Column Chart)
PIC: Goh QIn Long
Gender Count by Role (Pie Chart)
Age Group Distribution of Parents (Column Chart)
PIC: Leong Zhi Yen
Class Allocation Report (Pie Chart)
Student-Class Report (Column Chart)
PIC: Yong Choy Mun 
Class Attendance Report (Column Chart)
Top 10 Absent Student By Month (Column)

6. Others
PIC: Goh Qin Long
Google Maps (Store Location)
PIC: Leong Zhi Yen
Real-Time Chat Room (Public)
