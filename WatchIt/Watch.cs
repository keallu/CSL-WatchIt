using ColossalFramework;
using ColossalFramework.UI;
using System;
using UnityEngine;

namespace WatchIt
{
    public class Watch
    {
        public string Name { get; set; }

        private UISprite _sprite;
        private UIButton _button;

        public void CreateWatch(UIComponent parent, string name, bool verticalLayout, int index, UITextureAtlas atlas, string spriteName, string toolTip)
        {
            try
            {
                Name = name;

                _button = UIUtils.CreateButton(parent, name, atlas, "InfoIconBase");
                _button.tooltip = toolTip;
                _button.size = new Vector2(33f, 33f);
                _button.relativePosition = verticalLayout ? new Vector3(0f, (34f * index) + 22f) : new Vector3((34f * index) + 22f, 0f);

                _button.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
                _button.normalFgSprite = spriteName;
                _button.hoveredFgSprite = spriteName;
                _button.pressedFgSprite = spriteName;
                _button.disabledFgSprite = spriteName;

                _button.eventClick += (component, eventParam) =>
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
                };

                _sprite = UIUtils.CreateSprite(parent, name, atlas, "WatchRed");
                _sprite.size = new Vector2(34f, 34f);
                _sprite.relativePosition = verticalLayout ? new Vector3(-0.5f, (34f * index) + 21.5f) : new Vector3((34f * index) + 21.5f, 0.5f);
                _sprite.isInteractive = false;
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watch:CreateWatch -> Exception: " + e.Message);
            }
        }

        public void UpdateWatch()
        {
            try
            {
                GetCapacityAndNeed(Name, out int capacity, out int need);
                int percentage = GetPercentage(capacity, need);
                _sprite.spriteName = GetSpriteName(percentage);
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
                UnityEngine.Object.Destroy(_sprite);
                UnityEngine.Object.Destroy(_button);
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watch:DestroyWatch -> Exception: " + e.Message);
            }
        }

        private void GetInfoModes(string name, out InfoManager.InfoMode infoMode, out InfoManager.SubInfoMode subInfoMode)
        {
            infoMode = InfoManager.InfoMode.None;
            subInfoMode = InfoManager.SubInfoMode.None;

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
                case "Jail":
                    infoMode = InfoManager.InfoMode.CrimeRate;
                    subInfoMode = InfoManager.SubInfoMode.Prisons;
                    break;
                case "Heating":
                    infoMode = InfoManager.InfoMode.Heating;
                    subInfoMode = InfoManager.SubInfoMode.Default;
                    break;
                default:
                    infoMode = InfoManager.InfoMode.None;
                    subInfoMode = InfoManager.SubInfoMode.None;
                    break;
            }
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
                    default:
                        capacity = 0;
                        need = 100;
                        break;
                }
            }
        }

        private int GetPercentage(int capacity, int need)
        {
            return GetPercentage(capacity, need, 45, 55);
        }

        private int GetPercentage(int capacity, int need, int needMin, int needMax)
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

        private string GetSpriteName(int percentage)
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
