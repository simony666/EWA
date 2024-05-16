﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace Demo.Models;
public class DB : DbContext
{
    public DB(DbContextOptions<DB> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Tutor> Tutors { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<ClassSubject> ClassesSubjects { get; set; }

}

#nullable disable warnings

public class User
{
    [Key, MaxLength(100)]
    public string Id { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }

    [MaxLength(100)]
    public string Hash { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(1)]
    public string Gender { get; set; }

    [MaxLength(100)]
    public string? PhotoURL { get; set; }

    [MaxLength(100)]
    public int Age { get; set; }

    [MaxLength(11)]
    public string Phone { get; set; }

    [NotMapped]
    public string Role => GetType().Name;
}

public class Student : User
{

    [MaxLength(100)]
    public new string? Email { get; set; }

    [MaxLength(100)]
    public new string? Hash { get; set; }

    public string ClassesId { get; set; }
    public Class Class { get; set; }
    public List<Attendance> Attendances { get; set; } // Navigation property for the Attendances
}

public class Parent : User
{

}

public class Admin : User
{

}

public class Tutor : User
{
    public Class Class { get; set; } // Navigation property for the Class
    public List<Subject> Subjects { get; set; } // Navigation property for the Subjects
}

public class Subject
{
    [Key, MaxLength(100)]
    public string Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [Precision(6, 2)]
    public decimal Fees { get; set; }

    public string TutorId { get; set; }
    public Tutor Tutor { get; set; }
    public List<ClassSubject> ClassesSubjects { get; set; } // Navigation property for the ClassSubjects
}

public class Attendance
{
    [Key, MaxLength(100), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(1)]
    public bool IsAttend { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public Student Student { get; set; }
}

public class Class
{
    [Key, MaxLength(100)]
    public string Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(100)]
    public string ClassType { get; set; }

    public int Capacity { get; set; }
    public List<Student> Students { get; set; } // Navigation property for the Students
    public List<ClassSubject> ClassSubjects { get; set; } // Navigation property for the ClassSubjects
}

public class ClassSubject
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Column(TypeName = "Time")]
    public TimeSpan StartTime { get; set; }
    [Column(TypeName = "Time")]
    public TimeSpan EndTime { get; set; }
    public int Duration { get; set; }

    [MaxLength(100)]
    public string DayOfWeek { get; set; }

    public string SubjectId { get; set; }
    public string ClassestId { get; set; }


    public Class Class { get; set; } // Navigation property for the Class
    public Subject Subject { get; set; } // Navigation property for the Subject
}

public class ResetToken
{
    [MaxLength(100)]
    public string UserId { get; set; }
    [MaxLength(6)]
    public string Token { get; set; }
    public DateTime Expire { get; set; }


    public User User { get; set; }
}

public class ActiveToken
{
    [MaxLength(100)]
    public string UserId { get; set; }
    [MaxLength(6)]
    public string Token { get; set; }
    public DateTime Expire { get; set; }


    public User User { get; set; }
}