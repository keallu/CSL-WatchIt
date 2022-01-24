using ColossalFramework.UI;
using System;
using UnityEngine;

namespace WatchIt.Panels
{
    public class ProblemItem
    {
        public string Name { get; set; }

        private InstanceID _instanceID;
        private Vector3 _position;

        private UIPanel _panel;
        private UILabel _title;
        private UISprite[] _sprites;
        private UIButton _button;

        public void CreateProblemItem(UIComponent parent, string name, int index, UITextureAtlas atlas)
        {
            try
            {
                Name = name;

                _panel = UIUtils.CreatePanel(parent, name);
                _panel.anchor = UIAnchorStyle.Top | UIAnchorStyle.Left;
                _panel.backgroundSprite = "InfoviewPanel";
                _panel.color = index % 2 != 0 ? new Color32(56, 61, 63, 255) : new Color32(49, 52, 58, 255);
                _panel.height = 25f;
                _panel.width = parent.width;
                _panel.relativePosition = new Vector3(25f, 75f + (25f * index));
                _panel.eventMouseEnter += (component, eventParam) =>
                {
                    _panel.color = new Color32(73, 78, 87, 255);
                };
                _panel.eventMouseLeave += (component, eventParam) =>
                {
                    _panel.color = index % 2 != 0 ? new Color32(56, 61, 63, 255) : new Color32(49, 52, 58, 255);
                };
                _panel.eventClicked += (component, eventParam) =>
                {
                    if (!eventParam.used)
                    {
                        if (ModConfig.Instance.ProblemAutoClose)
                        {
                            _panel.parent.parent.parent.isVisible = false;
                        }

                        ToolsModifierControl.cameraController.SetTarget(_instanceID, _position, false);

                        eventParam.Use();
                    }
                };

                _title = UIUtils.CreateLabel(_panel, "Title", "");
                _title.textScale = 0.875f;
                _title.textColor = new Color32(185, 221, 254, 255);
                _title.textAlignment = UIHorizontalAlignment.Left;
                _title.verticalAlignment = UIVerticalAlignment.Middle;
                _title.autoSize = false;
                _title.height = 23f;
                _title.width = 377.5f;
                _title.relativePosition = new Vector3(10f, 2f);

                _sprites = new UISprite[10];

                for (int i = 0; i < _sprites.Length; i++)
                {
                    _sprites[i] = UIUtils.CreateSprite(_panel, "Sprite" + i, "BuildingNotification");
                    _sprites[i].atlas = atlas;
                    _sprites[i].height = 20f;
                    _sprites[i].width = 20f;
                    _sprites[i].relativePosition = new Vector3(347.5f + (i * 25f), 2f);
                }

                _button = UIUtils.CreateButton(_panel, "Button", "LocationMarker");
                _button.height = 20f;
                _button.width = 20f;
                _button.relativePosition = new Vector3(655f, 2.5f);
                _button.eventClicked += (component, eventParam) =>
                {
                    if (!eventParam.used)
                    {
                        if (ModConfig.Instance.ProblemAutoClose)
                        {
                            _panel.parent.parent.parent.isVisible = false;
                        }

                        DefaultTool.OpenWorldInfoPanel(_instanceID, _position);
                        ToolsModifierControl.cameraController.SetTarget(_instanceID, _position, true);

                        eventParam.Use();
                    }
                };

            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemItem:CreateProblemItem -> Exception: " + e.Message);
            }
        }
        public void DestroyProblemItem()
        {
            try
            {
                if (_button != null)
                {
                    UnityEngine.Object.Destroy(_button.gameObject);
                }
                if (_sprites != null)
                {
                    foreach (UISprite sprite in _sprites)
                    {
                        if (sprite != null)
                        {
                            UnityEngine.Object.Destroy(sprite.gameObject);
                        }
                    }
                }
                if (_title != null)
                {
                    UnityEngine.Object.Destroy(_title.gameObject);
                }
                if (_panel != null)
                {
                    UnityEngine.Object.Destroy(_panel.gameObject);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemItem:DestroyProblemItem -> Exception: " + e.Message);
            }
        }

        public void Show()
        {
            try
            {
                _panel.Show();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemItem:Show -> Exception: " + e.Message);
            }
        }

        public void Hide()
        {
            try
            {
                _panel.Hide();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemItem:Hide -> Exception: " + e.Message);
            }
        }

        public void UpdateProblemItem(string title, string[] sprites, InstanceID instanceID, Vector3 position)
        {
            try
            {
                _title.text = title;

                for (int i = 0; i < _sprites.Length; i++)
                {
                    if (i < sprites.Length)
                    {
                        _sprites[i].spriteName = sprites[i];
                        _sprites[i].Show();
                    }
                    else
                    {
                        _sprites[i].Hide();
                    }
                }

                _instanceID = instanceID;
                _position = position;
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemItem:UpdateProblemItem -> Exception: " + e.Message);
            }
        }

    }
}
