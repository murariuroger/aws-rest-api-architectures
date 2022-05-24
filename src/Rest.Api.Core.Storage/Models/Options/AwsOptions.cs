namespace Rest.Api.Core.Storage.Models.Options
{
    internal class AwsOptions
    {
        public static string Section = "AWS";

        public bool UseLocal { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}
