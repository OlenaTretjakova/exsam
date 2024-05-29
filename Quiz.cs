using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exsam
{
    public class Quiz
    {
        public string Name { get; set; }

        public List<Question> Questions { get; set; }

        public Quiz(string name, List<Question> questions)
        {
            Name = name;
            Questions = questions;
        }

    }
}
