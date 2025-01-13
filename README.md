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

### Report1 - User Roles Count (Pie Chart)
  <img src="https://github.com/user-attachments/assets/0b0f61d4-6846-413a-a7be-dbaf1c4dd3b3" width="550" title="" >


### Report2 - User Count By Role By Gender By Activity (Column Chart)
  <img src="https://github.com/user-attachments/assets/885835bf-5ef5-40f6-9ebf-3303cc12c8d3" width="550" title="" >
 

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

### Report1 - Gender Count by Role (Pie Chart)
  <img src="https://github.com/user-attachments/assets/e11cc73e-ae74-4d67-ae1e-000af0ce6b85" width="550" title="" >

### Report2 - Age Group Distribution of Parents (Column Chart)
  <img src="https://github.com/user-attachments/assets/88590a54-f380-4f49-bff0-6553ba8fa180" width="550" title="" >

<hr>

## 3. Subject & Class Allocation
### PIC: Leong Zhi Yen - Admin side: Admin, Tutor
### Class Maintenance (CRUD)
<ol>
  <li> 
    <b>Create a Class</b> <br>
    <img src="https://github.com/user-attachments/assets/b1f7b50b-9eae-421a-aa90-768e5fde7458" width="550" title="">
    <p align="justify">Admin have to create a class first before creating a subject</p>
  </li>
  <li> 
    <b>View Classes</b> <br>
    <img src="https://github.com/user-attachments/assets/5b032daf-8cb5-430e-bd96-99c3707ed9b1" width="550" title="">
    <p align="justify">Display the classes that have been created</p>
  </li>
  <li> 
    <b>Update Class Details</b> <br>
    <img src="https://github.com/user-attachments/assets/d16f808a-0441-4767-bbfb-9627493efebe" width="550" title="">
    <p align="justify"> Admin can update the classes when the class capacity is 0</p>
  </li>
  <li> 
    <b>Error Message - Update Class Details</b> <br>
    <img src="https://github.com/user-attachments/assets/040c8bbe-eb95-40b3-ab38-183113aac744" width="550" title="">
    <p align="justify"> Admin are unable to update the classes when the class capacity is more than 0</p>
  </li>
  <li> 
    <b>Delete Class</b> <br>
    <img src="https://github.com/user-attachments/assets/8920ea59-6c38-4150-92b7-7441c9f90cd3" width="550" title="">
    <p align="justify"> Admin can delete the classes when the class capacity is 0</p>
  </li>
  <li> 
    <b>Error Message - Delete Class</b> <br>
    <img src="https://github.com/user-attachments/assets/93a2efce-72f5-4307-bcca-0094f59711d2" width="550" title="">
    <p align="justify">Admin cannot delete the classes when there are students allocated inside the class.</p>
  </li>
</ol>

### Subject Maintenance (CRUD)
<ol>
  <li> 
    <b>Create a Subject</b> <br>
    <img src="https://github.com/user-attachments/assets/a1f25308-83fb-40bd-ac55-1366bc687c9b" width="550" title="">
    <p align="justify">Admin can create a subject</p>
  </li>
  <li> 
    <b>View Subjects</b> <br>
    <img src="https://github.com/user-attachments/assets/691dc831-74bb-452c-905b-1ea5dae80849" width="550" title="">
    <p align="justify">Successfully created a subject.</p>
  </li>
  <li> 
    <b>Update Subject's Details</b> <br>
    <img src="https://github.com/user-attachments/assets/f17cc459-73d2-48cf-a110-1f47de1503d5" width="550" title="">
    <p align="justify"> Update a subject (SU002) tutor name</p>
  </li>
  <li> 
    <b>Successful Message - Update Class Details</b> <br>
    <img src="https://github.com/user-attachments/assets/4db87d51-b0f1-46e9-94bd-eec9aab43332" width="550" title="">
    <p align="justify"> Successfully updated a subject (SU002) tutor name</p>
  </li>
  <li> 
    <b>Delete Subject</b> <br>
    <img src="https://github.com/user-attachments/assets/21e7006e-452a-4bed-93c5-595d7186c8f9" width="550" title="">
    <p align="justify"> Successfully delete subject (SU002) as it has not been assigned to a class</p>
  </li>
  <li> 
    <b>Error Message - Delete Subject</b> <br>
    <img src="https://github.com/user-attachments/assets/a5a02127-51f0-4087-8239-b6d6f37b07a7" width="550" title="">
    <p align="justify">Cannot delete subject (SU000) as it has been assigned to a class</p>
  </li>
</ol>

### Class-Subject & Tutor-Class Allocation
<ol>
  <li> 
    <b>Assign Subject to a class</b> <br>
    <img src="https://github.com/user-attachments/assets/90614433-2428-43ce-8a3b-47b0dec70be0" width="550" title="">
    <p align="justify">Assigned subject (Drawing) to a class (Junior class 1)</p>
  </li>
  <li> 
    <b>View Class Subject Allocation</b> <br>
    <img src="https://github.com/user-attachments/assets/f851c3f9-362b-44bc-ba28-aa1dbf375ecd" width="550" title="">
    <p align="justify">The subject(SU001) is successfully assigned on Thursday at 11 am - 1 pm</p>
  </li>
  <li> 
    <b>Error Message - Class-Subject Allocation</b> <br>
    <img src="https://github.com/user-attachments/assets/1e5cf559-f36f-4387-aa68-a44eceb09939" width="550" title="">
    <p align="justify"> Cannot assign Dancing class on Thursday from 11 am - 1 pm as there is another subject is taken at that time for (Junior Class 1)</p>
  </li>
