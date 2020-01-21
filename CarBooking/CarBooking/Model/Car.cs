using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarBooking.Model
{
    public class Car
    {
        public int CarId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Brand { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
