namespace ADManager
{
    public sealed class ADManagerOptions
    {
        public const string SectionName = "AD";

        public string? Address { get; set; }

        public int Port { get; set; }
    }
}
