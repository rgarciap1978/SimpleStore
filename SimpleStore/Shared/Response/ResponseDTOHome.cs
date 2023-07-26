using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStore.Shared.Response
{
    public class ResponseDTOHome : ResponseBase
    {
        public ICollection<ResponseDTOCategory> Categories { get; set; }
        public ICollection<ResponseDTOProduct> Products { get; set; }
    }
}
