using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Enums
{
    public enum SessionCategory
    {
        [EnumMember(Value = "mental")]
        Mental,

        [EnumMember(Value = "physical")]
        Physical,

        [EnumMember(Value = "passive")]
        Passive,

        [EnumMember(Value = "organizational")]
        Organizational,

        [EnumMember(Value = "social")]
        Social
    }

}
