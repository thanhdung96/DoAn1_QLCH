using System;
using System.Linq;
using System.Web.Security;
using sqlQuanLiBanHang;

namespace DoAn1.Models.Security
{
    public class Roles : RoleProvider
    {
        BH db = new BH ();
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {

            string roles = db.UserNames.Single(a => a.Username1 == username).Roles;
            if (roles != null)
                return new string[] { roles };
            else
                return new string[] { };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = db.UserNames.Single(a => a.Username1 == username);
            var roles = user.Roles;
            if (user != null)
                return roles.Any(r => r.Equals(roleName));
            else
                return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}