//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskModelDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Task
    {
        public string Name { get; set; }
        public int id { get; set; }
        public System.DateTime DueDate { get; set; }
        public string EstimatedDuration { get; set; }
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public string Tag3 { get; set; }
        public bool IsCompleted { get; set; }
        public System.DateTime DateCompleted { get; set; }
        public System.DateTime DateStart { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public int TaskValue { get; set; }
    }
}
