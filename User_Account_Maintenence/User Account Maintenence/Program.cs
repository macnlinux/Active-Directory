using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Authentication;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;



namespace User_Account_Maintenence
{
    class Program
    {
        static void Main(string[] args)
        {
            User n = new User();
            Console.WriteLine("Would you like to query the current domain? Y or N");
            string a = Console.ReadLine();
            if (a=="Y" || a=="y")
            {
            n.username= Environment.UserName;
            Console.WriteLine("What is your password?");
            n.password = Console.ReadLine();
            n.domain = Environment.UserDomainName.ToString().ToLower();
            Console.WriteLine(n.domain);
            Console.WriteLine("Please enter username to query: ");
            n.name = Console.ReadLine();
            try
            {
                n.ConnectionStatus();
                Console.ReadKey();
            }
                catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            }
            else {
                    runtime r = new runtime();
                    r.Start();
            }
          
        }
        

    }
    class runtime
    {
        public void Start()
        {
            User U = new User();
            Console.WriteLine("Please enter preferred domain name: ");
            U.domain = Console.ReadLine();
            Console.WriteLine(U.domain);
            Console.WriteLine("Please enter domain credentials");
            Console.WriteLine("Username: ");
            U.username = Console.ReadLine();
            Console.WriteLine("Password: ");
            U.password = Console.ReadLine();
            Console.WriteLine("enter username to query: ");
            U.name = Console.ReadLine();
            U.ConnectionStatus();
            Console.ReadKey();
        }
    }


    class User
    {
        public string username;
        public string password;
        public string domain;
        public string name;

        public void ConnectionStatus()
        {

            try
            {

                DirectoryEntry entry = createDirectoryEntry(username, password);
            DirectorySearcher srch = new DirectorySearcher(entry);
            srch.Filter = "(sAMAccountName=" + name + ")";
            string[] requiredProperties = new string[] {"sAMAccountName", "displayname", "userPassword", "accountExpires", "memberOf", "canonicalName", "ou", "distinguishedName", "groupMembershipSAM" };

                foreach (string property in requiredProperties)
                        srch.PropertiesToLoad.Add(property);
                SearchResult result = srch.FindOne();

                
         
            if (result != null)
            {
                Console.WriteLine("==========================");
                Console.WriteLine("AD User information");
                Console.WriteLine("===========================");

                foreach (string property in requiredProperties)
                    foreach (object myCollection in result.Properties[property])
                    Console.WriteLine(string.Format("{0,-20} : {1}", 
                        property, myCollection.ToString()));
                
                PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
                
                UserPrincipal lanID = UserPrincipal.FindByIdentity(ctx, "sAMAccountName");
                //if (lanID.IsAccountLockedOut()==false)
                //{
                //    Console.WriteLine("Account Active : Yes");
                //}
                //else
                //{
                //    Console.WriteLine("Account Active : No");
                //}
            }
         else  
            {  
               // user does not exist  
               Console.WriteLine("User not found! Please enter another username:");
               name = Console.ReadLine();
               ConnectionStatus();  
               
            }  
         }  
  
         catch (Exception e)  
         {  
            Console.WriteLine("Exception caught:\n\n" + e.ToString());
            runtime r = new runtime();
            r.Start();
         }  
        
        }

        public DirectoryEntry createDirectoryEntry(string Lusername, string LpwdWord)
        {
            
            User L = new User();
            DirectoryEntry ldapconnection = new DirectoryEntry(L.domain);
            ldapconnection.Path = "LDAP://CN=Users,DC=Blockhead,DC=com";
            Lusername= L.username;
            LpwdWord= L.password;
            ldapconnection.Username = Lusername;
            ldapconnection.Password = LpwdWord;
            ldapconnection.AuthenticationType = AuthenticationTypes.Secure;
            return ldapconnection;
        }
    }

    class ModifyUser
    {
      
        

       

    }

    

}


