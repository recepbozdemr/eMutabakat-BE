using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrate
{
    public class BaBsReconciliationsDetail : IEntity
    {      
        public int Id { get; set; }

        public int BaBsReconciliationsId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

    }
}
