using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class Lesson
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int View { get; set; }
        public string Status { get; set; }

        public Lesson() { }

        public Lesson(string id, string title, string type, int view, string status)
        {
            ID = id;
            Title = title;
            Type = type;
            View = view;
            Status = status;
        }
    }
}
