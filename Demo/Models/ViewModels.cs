using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

    public IFormFile Photo { get; set; }
}

public class UpdateProfileVM
{
    public string? Email { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    public string? PhotoURL { get; set; }

    public IFormFile? Photo { get; set; }
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

// create Subjects
public class CreateSubjectsVM
{
    public string? Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [Precision(6, 2)]
    public decimal Fees { get; set; }

    [Display(Name = "Start Time"), DataType(DataType.Time)]
    public DateTime StartTime { get; set; }

    [Display(Name = "End Time"), DataType(DataType.Time)]
    public DateTime EndTime { get; set; }

    public int Duration { get; set; }
    [StringLength(100)]
    public string DayOfWeek { get; set; }

    [Display(Name = "Tutor Name")]
    public string TutorId { get; set; }
}

// classes
public class ClassesVM
{
    public string? Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [Range(1, 20)]
    public int? Capacity { get; set; }

}

public class UpdateClassesVM
{
    public string? Id { get; set; }

    [StringLength(100)]
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
}
