![{8F3C0AC3-33F6-40D0-BC89-BFD31C3C2121}](https://github.com/user-attachments/assets/fc7b7ef0-06a1-48f7-891e-4eec2b444877)# Kindergarten Management System

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
### PIC: Wang Siew You - Client-side: Parent
### Registration
<ol>
  <li> 
    <b>Register an account</b> <br>
    <img src="https://github.com/user-attachments/assets/fe674958-37d7-4c52-8c30-01f44f5488f4" width="550" title="Parents account registration">
  </li>
  
  <li> 
    <b>Error Message will be displayed if the user inputs the wrong information</b> <br>
    <img src="https://github.com/user-attachments/assets/f234f259-c171-4b44-9034-17e0e0e9c70e" width="550" title="Inputs validation">
  </li>
  <li> 
    <b>Email Activation</b> <br>
    <img src="https://github.com/user-attachments/assets/a017bce4-cd4b-4f66-ad0d-4fb672236e73" width="350" title="Successful register an account">
    <img src="https://github.com/user-attachments/assets/19fcbd69-90db-4cd4-aa2b-a7f96cf003fa" width="350" title="Activation Email"/>
    <p align="justify">
      After a user registers an account, the account is not activated. The user will receive an activation email, and the user needs to click on it to activate the account. If a user registers the account but does not activate the account, a message showing the account is not activated will display when the user tries to log in.
    </p>
  </li>
</ol>

### Login and Logout
<ol>
  <li> 
    <b>Google Recaptcha in Login page</b> <br>
    <img src="https://github.com/user-attachments/assets/3c908b87-819b-4312-ad0e-c396194f231d" width="350" title="Activation Email"/><br>
    <p align="justify">
      Users simply click a checkbox indicating they are human. This is followed by a challenge-response test if necessary. Automatically triggers verification when users click on an existing button on the site,         with no need for an explicit checkbox.
    </p>
    <b>The error message will display where the email is not activated</b> <br>
    <img src="https://github.com/user-attachments/assets/23d9729b-c7d2-494e-842f-da1aaf544952" width="350" title="Email validation"/>
  </li>
 <li> 
    <b>The error message will display where the email is not activated</b> <br>
    <img src="https://github.com/user-attachments/assets/23d9729b-c7d2-494e-842f-da1aaf544952" width="350" title="Email validation"/>
  </li>
  <li> 
    <b>A successful message will display when the user logs in correctly</b> <br>
    <img src="https://github.com/user-attachments/assets/4a01d337-7a9b-48a0-af5f-6a0147888ec1" width="550" title="">
  </li>
</ol>

### Update Password
<ol>
  <li> 
    <b>Update Password Page for the user to update the password</b> <br>
    <img src="https://github.com/user-attachments/assets/0cac1217-bc36-4ee7-9b9b-01cee162c92d" width="550" title="">
  </li>
  <li> 
    <b>The error message will be displayed showing the current password does not match</b> <br>
    <img src="https://github.com/user-attachments/assets/ee9f3738-76f1-4421-93e7-8752fa8b4b1d" width="550" title="">
  </li>
  <li> 
    <b> A successful message will display when a user successfully updates the password</b> <br>
    <img src="https://github.com/user-attachments/assets/87eec0c4-fef4-4b7a-99a0-6faa117ffb90" width="550" title="">
  </li>
</ol>

### Update Profile
<ol>
  <li> 
    <b>The updated profile displays the information of the user to let the user change.</b> <br>
    <img src="https://github.com/user-attachments/assets/2d89d7a3-7f16-40d6-b433-d5df3a1cbb40" width="550" title="">
  </li>
  <li> 
    <b>After updating the profile, the user information will be changed.</b> <br>
    <img src="https://github.com/user-attachments/assets/67d366d4-3b7b-4453-a4e8-55a202524292" width="550" title="">
  </li>
  <li> 
    <b> After the user logs out of the account, then will display the message. </b> <br>
    <img src="https://github.com/user-attachments/assets/f042475e-676e-4697-963b-c1c49bdf164c" width="550" title="">
  </li>
</ol>
<hr> 

## 2. User Account Maintenance
### PIC: Goh Qin Long - Admin-side: Admin, Tutor, Parents and Student
### Admin,Tutor, Parent & Student Account Maintenance (Create Section)
<ol>
  <li> 
    <b>Insert Admin Account</b> <br>
    <img src="https://github.com/user-attachments/assets/ffee59d1-faae-4782-b2f2-0828bd808772" width="550" title="">
  </li>
  <li> 
    <b>Insert Tutor Account</b> <br>
    <img src="https://github.com/user-attachments/assets/ae389536-886c-44a1-b550-1269ccf35f69" width="550" title="">
  </li>
  <li> 
    <b> Insert Parent Account</b> <br>
    <img src="https://github.com/user-attachments/assets/78cbf9cc-202d-4aa6-b7b2-6409bf3ab668" width="550" title="">
  </li>
  <li> 
    <b>Insert Student Account</b> <br>
    <img src="https://github.com/user-attachments/assets/56d1da22-13bf-4eff-9f0d-7e2cc81d2c9a" width="550" title="">
  </li>
  <li> 
    <b> Insert User Account Error Messages</b> <br>
    <img src="https://github.com/user-attachments/assets/8dbae440-96cd-4ca8-9670-ba261ae8ab03" width="550" title="">
  </li>
</ol>

### Searching, Filtering, Paging & Sort on Users List (Read Section) 
<ol>
  <li> 
    <b>View Users List</b> <br>
    <img src="https://github.com/user-attachments/assets/5c1bd204-a762-40c6-a0a8-47b6c0762d55" width="550" title="">
  </li>
  <li> 
    <b>View Users' Details (Admin, Tutor, and Parent detail layout is same)</b> <br>
    <img src="https://github.com/user-attachments/assets/bb457d93-bd7e-4e17-9c98-147b73b26671" width="550" title="">
    <p></p>
  </li>
  <li> 
    <b>View Student's Details</b> <br>
    <img src="https://github.com/user-attachments/assets/e1665e8b-6869-41c3-9035-c749560a5304" width="550" title="">
  </li>
</ol>

### Update User's Profile (Update Section) 
<ol>
  <li> 
    <b>Update Admin, Tutor, and Parent Profile Layout is Same</b> <br>
    <img src="https://github.com/user-attachments/assets/41731492-9456-4f3c-b0fa-486b0f9db1d5" width="550" title="">
  </li>
  <li> 
    <b>Update Student Profile</b> <br>
    <img src="https://github.com/user-attachments/assets/85b5ab5e-b233-4b75-96a7-6c39c638e92d" width="550" title="">
    <p></p>
  </li>
</ol>

### Delete Records (Delete Section) 
<ol>
  <li> 
    <b>Deleted User and Alert “Record Deleted” Message</b> <br>
    <img src="https://github.com/user-attachments/assets/c4385ba7-ecb3-4e68-9116-a9641b987fca" width="550" title="">
  </li>
</ol>

### Send Account Password to User by Email
<ol>
  <li> 
    <b>Send Account Password to User by Email</b> <br>
    <img src="https://github.com/user-attachments/assets/c19912ab-4787-433b-a27d-6942c4762326" width="550" title="" >
    <p align="justify">
      Admin allows the creation of an Eden Academy account for admin, tutor, and parents. When an Admin successfully creates an Eden Academy account, the system will send an email automatically based on what           email was created for the user. The information includes Email, password, and creation time and will be stored in a pdf file 
    </p>
  </li>
</ol>

### Google Map
<ol>
  <li>
    <br>
    <img src="https://github.com/user-attachments/assets/c78ce237-7f7f-4b86-937f-9661184f8059" width="550" title="" >
    <p align="justify">
      The Map allows users or guests can quickly know where our address is.
    </p>
  </li>
</ol>
<hr>

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
