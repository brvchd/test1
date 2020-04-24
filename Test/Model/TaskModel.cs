using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Model
{
    public class TaskModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public String DeadLine { get; set; }
        public int ProjectId { get; set; }
        public int TypeId { get; set; }


    }
}
