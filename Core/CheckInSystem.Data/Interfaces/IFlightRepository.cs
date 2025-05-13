using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSystem.Data.Interfaces
{
    public interface IFlightRepository
    {
        void UpdateStatus(int flightId, string status);
    }
}
