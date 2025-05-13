using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.Business.Interfaces
{
    public interface IFlightNotifier
    {
        Task NotifyFlightStatusAsync(int flightId, string status);
    }

}
