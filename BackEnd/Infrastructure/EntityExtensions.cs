using BackEnd.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Infrastructure
{
    public static class EntityExtensions
    {
        public static EventDTO.SessionResponse MapSessionResponse(this Session session) =>
            new EventDTO.SessionResponse
            {
                ID = session.ID,
                Title = session.Title,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Tags = session.SessionTags?
                              .Select(st => new EventDTO.Tag
                              {
                                  ID = st.TagID,
                                  Name = st.Tag.Name
                              })
                               .ToList(),
                Speakers = session.SessionSpeakers?
                                  .Select(ss => new EventDTO.Speaker
                                  {
                                      ID = ss.SpeakerId,
                                      Name = ss.Speaker.Name
                                  })
                                   .ToList(),
                TrackId = session.TrackId,
                Track = new EventDTO.Track
                {
                    TrackID = session?.TrackId ?? 0,
                    Name = session.Track?.Name
                },
                ConferenceID = session.ConferenceID,
                Abstract = session.Abstract
            };

        public static EventDTO.SpeakerResponse MapSpeakerResponse(this Speaker speaker) =>
            new EventDTO.SpeakerResponse
            {
                ID = speaker.ID,
                Name = speaker.Name,
                Bio = speaker.Bio,
                WebSite = speaker.WebSite,
                Sessions = speaker.SessionSpeakers?
                    .Select(ss =>
                        new EventDTO.Session
                        {
                            ID = ss.SessionId,
                            Title = ss.Session.Title
                        })
                    .ToList()
            };

        public static EventDTO.AttendeeResponse MapAttendeeResponse(this Attendee attendee) =>
            new EventDTO.AttendeeResponse
            {
                ID = attendee.ID,
                FirstName = attendee.FirstName,
                LastName = attendee.LastName,
                UserName = attendee.UserName,
                Sessions = attendee.SessionsAttendees?
                    .Select(s =>
                        new EventDTO.Session
                        {
                            ID = s.SessionID,
                            Title = s.Session.Title,
                            StartTime = s.Session.StartTime,
                            EndTime = s.Session.EndTime
                        })
                    .ToList(),
                Conferences = attendee.ConferenceAttendees?
                    .Select(ca =>
                        new EventDTO.Conference
                        {
                            ID = ca.ConferenceID,
                            Name = ca.Conference.Name
                        })
                    .ToList(),
            };
    }
}