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
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public int Duration { get; set; }

    [MaxLength(100)]
    public string DayOfWeek { get; set; }

    // FK
    public string TutorsId { get; set; }
    public string StudentsId { get; set; }

    // Navigation
    public List<Tutors> TutorSubjects { get; set; } = new();
    public List<Students> StudentSubjects { get; set; } = new();
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

    // FK
    public string ClassesId { get; set; }
}

public class Students
{
    [Key, MaxLength(100)]
    public string Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(100)]
    public string Gender { get; set; }
    [MaxLength(100)]
    public string PhotoURL { get; set; }

    // FK
    public string SubjectsId { get; set; }
    public string ClassesId { get; set; }

    // Navigation
    public Tutors Tutors { get; set; }
    public Classes Classes { get; set; }
}

public class Classes
{
    [Key, MaxLength(100)]
    public string Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    
    public int Capacity { get; set; }

}