using System;
using System.Threading.Tasks;
using PrescriptionFiller.Model;

namespace PrescriptionFiller.Services
{
    public interface IPrescriptionRecord
    {
        Task<PrescriptionRecordModel> GetPrescriptionRecordResponse();
    }
}
