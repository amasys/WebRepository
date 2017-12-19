using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PanArabInternationalApp.DataAccess.Gatway;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.Interface
{
   public interface IPassengerRepository
    {
       
        void Add(Passenger passenger);
        void Remove(Passenger passenger);
        void Edit(Passenger passenger);

        bool IsExist(Passenger passenger);
       
        IEnumerable<Passenger> GetAllPassengers();
    }
}
