using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
namespace Cinema.Models
{
    public class Actor
    {
        [ScaffoldColumn(false)]
        public int Id { set; get; }

        [Required(ErrorMessage = "Name must be specified necessarily")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Size of name must consist of [2-50] letters")]
        [Display(Name = "Name")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Surname must be specified necessarily")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Size of surname must consist of [2-50] letters")]
        [Display(Name = "Surname")]
        public string Surname { set; get; }


        [Range(1900, 2014, ErrorMessage = "Year should be between 1900-2014")]
        [Display(Name = "Date of birth")]
        public int Year { set; get; }

        [UIHint("Collection")]
        public virtual ICollection<Film> Films { set; get; }

        public Actor()
        {
            Films = new List<Film>();
        }
    }
}