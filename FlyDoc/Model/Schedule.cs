using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyDoc.Model
{
    public class ScheduleModel
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public bool Approved { get; set; }
        public string Dir { get; set; }
        public DateTime Date { get; set; }
        
    }


}

