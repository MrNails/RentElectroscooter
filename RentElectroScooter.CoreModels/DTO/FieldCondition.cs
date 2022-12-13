namespace RentElectroScooter.CoreModels.DTO
{
    public enum ComprasionType
    {
        AND,
        OR,
        //NOT,
        //UPPER,
        //UPPER_EQUAL,
        //LOWER,
        //LOWER_EQUAL,
        //IN,
        //NOT_IN,
        //BETWEEN
    }

    public class FieldCondition : ValidableModel
    {
        private string _fieldName;

        public string FieldName
        {
            get => _fieldName;
            set
            {
                _errors[nameof(FieldName)] = string.IsNullOrEmpty(value)
                    ? "Field name cannot be empty."
                    : string.Empty;

                _fieldName = value;
            }
        }

        /// <summary>
        /// Test
        /// </summary>
        public ComprasionType ComprasionType { get; set; }

        public string Value { get; set; }
    }
}
