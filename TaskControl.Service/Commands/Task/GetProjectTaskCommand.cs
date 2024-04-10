using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskControl.Domain.Commands.Task
{
    public class GetProjectTaskCommand
    {
        public int ProjectId { get; set; }
    }
}
