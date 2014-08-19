using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Angular4DotNetMvc.Models
{
    public class CourseViewModel
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Instructor { get; set; }              
    }
}