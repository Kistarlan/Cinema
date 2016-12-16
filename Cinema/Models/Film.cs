using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
namespace Cinema.Models
{
    public class Film
    {
        public int Id { set; get; }

        [Required(ErrorMessage = "Movie title must be specified necessarily")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Size of title must consist of [2-50] letters")]
        [Display(Name = "Movie title")]
        public string Name { set; get; }
        public string Description { set; get; }

        [Required(ErrorMessage = "Name must be specified necessarily")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Size of name must consist of [2-50] letters")]
        [Display(Name = "Director Name")]
        public string Director { set; get; }

        [Range(1900, 2016, ErrorMessage = "Year should be between 1900-2016")]
        [Display(Name = "Movie Release Date")]
        public int year { set; get; }
        public virtual ICollection<Genre> Genres { set; get; }
        public virtual ICollection<Actor> Actors { set; get; }
        public Film()
        {
            Genres = new List<Genre>();
            Actors = new List<Actor>();
        }


    }
}