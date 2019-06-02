using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDSL.Board.ApiModels
{
    public class Thread
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }

    }
}
