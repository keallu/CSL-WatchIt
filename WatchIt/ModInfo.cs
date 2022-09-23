using ICities;
using System;
using System.Reflection;

namespace WatchIt
{
    public class ModInfo : IUserMod
    {
        public string Name => "Watch It!";
        public string Description => "Watch status of the important capacities in the game.";

        private static readonly string[] ShowNumericalDigitsLabels =
        {
            "Never",
            "When Hover",
            "Always"
        };

        private static readonly int[] ShowNumericalDigitsValues =
        {
            1,
            2,
            3
        };

        private static readonly string[] NumericalDigitsAnchorLabels =
        {
            "Left",
            "Right",
            "Top",
            "Buttom"
        };

        private static readonly int[] NumericalDigitsAnchorValues =
        {
            1,
            2,
            3,
            4
        };

        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group;

            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();

            group = helper.AddGroup(Name + " - " + assemblyName.Version.Major + "." + assemblyName.Version.Minor);

            bool selected;
            int selectedIndex;
            float selectedValue;

            selectedValue = ModConfig.Instance.RefreshInterval;
            group.AddTextfield("Refresh Interval (in seconds)", selectedValue.ToString(), sel =>
            {
                float.TryParse(sel, out float result);
                ModConfig.Instance.RefreshInterval = result;
                ModConfig.Instance.Save();
            });

            group.AddSpace(10);

            group.AddButton("Reset Positioning of Panels", () =>
            {
                ModProperties.Instance.ResetWarningPanelPosition();
                ModProperties.Instance.ResetPanelPosition();
            });

            group = helper.AddGroup("Gauge Panel");

