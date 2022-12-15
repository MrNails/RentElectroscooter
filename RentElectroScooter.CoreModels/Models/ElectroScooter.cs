namespace RentElectroScooter.CoreModels.Models;

public class ElectroScooter : BindableModel
{
    private string m_name;
    private string m_description;
    private Coordinate m_position;
    private float m_batteryCharge;
    private VehicleStatus m_status;
    private VehicleData m_additionalData;
    private Guid? _userId;

    public ElectroScooter()
    {
        Modified = DateTime.UtcNow;
        Created = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public Guid? UserId
    {
        get => _userId;
        set
        {
            _userId = value;

            OnPropertyChanged();
        }
    }

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

    public Coordinate Position
    {
        get => m_position;
        set
        {
            if (value.Equals(m_position)) return;

            m_position = value;
            OnPropertyChanged();
        }
    }

    public float BatteryCharge
    {
        get => m_batteryCharge;
        set
        {
            if (value.Equals(m_batteryCharge)) return;

            m_errors[nameof(BatteryCharge)] = value < 0
                ? "Battery charge cannot be less then 0."
                : string.Empty;

            m_batteryCharge = value;
            OnPropertyChanged();
        }
    }

    public VehicleStatus Status
    {
        get => m_status;
        set
        {
            if (value == m_status) return;
            m_status = value;
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

    public int AdditionalDataId { get; set; }
    public virtual VehicleData AdditionalData
    {
        get => m_additionalData;
        set
        {
            if (Equals(value, m_additionalData)) return;

            m_errors[nameof(AdditionalData)] = value == null
                ? "Electroscooter additional data cannot be null."
                : string.Empty;

            m_additionalData = value;
            OnPropertyChanged();
        }
    }

    public DateTime Modified { get; set; }
    public DateTime Created { get; set; }
}