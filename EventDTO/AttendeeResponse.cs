﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EventDTO
{
    public class AttendeeResponse : Attendee
    {
        public ICollection<Conference> Conferences { get; set; } = new List<Conference>();

        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
