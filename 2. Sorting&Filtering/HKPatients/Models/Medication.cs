using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HKPatients.Models
{
    public partial class Medication
    {
        public Medication()
        {
            PatientMedication = new HashSet<PatientMedication>();
            TreatmentMedication = new HashSet<TreatmentMedication>();
        }

        [Required(ErrorMessage = "Din is required.")]
        [StringLength(8, ErrorMessage = "The Din must be less than {1} charcters.")]
        public string Din { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Medication Name")]
        public string Name { get; set; }
        public string Image { get; set; }
        public int MedicationTypeId { get; set; }
        public string DispensingCode { get; set; }
        public double Concentration { get; set; }
        public string ConcentrationCode { get; set; }

        public virtual ConcentrationUnit ConcentrationCodeNavigation { get; set; }
        public virtual DispensingUnit DispensingCodeNavigation { get; set; }
        public virtual MedicationType MedicationType { get; set; }
        public virtual ICollection<PatientMedication> PatientMedication { get; set; }
        public virtual ICollection<TreatmentMedication> TreatmentMedication { get; set; }
    }
}
