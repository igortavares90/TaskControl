using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskControl.Domain.Commands.Project
{
    public class ProjectCommandQueryResult
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectStatus { get; set; }
        public string UserName { get; set; }
    }
}
