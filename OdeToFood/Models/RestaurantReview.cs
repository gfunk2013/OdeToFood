using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//also consider RestaurantReview : IValidatableObject

namespace OdeToFood.Models
{
    public class MaxWordsAttribute : ValidationAttribute
    {
        private int _maxWords;

        public MaxWordsAttribute(int maxWords)
            : base("{0} has too many words.")
        {
            _maxWords = maxWords;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string valueAsString = value.ToString();
                if (valueAsString.Split(' ').Length > _maxWords)
                {
                    string errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }

    public class RestaurantReview
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }

        [Range(0, 10)]
        [Required]
        public int Rating { get; set; }

        [Display(Name = "Full Review")]
        [StringLength(200, ErrorMessage = "this is a custom error message")]
        [MaxWords(50)]
        [Required]
        public string Body { get; set; }
    }
}