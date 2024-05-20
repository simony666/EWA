using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models;

// View Models ----------------------------------------------------------------

#nullable disable warnings

public class LoginVM
{
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [StringLength(100, MinimumLength = 5)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool RememberMe { get; set; }

    public string RecaptchaResponse { get; set; }

}

public class RegisterVM
{
    [StringLength(100)]
    [EmailAddress]
    [Remote("CheckEmail", "Account", ErrorMessage = "Duplicated {0}.")]
    public string Email { get; set; }

    [StringLength(100, MinimumLength = 5, ErrorMessage = "{0} must be {2}-{1} characters long.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [StringLength(100, MinimumLength = 5, ErrorMessage = "{0} must be {2}-{1} characters long.")]
    [Compare("Password")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string Confirm { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please choose a gender.")]
    public string Gender { get; set; }

    [RegularExpression(@"^\d{10,11}$", ErrorMessage = "{0} must be 10 or 11 digits.")]
    public string Phone { get; set; }

    public IFormFile Photo { get; set; }

    [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Age must be a number.")]
    public int Age { get; set; } // New Age property with validation
}

public class UpdateProfileVM
{
    public string? Email { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    public string? PhotoURL { get; set; }

    public IFormFile? Photo { get; set; }

    [Phone]
    public string? Phone { get; set; }

    [Range(1, 120, ErrorMessage = "Please enter a valid age.")]
    public int? Age { get; set; }

    [Required(ErrorMessage = "Gender is required.")]
    [RegularExpression("^(M|F)$", ErrorMessage = "Gender must be 'M' or 'F'.")]
    public string? Gender { get; set; }
}

public class UpdatePasswordVM
{
    [StringLength(100, MinimumLength = 5, ErrorMessage = "{0} must be {2}-{1} characters long.")]
    [DataType(DataType.Password)]
    [Display(Name = "Current Password")]
    public string Current { get; set; }

    [StringLength(100, MinimumLength = 5, ErrorMessage = "{0} must be {2}-{1} characters long.")]
    [DataType(DataType.Password)]
    [Display(Name = "New Password")]
    public string New { get; set; }

    [StringLength(100, MinimumLength = 5, ErrorMessage = "{0} must be {2}-{1} characters long.")]
    [Compare("New")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string Confirm { get; set; }
}

public class ResetPasswordVM
{
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }
}

//--------------------------------------------------------------
//                     Leong Zhi Yen
//--------------------------------------------------------------
// create Tutors
    public class CreateTutorsVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string? PhotoURL { get; set; }
        public int Age { get; set; }
        public string Phone{ get; set;}
    }

// Students
public class CreateStudentsVM
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Hash { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public string? PhotoURL { get; set; }
    public int Age { get; set; }
    public string Phone { get; set; }
    [Display(Name = "Student Name")]
    public string ClassId { get; set; }
}


// create Subjects
public class CreateSubjectsVM
    {
        public string? Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Precision(6, 2)]
        public decimal Fees { get; set; }

        [Display(Name = "Tutor Name")]
        public string TutorId { get; set; }

    }

    public class UpdateSubjectsVM
    {
        public string? Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Precision(6, 2)]
        public decimal Fees { get; set; }

        [Display(Name = "Tutor Name")]
        public string TutorId { get; set; }
    }

    // classes
    public class ClassesVM
    {
        public string? Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string ClassType { get; set; }

        [Range(1, 20)]
        public int? Capacity { get; set; }

}

    public class UpdateClassesVM
    {
        public string? Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Range(1, 20)]
        public int? Capacity { get; set; }
    }

    public class TutorClassVM
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ClassId { get; set; }
    }

// students
//public class StudentsVM
//    {
//        public string? Id { get; set; }

//        [StringLength(100)]
//        public string Name { get; set; }

//        [StringLength(1)]
//        public string Gender { get; set; }

//        public int Age { get; set; }

//        public string? PhotoURL { get; set; }

//        public IFormFile? Photo { get; set; }

//        public string ClassesId { get; set; }

//    }

    // tutors
    public class TutorsVM
    {
        public string? Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }

        public string? PhotoURL { get; set; }

        public IFormFile? Photo { get; set; }

        public int Age { get; set; }

    }

    // subject Class
    public class SubjectsClassVM
    {
        //public string Id { get; set; }

        [Display(Name = "Start Time"), DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "End Time"), DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        public int Duration { get; set; }
        [StringLength(100)]
        public string DayOfWeek { get; set; }

        public string SubjectsId { get; set; }

        public string? StudentsId { get; set; }

        public string ClassesId { get; set; }
    }


//--------------------------------------------------------------
//                     Yong Choy Mun
//--------------------------------------------------------------
public class AttendanceVM
{
    public int Id { get; set; }

    [Display(Name = "Start Time"), DataType(DataType.Time)]
    public TimeSpan StartTime { get; set; }

    [Display(Name = "End Time"), DataType(DataType.Time)]
    public TimeSpan EndTime { get; set; }

    public int Duration { get; set; }
    [StringLength(100)]
    public string DayOfWeek { get; set; }

    public string SubjectsId { get; set; }

    public string? StudentsId { get; set; }

    public Subject Subject { get; set; }

    public Class Class {  get; set; }

