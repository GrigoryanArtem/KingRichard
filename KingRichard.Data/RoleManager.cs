using System;
using System.Collections.Generic;
using System.Linq;

namespace KingRichard.Data
{
    public class RoleManager
    {
        public static List<string> GetRoles<RolesEnum>(RolesEnum role)
            where RolesEnum : struct, IConvertible
        {
            ProjectRichardDBEntities context = new ProjectRichardDBEntities();

            return context.RoleConformities
                .AsNoTracking()
                .Where(conformity => conformity.RealRole.Equals(role.ToString(), StringComparison.OrdinalIgnoreCase))
                .Select(conformity => conformity.Role)
                .ToList();
        }

        public static RolesEnum GetRealRole<RolesEnum>(string role, long guildId)
            where RolesEnum : struct, IConvertible
        {
            ProjectRichardDBEntities context = new ProjectRichardDBEntities();

            var realRole = context.RoleConformities
                .AsNoTracking()
                .Where(conformity => (conformity.GuildId == guildId) 
                    && (conformity.Role.Equals(role, StringComparison.OrdinalIgnoreCase)))
                .Select(conformity => conformity.RealRole)
                .FirstOrDefault();

            return realRole == null ?  default(RolesEnum) : 
                (RolesEnum)Enum.Parse(typeof(RolesEnum), realRole);
        }

        public static List<RoleConformity> GetRoleList()
        {
            ProjectRichardDBEntities context = new ProjectRichardDBEntities();

            return context.RoleConformities.ToList();
        }

        public static void AddRole<RolesEnum>(string role, RolesEnum realRole, long guildId)
            where RolesEnum : struct, IConvertible
        {
            ProjectRichardDBEntities context = new ProjectRichardDBEntities();

            var newRole = new RoleConformity()
                { Role = role.ToLower(), RealRole = realRole.ToString(), GuildId = guildId };

            context.RoleConformities.Add(newRole);
            context.SaveChanges();
        }

        public static bool RemoveRole(string roleForRemove, long guildId)
        {
            ProjectRichardDBEntities context = new ProjectRichardDBEntities();

            var roles = context.RoleConformities.
                Where(conformity => conformity.Role.Equals(roleForRemove, StringComparison.OrdinalIgnoreCase) && 
                conformity.GuildId == guildId);

            if (roles.Count() == 0)
                return false;

            context.RoleConformities.RemoveRange(roles);
            context.SaveChanges();

            return true;
        }
    }
}
