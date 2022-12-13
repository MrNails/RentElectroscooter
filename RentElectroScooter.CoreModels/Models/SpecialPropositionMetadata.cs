namespace RentElectroScooter.CoreModels.Models
{
    public class SpecialPropositionMetadata : BindableModel
    {
        private string m_name;
        private string? m_description;
        private TimeSpan m_availabilityTime;
        private string m_activationRule;

        public SpecialPropositionMetadata()
        {
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

        public string ActivationRule
        {
            get => m_activationRule;
            set
            {
                if (m_activationRule == value) return;

                m_activationRule = value;
                OnPropertyChanged();
            }
        }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }
    }
}
