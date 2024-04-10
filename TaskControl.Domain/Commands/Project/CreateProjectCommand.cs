using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskControl.Domain.Commands.Project
{
    public class CreateProjectCommand
    {
        public string ProjectName { get; set; }
        public int UserId { get; set; }
    }
}
