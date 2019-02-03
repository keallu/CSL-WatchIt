using ColossalFramework.Globalization;
using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WatchIt
{
    public class Watcher : MonoBehaviour
    {
        private bool _initialized;
        private float _timer;

        private UIButton _esc;
        private UITextureAtlas _textureAtlas;
        private UIPanel _panel;
        private UIButton _statisticsButton;
        private UIButton _limitsButton;

        private List<Watch> _watches;

        private void Awake()
        {
            try
            {
                if (_esc == null)
                {
                    _esc = GameObject.Find("Esc").GetComponent<UIButton>();
                }

                _textureAtlas = LoadResources();

                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:Awake -> Exception: " + e.Message);
            }
        }

        private void OnEnable()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:OnEnable -> Exception: " + e.Message);
            }
        }

        private void Start()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:Start -> Exception: " + e.Message);
            }
        }

        private void Update()
        {
            try
            {
                if (!_initialized || ModConfig.Instance.ConfigUpdated)
                {
                    UpdateUI();
                    UpdateWatches();

                    _initialized = true;
                    ModConfig.Instance.ConfigUpdated = false;
                }
                else
                {
                    _timer += Time.deltaTime;

                    if (_timer > ModConfig.Instance.RefreshInterval)
                    {
                        _timer = _timer - ModConfig.Instance.RefreshInterval;

                        UpdateWatches();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:Update -> Exception: " + e.Message);
            }
        }

        private void OnDisable()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:OnDisable -> Exception: " + e.Message);
            }
        }

        private void OnDestroy()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:OnDestroy -> Exception: " + e.Message);
            }
        }

        private UITextureAtlas LoadResources()
        {
            try
            {
                if (_textureAtlas == null)
                {
                    string[] spriteNames = new string[]
                    {
                        "WatchGreen",
                        "WatchYellow",
                        "WatchRed",
                        "Electricity",
                        "Water",
                        "Sewage",
                        "Garbage",
                        "ElementarySchool",
                        "HighSchool",
                        "University",
                        "Healthcare",
                        "Crematorium",
                        "Jail",
                        "Heating",
                        "Statistics",
                        "Limits"
                    };

                    _textureAtlas = ResourceLoader.CreateTextureAtlas("WatchItAtlas", spriteNames, "WatchIt.Icons.");

                    UITextureAtlas defaultAtlas = ResourceLoader.GetAtlas("Ingame");
                    Texture2D[] textures = new Texture2D[]
                    {
                        defaultAtlas["InfoIconBaseNormal"].texture,
                        defaultAtlas["InfoIconBaseHovered"].texture,
                        defaultAtlas["InfoIconBasePressed"].texture,
                        defaultAtlas["InfoIconBaseDisabled"].texture
                    };

                    ResourceLoader.AddTexturesInAtlas(_textureAtlas, textures);
                }

                return _textureAtlas;
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:LoadResources -> Exception: " + e.Message);
                return null;
            }
        }

        private void CreateUI()
        {
            try
            {
                _panel = UIUtils.CreatePanel("WatchItMainPanel");
                _panel.anchor = UIAnchorStyle.Top | UIAnchorStyle.Right;
                _panel.width = _esc.width;
                _panel.absolutePosition = new Vector3(_esc.absolutePosition.x + 13f, _esc.absolutePosition.y + 50f);

                CreateOrUpdatePanelComponents();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:CreateUI -> Exception: " + e.Message);
            }
        }

        private void UpdateUI()
        {
            try
            {
                CreateOrUpdatePanelComponents();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:UpdateUI -> Exception: " + e.Message);
            }
        }

        private void CreateOrUpdatePanelComponents()
        {
            try
            {
                if (_watches == null)
                {
                    _watches = new List<Watch>();
                }
                else
                {
                    foreach (Watch watch in _watches)
                    {
                        watch.DestroyWatch();
                    }

                    _watches.Clear();
                }

                if (ModConfig.Instance.ElectricityAvailability)
                {
                    _watches.Add(CreateWatch("Electricity", "Electricity", Locale.Get("INFO_ELECTRICITY_AVAILABILITY")));
                }
                if (ModConfig.Instance.WaterAvailability)
                {
                    _watches.Add(CreateWatch("Water", "Water", Locale.Get("INFO_WATER_WATERAVAILABILITY")));
                }
                if (ModConfig.Instance.SewageAvailability)
                {
                    _watches.Add(CreateWatch("Sewage", "Sewage", Locale.Get("INFO_WATER_SEWAGEAVAILABILITY")));
                }
                if (ModConfig.Instance.GarbageAvailability)
                {
                    _watches.Add(CreateWatch("Garbage", "Garbage", Locale.Get("INFO_GARBAGE_INCINERATOR")));
                }
                if (ModConfig.Instance.ElementarySchoolAvailability)
                {
                    _watches.Add(CreateWatch("ElementarySchool", "ElementarySchool", Locale.Get("INFO_EDUCATION_AVAILABILITY1")));
                }
                if (ModConfig.Instance.HighSchoolAvailability)
                {
                    _watches.Add(CreateWatch("HighSchool", "HighSchool", Locale.Get("INFO_EDUCATION_AVAILABILITY2")));
                }
                if (ModConfig.Instance.UniversityAvailability)
                {
                    _watches.Add(CreateWatch("University", "University", Locale.Get("INFO_EDUCATION_AVAILABILITY3")));
                }
                if (ModConfig.Instance.HealthcareAvailability)
                {
                    _watches.Add(CreateWatch("Healthcare", "Healthcare", Locale.Get("INFO_HEALTH_HEALTHCARE_AVAILABILITY")));
                }
                if (ModConfig.Instance.CrematoriumAvailability)
                {
                    _watches.Add(CreateWatch("Crematorium", "Crematorium", Locale.Get("INFO_HEALTH_CREMATORIUMAVAILABILITY")));
                }
                if (ModConfig.Instance.JailAvailability)
                {
                    _watches.Add(CreateWatch("Jail", "Jail", Locale.Get("INFO_CRIME_JAIL_AVAILABILITY")));
                }
                if (ModConfig.Instance.HeatingAvailability)
                {
                    _watches.Add(CreateWatch("Heating", "Heating", Locale.Get("INFO_HEATING_AVAILABILITY")));
                }

                int buttonIndex = _watches.Count;

                if (ModConfig.Instance.ShowGameLimitsButton)
                {
                    buttonIndex++;

                    _limitsButton = UIUtils.CreateWatchButton(_panel, "LimitsButton", buttonIndex, _textureAtlas, "Limits", "Game Limits");

                    _limitsButton.eventClicked += (component, eventParam) =>
                    {
                        LimitsPanel limitsPanel = GameObject.Find("WatchItLimitsPanel").GetComponent<LimitsPanel>();

                        if (limitsPanel != null)
                        {
                            if (limitsPanel.isVisible)
                            {
                                limitsPanel.Hide();
                            }
                            else
                            {
                                limitsPanel.Show();
                            }
                        }
                    };
                }

                if (ModConfig.Instance.ShowCityStatisticsButton)
                {
                    buttonIndex++;

                    _statisticsButton = UIUtils.CreateWatchButton(_panel, "StatisticsButton", buttonIndex, _textureAtlas, "Statistics", "City Statistics");

                    _statisticsButton.eventClicked += (component, eventParam) =>
                    {
                        UIView.library.ShowModal("StatisticsPanel");
                    };
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:CreateOrUpdatePanelComponents -> Exception: " + e.Message);
            }
        }

        private Watch CreateWatch(string name, string spriteName, string toolTip)
        {
            Watch watch = new Watch();

            try
            {
                watch.CreateWatch(_panel, name, _watches.Count, _textureAtlas, spriteName, toolTip);
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:CreateWatch -> Exception: " + e.Message);
            }

            return watch;
        }

        private void UpdateWatches()
        {
            try
            {
                foreach (Watch watch in _watches)
                {
                    watch.UpdateWatch();
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:UpdateWatches -> Exception: " + e.Message);
            }
        }
    }
}