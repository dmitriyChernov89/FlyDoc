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
        public bool AllowApprAvtor { get; set; }
        public bool AllowApprComdir { get; set; }
        public bool AllowApprSBNach { get; set; }
        public bool AllowApprKasa { get; set; }
        public bool AllowApprFin { get; set; }
        public bool AllowApprDostavka { get; set; }
        public bool AllowApprEnerg { get; set; }
        public bool AllowApprSklad { get; set; }
        public bool AllowApprBuh { get; set; }
        public bool AllowApprASU { get; set; }
        public string HeadNach { get; set; }
        public string Name { get; set; }
        public string enterMail { get; set; }
    }
}
