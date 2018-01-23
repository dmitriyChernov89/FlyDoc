using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyDoc.Model
{
    // Согласователь
    public class Coordinator
    {
        // соответствует имени поля из табл. Notes
        public string CoordinatorName { get; set; }

        // описание Согласователя на русском
        public string Title { get; set; }

        // уровень согласования
        public int SeqNumber { get; set; }
    }
}
