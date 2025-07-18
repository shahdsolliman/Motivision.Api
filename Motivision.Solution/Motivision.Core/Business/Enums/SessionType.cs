using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Enums
{
    public enum SessionType
    {
        [EnumMember(Value = "deep_work")]
        DeepWork,

        [EnumMember(Value = "review")]
        Review,

        [EnumMember(Value = "planning")]
        Planning,

        [EnumMember(Value = "learning")]
        Learning,

        [EnumMember(Value = "break")]
        Break,

        [EnumMember(Value = "exercise")]
        Exercise,

        [EnumMember(Value = "creative")]
        Creative,

        [EnumMember(Value = "meeting")]
        Meeting,

        [EnumMember(Value = "chores")]
        Chores,

        [EnumMember(Value = "focusSprint")]
        FocusSprint
    }

}
