using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models;

public class DB : DbContext
{
    public DB(DbContextOptions<DB> options) : base(options) { }

    // DB Sets
    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Subjects> Subjects { get; set; }
    public DbSet<Tutors> Tutors { get; set; }
    public DbSet<Students> Students { get; set; }
    public DbSet<Classes> Classes { get; set; }
    public DbSet<ClassesSubjects> ClassesSubjects { get; set; }

}

// Entity Classes

#nullable disable warnings

public class User
{
    [Key, MaxLength(100)]
    public string Email { get; set; }
    [MaxLength(100)]
    public string Hash { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(100)]
    public string PhotoURL { get; set; }

    [NotMapped]
    public string Role => GetType().Name;
}


public class Admin : User
{

}

public class Subjects
{
    [Key, MaxLength(100)]
    public string Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [Precision(6, 2)]
    public decimal Fees { get; set; }
    

    // Navigation properties
    public string TutorId { get; set; }
    public Tutors Tutor { get; set; }

    public List<ClassesSubjects> ClassesSubjects { get; set; } = new();
}

public class Tutors
{
    [Key, MaxLength(100)]
    public string Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(100)]
    public string Gender { get; set; }
    [MaxLength(100)]
    public string PhotoURL { get; set; }

    public int Age { get; set; }

    // Navigation properties
    public List<Subjects> Subjects { get; set; }
    public string? ClassId { get; set; } // Reference to the class the tutor handles
}

public class Students
{
    [Key, MaxLength(100)]
    public string Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(1)]
    public string Gender { get; set; }

    public int Age { get; set; }
    [MaxLength(100)]
    public string PhotoURL { get; set; }

    // FK
    public string? ClassesId { get; set; }

    // Navigation properties
    public Classes Classes { get; set; }
}

public class Classes
{
    [Key, MaxLength(100)]
    public string Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(100)]
    public string ClassType { get; set; }
    public int Capacity { get; set; }

    // Navigation properties
    public List<ClassesSubjects> ClassesSubjects { get; set; } = new();
    public Tutors Tutor { get; set; } // Each class is handled by one tutor

    public List<Students> Students { get; set; } = [];
}

public class ClassesSubjects
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

    // FK
    public string SubjectsId { get; set; }
    public string ClassesId { get; set; }

    // navigation properties
    public Subjects Subjects { get; set; }
    public Classes Classes { get; set; }
   
}

