namespace PlatformService.EndPoints{
    public static class PlatformsUrls
    {
        private const string BaseUrl = "api/platforms";
        public const string GetAll = BaseUrl + "";
        public const string Get = BaseUrl + "/{id}";
        public const string Create = BaseUrl + "";
    }
}