</ol>

### Student-Class Allocation
<ol>
  <li> 
    <b>Assign students to a class</b> <br>
    <img src="https://github.com/user-attachments/assets/90780a12-bc38-4540-9cd2-c4007d2b5df6 width="550" title="">
  </li>
  <li> 
    <b>View Student-Class Allocation</b> <br>
    <img src="https://github.com/user-attachments/assets/9ef8503b-435a-482b-aa07-762512b02556" width="550" title="">
    <p align="justify">Successfully assign students to a class (Junior Class 1)</p>
  </li>
  <li> 
    <b>Error Message - Student-Class Allocation</b> <br>
    <img src="https://github.com/user-attachments/assets/c4a11f94-3f8a-4324-bf72-0a0569bd92ef" width="550" title="">
    <p align="justify"> Cannot assign students to a class (Sophomore Class 1) because the student age is not equal to 5</p>
  </li>
</ol>

### Generates and sends timetable to tutors and student’s parent by Email
<ol>
  <li> 
    <b>Tutor's Timetable </b><br>
    <img src="https://github.com/user-attachments/assets/52919fcf-3bec-4111-ace3-e40e1a9227b7" width="550" title="" >
    <p align="justify">
      A timetable showing the subject name and subject ID that the tutor is in charge of. It also shows the class name that the class have that subject. This is done by including the Tutors table, Subjects     table, ClassesSubjects table and Class Table. Before this, the tutor must assigned to a class and teach a subject
    </p>
  </li>
  <li> 
    <b>Student's Timetable </b><br>
    <img src="https://github.com/user-attachments/assets/efda9c7f-f335-47ce-b5de-cb0267584cf0" width="550" title="" >
    <p align="justify">
      A timetable showing the subject name and subject ID of that particular student class involved. It also shows the tutor's name of the subject. This is done by including the Students table, Class table,             ClassesSubjects table, Subjects Table and Tutor table. Before this, the class must be eligible to take that particular subject when assigned a subject to the class.
    </p>
  </li>
</ol>

### Real-time Chat room
<ol>
  <li> 
    <b>A real-time live chat between admin and parents</b><br>
    <img src="https://github.com/user-attachments/assets/68215f39-4cd0-4f1b-a72a-dc60b8f8c9a3" width="550" title="" >
    <p align="justify">
     A real-time chat room allows the parents to talk to the admin or the customer to talk to the admin. The real-time chat functionality is implemented using SignalR, where the client establishes a connection with the server, and messages are exchanged via SignalR hubs, allowing instantaneous updates to all connected clients. When a user sends a message, it's broadcast to all clients. Each client updates its chat window accordingly.
    </p>
  </li>
</ol>

### Report1 - Count Total Classes Under Each Class Type (Pie Chart)
  <img src="https://github.com/user-attachments/assets/81624fd8-dae0-40df-b6b0-4a72bdb779ef" width="550" title="" >

### Report2 - Student Count by Classes (Column Chart)
  <img src="https://github.com/user-attachments/assets/673272a6-74a8-4fd3-a8e1-16fa7c6563ca" width="550" title="" >

<hr>

## 4. Attendance Tracking
### PIC: Yong Choy Mun - Admin side: Admin, Tutor
### Class Calendar (Daily/Weekly)
<ol>
  <li> 
    <br>
    <img src="https://github.com/user-attachments/assets/274099cc-08f5-41df-a439-6264ada0d66f" width="550" title="" >
  </li>
</ol>

### Class-Student Attendance List
<ol>
  <li> 
    <br>
    <img src="https://github.com/user-attachments/assets/013f7545-70e8-4be0-b5d8-dee4f5bb4965" width="550" title="" >
  </li>
</ol>

### Attendance Taking (Manual)
<ol>
  <li> 
    <br>
    <img src="https://github.com/user-attachments/assets/6dd1d2d1-3145-4073-b34e-3802e27f5726" width="550" title="" >
  </li>
</ol>

### Attendance Taking (QR Code Generating for students)
<ol>
  <li> 
    <br>
    <img src="https://github.com/user-attachments/assets/8cdc6e74-fa8f-47a0-942e-367695dfb9a0" width="550" title="" >
  </li>
</ol>

### Attendance Taking (QR Code Scanning)
<ol>
  <li> 
    <br>
    <img src="https://github.com/user-attachments/assets/9be45592-646a-4d9e-beeb-71452588de51" width="550" title="" >
  </li>
</ol>

### Warning Letter (Email)
<ol>
  <li> 
    <br>
    <img src="https://github.com/user-attachments/assets/40dfcfd5-cce7-4d07-93db-4ecdf4c7e804" width="550" title="" >
  </li>
</ol>

### Report1 - Class Attendance Status (Column Chart)
  <img src="https://github.com/user-attachments/assets/c0d6f3dd-9554-4185-a3d6-5a36cc15badb" width="550" title="" >

### Report2 - Top 10 Absent Student By Month (Column Chart)
  <img src="https://github.com/user-attachments/assets/90396f1d-e915-4d55-9f36-fd292cc7a0a2" width="550" title="" >


