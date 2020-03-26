using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PLC.Models.Course
{
    [DataContract]
    public class CourseModel
    {
        [DataMember(EmitDefaultValue =false)]
        public String CourseIdentity { get; set; }

        [DataMember(EmitDefaultValue =false)]
        public String CourseName { get; set; }
    }
}