            selected = ModConfig.Instance.ShowPanel;
            group.AddCheckbox("Show panel", selected, sel =>
            {
                ModConfig.Instance.ShowPanel = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.VerticalLayout;
            group.AddCheckbox("Vertical Layout", selected, sel =>
            {
                ModConfig.Instance.VerticalLayout = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.DoubleRibbonLayout;
            group.AddCheckbox("Double Ribbon Layout", selected, sel =>
            {
                ModConfig.Instance.DoubleRibbonLayout = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.ShowDragIcon;
            group.AddCheckbox("Show Drag Icon", selected, sel =>
            {
                ModConfig.Instance.ShowDragIcon = sel;
                ModConfig.Instance.Save();
            });

            selectedIndex = GetSelectedOptionIndex(ShowNumericalDigitsValues, ModConfig.Instance.ShowNumericalDigits);
            group.AddDropdown("Show Numerical Digits", ShowNumericalDigitsLabels, selectedIndex, sel =>
            {
                ModConfig.Instance.ShowNumericalDigits = ShowNumericalDigitsValues[sel];
                ModConfig.Instance.Save();
            });

            selectedIndex = GetSelectedOptionIndex(NumericalDigitsAnchorValues, ModConfig.Instance.NumericalDigitsAnchor);
            group.AddDropdown("Numerical Digits Anchor (Only for Single Ribbon Layout)", NumericalDigitsAnchorLabels, selectedIndex, sel =>
            {
                ModConfig.Instance.NumericalDigitsAnchor = NumericalDigitsAnchorValues[sel];
                ModConfig.Instance.Save();
            });

            selectedValue = ModConfig.Instance.Opacity;
            group.AddSlider("Opacity", 0.0f, 1f, 0.05f, selectedValue, sel =>
            {
                ModConfig.Instance.Opacity = sel;
                ModConfig.Instance.Save();
            });

            selectedValue = ModConfig.Instance.OpacityWhenHover;
            group.AddSlider("Opacity When Hover", 0.0f, 1f, 0.05f, selectedValue, sel =>
            {
                ModConfig.Instance.OpacityWhenHover = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.ShowLimitsButton;
            group.AddCheckbox("Show Limits Button", selected, sel =>
            {
                ModConfig.Instance.ShowLimitsButton = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.ShowProblemsButton;
            group.AddCheckbox("Show Problems Button", selected, sel =>
            {
                ModConfig.Instance.ShowProblemsButton = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.ShowStatisticsButton;
            group.AddCheckbox("Show Statistics Button", selected, sel =>
            {
                ModConfig.Instance.ShowStatisticsButton = sel;
                ModConfig.Instance.Save();
            });

            group = helper.AddGroup("Gauges");

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

            selected = ModConfig.Instance.LibraryUsage;
            group.AddCheckbox("Library Usage", selected, sel =>
            {
                ModConfig.Instance.LibraryUsage = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.CemeteryUsage;
            group.AddCheckbox("Cemetery Usage", selected, sel =>
            {
                ModConfig.Instance.CemeteryUsage = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.TrafficFlow;
            group.AddCheckbox("Traffic Flow", selected, sel =>
            {
                ModConfig.Instance.TrafficFlow = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.GroundPollution;
            group.AddCheckbox("Ground Pollution", selected, sel =>
            {
                ModConfig.Instance.GroundPollution = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.DrinkingWaterPollution;
            group.AddCheckbox("Drinking Water Pollution", selected, sel =>
            {
                ModConfig.Instance.DrinkingWaterPollution = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.NoisePollution;
            group.AddCheckbox("Noise Pollution", selected, sel =>
            {
                ModConfig.Instance.NoisePollution = sel;
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

            selected = ModConfig.Instance.UnemploymentRate;
            group.AddCheckbox("Unemployment Rate", selected, sel =>
            {
                ModConfig.Instance.UnemploymentRate = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.HealthAverage;
            group.AddCheckbox("Health Average", selected, sel =>
            {
                ModConfig.Instance.HealthAverage = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.CityAttractiveness;
            group.AddCheckbox("City Attractiveness", selected, sel =>
            {
                ModConfig.Instance.CityAttractiveness = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.Happiness;
            group.AddCheckbox("Happiness", selected, sel =>
            {
                ModConfig.Instance.Happiness = sel;
                ModConfig.Instance.Save();
            });

            group = helper.AddGroup("Limit Panel");

            selected = ModConfig.Instance.LimitAutoRefresh;
            group.AddCheckbox("Auto-refresh when panel is open", selected, sel =>
            {
                ModConfig.Instance.LimitAutoRefresh = sel;
                ModConfig.Instance.Save();
            });

            group = helper.AddGroup("Warning Panel");

            selected = ModConfig.Instance.ShowWarningPanel;
            group.AddCheckbox("Show panel", selected, sel =>
            {
                ModConfig.Instance.ShowWarningPanel = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.WarningIncludeProblemsForBuildings;
            group.AddCheckbox("Include Problems for Buildings", selected, sel =>
            {
                ModConfig.Instance.WarningIncludeProblemsForBuildings = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.WarningIncludeProblemsForNetworks;
            group.AddCheckbox("Include Problems for Networks", selected, sel =>
            {
                ModConfig.Instance.WarningIncludeProblemsForNetworks = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.WarningAutoOpenClose;
            group.AddCheckbox("Auto-open/close panel", selected, sel =>
            {
                ModConfig.Instance.WarningAutoOpenClose = sel;
                ModConfig.Instance.Save();
            });

            selectedValue = ModConfig.Instance.WarningMaxItems;
            group.AddTextfield("Maximum Warnings (requires re-load)", selectedValue.ToString(), sel =>
            {
                int.TryParse(sel, out int result);
                ModConfig.Instance.WarningMaxItems = result;
                ModConfig.Instance.Save();
            });

            group = helper.AddGroup("Problem Panel");

            selected = ModConfig.Instance.ProblemAutoRefresh;
            group.AddCheckbox("Auto-refresh when panel is open", selected, sel =>
            {
                ModConfig.Instance.ProblemAutoRefresh = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.ProblemAutoClose;
            group.AddCheckbox("Auto-close panel when focusing on building or network", selected, sel =>
            {
                ModConfig.Instance.ProblemAutoClose = sel;
                ModConfig.Instance.Save();
            });

            selectedValue = ModConfig.Instance.ProblemMaxItems;
            group.AddTextfield("Maximum Problems (requires re-load)", selectedValue.ToString(), sel =>
            {
                int.TryParse(sel, out int result);
                ModConfig.Instance.ProblemMaxItems = result;
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