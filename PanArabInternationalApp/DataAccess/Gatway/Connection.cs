using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PanArabInternationalApp.DataAccess.Gatway
{
    public class Connection
    {
        public PANARAB_dbEntities DbEntities = null;
        public bool IsConnection = false;
      public  Connection()
        {
            DbEntities=new PANARAB_dbEntities();
            if (DbEntities.Database.Exists())
            {
                IsConnection = true;
            }
            
        }
    }
}