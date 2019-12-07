using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Data
{
    public class Tag : EventDTO.Tag
    {
        public virtual ICollection<SessionTag> SessionTags { get; set; }
    }
}
