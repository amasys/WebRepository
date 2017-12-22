using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PanArabInternationalApp.DataAccess.Gatway;
using PanArabInternationalApp.Models;

namespace PanArabInternationalApp.DataAccess.Bll.Manager
{
    public class AccountingManager:Connection
    {
        public void UpdatePassengerLedgerStatement(Passenger passenger)
        {
            if (IsConnection)
            {
                AccountLedgerPostingupdate(DbEntities,passenger);
                
            }
        }

        private void AccountLedgerPostingupdate(PANARAB_dbEntities dbEntities, Passenger passenger)
        {

            dbEntities.Sp_LedgerAndJournalPostingUpdateDrCr(passenger.voucherno.ToString(),passenger.ContractAmmount,passenger.ContractAmmount,"cr","");

            dbEntities.Sp_LedgerAndJournalPostingUpdateDrCr(passenger.voucherno.ToString(),passenger.ContractAmmount,passenger.ContractAmmount,"dr","");
        }
    }
}