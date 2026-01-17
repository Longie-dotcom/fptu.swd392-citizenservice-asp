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
        public double MinLat { get; private set; }
        public double MaxLat { get; private set; }
        public double MinLng { get; private set; }
        public double MaxLng { get; private set; }
        public bool IsActive { get; private set; } 
        #endregion

        protected CitizenArea() { }

        public CitizenArea(
            Guid citizenAreaId, 
            string name, 
            string regionCode,
            double minLat,
            double maxLat,
            double minLng,
            double maxLng,
            bool isActive = true)
        {
            CitizenAreaID = citizenAreaId;
            Name = name;
            RegionCode = regionCode;
            MinLat = minLat;
            MaxLat = maxLat;
            MinLng = minLng;
            MaxLng = maxLng;
            IsActive = isActive;
        }

        #region Methods
        #endregion

        #region Private Validators
        #endregion
    }
}
