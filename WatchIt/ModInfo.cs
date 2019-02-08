using ICities;
using System;

namespace WatchIt
{
    public class ModInfo : IUserMod
    {
        public string Name => "Watch It!";
        public string Description => "Watch status of the important capacities in the game.";
        
        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group;
            bool selected;
            float selectedValue;
            float result;

            group = helper.AddGroup(Name);

            selectedValue = ModConfig.Instance.RefreshInterval;
            group.AddTextfield("Refresh Interval (in seconds)", selectedValue.ToString(), sel =>
            {
                float.TryParse(sel, out result);
                ModConfig.Instance.RefreshInterval = result;
                ModConfig.Instance.Save();
            });

            group = helper.AddGroup("Watchers");

            selected = ModConfig.Instance.ElectricityAvailability;
            group.AddCheckbox("Electricity Availability", selected, sel =>
            {
                ModConfig.Instance.ElectricityAvailability = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.WaterAvailability;
            group.AddCheckbox("Water Availability", selected, sel =>
            {
                ModConfig.Instance.WaterAvailability = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.SewageAvailability;
            group.AddCheckbox("Sewage Availability", selected, sel =>
            {
                ModConfig.Instance.SewageAvailability = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.GarbageAvailability;
            group.AddCheckbox("Garbage Availability", selected, sel =>
            {
                ModConfig.Instance.GarbageAvailability = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.ElementarySchoolAvailability;
            group.AddCheckbox("Elementary School Availability", selected, sel =>
            {
                ModConfig.Instance.ElementarySchoolAvailability = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.HighSchoolAvailability;
            group.AddCheckbox("High School Availability", selected, sel =>
            {
                ModConfig.Instance.HighSchoolAvailability = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.UniversityAvailability;
            group.AddCheckbox("University Availability", selected, sel =>
            {
                ModConfig.Instance.UniversityAvailability = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.HealthcareAvailability;
            group.AddCheckbox("Healthcare Availability", selected, sel =>
            {
                ModConfig.Instance.HealthcareAvailability = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.CrematoriumAvailability;
            group.AddCheckbox("Crematorium Availability", selected, sel =>
            {
                ModConfig.Instance.CrematoriumAvailability = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.FireDepartmentAvailability;
            group.AddCheckbox("Fire Department Availability", selected, sel =>
            {
                ModConfig.Instance.FireDepartmentAvailability = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.PoliceDepartmentAvailability;
            group.AddCheckbox("Police Department Availability", selected, sel =>
            {
                ModConfig.Instance.PoliceDepartmentAvailability = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.JailAvailability;
            group.AddCheckbox("Jail Availability", selected, sel =>
            {
                ModConfig.Instance.JailAvailability = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.HeatingAvailability;
            group.AddCheckbox("Heating Availability", selected, sel =>
            {
                ModConfig.Instance.HeatingAvailability = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.LandfillUsage;
            group.AddCheckbox("Landfill Usage", selected, sel =>
            {
                ModConfig.Instance.LandfillUsage = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.CemeteryUsage;
            group.AddCheckbox("Cemetery Usage", selected, sel =>
            {
                ModConfig.Instance.CemeteryUsage = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.HealthAverage;
            group.AddCheckbox("Health Average", selected, sel =>
            {
                ModConfig.Instance.HealthAverage = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.FireHazard;
            group.AddCheckbox("Fire Hazard", selected, sel =>
            {
                ModConfig.Instance.FireHazard = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.CrimeRate;
            group.AddCheckbox("Crime Rate", selected, sel =>
            {
                ModConfig.Instance.CrimeRate = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.EmploymentRate;
            group.AddCheckbox("Employment Rate", selected, sel =>
            {
                ModConfig.Instance.EmploymentRate = sel;
                ModConfig.Instance.Save();
            });

            group = helper.AddGroup("Extra");

            selected = ModConfig.Instance.ShowGameLimitsButton;
            group.AddCheckbox("Show Game Limits Button", selected, sel =>
            {
                ModConfig.Instance.ShowGameLimitsButton = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.ShowCityStatisticsButton;
            group.AddCheckbox("Show City Statistics Button", selected, sel =>
            {
                ModConfig.Instance.ShowCityStatisticsButton = sel;
                ModConfig.Instance.Save();
            });
        }

        private int GetSelectedOptionIndex(int[] option, int value)
        {
            int index = Array.IndexOf(option, value);
            if (index < 0) index = 0;

            return index;
        }
    }
}