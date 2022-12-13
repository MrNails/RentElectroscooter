namespace RentElectroScooter.CoreModels.Models;

public class Subscription : BindableModel
{
    private DateTime m_beginAt;
    private DateTime m_finishAt;
    private SubscriptionMetadata m_subscriptionMetadata;

    public Subscription()
    {
        Created = DateTime.UtcNow;
    }

    public Guid UserId { get; set; }
        
    public DateTime BeginAt
    {
        get => m_beginAt;
        set
        {
            if (m_beginAt == value) return;
            
            m_errors[nameof(BeginAt)] = FinishAt != default && value > FinishAt
                ? "Begin date cannot be less then finish date."
                : string.Empty;

            m_beginAt = value;
            OnPropertyChanged();
        }
    }

    public DateTime FinishAt
    {
        get => m_finishAt;
        set
        {
            if (m_finishAt == value) return;
            
            m_errors[nameof(FinishAt)] = value < BeginAt
                ? "Begin date cannot be less then finish date."
                : string.Empty;
                
            m_finishAt = value;
            OnPropertyChanged();
        }
    }

    public int SubscriptionMetadataId { get; set; }

    public SubscriptionMetadata SubscriptionMetadata
    {
        get => m_subscriptionMetadata;
        set
        {
            if (m_subscriptionMetadata == value) return;
            
            m_errors[nameof(SubscriptionMetadata)] = value == null
                ? "Special proposition metadata cannot be null."
                : string.Empty;
                
            m_subscriptionMetadata = value;
            OnPropertyChanged();
        }
    }

    public DateTime Created { get; set; }
}