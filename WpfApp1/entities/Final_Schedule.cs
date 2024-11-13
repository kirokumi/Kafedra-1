//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1.entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Final_Schedule
    {
        public int ID { get; set; }
        public Nullable<int> ID_Professors { get; set; }
        public Nullable<int> ID_Courses { get; set; }
        public Nullable<int> NumClass { get; set; }
        public Nullable<int> NumRoom { get; set; }
        public Nullable<System.DateTime> DateOfClass { get; set; }
        public Nullable<int> ClassLeft { get; set; }
        public bool IsDateValid => DateOfClass > DateTime.Now.Date;

        public virtual Final_Courses Final_Courses { get; set; }
        public virtual Final_Professors Final_Professors { get; set; }
    }
}