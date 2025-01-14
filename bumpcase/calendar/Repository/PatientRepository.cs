using calendar.Context;
using calendar.Entites;

namespace calendar.Repository
{
    public class PatientRepository
    {

        public PatientRepository()
        {
            using (var context = new PatientContext())
            {
                var customers = new List<Patient>
                {
                    new Patient
                    {
                        Family = Patient.PatientFamily.Feline,
                        Name = "félix",
                        OwnerId = 1,
                        PatientSpecies = "chat"
                    },
                    new Patient
                    {
                        Family = Patient.PatientFamily.Feline,
                        Name = "ouaf",
                        OwnerId = 1,
                        PatientSpecies = "chien"
                    },
                };

                context.Patients.AddRange(customers);
                context.SaveChanges();
            }
        }

        public List<Patient> GetPatients()
        {
            using (var context = new PatientContext())
            {
                return context.Patients.ToList();
            }
        }

        public Patient? GetPatient(int id)
        {
            using (var context = new PatientContext())
            {
                return context.Patients.Where(x => x.Id == id).FirstOrDefault();
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
