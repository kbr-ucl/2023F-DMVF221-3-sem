using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RazorPageValidation.Data
{
    public class BookModel
    {
        [Required]
        public string SKU { get; set; }

        [Key]
        [Required]
        //[RegularExpression("^(?:ISBN(?:-13)?:?\\ )?(?=[0-9]{13}$|(?=(?:[0-9]+[-\\ ]){4})[-\\ 0-9]{17}$)97[89][-\\ ]?[0-9]{1,5}[-\\ ]?[0-9]+[-\\ ]?[0-9]+[-\\ ]?[0-9]$",
        //    ErrorMessage = "{0} must be a valid ISBN code")]
        [DisplayName("ISBN (example 978-3-16-148410-0)")]
        public string ISBN { get; set; }

        [Required]
        [StringLength(maximumLength: 10, MinimumLength = 4)]

        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        // [Compare("Email")]
        [DisplayName("Confirm Email")]
        public string EmailRepeated { get; set; }

        [Url]
        public string Url { get; set; }

        [Range(0, 200)]
        [DisplayName("Number of readers")]
        public int NumberOfReaders { get; set; }
    }
}
