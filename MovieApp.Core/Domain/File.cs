using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieApp.Core.Domain
{
    public class Image : BaseEntities
    {
        public string FileName { get; set; }
    }
}
