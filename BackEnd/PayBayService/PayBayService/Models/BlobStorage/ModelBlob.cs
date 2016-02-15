using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayBayService.Models.BlobStorage
{
    public class ModelBlob
    {
        public string ImageUri { get; set; }
        public string SasQuery { get; set; }
    }
}