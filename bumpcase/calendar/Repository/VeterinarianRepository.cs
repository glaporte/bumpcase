using calendar.Context;
using calendar.Entites;

namespace calendar.Repository
{
    public class VeterinarianRepository
    {
        public VeterinarianRepository()
        {
            using (var context = new VeterinarianContext())
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

                context.Veterinarians.AddRange(vetenerians);
                context.SaveChanges();
            }
        }
    }
}
