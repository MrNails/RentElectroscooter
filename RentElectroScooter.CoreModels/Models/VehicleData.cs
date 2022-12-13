namespace RentElectroScooter.CoreModels.Models;

public enum TimeUnits
{
    Minutes,
    Houres,
}

public class VehicleData : BindableModel
{
    private string m_manufacturerName;
    private float m_maxBatteryCharge;
    private float m_maxLoadWeight;
    private float m_maxSpeed;
    private float m_time;
    private double m_pricePerTime;
    private TimeUnits m_timeUnits;

    public VehicleData()
    {
        Modified = DateTime.UtcNow;
        Created = DateTime.UtcNow;
    }

    public int Id { get; set; }

    public string ManufacturerName
    {
        get => m_manufacturerName;
        set
        {
            if (value == m_manufacturerName) return;

            m_errors[nameof(ManufacturerName)] = string.IsNullOrWhiteSpace(value) || value == string.Empty
                ? "Manufacturer name cannot be empty."
                : string.Empty;

            m_manufacturerName = value;
            OnPropertyChanged();
        }
    }

    public float MaxBatteryCharge
    {
        get => m_maxBatteryCharge;
        set
        {
            if (value.Equals(m_maxBatteryCharge)) return;
            
            m_errors[nameof(MaxBatteryCharge)] = value < 0
                ? "Max battery charge cannot be less then 0."
                : string.Empty;
            
            m_maxBatteryCharge = value;
            OnPropertyChanged();
        }
    }

    public float MaxLoadWeight
    {
        get => m_maxLoadWeight;
        set
        {
            if (value.Equals(m_maxLoadWeight)) return;
            
            m_errors[nameof(MaxLoadWeight)] = value < 0
                ? "Max load weight cannot be less then 0."
                : string.Empty;
            
            m_maxLoadWeight = value;
            OnPropertyChanged();
        }
    }

    public float MaxSpeed
    {
        get => m_maxSpeed;
        set
        {
            if (value.Equals(m_maxSpeed)) return;
            
            m_errors[nameof(MaxSpeed)] = value < 0
                ? "Max speed cannot be less then 0."
                : string.Empty;
            
            m_maxSpeed = value;
            OnPropertyChanged();
        }
    }

    public double PricePerTime
    {
        get => m_pricePerTime;
        set
        {
            if (value.Equals(m_pricePerTime)) return;

            m_errors[nameof(PricePerTime)] = value < 0
                ? "Price per time cannot be less then 0."
                : string.Empty;

            m_pricePerTime = value;
            OnPropertyChanged();
        }
    }

    public float Time
    {
        get => m_time;
        set
        {
            m_time = value;

            OnPropertyChanged();
        }
    }

    public TimeUnits TimeUnits
    {
        get => m_timeUnits;
        set
        {
            if (value.Equals(m_timeUnits)) return;

            m_timeUnits = value;
            OnPropertyChanged();
        }
    }

    public DateTime Modified { get; set; }

    public DateTime Created { get; set; }
}