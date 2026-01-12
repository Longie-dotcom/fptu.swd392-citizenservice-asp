namespace Domain.Aggregate
{
    public class CitizenArea
    {
        #region Attributes
        #endregion

        #region Properties
        public Guid CitizenAreaID { get; private set; }
        public string Name { get; private set; }
        public string RegionCode { get; private set; }
        public bool IsActive { get; private set; } 
        #endregion

        protected CitizenArea() { }

        public CitizenArea(
            Guid citizenAreaId, 
            string name, 
            string regionCode,
            bool isActive = true)
        {
            CitizenAreaID = citizenAreaId;
            Name = name;
            RegionCode = regionCode;
            IsActive = isActive;
        }

        #region Methods
        #endregion

        #region Private Validators
        #endregion
    }
}
