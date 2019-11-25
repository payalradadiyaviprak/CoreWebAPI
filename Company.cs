using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPI
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string ProjectName { get; set; }
        public string GroupMeetingLeadName { get; set; }
        public string Radio { get; set; }
        public string Checkbox { get; set; }
        public string File { get; set; }
        public string Dropdown { get; set; }
        public DateTime DatePicker { get; set; }
        public string TimePicker { get; set; }
        public string Oper { get; set; }
    }
}
