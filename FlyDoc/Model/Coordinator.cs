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
        public string Key { get; set; }

        // описание Согласователя на русском
        public string Title { get; set; }

        // если true, то этот Согласователь должен согласовать документ
        // (входит в список согласователей)
        public bool Enable { get; set; }

        // если true, то этот Согласователь уже утвердил документ
        public bool Checked { get; set; }

        // уровень согласования
        public int SeqNumber { get; set; }
    }
}
