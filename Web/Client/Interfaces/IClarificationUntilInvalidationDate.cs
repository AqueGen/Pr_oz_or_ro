using System;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IClarificationUntilInvalidationDate
    {
        DateTime? InvalidationDate { get; set; }
        DateTime? ClarificationsUntil { get; set; }
    }
}