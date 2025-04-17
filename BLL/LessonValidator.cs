using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    class LessonValidator
    {
        public string CheckTitle(string title)
        {
            if(title.Length >= 255)
            {
                return "Title_to_long";
            }
            return "valid_true";
        }
    }
}
