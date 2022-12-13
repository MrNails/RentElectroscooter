using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentElectroScooter.CoreModels.Models
{
    public class SpecialProposition : BindableModel
    {
        private DateTime m_beginAt;
        private DateTime m_finishAt;
        private SpecialPropositionMetadata m_specialPropositionMetadata;

        public SpecialProposition()
        {
            Created= DateTime.UtcNow;
        }

        public int SpecPropMetadataId { get; set; }
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

        public int SpecialPropositionMetadataId { get; set; }

        public SpecialPropositionMetadata SpecialPropositionMetadata
        {
            get => m_specialPropositionMetadata;
            set
            {
                if (m_specialPropositionMetadata == value) return;

                m_errors[nameof(SpecialPropositionMetadata)] = value == null
                    ? "Special proposition metadata cannot be null."
                    : string.Empty;
                
                m_specialPropositionMetadata = value;
                OnPropertyChanged();
            }
        }

        public DateTime Created { get; set; }
    }
}
