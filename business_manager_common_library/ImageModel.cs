namespace business_manager_common_library
{
    public class ImageModel
    {
        public ImageModel(long id, long businessId, string imageData)
        {
            Id = id;
            BusinessId = businessId;
            ImageData = imageData;
        }

        public long Id { get; set; }
        public long BusinessId { get; set; }
        public string ImageData { get; set; }
    }
}
