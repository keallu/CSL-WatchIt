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
        private UIDragHandle _dragHandle;
        private UISprite _dragSprite;
        private UISprite _orientationSprite;
        private UIButton _limitsButton;
        private UIButton _statisticsButton;

        private List<Watch> _watches;

        private void Awake()
        {
            try
            {
                if (_esc == null)
                {
                    _esc = GameObject.Find("Esc").GetComponent<UIButton>();
                }

                if (ModConfig.Instance.PositionX == 0.0f)
                {
                    ModConfig.Instance.PositionX = _esc.absolutePosition.x + 13f;
                }

                if (ModConfig.Instance.PositionY == 0.0f)
                {
                    ModConfig.Instance.PositionY = _esc.absolutePosition.y + 50f;
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
                if (_panel != null)
                {
                    Destroy(_panel);
                }
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
                        "Drag",
                        "Horizontal",
                        "Vertical",
                        "DragHover",
                        "HorizontalHover",
                        "VerticalHover",
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
                _panel = UIUtils.CreatePanel("WatchIt");
                _panel.autoSize = false;
                _panel.autoLayout = false;

                _panel.absolutePosition = new Vector3(ModConfig.Instance.PositionX, ModConfig.Instance.PositionY);

                _dragHandle = UIUtils.CreateDragHandle(_panel);
                _dragHandle.tooltip = "Drag to move panel";
                _dragHandle.size = new Vector2(15f, 15f);
                _dragHandle.relativePosition = new Vector3(1f, 1f);
                _dragHandle.eventMouseEnter += (component, eventParam) =>
                {
                    _dragSprite.spriteName = "DragHover";
                };
                _dragHandle.eventMouseLeave += (component, eventParam) =>
                {
                    _dragSprite.spriteName = "Drag";
                };
                _dragHandle.eventMouseUp += (component, eventParam) =>
                {
                    ModConfig.Instance.PositionX = _panel.absolutePosition.x;
                    ModConfig.Instance.PositionY = _panel.absolutePosition.y;
                    ModConfig.Instance.Save();
                };

                _dragSprite = UIUtils.CreateSprite(_panel, "Drag", _textureAtlas, "Drag");
                _dragSprite.isInteractive = false;
                _dragSprite.size = new Vector2(15f, 15f);
                _dragSprite.relativePosition = new Vector3(1f, 1f);

                _orientationSprite = UIUtils.CreateSprite(_panel, "Orientation", _textureAtlas, "Vertical");
                _orientationSprite.size = new Vector2(15f, 15f);
                _orientationSprite.eventClick += (component, eventParam) =>
                {
                    ModConfig.Instance.VerticalLayout = !ModConfig.Instance.VerticalLayout;
                    ModConfig.Instance.Save();
                    UpdateUI();
                };

                UpdateUI();
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
                if (ModConfig.Instance.VerticalLayout)
                {
                    _panel.width = 34f;                   

                    _orientationSprite.tooltip = "Click to layout panel horizontally";
                    _orientationSprite.spriteName = "Horizontal";
                    _orientationSprite.relativePosition = new Vector3(18f, 1f);
                    _orientationSprite.eventMouseEnter += (component, eventParam) =>
                    {
                        _orientationSprite.spriteName = "HorizontalHover";
                    };
                    _orientationSprite.eventMouseLeave += (component, eventParam) =>
                    {
                        _orientationSprite.spriteName = "Horizontal";
                    };
                }
                else
                {
                    _panel.height = 34f;

                    _orientationSprite.tooltip = "Click to layout panel vertically";
                    _orientationSprite.spriteName = "Vertical";
                    _orientationSprite.relativePosition = new Vector3(1f, 18f);
                    _orientationSprite.eventMouseEnter += (component, eventParam) =>
                    {
                        _orientationSprite.spriteName = "VerticalHover";
                    };
                    _orientationSprite.eventMouseLeave += (component, eventParam) =>
                    {
                        _orientationSprite.spriteName = "Vertical";
                    };
                }

                CreateOrUpdatePanelButtons();

                UpdateWatches();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:UpdateUI -> Exception: " + e.Message);
            }
        }

        private void CreateOrUpdatePanelButtons()
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

                    Destroy(_limitsButton);
                    Destroy(_statisticsButton);
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

                    _limitsButton = UIUtils.CreateButton(_panel, "Limits", _textureAtlas, "InfoIconBase");
                    _limitsButton.tooltip = "Game Limits";
                    _limitsButton.size = new Vector2(33f, 33f);
                    _limitsButton.relativePosition = ModConfig.Instance.VerticalLayout ? new Vector3(0f, 34f * buttonIndex + 22f) : new Vector3(34f * buttonIndex + 22f, 0f);

                    _limitsButton.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
                    _limitsButton.normalFgSprite = "Limits";
                    _limitsButton.hoveredFgSprite = "Limits";
                    _limitsButton.pressedFgSprite = "Limits";
                    _limitsButton.disabledFgSprite = "Limits";

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

                    _statisticsButton = UIUtils.CreateButton(_panel, "Statistics", _textureAtlas, "InfoIconBase");
                    _statisticsButton.tooltip = "City Statistics";
                    _statisticsButton.size = new Vector2(33f, 33f);
                    _statisticsButton.relativePosition = ModConfig.Instance.VerticalLayout ? new Vector3(0f, 34f * buttonIndex + 22f) : new Vector3(34f * buttonIndex + 22f, 0f);

                    _statisticsButton.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
                    _statisticsButton.normalFgSprite = "Statistics";
                    _statisticsButton.hoveredFgSprite = "Statistics";
                    _statisticsButton.pressedFgSprite = "Statistics";
                    _limitsButton.disabledFgSprite = "Statistics";

                    _statisticsButton.eventClicked += (component, eventParam) =>
                    {
                        UIView.library.ShowModal("StatisticsPanel");
                    };
                }

                if (ModConfig.Instance.VerticalLayout)
                {
                    _panel.height = 34f * ++buttonIndex + 22f;
                }
                else
                {
                    _panel.width = 34f * ++buttonIndex + 22f;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Watcher:CreateOrUpdatePanelButtons -> Exception: " + e.Message);
            }
        }

        private Watch CreateWatch(string name, string spriteName, string toolTip)
        {
            Watch watch = new Watch();

            try
            {
                watch.CreateWatch(_panel, name, ModConfig.Instance.VerticalLayout, _watches.Count, _textureAtlas, spriteName, toolTip);
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