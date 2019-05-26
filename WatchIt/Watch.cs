using ColossalFramework;
using ColossalFramework.UI;
using System;
using UnityEngine;

namespace WatchIt
{
    public class Watch
    {
        public string Name { get; set; }
        public WatchType Type { get; set; }

        public enum WatchType { Unset, Aspect, Pillar };

        private UIButton _button;
        private UISprite _sprite;
        private UILabel _label;

        public void CreateWatch(UIComponent parent, string name, WatchType type, int index, UITextureAtlas atlas, string spriteName, string toolTip)
        {
            try
            {
                Name = name;
                Type = type;

                if (Type == WatchType.Aspect)
                {
                    CreateAsAspect(parent, ModConfig.Instance.VerticalLayout, ModConfig.Instance.DoubleRibbonLayout, index, atlas, spriteName, toolTip);
                }
                else if (Type == WatchType.Pillar)
                {
                    CreateAsPillar(parent, ModConfig.Instance.VerticalLayout, ModConfig.Instance.DoubleRibbonLayout, index, atlas, spriteName, toolTip);
                }

                _button.eventClick += (component, eventParam) =>
                {
                    SetInfoMode(name);
                };

                if (ModConfig.Instance.ShowNumericalDigits == 2 || ModConfig.Instance.ShowNumericalDigits == 3)
                {
                    CreateNumericalDigits(_button, ModConfig.Instance.NumericalDigitsAnchor, ModConfig.Instance.VerticalLayout, ModConfig.Instance.DoubleRibbonLayout, index);

                    if (ModConfig.Instance.ShowNumericalDigits == 2)
                    {
                        _label.isVisible = false;

                        _button.eventMouseEnter += (component, eventParam) =>
                        {
                            _label.isVisible = true;
                        };
                        _button.eventMouseLeave += (component, eventParam) =>
                        {
                            _label.isVisible = false;
                        };
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watch:CreateWatch -> Exception: " + e.Message);
            }
        }

        private void CreateAsAspect(UIComponent parent, bool verticalLayout, bool doubleRibbonLayout, int index, UITextureAtlas atlas, string spriteName, string toolTip)
        {
            try
            {
                _button = UIUtils.CreateButton(parent, Name, atlas, "Circle");
                _button.tooltip = toolTip;
                _button.size = new Vector2(33f, 33f);

                if (doubleRibbonLayout)
                {
                    _button.relativePosition = verticalLayout ? new Vector3(36f * (index % 2) + 1.5f, 36f * (index / 2) + 36f) : new Vector3(36f * (index / 2) + 36f, 36f * (index % 2) + 1.5f);
                }
                else
                {
                    _button.relativePosition = verticalLayout ? new Vector3(1.5f, 36f * index + 36f) : new Vector3(36f * index + 36f, 1.5f);
                }

                _button.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
                _button.normalFgSprite = spriteName;
                _button.hoveredFgSprite = spriteName;
                _button.pressedFgSprite = spriteName;

                _sprite = UIUtils.CreateSprite(_button, Name, atlas, "WatchRed");
                _sprite.isInteractive = false;
                _sprite.size = new Vector2(36f, 36f);
                _sprite.relativePosition = new Vector3(-1.5f, -1.5f);
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watch:CreateAsAspect -> Exception: " + e.Message);
            }
        }

        private void CreateAsPillar(UIComponent parent, bool verticalLayout, bool doubleRibbonLayout, int index, UITextureAtlas atlas, string spriteName, string toolTip)
        {
            try
            {
                _button = UIUtils.CreateButton(parent, Name, atlas, "Rect");
                _button.tooltip = toolTip;
                _button.size = new Vector2(33f, 33f);

                if (doubleRibbonLayout)
                {
                    _button.relativePosition = verticalLayout ? new Vector3(36f * (index % 2) + 1.5f, 36f * (index / 2) + 36f) : new Vector3(36f * (index / 2) + 36f, 36f * (index % 2) + 1.5f);
                }
                else
                {
                    _button.relativePosition = verticalLayout ? new Vector3(1.5f, 36f * index + 36f) : new Vector3(36f * index + 36f, 1.5f);
                }

                _button.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
                _button.normalFgSprite = spriteName;
                _button.hoveredFgSprite = spriteName;
                _button.pressedFgSprite = spriteName;

                _sprite = UIUtils.CreateSprite(_button, Name, "EmptySprite");
                _sprite.isInteractive = false;
                _sprite.color = new Color32(185, 221, 254, 255);
                _sprite.fillDirection = UIFillDirection.Vertical;
                _sprite.flip = UISpriteFlip.FlipVertical;
                _sprite.opacity = 0.2f;
                _sprite.size = new Vector2(27f, 27f);
                _sprite.relativePosition = new Vector3(3f, 3f);
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watch:CreateAsPillar -> Exception: " + e.Message);
            }
        }

        private void CreateNumericalDigits(UIComponent parent, int anchor, bool verticalLayout, bool doubleRibbonLayout, int index)
        {
            try
            {
                _label = parent.AddUIComponent<UILabel>();
                _label.name = Name;
                _label.text = "0";

                _label.textScale = 0.6f;
                _label.useOutline = true;
                _label.isInteractive = false;
                _label.autoSize = false;
                _label.height = 15f;
                _label.width = parent.width;
                _label.verticalAlignment = UIVerticalAlignment.Bottom;

                if (doubleRibbonLayout)
                {
                    if (index % 2 == 0)
                    {
                        _label.textAlignment = verticalLayout ? UIHorizontalAlignment.Right : UIHorizontalAlignment.Center;
                        _label.relativePosition = verticalLayout ? new Vector3(0f - parent.width * 1.1f, parent.height / 2 - _label.height / 2) : new Vector3(parent.width / 2 - _label.width / 2, 0f - _label.height);
                    }
                    else
                    {
                        _label.textAlignment = verticalLayout ? UIHorizontalAlignment.Left : UIHorizontalAlignment.Center;
                        _label.relativePosition = verticalLayout ? new Vector3(parent.width * 1.1f, parent.height / 2 - _label.height / 2) : new Vector3(parent.width / 2 - _label.width / 2, parent.height);
                    }
                }
                else
                {
                    if (anchor == 1)
                    {
                        _label.textAlignment = UIHorizontalAlignment.Right;
                        _label.relativePosition = new Vector3(0f - parent.width * 1.1f, parent.height / 2 - _label.height / 2);
                    }
                    else if (anchor == 2)
                    {
                        _label.textAlignment = UIHorizontalAlignment.Left;
                        _label.relativePosition = new Vector3(parent.width * 1.1f, parent.height / 2 - _label.height / 2);
                    }
                    else if (anchor == 3)
                    {
                        _label.textAlignment = UIHorizontalAlignment.Center;
                        _label.relativePosition = new Vector3(parent.width / 2 - _label.width / 2, 0f - _label.height);
                    }
                    else
                    {
                        _label.textAlignment = UIHorizontalAlignment.Center;
                        _label.relativePosition = new Vector3(parent.width / 2 - _label.width / 2, parent.height);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watch:CreateNumericalDigits -> Exception: " + e.Message);
            }
        }

        public void SetInfoMode(string name)
        {
            try
            {
                InfoManager infoManager = Singleton<InfoManager>.instance;

                if (infoManager != null)
                {
                    InfoManager.InfoMode infoMode = InfoManager.InfoMode.None;
                    InfoManager.SubInfoMode subInfoMode = InfoManager.SubInfoMode.None;

                    GetInfoModes(name, out infoMode, out subInfoMode);

                    if (infoManager.CurrentMode == infoMode && infoManager.CurrentSubMode == subInfoMode)
                    {
                        infoManager.SetCurrentMode(InfoManager.InfoMode.None, InfoManager.SubInfoMode.None);
                    }
                    else
                    {
                        infoManager.SetCurrentMode(infoMode, subInfoMode);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watch:SetInfoMode -> Exception: " + e.Message);
            }
        }

        public void UpdateWatch()
        {
            try
            {
                int percentage = GetPercentage(Name);

                if (Type == WatchType.Aspect)
                {
                    _sprite.spriteName = GetAspectSpriteName(percentage);
                }
                else if (Type == WatchType.Pillar)
                {
                    _sprite.fillAmount = percentage / 100f;
                }

                if (ModConfig.Instance.ShowNumericalDigits == 2 || ModConfig.Instance.ShowNumericalDigits == 3)
                {
                    _label.text = percentage.ToString();
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watch:UpdateWatch -> Exception: " + e.Message);
            }
        }

        public void DestroyWatch()
        {
            try
            {
                if (_sprite != null)
                {
                    UnityEngine.Object.Destroy(_sprite);
                }

                if (_button != null)
                {
                    UnityEngine.Object.Destroy(_button);
                }

                if (_label != null)
                {
                    UnityEngine.Object.Destroy(_label);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watch:DestroyWatch -> Exception: " + e.Message);
            }
        }

        private void GetInfoModes(string name, out InfoManager.InfoMode infoMode, out InfoManager.SubInfoMode subInfoMode)
        {
            switch (name)
            {
                case "Electricity":
                    infoMode = InfoManager.InfoMode.Electricity;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                case "Water":
                    infoMode = InfoManager.InfoMode.Water;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                case "Sewage":
                    infoMode = InfoManager.InfoMode.Water;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                case "Garbage":
                    infoMode = InfoManager.InfoMode.Garbage;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                case "ElementarySchool":
                    infoMode = InfoManager.InfoMode.Education;
                    subInfoMode = InfoManager.SubInfoMode.ElementarySchool;
                    break;
                case "HighSchool":
                    infoMode = InfoManager.InfoMode.Education;
                    subInfoMode = InfoManager.SubInfoMode.HighSchool;
                    break;
                case "University":
                    infoMode = InfoManager.InfoMode.Education;
                    subInfoMode = InfoManager.SubInfoMode.University;
                    break;
                case "Healthcare":
                    infoMode = InfoManager.InfoMode.Health;
                    subInfoMode = InfoManager.SubInfoMode.HealthCare;
                    break;
                case "Crematorium":
                    infoMode = InfoManager.InfoMode.Health;
                    subInfoMode = InfoManager.SubInfoMode.DeathCare;
                    break;
                case "FireDepartment":
                    infoMode = InfoManager.InfoMode.FireSafety;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                case "PoliceDepartment":
                    infoMode = InfoManager.InfoMode.CrimeRate;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                case "Jail":
                    infoMode = InfoManager.InfoMode.CrimeRate;
                    subInfoMode = InfoManager.SubInfoMode.Prisons;
                    break;
                case "Heating":
                    infoMode = InfoManager.InfoMode.Heating;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                case "Landfill":
                    infoMode = InfoManager.InfoMode.Garbage;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                case "Library":
                    infoMode = InfoManager.InfoMode.Education;
                    subInfoMode = InfoManager.SubInfoMode.LibraryEducation;
                    break;
                case "Cemetery":
                    infoMode = InfoManager.InfoMode.Health;
                    subInfoMode = InfoManager.SubInfoMode.DeathCare;
                    break;
                case "Traffic":
                    infoMode = InfoManager.InfoMode.Traffic;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                case "GroundPollution":
                    infoMode = InfoManager.InfoMode.Pollution;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                case "DrinkingWaterPollution":
                    infoMode = InfoManager.InfoMode.Pollution;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                case "NoisePollution":
                    infoMode = InfoManager.InfoMode.NoisePollution;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                case "Fire":
                    infoMode = InfoManager.InfoMode.FireSafety;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                case "Crime":
                    infoMode = InfoManager.InfoMode.CrimeRate;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                case "Unemployment":
                    infoMode = InfoManager.InfoMode.Density;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                case "Health":
                    infoMode = InfoManager.InfoMode.Health;
                    subInfoMode = InfoManager.SubInfoMode.HealthCare;
                    break;
                case "CityAttractiveness":
                    infoMode = InfoManager.InfoMode.Tourism;
                    subInfoMode = InfoManager.SubInfoMode.Attractiveness;
                    break;
                case "Happiness":
                    infoMode = InfoManager.InfoMode.Happiness;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                default:
                    infoMode = InfoManager.InfoMode.None;
                    subInfoMode = InfoManager.SubInfoMode.None;
                    break;
            }
        }

        private int GetPercentage(string name)
        {
            int percentage;
            switch (name)
            {
                case "Electricity":
                case "Water":
                case "Sewage":
                case "Garbage":
                case "ElementarySchool":
                case "HighSchool":
                case "University":
                case "Healthcare":
                case "Crematorium":
                case "Jail":
                case "Heating":
                    GetCapacityAndNeed(name, out int capacityAvailability, out int needAvailability);
                    percentage = GetAvailabilityPercentage(capacityAvailability, needAvailability);
                    break;
                case "FireDepartment":
                    Singleton<ImmaterialResourceManager>.instance.CheckTotalResource(ImmaterialResourceManager.Resource.FireDepartment, out int fireDepartment);
                    percentage = (int)Mathf.Clamp(fireDepartment, 0f, 100f);
                    break;
                case "PoliceDepartment":
                    Singleton<ImmaterialResourceManager>.instance.CheckTotalResource(ImmaterialResourceManager.Resource.PoliceDepartment, out int policeDepartment);
                    percentage = (int)Mathf.Clamp(policeDepartment, 0f, 100f);
                    break;
                case "Landfill":
                case "Library":
                case "Cemetery":
                    GetCapacityAndNeed(name, out int capacityUsage, out int needUsage);
                    percentage = GetUsagePercentage(capacityUsage, needUsage);
                    break;
                case "Traffic":
                    int traffic = (int)Singleton<VehicleManager>.instance.m_lastTrafficFlow;
                    percentage = traffic;
                    break;
                case "GroundPollution":
                    int groundPollution = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetGroundPollution();
                    percentage = groundPollution;
                    break;
                case "DrinkingWaterPollution":
                    int drinkingWaterPollution = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetWaterPollution();
                    percentage = drinkingWaterPollution;
                    break;
                case "NoisePollution":
                    Singleton<ImmaterialResourceManager>.instance.CheckTotalResource(ImmaterialResourceManager.Resource.NoisePollution, out int noisePollution);
                    percentage = (int)Mathf.Clamp(noisePollution, 0f, 100f);
                    break;
                case "Fire":
                    Singleton<ImmaterialResourceManager>.instance.CheckTotalResource(ImmaterialResourceManager.Resource.FireHazard, out int fireHazard);
                    percentage = (int)Mathf.Clamp(fireHazard, 0f, 100f);
                    break;
                case "Crime":
                    Singleton<ImmaterialResourceManager>.instance.CheckTotalResource(ImmaterialResourceManager.Resource.CrimeRate, out int crimeRate);
                    percentage = (int)Mathf.Clamp(crimeRate, 0f, 100f);
                    break;
                case "Unemployment":
                    int unemployment = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetUnemployment();
                    percentage = unemployment;
                    break;
                case "Health":
                    int health = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_residentialData.m_finalHealth;
                    percentage = health;
                    break;
                case "CityAttractiveness":
                    Singleton<ImmaterialResourceManager>.instance.CheckTotalResource(ImmaterialResourceManager.Resource.Attractiveness, out int attractiveness);
                    Singleton<ImmaterialResourceManager>.instance.CheckTotalResource(ImmaterialResourceManager.Resource.LandValue, out int landValue);
                    attractiveness += landValue;
                    percentage = (int)Mathf.Clamp(100 * attractiveness / Mathf.Max(attractiveness + 200, 200), 0f, 100f);
                    break;
                case "Happiness":
                    int happiness = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_officeData.m_finalHappiness;
                    percentage = happiness;
                    break;
                default:
                    percentage = 0;
                    break;
            }

            return percentage;
        }

        private void GetCapacityAndNeed(string name, out int capacity, out int need)
        {
            capacity = 0;
            need = 100;

            DistrictManager districtManager = Singleton<DistrictManager>.instance;

            if (districtManager != null)
            {
                District district = districtManager.m_districts.m_buffer[0];

                switch (name)
                {
                    case "Electricity":
                        capacity = district.GetElectricityCapacity();
                        need = district.GetElectricityConsumption();
                        break;
                    case "Water":
                        capacity = district.GetWaterCapacity();
                        need = district.GetWaterConsumption();
                        break;
                    case "Sewage":
                        capacity = district.GetSewageCapacity();
                        need = district.GetSewageAccumulation();
                        break;
                    case "Garbage":
                        capacity = district.GetIncinerationCapacity();
                        need = district.GetGarbageAccumulation();
                        break;
                    case "ElementarySchool":
                        capacity = district.GetEducation1Capacity();
                        need = district.GetEducation1Need();
                        need = district.GetEducation1Need();
                        break;
                    case "HighSchool":
                        capacity = district.GetEducation2Capacity();
                        need = district.GetEducation2Need();
                        break;
                    case "University":
                        capacity = district.GetEducation3Capacity();
                        need = district.GetEducation3Need();
                        break;
                    case "Healthcare":
                        capacity = district.GetHealCapacity();
                        need = district.GetSickCount();
                        break;
                    case "Crematorium":
                        capacity = district.GetCremateCapacity();
                        need = district.GetDeadCount();
                        break;
                    case "Jail":
                        capacity = district.GetCriminalCapacity();
                        need = district.GetCriminalAmount() + district.GetExtraCriminals();
                        break;
                    case "Heating":
                        capacity = district.GetHeatingCapacity();
                        need = district.GetHeatingConsumption();
                        break;
                    case "Landfill":
                        capacity = district.GetGarbageCapacity();
                        need = district.GetGarbageAmount();
                        break;
                    case "Library":
                        capacity = district.GetLibraryCapacity();
                        need = district.GetLibraryVisitorCount();
                        break;
                    case "Cemetery":
                        capacity = district.GetDeadCapacity();
                        need = district.GetDeadAmount();
                        break;
                    default:
                        capacity = 0;
                        need = 100;
                        break;
                }
            }
        }

        private int GetAvailabilityPercentage(int capacity, int need)
        {
            return GetAvailabilityPercentage(capacity, need, 45, 55);
        }

        private int GetAvailabilityPercentage(int capacity, int need, int needMin, int needMax)
        {
            if (need != 0)
            {
                float num = (float)capacity / (float)need;
                return (int)(num * (float)((needMin + needMax) / 2));
            }
            if (capacity == 0)
            {
                return 0;
            }
            return 100;
        }

        private int GetUsagePercentage(int capacity, int need)
        {
            if (need != 0)
            {
                return (int)((float)need / (float)capacity * 100f);
            }
            if (capacity == 0)
            {
                return 0;
            }
            return 100;
        }

        private string GetAspectSpriteName(int percentage)
        {
            if (percentage > 55)
            {
                return "WatchGreen";
            }
            else if (percentage > 45)
            {
                return "WatchYellow";
            }
            else
            {
                return "WatchRed";
            }
        }
    }
}
