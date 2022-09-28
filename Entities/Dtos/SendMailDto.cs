using Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class SendMailDto
    {
    public MailParemeter mailParemeter { get; set; }
    public string Mail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
}
