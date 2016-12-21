using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class MCinema
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "Cinema title must be specified necessarily")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Size of title must consist of [2-50] letters")]
        [Display(Name = "Cinema title")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Adress must be specified necessarily")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Size of adress title must consist of [10-1000] letters")]
        [Display(Name = "Adress")]
        public string Adress { set; get; }

        public ICollection<Hall> Halls { get; set; }
        public MCinema()
        {
            Halls = new List<Hall>();
        }
    }

    public class Hall
    {
        public int Id { set; get; }
        [Range(0, 20, ErrorMessage = "Year should be between 0-20")]
        [Display(Name = "Hall's number")]
        public int Number { set; get; }

        [Range(10, 50, ErrorMessage = "Numbers of hall must be between 10-50")]
        [Display(Name = "Number seats")]
        public int NumberSeats { set; get; }

        public int? CinemaId { set; get; }
        [Required(ErrorMessage = "Cinema must be specified necessarily")]
        public MCinema cinema { set; get; }

        public ICollection<Session> Sessions { get; set; }
        public Hall()
        {
            Sessions = new List<Session>();
        }
    }

    public class Session
    {
        public int Id { set; get; }
        public DateTime dataTime { set; get; }

        
        public int? filmId { set; get; }
        [Required(ErrorMessage = "Film must be specified necessarily")]
        public Film film { set; get; }

        [Required(ErrorMessage = "Hall must be specified necessarily")]
        public int? HallId { set; get; }
        public Hall hall { set; get; }
        public int FreeSeats { set; get; }
    }

}