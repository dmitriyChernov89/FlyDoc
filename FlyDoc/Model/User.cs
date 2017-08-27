using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyDoc.Model
{
    public class User
    {
        public int Id { get; set; }
        public string PC { get; set; }
        public string UserName { get; set; }
        public int DepartmentId { get; set; }
        public bool AllowNote { get; set; }
        public bool AllowSchedule { get; set; }
        public bool AllowPhonebook { get; set; }
        public bool AllowConfig { get; set; }
        public bool AllowApprovedNach { get; set; }
        public bool AllowApproverSB { get; set; }
        public bool AllowApproverDir { get; set; }
        public string enterMail { get; set; }
    }
}
