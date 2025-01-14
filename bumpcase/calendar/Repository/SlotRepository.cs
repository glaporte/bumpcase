﻿using calendar.Context;
using calendar.Entites;

namespace calendar.Repository
{
    public class SlotRepository
    {
        public static void SetDefaultConfigurationForVeterinarian(Veterinarian veterinarian)
        {
            // Assumue a veterinarian works everyday except saturday and sunday from 9am to 18pm
            // with a lunch break betweem 12 and 2pm
            // during one month starting now

            using (var context = new SlotContext())
            {
                var slots = new List<Slot>();
                for (DateTime now = DateTime.Today; now <= DateTime.Today + TimeSpan.FromDays(30); now += TimeSpan.FromDays(1))
                {
                    slots.Add(new Slot(now + TimeSpan.FromHours(9), now + TimeSpan.FromHours(12), veterinarian.Id, Slot.SlotState.Available));
                    slots.Add(new Slot(now + TimeSpan.FromHours(14), now + TimeSpan.FromHours(18), veterinarian.Id, Slot.SlotState.Available));
                }

                context.Slots.AddRange(slots);
                context.SaveChanges();
            }
        }

        public Slot? GetSlot(int id)
        {
            using (var context = new SlotContext())
            {
                return context.Slots.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public List<Slot> GetSlots(int vete)
        {
            using (var context = new SlotContext())
            {
                return context.Slots.Where(x => x.VeterinarianId == vete).ToList();
            }
        }

        /// <summary>
        /// Date included in target slot.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Slot found, null otherwise</returns>
        public Slot? FindSlot(DateTime date, int veteId)
        {
            using (var context = new SlotContext())
            {
                return context.Slots.Where(x => x.VeterinarianId == veteId && x.Start <= date && x.End >= date).FirstOrDefault();
            }
        }

        /// <summary>
        /// Split initial slot between 2 or 3 new slots regarding included date range.
        /// </summary>
        /// <param name="initialSlot"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>False is no operation, True otherwise</returns>
        /// <exception cref="ArgumentException"></exception>
        public bool SplitSlot(Slot initialSlot, DateTime startDate, DateTime endDate)
        {
            using (var context = new SlotContext())
            {
                if (initialSlot == null)
                    throw new ArgumentException($"cannot be null", nameof(initialSlot));
                if (startDate < initialSlot.Start)
                    throw new ArgumentException($"Start date '{startDate}' cannot be before initial slot start '{initialSlot.Start}'.");
                if (endDate > initialSlot.End)
                    throw new ArgumentException($"End date '{endDate}' cannot be after initial slot end '{initialSlot.End}'.");
                if (endDate <= startDate)
                    throw new ArgumentException($"End date '{endDate}' cannot be before or equal to start Date '{startDate}'.");


                if (initialSlot.Start == startDate && initialSlot.End == endDate)
                    return false;

                var slots = new List<Slot>();
                if (initialSlot.Start < startDate)
                    slots.Add(new Slot(initialSlot.Start, startDate, initialSlot.VeterinarianId, initialSlot.State));
                slots.Add(new Slot(startDate, endDate, initialSlot.VeterinarianId, initialSlot.State));
                if (endDate < initialSlot.End)
                    slots.Add(new Slot(endDate, initialSlot.End, initialSlot.VeterinarianId, initialSlot.State));


                context.Slots.Remove(initialSlot);
                context.SaveChanges();

                context.Slots.AddRange(slots);
                context.SaveChanges();
            }

            return true;
        }

        public void UpdateSlotState(int slotId, Slot.SlotState state)
        {
            using (var context = new SlotContext())
            {
                Slot slot = context.Slots.Where(x => x.Id == slotId).First();
                slot.State = state;
                context.SaveChanges();
            }
        }
    }
}