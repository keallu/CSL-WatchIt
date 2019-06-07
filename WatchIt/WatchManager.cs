using ColossalFramework;
using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WatchIt
{
    public class WatchManager : MonoBehaviour
    {
        private bool _initialized;
        private float _timer;

        private UIButton _esc;
        private UITextureAtlas _watchItAtlas;
        private UITextureAtlas _notificationsAtlas;
        
        private UIPanel _warningPanel;
        private UIButton _warningButton;
        private UIDragHandle _warningDragHandle;
        private UIPanel _openClosePanel;
        private UISprite _warningTop1Sprite;
        private UILabel _warningTop1Label;
        private UISprite _warningTop2Sprite;
        private UILabel _warningTop2Label;
        private UISprite _warningTop3Sprite;
        private UILabel _warningTop3Label;

        private UIPanel _panel;
        private UIDragHandle _dragHandle;
        private UISprite _dragSprite;
        private UIButton _limitsButton;
        private UIButton _statisticsButton;

        private List<Watch> _watches;

        public void Awake()
        {
            try
            {
                if (_esc == null)
                {
                    _esc = GameObject.Find("Esc").GetComponent<UIButton>();
                    WatchProperties.Instance.WarningPanelDefaultPositionX = _esc.absolutePosition.x - 1300f;
                    WatchProperties.Instance.WarningPanelDefaultPositionY = _esc.absolutePosition.y;
                    WatchProperties.Instance.PanelDefaultPositionX = ModConfig.Instance.DoubleRibbonLayout ? _esc.absolutePosition.x - 29f : _esc.absolutePosition.x - 13f;
                    WatchProperties.Instance.PanelDefaultPositionY = _esc.absolutePosition.y + 50f;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WatchManager:Awake -> Exception: " + e.Message);
            }
        }

        public void Start()
        {
            try
            {
                if (ModConfig.Instance.WarningPositionX == 0.0f)
                {
                    ModConfig.Instance.WarningPositionX = WatchProperties.Instance.WarningPanelDefaultPositionX;
                }

                if (ModConfig.Instance.WarningPositionY == 0.0f)
                {
                    ModConfig.Instance.WarningPositionY = WatchProperties.Instance.WarningPanelDefaultPositionY;
                }

                if (ModConfig.Instance.PositionX == 0.0f)
                {
                    ModConfig.Instance.PositionX = WatchProperties.Instance.PanelDefaultPositionX;
                }

                if (ModConfig.Instance.PositionY == 0.0f)
                {
                    ModConfig.Instance.PositionY = WatchProperties.Instance.PanelDefaultPositionY;
                }

                _watchItAtlas = LoadResources();
                _notificationsAtlas = ResourceLoader.GetAtlas("Notifications");

                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WatchManager:Start -> Exception: " + e.Message);
            }
        }

        public void Update()
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
                        _timer -= ModConfig.Instance.RefreshInterval;

                        if (ModConfig.Instance.ShowWarningPanel)
                        {
                            UpdateWarnings();
                        }

                        if (ModConfig.Instance.ShowPanel)
                        {
                            UpdateWatches();
                        }
                    }
                }

                if (ModConfig.Instance.WarningKeyMappingEnabled && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.W))
                {
                    ToggleWarningPanel();
                }
                else if (ModConfig.Instance.KeyMappingEnabled && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.W))
                {
                    TogglePanel();
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WatchManager:Update -> Exception: " + e.Message);
            }
        }

        public void OnDestroy()
        {
            try
            {
                foreach (Watch watch in _watches)
                {
                    watch.DestroyWatch();
                }
                if (_limitsButton != null)
                {
                    Destroy(_limitsButton);
                }
                if (_statisticsButton != null)
                {
                    Destroy(_statisticsButton);
                }
                if (_dragHandle != null)
                {
                    Destroy(_dragHandle);
                }
                if (_dragSprite != null)
                {
                    Destroy(_dragSprite);
                }
                if (_panel != null)
                {
                    Destroy(_panel);
                }
                if (_warningTop3Label != null)
                {
                    Destroy(_warningTop3Label);
                }
                if (_warningTop3Sprite != null)
                {
                    Destroy(_warningTop3Sprite);
                }
                if (_warningTop2Label != null)
                {
                    Destroy(_warningTop2Label);
                }
                if (_warningTop2Sprite != null)
                {
                    Destroy(_warningTop2Sprite);
                }
                if (_warningTop1Label != null)
                {
                    Destroy(_warningTop1Label);
                }
                if (_warningTop1Sprite != null)
                {
                    Destroy(_warningTop1Sprite);
                }
                if (_openClosePanel != null)
                {
                    Destroy(_openClosePanel);
                }
                if (_warningDragHandle != null)
                {
                    Destroy(_warningDragHandle);
                }
                if (_warningButton != null)
                {
                    Destroy(_warningButton);
                }
                if (_warningPanel != null)
                {
                    Destroy(_warningPanel);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WatchManager:OnDestroy -> Exception: " + e.Message);
            }
        }

        private UITextureAtlas LoadResources()
        {
            try
            {
                if (_watchItAtlas == null)
                {
                    string[] spriteNames = new string[]
                    {
                        "CircleNormal",
                        "CircleHovered",
                        "CirclePressed",
                        "RectNormal",
                        "RectHovered",
                        "RectPressed",
                        "Drag",
                        "DragHover",
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
                        "FireDepartment",
                        "PoliceDepartment",
                        "Jail",
                        "Heating",
                        "Landfill",
                        "Library",
                        "Cemetery",
                        "Traffic",
                        "GroundPollution",
                        "DrinkingWaterPollution",
                        "NoisePollution",
                        "Fire",
                        "Crime",
                        "Unemployment",
                        "Health",
                        "CityAttractiveness",
                        "Happiness",
                        "Statistics",
                        "Limits"
                    };

                    _watchItAtlas = ResourceLoader.CreateTextureAtlas("WatchItAtlas", spriteNames, "WatchIt.Icons.");
                }

                return _watchItAtlas;
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WatchManager:LoadResources -> Exception: " + e.Message);
                return null;
            }
        }

        private void CreateUI()
        {
            try
            {
                _warningPanel = UIUtils.CreatePanel("WatchItWarningPanel");
                _warningPanel.zOrder = 0;
                _warningPanel.size = new Vector2(46f, 46f);

                _openClosePanel = UIUtils.CreatePanel(_warningPanel, "OpenClosePanel");
                _openClosePanel.backgroundSprite = "WarningPhasePanel";
                _openClosePanel.size = new Vector2(250f, 46f);
                _openClosePanel.color = new Color32(82, 82, 82, 255);
                _openClosePanel.relativePosition = new Vector3(23f, 0f);
                _openClosePanel.isInteractive = false;
                _openClosePanel.isVisible = false;

                _warningTop1Sprite = UIUtils.CreateSprite(_openClosePanel, "Top1Sprite", "BuildingEventSad");
                _warningTop1Sprite.atlas = _notificationsAtlas;
                _warningTop1Sprite.size = new Vector2(25f, 25f);
                _warningTop1Sprite.relativePosition = new Vector3(25f, 10.5f);

                _warningTop1Label = UIUtils.CreateLabel(_openClosePanel, "Top1Label", "");
                _warningTop1Label.relativePosition = new Vector3(55f, 15f);

                _warningTop2Sprite = UIUtils.CreateSprite(_openClosePanel, "Top2Sprite", "BuildingEventSad");
                _warningTop2Sprite.atlas = _notificationsAtlas;
                _warningTop2Sprite.size = new Vector2(25f, 25f);
                _warningTop2Sprite.relativePosition = new Vector3(100f, 10.5f);

                _warningTop2Label = UIUtils.CreateLabel(_openClosePanel, "Top2Label", "");
                _warningTop2Label.relativePosition = new Vector3(130f, 15f);

                _warningTop3Sprite = UIUtils.CreateSprite(_openClosePanel, "Top3Sprite", "BuildingEventSad");
                _warningTop3Sprite.atlas = _notificationsAtlas;
                _warningTop3Sprite.size = new Vector2(25f, 25f);
                _warningTop3Sprite.relativePosition = new Vector3(175f, 10.5f);

                _warningTop3Label = UIUtils.CreateLabel(_openClosePanel, "Top3Label", "");
                _warningTop3Label.relativePosition = new Vector3(205f, 15f);

                _warningButton = UIUtils.CreateLargeButton(_warningPanel, "WarningButton");
                _warningButton.size = new Vector2(46f, 46f);
                _warningButton.relativePosition = new Vector3(0f, 0f);
                _warningButton.foregroundSpriteMode = UIForegroundSpriteMode.Fill;
                _warningButton.normalFgSprite = "LineDetailButton";
                _warningButton.focusedFgSprite = "LineDetailButton";
                _warningButton.hoveredFgSprite = "LineDetailButton";
                _warningButton.pressedFgSprite = "LineDetailButton";
                _warningButton.disabledFgSprite = "LineDetailButton";
                _warningButton.eventClick += (component, eventParam) =>
                {
                    if (!eventParam.used)
                    {
                        _openClosePanel.isVisible = !_openClosePanel.isVisible;

                        _warningButton.Unfocus();
                        _warningButton.normalBgSprite = _openClosePanel.isVisible ? "RoundBackBigFocused" : "RoundBackBig";

                        eventParam.Use();
                    }
                };

                _warningDragHandle = UIUtils.CreateDragHandle(_warningButton, _warningPanel);
                _warningDragHandle.tooltip = "Drag to move button";
                _warningDragHandle.size = new Vector2(46f, 46f);
                _warningDragHandle.relativePosition = new Vector3(0f, 0f);
                _warningDragHandle.eventMouseUp += (component, eventParam) =>
                {
                    ModConfig.Instance.WarningPositionX = _warningPanel.absolutePosition.x;
                    ModConfig.Instance.WarningPositionY = _warningPanel.absolutePosition.y;
                    ModConfig.Instance.Save();
                };

                _panel = UIUtils.CreatePanel("WatchItPanel");
                _panel.zOrder = 0;
                _panel.autoSize = false;
                _panel.autoLayout = false;
                _panel.eventMouseEnter += (component, eventParam) =>
                {
                    _panel.opacity = ModConfig.Instance.OpacityWhenHover;
                };
                _panel.eventMouseLeave += (component, eventParam) =>
                {
                    _panel.opacity = ModConfig.Instance.Opacity;
                };

                _dragHandle = UIUtils.CreateDragHandle(_panel);
                _dragHandle.tooltip = "Drag to move panel";
                _dragHandle.size = new Vector2(30f, 30f);
                _dragHandle.relativePosition = new Vector3(3f, 3f);
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

                _dragSprite = UIUtils.CreateSprite(_panel, "Drag", _watchItAtlas, "Drag");
                _dragSprite.isInteractive = false;
                _dragSprite.size = new Vector2(30f, 30f);
                _dragSprite.relativePosition = new Vector3(3f, 3f);

                UpdateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WatchManager:CreateUI -> Exception: " + e.Message);
            }
        }

        private void UpdateUI()
        {
            try
            {
                _warningPanel.absolutePosition = new Vector3(ModConfig.Instance.WarningPositionX, ModConfig.Instance.WarningPositionY);
                _warningPanel.isVisible = ModConfig.Instance.ShowWarningPanel;

                _panel.absolutePosition = new Vector3(ModConfig.Instance.PositionX, ModConfig.Instance.PositionY);
                _panel.isVisible = ModConfig.Instance.ShowPanel;
                _panel.opacity = ModConfig.Instance.Opacity;

                if (ModConfig.Instance.VerticalLayout)
                {
                    _panel.width = ModConfig.Instance.DoubleRibbonLayout ? 72f : 36f;
                }
                else
                {
                    _panel.height = ModConfig.Instance.DoubleRibbonLayout ? 72f : 36f;
                }

                if (ModConfig.Instance.ShowDragIcon)
                {
                    _dragHandle.isEnabled = true;
                    _dragSprite.isVisible = true;
                }
                else
                {
                    _dragHandle.isEnabled = false;
                    _dragSprite.isVisible = false;
                }

                if (ModConfig.Instance.DoubleRibbonLayout)
                {
                    _dragHandle.relativePosition = ModConfig.Instance.VerticalLayout ? new Vector3(21f, 3f) : new Vector3(3f, 21f);
                    _dragSprite.relativePosition = ModConfig.Instance.VerticalLayout ? new Vector3(21f, 3f) : new Vector3(3f, 21f);
                }
                else
                {
                    _dragHandle.relativePosition = new Vector3(3f, 3f);
                    _dragSprite.relativePosition = new Vector3(3f, 3f);
                }

                CreateOrUpdateWatchPanelButtons();

                UpdateWatches();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WatchManager:UpdateUI -> Exception: " + e.Message);
            }
        }

        private void ToggleWarningPanel()
        {
            try
            {
                _warningPanel.isVisible = !_warningPanel.isVisible;
                ModConfig.Instance.ShowWarningPanel = _warningPanel.isVisible;
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ResizeManager:ToggleWarningPanel -> Exception: " + e.Message);
            }
        }

        private void TogglePanel()
        {
            try
            {
                _panel.isVisible = !_panel.isVisible;
                ModConfig.Instance.ShowPanel = _panel.isVisible;
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ResizeManager:TogglePanel -> Exception: " + e.Message);
            }
        }

        private void UpdateWarnings()
        {
            try
            {
                _warningTop1Sprite.isVisible = false;
                _warningTop1Label.isVisible = false;
                _warningTop2Sprite.isVisible = false;
                _warningTop2Label.isVisible = false;
                _warningTop3Sprite.isVisible = false;
                _warningTop3Label.isVisible = false;

                List<Warning> warnings = WatchUtils.GetWarnings(ModConfig.Instance.WarningBuildings, ModConfig.Instance.WarningNetworks, ModConfig.Instance.WarningThreshold);

                if (warnings != null && warnings.Count > 0)
                {
                    _openClosePanel.isVisible = true;
                    _warningButton.normalBgSprite = "RoundBackBigFocused";                    

                    string i = Utils.GetNameByValue(warnings[0].Problem, "Normal");

                    _warningTop1Sprite.spriteName = Utils.GetNameByValue(warnings[0].Problem, "Normal");
                    _warningTop1Sprite.tooltip = TextUtils.AddSpacesBeforeCapitalLetters(Utils.GetNameByValue(warnings[0].Problem, "Text"));
                    _warningTop1Label.text = warnings[0].Count.ToString();
                    _warningTop1Sprite.isVisible = true;
                    _warningTop1Label.isVisible = true;

                    if (warnings.Count > 1)
                    {
                        _warningTop2Sprite.spriteName = Utils.GetNameByValue(warnings[1].Problem, "Normal");
                        _warningTop2Sprite.tooltip = TextUtils.AddSpacesBeforeCapitalLetters(Utils.GetNameByValue(warnings[1].Problem, "Text"));
                        _warningTop2Label.text = warnings[1].Count.ToString();
                        _warningTop2Sprite.isVisible = true;
                        _warningTop2Label.isVisible = true;
                    }

                    if (warnings.Count > 2)
                    {
                        _warningTop3Sprite.spriteName = Utils.GetNameByValue(warnings[2].Problem, "Normal");
                        _warningTop3Sprite.tooltip = TextUtils.AddSpacesBeforeCapitalLetters(Utils.GetNameByValue(warnings[2].Problem, "Text"));
                        _warningTop3Label.text = warnings[2].Count.ToString();
                        _warningTop3Sprite.isVisible = true;
                        _warningTop3Label.isVisible = true;
                    }
                }
                else
                {
                    _openClosePanel.isVisible = false;
                    _warningButton.normalBgSprite = "RoundBackBig";
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WatchManager:UpdateWarnings -> Exception: " + e.Message);
            }
        }

        private void CreateOrUpdateWatchPanelButtons()
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
                    _watches.Add(CreateWatch("Electricity", Watch.WatchType.Aspect, "Electricity", "Electricity Availability"));
                }
                if (ModConfig.Instance.WaterAvailability)
                {
                    _watches.Add(CreateWatch("Water", Watch.WatchType.Aspect, "Water", "Water Availability"));
                }
                if (ModConfig.Instance.SewageAvailability)
                {
                    _watches.Add(CreateWatch("Sewage", Watch.WatchType.Aspect, "Sewage", "Sewage Availability"));
                }
                if (ModConfig.Instance.GarbageAvailability)
                {
                    _watches.Add(CreateWatch("Garbage", Watch.WatchType.Aspect, "Garbage", "Garbage Availability"));
                }
                if (ModConfig.Instance.ElementarySchoolAvailability)
                {
                    _watches.Add(CreateWatch("ElementarySchool", Watch.WatchType.Aspect, "ElementarySchool", "Elementary School Availability"));
                }
                if (ModConfig.Instance.HighSchoolAvailability)
                {
                    _watches.Add(CreateWatch("HighSchool", Watch.WatchType.Aspect, "HighSchool", "High School Availability"));
                }
                if (ModConfig.Instance.UniversityAvailability)
                {
                    _watches.Add(CreateWatch("University", Watch.WatchType.Aspect, "University", "University Availability"));
                }
                if (ModConfig.Instance.HealthcareAvailability)
                {
                    _watches.Add(CreateWatch("Healthcare", Watch.WatchType.Aspect, "Healthcare", "Healthcare Availability"));
                }
                if (ModConfig.Instance.CrematoriumAvailability)
                {
                    _watches.Add(CreateWatch("Crematorium", Watch.WatchType.Aspect, "Crematorium", "Crematorium Availability"));
                }
                if (ModConfig.Instance.FireDepartmentAvailability)
                {
                    _watches.Add(CreateWatch("FireDepartment", Watch.WatchType.Aspect, "FireDepartment", "Fire Department Availability"));
                }
                if (ModConfig.Instance.PoliceDepartmentAvailability)
                {
                    _watches.Add(CreateWatch("PoliceDepartment", Watch.WatchType.Aspect, "PoliceDepartment", "Police Department Availability"));
                }
                if (ModConfig.Instance.JailAvailability)
                {
                    _watches.Add(CreateWatch("Jail", Watch.WatchType.Aspect, "Jail", "Jail Availability"));
                }
                if (ModConfig.Instance.HeatingAvailability)
                {
                    _watches.Add(CreateWatch("Heating", Watch.WatchType.Aspect, "Heating", "Heating Availability"));
                }
                if (ModConfig.Instance.LandfillUsage)
                {
                    _watches.Add(CreateWatch("Landfill", Watch.WatchType.Pillar, "Landfill", "Landfill Usage"));
                }
                if (ModConfig.Instance.LibraryUsage)
                {
                    _watches.Add(CreateWatch("Library", Watch.WatchType.Pillar, "Library", "Library Usage"));
                }
                if (ModConfig.Instance.CemeteryUsage)
                {
                    _watches.Add(CreateWatch("Cemetery", Watch.WatchType.Pillar, "Cemetery", "Cemetery Usage"));
                }
                if (ModConfig.Instance.TrafficFlow)
                {
                    _watches.Add(CreateWatch("Traffic", Watch.WatchType.Pillar, "Traffic", "Traffic Flow"));
                }
                if (ModConfig.Instance.GroundPollution)
                {
                    _watches.Add(CreateWatch("GroundPollution", Watch.WatchType.Pillar, "GroundPollution", "Ground Pollution"));
                }
                if (ModConfig.Instance.DrinkingWaterPollution)
                {
                    _watches.Add(CreateWatch("DrinkingWaterPollution", Watch.WatchType.Pillar, "DrinkingWaterPollution", "Drinking Water Pollution"));
                }
                if (ModConfig.Instance.NoisePollution)
                {
                    _watches.Add(CreateWatch("NoisePollution", Watch.WatchType.Pillar, "NoisePollution", "Noise Pollution"));
                }
                if (ModConfig.Instance.FireHazard)
                {
                    _watches.Add(CreateWatch("Fire", Watch.WatchType.Pillar, "Fire", "Fire Hazard"));
                }
                if (ModConfig.Instance.CrimeRate)
                {
                    _watches.Add(CreateWatch("Crime", Watch.WatchType.Pillar, "Crime", "Crime Rate"));
                }
                if (ModConfig.Instance.UnemploymentRate)
                {
                    _watches.Add(CreateWatch("Unemployment", Watch.WatchType.Pillar, "Unemployment", "Unemployment Rate"));
                }
                if (ModConfig.Instance.HealthAverage)
                {
                    _watches.Add(CreateWatch("Health", Watch.WatchType.Pillar, "Health", "Health Average"));
                }
                if (ModConfig.Instance.CityAttractiveness)
                {
                    _watches.Add(CreateWatch("CityAttractiveness", Watch.WatchType.Pillar, "CityAttractiveness", "City Attractiveness"));
                }
                if (ModConfig.Instance.Happiness)
                {
                    _watches.Add(CreateWatch("Happiness", Watch.WatchType.Pillar, "Happiness", "Happiness"));
                }

                int buttonIndex = _watches.Count;

                bool numberOfWatchesAreOdd = buttonIndex % 2 != 0 ? true : false;

                if (ModConfig.Instance.DoubleRibbonLayout && !numberOfWatchesAreOdd)
                {
                    buttonIndex += 1;
                }

                if (ModConfig.Instance.ShowGameLimitsButton)
                {
                    buttonIndex++;

                    _limitsButton = UIUtils.CreateButton(_panel, "Limits", _watchItAtlas, "Circle");
                    _limitsButton.tooltip = "Game Limits";
                    _limitsButton.size = new Vector2(33f, 33f);

                    if (ModConfig.Instance.DoubleRibbonLayout)
                    {
                        _limitsButton.relativePosition = ModConfig.Instance.VerticalLayout ? new Vector3(36f * (buttonIndex % 2) + 1.5f, 36f * (buttonIndex / 2) + 36f) : new Vector3(36f * (buttonIndex / 2) + 36f, 36f * (buttonIndex % 2) + 1.5f);
                    }
                    else
                    {
                        _limitsButton.relativePosition = ModConfig.Instance.VerticalLayout ? new Vector3(1.5f, 36f * buttonIndex + 36f) : new Vector3(36f * buttonIndex + 36f, 1.5f);
                    }

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

                    _statisticsButton = UIUtils.CreateButton(_panel, "Statistics", _watchItAtlas, "Circle");
                    _statisticsButton.tooltip = "City Statistics";
                    _statisticsButton.size = new Vector2(33f, 33f);

                    if (ModConfig.Instance.DoubleRibbonLayout)
                    {
                        _statisticsButton.relativePosition = ModConfig.Instance.VerticalLayout ? new Vector3(36f * (buttonIndex % 2) + 1.5f, 36f * (buttonIndex / 2) + 36f) : new Vector3(36f * (buttonIndex / 2) + 36f, 36f * (buttonIndex % 2) + 1.5f);
                    }
                    else
                    {
                        _statisticsButton.relativePosition = ModConfig.Instance.VerticalLayout ? new Vector3(1.5f, 36f * buttonIndex + 36f) : new Vector3(36f * buttonIndex + 36f, 1.5f);
                    }

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
                    _panel.height = ModConfig.Instance.DoubleRibbonLayout ? (36f * ++buttonIndex / 2) + 36f : 36f * ++buttonIndex + 36f;
                }
                else
                {
                    _panel.width = ModConfig.Instance.DoubleRibbonLayout ? (36f * ++buttonIndex / 2) + 36f : 36f * ++buttonIndex + 36f;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WatchManager:CreateOrUpdatePanelButtons -> Exception: " + e.Message);
            }
        }

        private Watch CreateWatch(string name, Watch.WatchType type, string spriteName, string toolTip)
        {
            Watch watch = new Watch();

            try
            {
                watch.CreateWatch(_panel, name, type, _watches.Count, _watchItAtlas, spriteName, toolTip);
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WatchManager:CreateWatch -> Exception: " + e.Message);
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
                Debug.Log("[Watch It!] WatchManager:UpdateWatches -> Exception: " + e.Message);
            }
        }
    }
}