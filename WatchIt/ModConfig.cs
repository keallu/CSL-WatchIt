namespace WatchIt
{
    [ConfigurationPath("WatchItConfig.xml")]
    public class ModConfig
    {
        public bool ConfigUpdated { get; set; }
        public float RefreshInterval { get; set; } = 5.0f;
        public bool ElectricityAvailability { get; set; } = true;
        public bool WaterAvailability { get; set; } = true;
        public bool SewageAvailability { get; set; } = true;
        public bool GarbageAvailability { get; set; } = true;
        public bool ElementarySchoolAvailability { get; set; } = true;
        public bool HighSchoolAvailability { get; set; } = true;
        public bool UniversityAvailability { get; set; } = true;
        public bool HealthcareAvailability { get; set; } = true;
        public bool CrematoriumAvailability { get; set; } = true;
        public bool JailAvailability { get; set; } = true;
        public bool HeatingAvailability { get; set; } = true;
        public bool ShowGameLimitsButton { get; set; } = true;
        public bool ShowCityStatisticsButton { get; set; } = true;

        private static ModConfig instance;

        public static ModConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Configuration<ModConfig>.Load();
                }

                return instance;
            }
        }

        public void Save()
        {
            Configuration<ModConfig>.Save();
            ConfigUpdated = true;
        }
    }
}