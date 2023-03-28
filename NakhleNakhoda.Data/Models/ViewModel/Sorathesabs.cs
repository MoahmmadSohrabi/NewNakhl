using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.ViewModel
{
    public class Sorathesabs
    {
        [Key]
        public int id { get; set; }
        public int IDHotel { get; set; }
        public long Gheymat { get; set; }
        public DateTime Tarikhvariz { get; set; }
        public string NameNameKhanevadgy { get; set; }
        public string Codepigiry { get; set; }
        public string Trakonesh { get; set; }

    }
}
