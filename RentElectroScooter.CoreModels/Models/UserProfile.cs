namespace RentElectroScooter.CoreModels.Models
{
    public class UserProfile : BindableModel
    {
        private string m_name;
        private double m_balance;
        private float m_totalDrivenDistance;
        private float m_todayDrivenDistance;
        private DateTime m_registrationAt;
        private TimeSpan m_totalDrivenTime;
        private TimeSpan m_todayDrivenTime;

        public UserProfile()
        {
            SpecialPropositions = new HashSet<SpecialProposition>();
            RegistrationAt = DateTime.UtcNow;
        }

        public Guid UserId { get; set; }

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

        public double Balance
        {
            get => m_balance;
            set
            {
                if (m_balance == value) return;

                m_errors[nameof(Balance)] = value < 0
                    ? "User balance cannot be less then 0."
                    : string.Empty;

                m_balance = value;
                OnPropertyChanged();
            }
        }

        public float TotalDrivenDistance
        {
            get => m_totalDrivenDistance;
            set
            {
                if (m_totalDrivenDistance == value) return;

                m_errors[nameof(TotalDrivenDistance)] = value < 0
                    ? "Total driven distance cannot be less then 0."
                    : string.Empty;

                m_totalDrivenDistance = value;
                OnPropertyChanged();
            }
        }

        public float TodayDrivenDistance
        {
            get => m_todayDrivenDistance;
            set
            {
                if (value == m_todayDrivenDistance) return;

                m_errors[nameof(TodayDrivenDistance)] = value < 0
                    ? "Today driven distance cannot be less then 0."
                    : string.Empty;

                m_todayDrivenDistance = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan TotalDrivenTime
        {
            get => m_totalDrivenTime;
            set
            {
                m_totalDrivenTime = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan TodayDrivenTime
        {
            get => m_todayDrivenTime;
            set
            {
                m_todayDrivenTime = value;
                OnPropertyChanged();
            }
        }

        public DateTime RegistrationAt
        {
            get => m_registrationAt;
            set
            {
                if (value == m_registrationAt) return;

                m_registrationAt = value;
                OnPropertyChanged();
            }
        }
        public DateTime Modified { get; set; }

        public virtual ICollection<SpecialProposition>? SpecialPropositions { get; set; }

        public virtual Subscription? Subscription { get; set; }
    }
}