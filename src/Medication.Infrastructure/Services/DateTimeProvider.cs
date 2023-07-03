namespace Medication.Infrastructure.Services
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }

    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
