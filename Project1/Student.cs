//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string PhotoURL { get; set; }
        public string ClassesId { get; set; }
    
        public virtual Class Class { get; set; }
    }
}