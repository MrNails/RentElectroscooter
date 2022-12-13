using System.IO.Pipes;

namespace RentElectroScooter.CoreModels.Models;

public class SubscriptionMetadata : BindableModel
{
    private string m_name;
    private string? m_description;
    private TimeSpan m_availabilityTime;
    private TimeSpan m_dailyAvailabilityTime;
    private float m_price;
    private float m_discount;


    public SubscriptionMetadata()
    {
        Modified = DateTime.UtcNow;
        Created = DateTime.UtcNow;
    }

    public int Id { get; set; }

    public string Name
    {
        get => m_name;
        set
        {
            if (m_name == value) return;
            
            m_errors[nameof(Name)] = string.IsNullOrWhiteSpace(value) || value == string.Empty
                ? "Name cannot be empty."
                : string.Empty;

            m_name = value;
            OnPropertyChanged();
        }
    }
    
    public string? Description
    {
        get => m_description;
        set
        {
            if (m_description == value) return;

            m_description = value;
            OnPropertyChanged();
        }
    }

    public TimeSpan AvailabilityTime
    {
        get => m_availabilityTime;
        set
        {
            if (m_availabilityTime == value) return;
            
            m_availabilityTime = value;
            OnPropertyChanged();
        }
    }

    public TimeSpan DailyAvailabilityTime
    {
        get => m_dailyAvailabilityTime;
        set
        {
            if (m_dailyAvailabilityTime == value) return;
            
            m_dailyAvailabilityTime = value;
            OnPropertyChanged();
        }
    }

    public float Price
    {
        get => m_price;
        set
        {
            if (m_price == value) return;
            
            m_errors[nameof(Price)] = value < 0
                ? "Subscription price cannot be less then 0."
                : string.Empty;
            
            m_price = value;
            OnPropertyChanged();
        }
    }

    public float Discount
    {
        get => m_discount;
        set
        {
            if (m_discount == value) return; 

            m_errors[nameof(Discount)] = value < 0
                ? "Subscription discount cannot be less then 0."
                : string.Empty;
            
            m_discount = value;
            OnPropertyChanged();
        }
    }

    public DateTime Modified { get; set; }

    public DateTime Created { get; set; }
}