using System;
using System.Collections.Generic;

namespace JMS.DAL.Models
{
    public partial class CodeException
    {
        public int Id { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime? ExceptionTime { get; set; }
        public string MachineName { get; set; }
        public string UserId { get; set; }
        public string AssemblyName { get; set; }
        public string AssemblyVersion { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string StackDump { get; set; }
    }
}
