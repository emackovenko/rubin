//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.Admission
{
    using System;
    using System.Collections.Generic;
    
    public partial class CompetitionEntranceTest
    {
        public int Id { get; set; }
        public Nullable<int> CompetitiveGroupId { get; set; }
        public Nullable<int> EntranceTestId { get; set; }
        public Nullable<int> MinScore { get; set; }
        public Nullable<int> Priority { get; set; }
    
        public virtual CompetitiveGroup CompetitiveGroup { get; set; }
        public virtual EntranceTest EntranceTest { get; set; }
    }
}
