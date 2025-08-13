using Domain.Entites;

namespace Application.Interfaces
{
    public interface IMaintenanceService
    {
        /// <summary>
        /// Belirli bir bakım tipi için gelecekteki bakım tarihlerini hesaplar.
        /// </summary>
        /// <param name="maintenanceType">Bakım tipi</param>
        /// <param name="numberOfOccurrences">Kaç adet gelecekteki tarih hesaplanacak</param>
        /// <returns>Tarih listesi</returns>
        List<DateTime> GetUpcomingDates(MaintenanceType maintenanceType, int numberOfOccurrences = 5);

        /// <summary>
        /// Bir bakım tipi için ilk planlanan tarihi alır veya bugünden sonraki ilk tarihi döner.
        /// </summary>
        /// <param name="maintenanceType">Bakım tipi</param>
        /// <returns>İlk planlanan tarih</returns>
        DateTime GetNextScheduledDate(MaintenanceType maintenanceType, DateTime? lastCompletedDate = null);
    }
}