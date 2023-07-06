using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStore.Shared.Response
{
    public class ResponseCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool Status { get; set; }
        public string StringStatus => Status ? "Activo" : "Inactivo";
    }
}
