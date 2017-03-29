using System.ComponentModel.DataAnnotations;
using System;
namespace DojoActivity.Models
{    
    public class ActivityViewModel: BaseEntity
    {
        [Key]
        public int ActivityId{get;set;}

        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        public string Description { get; set; }

        [Required]
        [DateRange()]
        public DateTime DateOfActivity { get; set; }

        [Required]
        public string TimeOfActivity { get; set; }

        [Required]
        public string Duration{get;set;}
    }
    public class DateRangeAttribute : ValidationAttribute
    {
        private DateTime maxDate;

       public DateRangeAttribute()
        {
            maxDate = DateTime.Now;
        }

       protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime inputDate = (DateTime)value;
                if (inputDate < maxDate)
                {
                    return new ValidationResult("Enter Valid Date");
                }
            
            }

           return ValidationResult.Success;
        }
    }
}