using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyDoc.Model
{
    public class PhoneModel
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string DepName { get; set; }
        public string Positions { get; set; }
        public string Name { get; set; }
        public string Dect { get; set; }
        public string PhoneNumber { get; set; }
        public string Mobile { get; set; }
        public string eMail { get; set; }
    }
}
