using MediatR;

namespace Bidder.Domain.Common.Entity
{
    public class IntBaseEntity : DBEntity
    {
        public virtual long Id { get; protected set; }
        public virtual DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        int? _requestedHashCode;
        private IList<INotification>? domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => domainEvents?.ToList();

        public void AddDomain(INotification notification)
        {
            domainEvents = domainEvents ?? new List<INotification>();
            domainEvents.Add(notification);
        }

        public void RemoveDomain(INotification notification)
        {
            domainEvents?.Remove(notification);
        }

        public void ClearDomainEvents()
        {
            domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return Id == default;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is IntBaseEntity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            IntBaseEntity item = (IntBaseEntity)obj;

            if (item.IsTransient() || IsTransient())
                return false;
            else
                return item.Id == Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Id.GetHashCode() ^ 31;

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

        public static bool operator ==(IntBaseEntity left, IntBaseEntity right)
        {
            if (Equals(left, null))
                return Equals(right, null) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(IntBaseEntity left, IntBaseEntity right)
        {
            return !(left == right);
        }
    }
}
