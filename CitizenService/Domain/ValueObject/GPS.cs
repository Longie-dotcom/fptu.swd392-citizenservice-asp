namespace Domain.ValueObject
{
    public class GPS
    {
        #region Attributes
        #endregion

        #region Properties 
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }
        #endregion

        public GPS(
            decimal latitude, 
            decimal longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        #region Methods
        public void UpdateGPS(
            decimal latitude,
            decimal longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        #endregion

        #region Private Helpers 
        #endregion
    }
}
