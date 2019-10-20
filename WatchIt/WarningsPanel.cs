using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WatchIt
{
    public class WarningsPanel : UIPanel
    {
        private bool _initialized;

        private UITextureAtlas _notificationsAtlas;

        private UILabel _title;
        private UIButton _close;
        private UIDragHandle _dragHandle;
        private UILabel _lastUpdated;

        private List<Warning> _warnings;
        private List<WarningItem> _warningItems;

        public override void Awake()
        {
            base.Awake();

            try
            {
                _notificationsAtlas = ResourceLoader.GetAtlas("Notifications");

                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningsPanel:Awake -> Exception: " + e.Message);
            }
        }

        public override void Start()
        {
            base.Start();
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
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningsPanel:Update -> Exception: " + e.Message);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            foreach (WarningItem warningItem in _warningItems)
            {
                warningItem.DestroyWarningItem();
            }

            if (_title != null)
            {
                Destroy(_title);
            }
            if (_close != null)
            {
                Destroy(_close);
            }
            if (_dragHandle != null)
            {
                Destroy(_dragHandle);
            }
            if (_lastUpdated != null)
            {
                Destroy(_lastUpdated);
            }
        }

        public void ForceUpdate(List<Warning> warnings)
        {
            _warnings = warnings;

            UpdateUI();
        }

        private void CreateUI()
        {
            try
            {
                name = "WatchItWarningsPanel";
                backgroundSprite = "MenuPanel2";
                isVisible = false;
                canFocus = true;
                isInteractive = true;
                width = 750f;
                height = 650f;
                relativePosition = new Vector3(Mathf.Floor((GetUIView().fixedWidth - width) / 2f), Mathf.Floor((GetUIView().fixedHeight - height) / 2f));

                _title = UIUtils.CreateMenuPanelTitle(this, "Game Warnings - Overview");
                _close = UIUtils.CreateMenuPanelCloseButton(this);
                _dragHandle = UIUtils.CreateMenuPanelDragHandle(this);

                _lastUpdated = UIUtils.CreateLabel(this, "UpdateTime", "Updated: Never");
                _lastUpdated.textScale = 0.7f;
                _lastUpdated.textAlignment = UIHorizontalAlignment.Right;
                _lastUpdated.verticalAlignment = UIVerticalAlignment.Middle;
                _lastUpdated.relativePosition = new Vector3(30f, height - 38f);

                CreateWarningItems();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningsPanel:CreateUI -> Exception: " + e.Message);
            }
        }

        private void UpdateUI()
        {
            try
            {
                UpdateWarningItems();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningsPanel:UpdateUI -> Exception: " + e.Message);
            }
        }

        private void CreateWarningItems()
        {
            try
            {
                if (_warningItems == null)
                {
                    _warningItems = new List<WarningItem>();
                }

                for (int i = 0; i < 52; i++)
                {

                    _warningItems.Add(CreateWarningItem("WarningItem" + i));
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningsPanel:CreateWarningItems -> Exception: " + e.Message);
            }
        }

        private WarningItem CreateWarningItem(string name)
        {
            WarningItem warningItem = new WarningItem();

            try
            {
                warningItem.CreateWarningItem(this, name, _warningItems.Count, _notificationsAtlas);
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningsPanel:CreateWarningItem -> Exception: " + e.Message);
            }

            return warningItem;
        }

        private void UpdateWarningItems()
        {
            try
            {
                int i = 0;

                foreach (WarningItem warningItem in _warningItems)
                {
                    if (_warnings != null && _warnings.Count > i)
                    {
                        warningItem.UpdateWarningItem(true, _warnings[i].SpriteName, _warnings[i].Name, _warnings[i].Count);
                    }
                    else
                    {
                        warningItem.UpdateWarningItem(false, "", "", 0);
                    }

                    i++;
                }

                _lastUpdated.text = "Updated at " + DateTime.Now.ToLongTimeString();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningsPanel:UpdateWarningItems -> Exception: " + e.Message);
            }
        }
    }
}
