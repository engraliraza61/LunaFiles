using System.Collections.Generic;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
public class Permissions
{
        public List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
        }

        public class FileCreation
        {
            public const string View = "Permissions.FileCreation.View";
            public const string Create = "Permissions.FileCreation.Create";
            public const string Edit = "Permissions.FileCreation.Edit";
            public const string Delete = "Permissions.FileCreation.Delete";
        }
        public class Selection
        {
            public const string View = "Permissions.Selection.View";
            public const string Create = "Permissions.Selection.Create";
            public const string Edit = "Permissions.Selection.Edit";
            public const string Delete = "Permissions.Selection.Delete";
            public const string AddQuantity = "Permissions.Selection.AddQuantity";
            public const string ActivateDeactivteSelection = "Permissions.Selection.ActivateDeactivteSelection";
        }
        public class Agent
        {
            public const string View = "Permissions.Agent.View";
            public const string Create = "Permissions.Agent.Create";
            public const string Edit = "Permissions.Agent.Edit";
            public const string Delete = "Permissions.Agent.Delete";
        }
        public class UnderProcessedCandidate
        {
            public const string View = "Permissions.UnderProcessedCandidate.View";
            public const string Create = "Permissions.UnderProcessedCandidate.Create";
            public const string Edit = "Permissions.UnderProcessedCandidate.Edit";
            public const string Delete = "Permissions.UnderProcessedCandidate.Delete";
            public const string selectionGroup = "Permissions.UnderProcessedCandidate.selectionGroup";
            public const string visaProcess = "Permissions.UnderProcessedCandidate.visaProcess";
            public const string QuickEdit = "Permissions.UnderProcessedCandidate.QuickEdit";
            public const string SMS = "Permissions.UnderProcessedCandidate.SMS";
            public const string Email = "Permissions.UnderProcessedCandidate.Email";
            public const string FollowUpHistory = "Permissions.UnderProcessedCandidate.FollowUpHistory";
            public const string reprocessCandidte = "Permissions.UnderProcessedCandidate.reprocessCandidte";
        }

    }
}