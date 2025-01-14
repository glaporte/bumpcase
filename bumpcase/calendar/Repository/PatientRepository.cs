using calendar.Context;
using calendar.Entites;

namespace calendar.Repository
{
    public class PatientRepository
    {
        public List<Patient> GetPatients()
        {
            using (var context = new PatientContext())
            {
                return context.Patients.ToList();
            }
        }

        public Patient AddPatient(Patient patient)
        {
            using (var context = new PatientContext())
            {
                context.Patients.Add(patient);
                context.SaveChanges();
            }
            return patient;
        }
    }
}
