// ====================================================


// ====================================================

export type PermissionNames =
    "Просмотр пользователей" | "Управление пользователями" |
    "Просмотр ролей" | "Управление ролями" | "Назначение ролей" |
    "Просмотр продуктов" | "Управление продуктами";

export type PermissionValues =
    "users.view" | "users.manage" |
    "roles.view" | "roles.manage" | "roles.assign" |
    "products.view" | "products.manage";

export class Permission {

    public static readonly viewUsersPermission: PermissionValues = "users.view";
    public static readonly manageUsersPermission: PermissionValues = "users.manage";

    public static readonly viewRolesPermission: PermissionValues = "roles.view";
    public static readonly manageRolesPermission: PermissionValues = "roles.manage";
    public static readonly assignRolesPermission: PermissionValues = "roles.assign";

    public static readonly viewProductsPermission: PermissionValues = "products.view";
    public static readonly manageProductsPermission: PermissionValues = "products.manage";

    constructor(name?: PermissionNames, value?: PermissionValues, groupName?: string, description?: string) {
        this.name = name;
        this.value = value;
        this.groupName = groupName;
        this.description = description;
    }

    public name: PermissionNames;
    public value: PermissionValues;
    public groupName: string;
    public description: string;
}


