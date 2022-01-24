using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using WatchIt.Managers;

namespace WatchIt.Panels
{
    public class WarningPanel : UIPanel
    {
        private bool _initialized;
        private float _timer;

        private UIButton _esc;
        private ProblemPanel _problemPanel;

        private UITextureAtlas _notificationsAtlas;
        private UIButton _button;
        private UIDragHandle _dragHandle;
        private UIPanel _openClosePanel;

        private List<WarningItem> _warningItems;

        public override void Awake()
        {
            base.Awake();

            try
            {
                if (_esc == null)
                {
                    _esc = GameObject.Find("Esc")?.GetComponent<UIButton>();

                    if (_esc != null)
                    {
                        ModProperties.Instance.WarningPanelDefaultPositionX = _esc.absolutePosition.x - 1300f;
                        ModProperties.Instance.WarningPanelDefaultPositionY = _esc.absolutePosition.y;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningPanel:Awake -> Exception: " + e.Message);
            }
        }

        public override void Start()
        {
            base.Start();

            try
            {
                if (_problemPanel == null)
                {
                    _problemPanel = GameObject.Find("WatchItProblemPanel")?.GetComponent<ProblemPanel>();
                }

                if (ModConfig.Instance.WarningPositionX == 0.0f)
                {
                    ModConfig.Instance.WarningPositionX = ModProperties.Instance.WarningPanelDefaultPositionX;
                }

                if (ModConfig.Instance.WarningPositionY == 0.0f)
                {
                    ModConfig.Instance.WarningPositionY = ModProperties.Instance.WarningPanelDefaultPositionY;
                }

                _notificationsAtlas = ResourceLoader.GetAtlas("Notifications");

                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningPanel:Start -> Exception: " + e.Message);
            }
        }

        public override void Update()
        {
            base.Update();

            try
            {
                if (!_initialized)
                {
                    UpdateUI();

                    _initialized = true;
                }

                _timer += Time.deltaTime;

                if (_timer > ModConfig.Instance.RefreshInterval)
                {
                    _timer -= ModConfig.Instance.RefreshInterval;

                    if (isVisible)
                    {
                        RefreshWarnings();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningPanel:Update -> Exception: " + e.Message);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            try
            {

                if (_openClosePanel != null)
                {
                    Destroy(_openClosePanel.gameObject);
                }
                if (_dragHandle != null)
                {
                    Destroy(_dragHandle.gameObject);
                }
                if (_button != null)
                {
                    Destroy(_button.gameObject);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningPanel:OnDestroy -> Exception: " + e.Message);
            }
        }

        private void CreateUI()
        {
            try
            {
                zOrder = 25;
                size = new Vector2(46f, 46f);

                _openClosePanel = UIUtils.CreatePanel(this, "OpenClosePanel");
                _openClosePanel.backgroundSprite = "WarningPhasePanel";
                _openClosePanel.size = new Vector2(90f * ModConfig.Instance.WarningMaxItems, 46f);
                _openClosePanel.color = new Color32(82, 82, 82, 255);
                _openClosePanel.relativePosition = new Vector3(23f, 0f);
                _openClosePanel.isVisible = false;
                _openClosePanel.eventClicked += (component, eventParam) =>
                {
                    if (!eventParam.used)
                    {
                        if (_problemPanel != null)
                        {
                            if (_problemPanel.isVisible)
                            {
                                _problemPanel.Hide();
                            }
                            else
                            {
                                _problemPanel.Show();
                            }
                        }

                        eventParam.Use();
                    }
                };

                _button = UIUtils.CreateLargeButton(this, "WarningButton");
                _button.size = new Vector2(46f, 46f);
                _button.relativePosition = new Vector3(0f, 0f);
                _button.foregroundSpriteMode = UIForegroundSpriteMode.Fill;
                _button.normalFgSprite = "LineDetailButton";
                _button.focusedFgSprite = "LineDetailButton";
                _button.hoveredFgSprite = "LineDetailButton";
                _button.pressedFgSprite = "LineDetailButton";
                _button.disabledFgSprite = "LineDetailButton";
                _button.eventClicked += (component, eventParam) =>
                {
                    if (!eventParam.used)
                    {
                        if (_openClosePanel != null)
                        {
                            _openClosePanel.isVisible = !_openClosePanel.isVisible;
                        }

                        if (_button != null)
                        {
                            _button.Unfocus();
                            _button.normalBgSprite = _openClosePanel.isVisible ? "RoundBackBigFocused" : "RoundBackBig";
                        }

                        eventParam.Use();
                    }
                };

                _dragHandle = UIUtils.CreateDragHandle(_button, this);
                _dragHandle.tooltip = "Drag to move button";
                _dragHandle.size = new Vector2(46f, 46f);
                _dragHandle.relativePosition = new Vector3(0f, 0f);
                _dragHandle.eventMouseUp += (component, eventParam) =>
                {
                    ModConfig.Instance.WarningPositionX = absolutePosition.x;
                    ModConfig.Instance.WarningPositionY = absolutePosition.y;
                    ModConfig.Instance.Save();
                };

                CreateWarnings();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningPanel:CreateUI -> Exception: " + e.Message);
            }
        }

        public void ForceUpdateUI()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            try
            {
                absolutePosition = new Vector3(ModConfig.Instance.WarningPositionX, ModConfig.Instance.WarningPositionY);
                isVisible = ModConfig.Instance.ShowWarningPanel;

                UpdateWarnings();
                RefreshWarnings();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningPanel:UpdateUI -> Exception: " + e.Message);
            }
        }

        private void CreateWarnings()
        {
            try
            {
                if (_warningItems == null)
                {
                    _warningItems = new List<WarningItem>();
                }

                for (int i = 0; i < ModConfig.Instance.WarningMaxItems; i++)
                {
                    _warningItems.Add(CreateWarning("Warning" + i));
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningPanel:CreateWarnings -> Exception: " + e.Message);
            }
        }

        private void UpdateWarnings()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningPanel:UpdateWarnings -> Exception: " + e.Message);
            }
        }
        private WarningItem CreateWarning(string name)
        {
            WarningItem warningItem = new WarningItem();

            try
            {
                warningItem.CreateWarningItem(_openClosePanel, name, _warningItems.Count, _notificationsAtlas);
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningPanel:CreateWarning -> Exception: " + e.Message);
            }

            return warningItem;
        }

        private void RefreshWarnings()
        {
            try
            {
                ProblemManager problemManager = ProblemManager.Instance;

                if (!problemManager.IsUpdatingData)
                {
                    if (_warningItems != null)
                    {
                        for (int i = 0; i < _warningItems.Count; i++)
                        {
                            if (i < problemManager.ProblemTypes.Count)
                            {
                                _warningItems[i].UpdateWarningItem(problemManager.ProblemTypes[i].Sprite, problemManager.ProblemTypes[i].Total.ToString());

                                _warningItems[i].Show();
                            }
                            else
                            {
                                _warningItems[i].Hide();
                            }
                        }

                        if (ModConfig.Instance.WarningAutoOpenClose)
                        {
                            if (problemManager.ProblemTypes.Count > 0)
                            {
                                _openClosePanel.isVisible = true;
                                _button.normalBgSprite = "RoundBackBigFocused";
                            }
                            else
                            {
                                _openClosePanel.isVisible = false;
                                _button.normalBgSprite = "RoundBackBig";
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningPanel:RefreshWarnings -> Exception: " + e.Message);
            }
        }
    }
}
