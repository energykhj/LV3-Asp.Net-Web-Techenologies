using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HKPatients.Models
{
    public partial class PatientTreatment
    {
        public PatientTreatment()
        {
            PatientMedication = new HashSet<PatientMedication>();
        }

        public int PatientTreatmentId { get; set; }

        [Display(Name = "Treatment")]
        public int TreatmentId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DatePrescribed { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
        [Display(Name= "Patient Diagnosis Name")]
        public int PatientDiagnosisId { get; set; }

        public virtual PatientDiagnosis PatientDiagnosis { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual ICollection<PatientMedication> PatientMedication { get; set; }
    }
}