    public string ClassesId { get; set; }

    public string Date {  get; set; }
}

public class StudentAttendanceVM
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Percentage { get; set; }
}

public class StudentAbsenceVM
{
    public string StudentId { get; set; }
    public string StudentName { get; set; }
    public string ClassName { get; set; }
    public int AbsencesCount { get; set; }
}

//--------------------------------------------------------------
//                     Goh Qin Long
//--------------------------------------------------------------

//User
public class UserVM
{
    [StringLength(100)]
    [EmailAddress]
    [Remote("CheckEmail", "AccountMaintenance", ErrorMessage = "Duplicated {0}.")]
    public string Email { get; set; }

    [StringLength(100, MinimumLength = 6, ErrorMessage = "{0} must be {2}-{1} characters long.")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Hash { get; set; }

    [StringLength(100, MinimumLength = 6, ErrorMessage = "{0} must be {2}-{1} characters long.")]
    [Compare("Hash", ErrorMessage = "The password and confirmation password do not match.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string Confirm { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [Range(0, 120, ErrorMessage = "{0} must be between 0 and 120.")]
    public int Age { get; set; }

    [StringLength(1)]
    [RegularExpression(@"[FM]", ErrorMessage = "Invalid {0}! This is Malaysia no America")]
    public string Gender { get; set; }

    [StringLength(12)]
    [RegularExpression(@"^(\+?6?01)[0-9]{8,9}$", ErrorMessage = "Invalid {0}! Follow the format: +601XXXXXXXX.")]
    [Remote("CheckPhone", "AccountMaintenance", ErrorMessage = "Duplicated {0}.")]
    [Display(Name = "Contact Number")]
    public string Phone { get; set; }

    public IFormFile Photo { get; set; }

}

//Admin
public class AdminVM : UserVM
{
    /*[StringLength(10)]
    [RegularExpression(@"[A]\d{5}", ErrorMessage = "Invalid {0}! First Alphabet must be <A>. (A00000)")]
    [Remote("CheckId", "AccountMaintenance", ErrorMessage = "Duplicated {0}.")]*/
    /*public string Id { get; set; }*/
}

public class TutorVM : UserVM
{
    /*[StringLength(10)]
    [RegularExpression(@"[T]\d{5}", ErrorMessage = "Invalid ID! Format: T followed by 5 digits (e.g., T00000).")]
    [Remote("CheckId", "Home", ErrorMessage = "Duplicated ID.")]
    public string Id { get; set; }*/
}

public class ParentVM : UserVM
{
    /*[StringLength(10)]
    [RegularExpression(@"[P]\d{5}", ErrorMessage = "Invalid ID! Format: P followed by 5 digits (e.g., P00000).")]
    [Remote("CheckId", "Home", ErrorMessage = "Duplicated ID.")]
    public string Id { get; set; }*/
}

// students
/*public class StudentsVM
{
    //public string? Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(1)]
    [Range(4, 6, ErrorMessage = "{0} must be between 4 and 6.")]
    public int Age { get; set; }

    [StringLength(1)]
    [RegularExpression(@"[FM]", ErrorMessage = "Invalid {0}! This is Malaysia no America")]
    public string Gender { get; set; }

    public string? PhotoURL { get; set; }

    public IFormFile? Photo { get; set; }

    //public Parent Parent { get; set; }
    public string ParentName { get; set; }

}*/

public class StudentsVM
{
    [StringLength(100)]
    public string Name { get; set; }

    [Range(4, 6, ErrorMessage = "{0} must be between 4 and 6.")]
    public int Age { get; set; }

    [StringLength(1)]
    [RegularExpression(@"[FM]", ErrorMessage = "Invalid {0}! This is Malaysia no America")]
    public string Gender { get; set; }

    public string? PhotoURL { get; set; }

    public IFormFile? Photo { get; set; }

    public string ParentId { get; set; } 
}

// students
/*public class TutorsVM
{
    public string? Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(1)]
    public string Gender { get; set; }

    public string? PhotoURL { get; set; }

    public IFormFile? Photo { get; set; }

    public int Age { get; set; }

}*/

public class UpdateProfileByAdminVM
{
    public string? Email { get; set; }
    public string? Id { get; set; }
    /*public string? Hash { get; set; }
    public string? Confirm { get; set; }*/

    [StringLength(100)]
    public string? Name { get; set; }

    [Range(0, 120, ErrorMessage = "{0} must be between 0 and 120.")]
    public int Age { get; set; }

    [StringLength(1)]
    [RegularExpression(@"[FM]", ErrorMessage = "Invalid {0}! This is Malaysia no America")]
    public string? Gender {  get; set; }

    [StringLength(12)]
    [RegularExpression(@"^(\+?6?01)[0-9]{8,9}$", ErrorMessage = "Invalid {0}! Follow the format: 01XXXXXXXX.")]
    public string? Phone { get; set; }
    public string? PhotoURL { get; set; }

    public IFormFile? Photo { get; set; }
    public string? Role { get; set; }
}

public class UserViewModel
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public string Phone { get; set; }
    public int Age { get; set; }
    public string Role { get; set; }
    public string? PhotoURL { get; set; }
    public IFormFile Photo { get; set; }
}

public class MapVM
{
    public string Url { get; set; }
}

