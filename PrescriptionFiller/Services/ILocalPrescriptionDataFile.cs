using System;
using System.Collections.Generic;
using PrescriptionFiller.Model;

namespace PrescriptionFiller.Services
{
    public interface ILocalPrescriptionDataFile
    {
        IEnumerable<PrescriptionItem> getPrescriptionItems();
        int savePrescriptionItem(PrescriptionItem prescriptionItem);
    }
}
