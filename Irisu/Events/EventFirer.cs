using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows.Documents;
using Irisu.Memory;

namespace Irisu.Events
{
  
    public static class EventFirer
    {
      
        public static List<EventBase> ComparerSnapShotAndFireEvent(MemorySnapshot? oldss, MemorySnapshot? newss)
        {
            //TODO
           return new List<EventBase>(){new TestEvent("ping!")};
            
        }
    }
}
