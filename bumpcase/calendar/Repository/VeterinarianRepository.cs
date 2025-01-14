using calendar.Context;
using calendar.Entites;

namespace calendar.Repository
{
    public class VeterinarianRepository
    {
        public VeterinarianRepository()
        {
            var vetenerians = new List<Veterinarian>
                {
                    new Veterinarian
                    {
                        Firstname ="vetenerian 1",
                        Lastname ="name",
                        Address = "0 street na",
                    },
                };
            using (var context = new VeterinarianContext())
            {

                context.Veterinarians.AddRange(vetenerians);
                context.SaveChanges();
            }
            SlotRepository.SetDefaultConfigurationForVeterinarian(vetenerians.First());
        }

        public List<Veterinarian> GetVeterinarians()
        {
            using (var context = new VeterinarianContext())
            {
                return context.Veterinarians.ToList();
            }
        }

        public Veterinarian? GetVeterinarian(int id)
        {
            using (var context = new VeterinarianContext())
            {
                return context.Veterinarians.Where(x => x.Id == id).FirstOrDefault();
            }
        }
    }
}
