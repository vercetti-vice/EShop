using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DAL.Core
{
    public static class ApplicationPermissions
    {
        public static ReadOnlyCollection<ApplicationPermission> AllPermissions;


        public const string UsersPermissionGroupName = "Права над пользователями";
        public static ApplicationPermission ViewUsers = new ApplicationPermission("Просмотр пользователей", "users.view", UsersPermissionGroupName, "Права на просмотр профилей других пользователй");
        public static ApplicationPermission ManageUsers = new ApplicationPermission("Управление пользователями", "users.manage", UsersPermissionGroupName, "Права на создание, удаление и изменение профилей других пользователей");

        public const string RolesPermissionGroupName = "Права над ролями";
        public static ApplicationPermission ViewRoles = new ApplicationPermission("Просмотр ролей", "roles.view", RolesPermissionGroupName, "Права на просмотр доступных ролей");
        public static ApplicationPermission ManageRoles = new ApplicationPermission("Управление ролями", "roles.manage", RolesPermissionGroupName, "Права на создание, удаление и изменение ролей");
        public static ApplicationPermission AssignRoles = new ApplicationPermission("Назначение ролей", "roles.assign", RolesPermissionGroupName, "Права на назначение ролей пользователям");


        static ApplicationPermissions()
        {
            List<ApplicationPermission> allPermissions = new List<ApplicationPermission>()
            {
                ViewUsers,
                ManageUsers,

                ViewRoles,
                ManageRoles,
                AssignRoles
            };

            AllPermissions = allPermissions.AsReadOnly();
        }

        public static ApplicationPermission GetPermissionByName(string permissionName)
        {
            return AllPermissions.Where(p => p.Name == permissionName).FirstOrDefault();
        }

        public static ApplicationPermission GetPermissionByValue(string permissionValue)
        {
            return AllPermissions.Where(p => p.Value == permissionValue).FirstOrDefault();
        }

        public static string[] GetAllPermissionValues()
        {
            return AllPermissions.Select(p => p.Value).ToArray();
        }

        public static string[] GetAdministrativePermissionValues()
        {
            return new string[] { ManageUsers, ManageRoles, AssignRoles };
        }
    }
    
    public class ApplicationPermission
    {
        public ApplicationPermission()
        { }

        public ApplicationPermission(string name, string value, string groupName, string description = null)
        {
            Name = name;
            Value = value;
            GroupName = groupName;
            Description = description;
        }
        
        public string Name { get; set; }
        public string Value { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Value;
        }


        public static implicit operator string(ApplicationPermission permission)
        {
            return permission.Value;
        }
    }
}
