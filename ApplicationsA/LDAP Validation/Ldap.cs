using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationsA.LDAP_Validation
{
    public class Ldap
    {

        public string GetUserNameFromWindowsSession()
        {
            return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }
        //public static bool Validate(string username, string password)
        //{
        //    try
        //    {
        //        using (System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry("LDAP://" + " " + " ", username, password))
        //        {
        //            object nativeObject = entry.NativeObject;
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        //public static void ValidateWithWindowsSessionWithoutPassword(string username)
        //{
        //    try
        //    {
        //        using (System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry("LDAP://" + " " + " ", username, ""))
        //        {
        //            object nativeObject = entry.NativeObject;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
