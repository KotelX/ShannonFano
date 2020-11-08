using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodShannonFano
{
    class IsChar
    {
        public char Name { get; set; }
        public double Frequency { get; set; }
        public string Code { get; set; }
        public override string ToString()
        {
            return $"Name:{Name}, Frequency:{Frequency}, Code:{Code}";
        }   //Можно удалить(в пограмме не используется, нужен для отладки)
    }
}
