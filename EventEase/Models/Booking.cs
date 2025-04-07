using System;
using System.Collections.Generic;

namespace EventEase.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int EventId { get; set; }

    public int VenueId { get; set; }

    public DateTime BookingDate { get; set; }

    public  Event? Event { get; set; }

    public  Venue? Venue { get; set; }
}
