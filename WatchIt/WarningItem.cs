using ColossalFramework.UI;
using System;
using UnityEngine;

namespace WatchIt
{
    public class WarningItem
    {
        public string Name { get; set; }

        private UIPanel _panel;
        private UISprite _sprite;
        private UILabel _count;

        public void CreateWarningItem(UIComponent parent, string name, int index, UITextureAtlas atlas)
        {
            try
            {
                Name = name;

                _panel = UIUtils.CreatePanel(parent, name);
                _panel.anchor = UIAnchorStyle.Top | UIAnchorStyle.Left;
                _panel.height = 30f;
                _panel.width = 180f;
                _panel.relativePosition = new Vector3(25f + (180f * (index % 4)), 75f + (30f * (index / 4)));

                _sprite = UIUtils.CreateSprite(_panel, "WarningSprite", "BuildingEventSad");
                _sprite.atlas = atlas;
                _sprite.size = new Vector2(25f, 25f);
                _sprite.relativePosition = new Vector3(0f, 0f);

                _count = UIUtils.CreateLabel(_panel, "WarningCount", "0");
                _count.relativePosition = new Vector3(30f, 4.5f);
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningItem:CreateWarningItem -> Exception: " + e.Message);
            }
        }

        public void UpdateWarningItem(bool visible, string spriteName, string tooltip, int count)
        {
            try
            {
                if (visible)
                {
                    _panel.isVisible = true;
                    _sprite.spriteName = spriteName;
                    _sprite.tooltip = tooltip;
                    _count.text = count.ToString();
                }
                else
                {
                    _panel.isVisible = false;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningItem:UpdateWarningItem -> Exception: " + e.Message);
            }
        }

        public void DestroyWarningItem()
        {
            try
            {
                if (_sprite != null)
                {
                    UnityEngine.Object.Destroy(_sprite);
                }
                if (_count != null)
                {
                    UnityEngine.Object.Destroy(_count);
                }
                if (_panel != null)
                {
                    UnityEngine.Object.Destroy(_panel);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningItem:DestroyWarningItem -> Exception: " + e.Message);
            }
        }
    }
}
