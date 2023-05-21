using System.Collections.Generic;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
public class FileCreation
{
        public List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
        {
            $"FileCreation.{module}.Create",
            $"FileCreation.{module}.View",
            $"FileCreation.{module}.Edit",
            $"FileCreation.{module}.Delete",
        };
        }

        public class Products
        {
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
        }
        public class Users
        {
            public const string View = "Permissions.User.View";
            public const string Create = "Permissions.User.Create";
            public const string Edit = "Permissions.User.Edit";
            public const string Delete = "Permissions.User.Delete";
        }
        public class FileCreations
        {
            public const string View = "FileCreation.FileCreation.View";
            public const string Create = "FileCreation.FileCreation.Create";
            public const string Edit = "FileCreation.FileCreation.Edit";
            public const string Delete = "FileCreation.FileCreation.Delete";
        }

    }
}