using System;
using System.ComponentModel.DataAnnotations;

namespace CarBooking.Model
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public DateTime BookedDate { get; set; }

        [Required]
        public int CarId { get; set; }

        public Car Car { get; set; }
    }
}
