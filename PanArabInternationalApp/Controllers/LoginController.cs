using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.DirectoryServices.AccountManagement;
using System.Web.Security;
using System.Web.UI;

namespace PanArabInternationalApp.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        //public bool ValidateCredentials(string userName,string password);

        public ActionResult AdminPannel()
        {
            //string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
            //String ecname = System.Environment.MachineName;
          
           
            ViewBag.PCName = "Administrator";
         //  ViewBag.PCName =computer_name[0].ToString(); ;

            // System.Security.Principal.WindowsIdentity.GetCurrent().Name;
          
            //  ViewBag.PCName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            return View();
        }
        [HttpPost]
        public ActionResult AdminPannel(string user, string pass)
        {
            
            //PrincipalContext pc = new PrincipalContext(ContextType.Machine);
            //bool isCredentialValid = pc.ValidateCredentials(user, pass);

            if (user == "Administrator" & pass=="admin")
            {
                return RedirectToAction("Index", "Home");
            }

            //if (isCredentialValid)
            //{
                
            //}
            ViewBag.PCName = "Administrator";
            return View();
        }

    }
}
