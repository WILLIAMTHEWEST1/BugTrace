using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrace.Models.Enums
{
    public enum BTTicketType
    {
        [Description("New Development")]
        NewDevelopment,

        [Description("Work Task")]
        WorkTask,

        [Description("Defect")]
        Defect,

        [Description("Change Request")]
        ChangeRequest,

        [Description("Enhancement")]
        Enhancement,

        [Description("General Task")]
        GeneralTask
    }
}
