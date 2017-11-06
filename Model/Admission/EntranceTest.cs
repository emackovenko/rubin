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
    
    public partial class EntranceTest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EntranceTest()
        {
            this.CompetitionEntranceTests = new HashSet<CompetitionEntranceTest>();
            this.EntranceTestResult = new HashSet<EntranceTestResult>();
            this.ConsultationsLocations = new HashSet<ConsultationsLocation>();
            this.ExaminationsLocations = new HashSet<ExaminationsLocation>();
        }
    
        public int Id { get; set; }
        public Nullable<int> CampaignId { get; set; }
        public Nullable<int> EducationFormId { get; set; }
        public Nullable<int> FinanceSourceId { get; set; }
        public Nullable<int> ExamSubjectId { get; set; }
        public Nullable<System.DateTime> ConsultationDate { get; set; }
        public Nullable<System.TimeSpan> ConsultationTime { get; set; }
        public Nullable<System.DateTime> ExaminationDate { get; set; }
        public Nullable<System.TimeSpan> ExaminationTime { get; set; }
        public Nullable<System.DateTime> RegistrationDateRangeBegin { get; set; }
        public Nullable<System.DateTime> RegistrationDateRangeEnd { get; set; }
        public Nullable<System.DateTime> ReserveDate { get; set; }
        public Nullable<System.TimeSpan> ReserveTime { get; set; }
        public Nullable<int> CheckProtocolId { get; set; }
    
        public virtual Campaign Campaign { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CompetitionEntranceTest> CompetitionEntranceTests { get; set; }
        public virtual EducationForm EducationForm { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntranceTestResult> EntranceTestResult { get; set; }
        public virtual ExamSubject ExamSubject { get; set; }
        public virtual FinanceSource FinanceSource { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConsultationsLocation> ConsultationsLocations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExaminationsLocation> ExaminationsLocations { get; set; }
        public virtual EntranceExaminationsCheckProtocol EntranceExaminationsCheckProtocol { get; set; }
    }
}