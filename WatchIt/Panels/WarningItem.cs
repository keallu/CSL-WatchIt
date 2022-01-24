using ColossalFramework.UI;
using System;
using UnityEngine;

namespace WatchIt.Panels
{
    public class WarningItem
    {
        public string Name { get; set; }

        private UIPanel _panel;
        private UISprite _sprite;
        private UILabel _total;

        public void CreateWarningItem(UIComponent parent, string name, int index, UITextureAtlas atlas)
        {
            try
            {
                Name = name;

                _panel = UIUtils.CreatePanel(parent, name);
                _panel.anchor = UIAnchorStyle.Top | UIAnchorStyle.Left;
                _panel.height = 30f;
                _panel.width = 75f;
                _panel.relativePosition = new Vector3(30f + (80f * index), 10.5f);

                _sprite = UIUtils.CreateSprite(_panel, "Sprite", "BuildingNotification");
                _sprite.atlas = atlas;
                _sprite.height = 25f;
                _sprite.width = 25f;
                _sprite.relativePosition = new Vector3(0f, 2.5f);

                _total = UIUtils.CreateLabel(_panel, "Total", "0");
                _total.relativePosition = new Vector3(30f, 6.5f);

            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningItem:CreateWarningItem -> Exception: " + e.Message);
            }
        }

        public void DestroyWarningItem()
        {
            try
            {
                if (_total != null)
                {
                    UnityEngine.Object.Destroy(_total.gameObject);
                }
                if (_sprite != null)
                {
                    UnityEngine.Object.Destroy(_sprite.gameObject);
                }
                if (_panel != null)
                {
                    UnityEngine.Object.Destroy(_panel.gameObject);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningItem:DestroyWarningItem -> Exception: " + e.Message);
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
                Debug.Log("[Watch It!] WarningItem:Show -> Exception: " + e.Message);
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
                Debug.Log("[Watch It!] WarningItem:Hide -> Exception: " + e.Message);
            }
        }

        public void UpdateWarningItem(string sprite, string total)
        {
            try
            {
                _sprite.spriteName = sprite;
                _total.text = total;
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningItem:UpdateWarningItem -> Exception: " + e.Message);
            }
        }
    }
}
