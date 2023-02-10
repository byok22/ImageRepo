using ApplicationsA.LDAP_Validation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace ApplicationsA.LDAP_Validation
{
    public class Ldap
    {

        public string GetUserNameFromWindowsSession()
        {
            return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }
        public bool NTLogin(string domainName, string userAccount, string password)
        {

            #region [Object Variables]
            Config config = new Config();
         
           
            bool ReturnedVariable = false;
            string path = "LDAP://CORP." + domainName + ".ORG/DC=CORP,DC=" + domainName + ",DC=ORG";
            #endregion

            try
            {
                using (DirectoryEntry x = new DirectoryEntry(path, domainName + "\\" + userAccount, password))
                {
                    object y = x.NativeObject;
                }
                ReturnedVariable = true;
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine("NTLogin: " + err.Message);
                ReturnedVariable = false;
            }

            if (!ReturnedVariable)
            {
                try
                {
                   
                    string URL = config.WebService_Autentication1;

                    Dictionary<string, string> dcParameters = new Dictionary<string, string>();
                    dcParameters.Add("user", userAccount);
                    dcParameters.Add("password", password);

                    string MethodName = "fnLogin";

                    string Request = HttpPostRequest(URL, MethodName, dcParameters);

                    if (Request.Contains("<fnLoginResult>1</fnLoginResult>"))
                    {
                        ReturnedVariable = true;
                    }
                    else
                    {
                        ReturnedVariable = false;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("NTLogin: " + ex.Message);
                    ReturnedVariable = false;
                }
            }

            if (!ReturnedVariable)
            {
                try
                {
                    string URL = config.WebService_Autentication2;

                    Dictionary<string, string> dcParameters = new Dictionary<string, string>();
                    dcParameters.Add("user", userAccount);
                    dcParameters.Add("password", password);

                    string MethodName = "fnLogin";

                    string Request = HttpPostRequest(URL, MethodName, dcParameters);

                    if (Request.Contains("<fnLoginResult>1</fnLoginResult>"))
                    {
                        ReturnedVariable = true;
                    }
                    else
                    {
                        ReturnedVariable = false;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("NTLogin: " + ex.Message);
                    ReturnedVariable = false;
                }
            }

            return ReturnedVariable;
        }

         /// <summary>
        /// HttpPostRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="MethodName"></param>
        /// <param name="postParameters"></param>
        /// <returns></returns>
        private string HttpPostRequest(string url, string MethodName, Dictionary<string, string> postParameters)
        {

            #region [Private Variables]
            string pageContent = string.Empty;
            XDocument ResultXML;
            string ResultString;
            #endregion

            #region [Private Constants]

            string soapStr =
                @"<?xml version=""1.0"" encoding=""utf-8""?>
                                        <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                                           xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
                                           xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                                          <soap:Body>
                                            <{0} xmlns=""http://tempuri.org/"">
                                              {1}
                                            </{0}>
                                          </soap:Body>
                                        </soap:Envelope>";

            #endregion

            try
            {

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Headers.Add("SOAPAction", "\"http://tempuri.org/" + MethodName + "\"");
                req.ContentType = "text/xml;charset=\"utf-8\"";
                req.Accept = "text/xml";
                req.Method = "POST";

                using (Stream stm = req.GetRequestStream())
                {
                    string postValues = "";
                    foreach (var param in postParameters)
                    {
                        postValues += string.Format("<{0}>{1}</{0}>", param.Key, param.Value);
                    }

                    soapStr = string.Format(soapStr, MethodName, postValues);
                    using (StreamWriter stmw = new StreamWriter(stm))
                    {
                        stmw.Write(soapStr);
                    }
                }

                using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    string result = responseReader.ReadToEnd();
                    ResultXML = XDocument.Parse(result);
                    ResultString = result;
                }
            }
            catch (WebException ex)
            {
                string Errormessage = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                ResultString = string.Empty;
            }

            return ResultString;
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
