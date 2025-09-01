using Domain.Entities;
using Application.Interfaces;
using Domain.Entites;
using Domain.Enums;

namespace Persistence.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        public List<DateTime> GetUpcomingDates(MaintenanceType maintenanceType, int numberOfOccurrences = 5)
        {
            var dates = new List<DateTime>();

            // Bugünden itibaren ilk planlı tarihi bul
            var nextDate = GetNextScheduledDate(maintenanceType);

            for (int i = 0; i < numberOfOccurrences; i++)
            {
                dates.Add(nextDate);

                nextDate = maintenanceType.Period switch
                {
                    MaintenancePeriod.Daily => nextDate.AddDays(1),
                    MaintenancePeriod.Weekly => nextDate.AddDays(7),
                    MaintenancePeriod.Monthly => nextDate.AddMonths(1),
                    MaintenancePeriod.Quarterly => nextDate.AddMonths(3),
                    MaintenancePeriod.SemiAnnual => nextDate.AddMonths(6),
                    MaintenancePeriod.Annual => nextDate.AddYears(1),
                    _ => nextDate
                };
            }

            return dates;
        }

        public DateTime GetNextScheduledDate(MaintenanceType maintenanceType, DateTime? lastCompletedDate = null)
        {
            var nextDate = lastCompletedDate ?? maintenanceType.StartDate;

            while (nextDate < DateTime.Today)
            {
                nextDate = maintenanceType.Period switch
                {
                    MaintenancePeriod.Daily => nextDate.AddDays(1),
                    MaintenancePeriod.Weekly => nextDate.AddDays(7),
                    MaintenancePeriod.Monthly => nextDate.AddMonths(1),
                    MaintenancePeriod.Quarterly => nextDate.AddMonths(3),
                    MaintenancePeriod.SemiAnnual => nextDate.AddMonths(6),
                    MaintenancePeriod.Annual => nextDate.AddYears(1),
                    _ => nextDate
                };
            }

            return nextDate;
        }
    }
}
