using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WatchIt
{
    public class LimitsPanel : UIPanel
    {
        private float _timer;
        
        private UILabel _title;
        private UIButton _close;
        private UIDragHandle _dragHandle;
        private UIPanel _header;
        private UILabel _headerName;
        private UILabel _headerAmount;
        private UILabel _headerCapacity;
        private UILabel _headerComsumption;
        private UILabel _lastUpdated;

        private List<LimitItem> _limitItems;

        public override void Awake()
        {
            base.Awake();

            try
            {
                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitsPanel:Awake -> Exception: " + e.Message);
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
                if (isVisible)
                {
                    _timer += Time.deltaTime;

                    if (_timer > ModConfig.Instance.RefreshInterval)
                    {
                        _timer -= ModConfig.Instance.RefreshInterval;

                        UpdateUI();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitsPanel:Update -> Exception: " + e.Message);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            foreach (LimitItem limitItem in _limitItems)
            {
                limitItem.DestroyLimitItem();
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
            if (_header != null)
            {
                Destroy(_header);
            }
            if (_headerName != null)
            {
                Destroy(_headerName);
            }
            if (_headerAmount != null)
            {
                Destroy(_headerAmount);
            }
            if (_headerCapacity != null)
            {
                Destroy(_headerCapacity);
            }
            if (_headerComsumption != null)
            {
                Destroy(_headerComsumption);
            }
            if (_lastUpdated != null)
            {
                Destroy(_lastUpdated);
            }
        }

        private void CreateUI()
        {
            try
            {
                name = "WatchItLimitsPanel";
                backgroundSprite = "MenuPanel2";
                isVisible = false;
                canFocus = true;
                isInteractive = true;
                width = 750f;
                height = 650f;
                relativePosition = new Vector3(Mathf.Floor((GetUIView().fixedWidth - width) / 2f), Mathf.Floor((GetUIView().fixedHeight - height) / 2f));

                _title = UIUtils.CreateMenuPanelTitle(this, "Game Limits - Overview");
                _close = UIUtils.CreateMenuPanelCloseButton(this);
                _dragHandle = UIUtils.CreateMenuPanelDragHandle(this);

                _header = UIUtils.CreatePanel(this, "Header");
                _header.anchor = UIAnchorStyle.Top | UIAnchorStyle.Left;
                _header.height = 25f;
                _header.width = width - 50f;
                _header.relativePosition = new Vector3(25f, 50f);

                _headerName = UIUtils.CreateLabel(_header, "HeaderName", "Limit");
                _headerName.textAlignment = UIHorizontalAlignment.Left;
                _headerName.verticalAlignment = UIVerticalAlignment.Middle;
                _headerName.autoSize = false;
                _headerName.height = 23f;
                _headerName.width = 150f;
                _headerName.relativePosition = new Vector3(5f, 2f);

                _headerAmount = UIUtils.CreateLabel(_header, "HeaderAmount", "Amount");
                _headerAmount.textAlignment = UIHorizontalAlignment.Right;
                _headerAmount.verticalAlignment = UIVerticalAlignment.Middle;
                _headerAmount.autoSize = false;
                _headerAmount.height = 23f;
                _headerAmount.width = 150f;
                _headerAmount.relativePosition = new Vector3(165f, 2f);

                _headerCapacity = UIUtils.CreateLabel(_header, "HeaderCapacity", "Capacity");
                _headerCapacity.textAlignment = UIHorizontalAlignment.Right;
                _headerCapacity.verticalAlignment = UIVerticalAlignment.Middle;
                _headerCapacity.autoSize = false;
                _headerCapacity.height = 23f;
                _headerCapacity.width = 150f;
                _headerCapacity.relativePosition = new Vector3(325f, 2f);

                _headerComsumption = UIUtils.CreateLabel(_header, "HeaderConsumption", "Consumption");
                _headerComsumption.textAlignment = UIHorizontalAlignment.Right;
                _headerComsumption.verticalAlignment = UIVerticalAlignment.Middle;
                _headerComsumption.autoSize = false;
                _headerComsumption.height = 23f;
                _headerComsumption.width = 150f;
                _headerComsumption.relativePosition = new Vector3(485f, 2f);

                _lastUpdated = UIUtils.CreateLabel(this, "UpdateTime", "Updated: Never");
                _lastUpdated.textScale = 0.7f;
                _lastUpdated.textAlignment = UIHorizontalAlignment.Right;
                _lastUpdated.verticalAlignment = UIVerticalAlignment.Middle;
                _lastUpdated.relativePosition = new Vector3(30f, height - 38f);

                CreateLimitItems();
                UpdateLimitItems();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitsPanel:CreateUI -> Exception: " + e.Message);
            }
        }

        private void UpdateUI()
        {
            try
            {
                UpdateLimitItems();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitsPanel:UpdateUI -> Exception: " + e.Message);
            }
        }

        private void CreateLimitItems()
        {
            try
            {
                if (_limitItems == null)
                {
                    _limitItems = new List<LimitItem>();
                }

                _limitItems.Add(CreateLimitItem("Areas"));
                _limitItems.Add(CreateLimitItem("Buildings"));
                _limitItems.Add(CreateLimitItem("Citizens"));
                _limitItems.Add(CreateLimitItem("Citizen Units"));
                _limitItems.Add(CreateLimitItem("Citizen Instances"));
                _limitItems.Add(CreateLimitItem("Disasters"));
                _limitItems.Add(CreateLimitItem("Districts"));
                _limitItems.Add(CreateLimitItem("Events"));
                _limitItems.Add(CreateLimitItem("Loans"));
                _limitItems.Add(CreateLimitItem("Net Segments"));
                _limitItems.Add(CreateLimitItem("Net Nodes"));
                _limitItems.Add(CreateLimitItem("Net Lanes"));                
                _limitItems.Add(CreateLimitItem("Path Units"));
                _limitItems.Add(CreateLimitItem("Props"));
                _limitItems.Add(CreateLimitItem("Radio Channels"));
                _limitItems.Add(CreateLimitItem("Radio Contents"));
                _limitItems.Add(CreateLimitItem("Transport Lines"));
                _limitItems.Add(CreateLimitItem("Trees"));
                _limitItems.Add(CreateLimitItem("Vehicles"));
                _limitItems.Add(CreateLimitItem("Vehicles Parked"));
                _limitItems.Add(CreateLimitItem("Zoned Blocks"));
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitsPanel:CreateLimitItems -> Exception: " + e.Message);
            }
        }

        private LimitItem CreateLimitItem(string name)
        {
            LimitItem limitItem = new LimitItem();

            try
            {
                limitItem.CreateLimitItem(this, name, _limitItems.Count);
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitsPanel:CreateLimitItem -> Exception: " + e.Message);
            }

            return limitItem;
        }

        private void UpdateLimitItems()
        {
            try
            {
                foreach (LimitItem limit in _limitItems)
                {
                    limit.UpdateLimitItem();
                }

                _lastUpdated.text = "Updated at " + DateTime.Now.ToLongTimeString();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitsPanel:UpdateLimitItems -> Exception: " + e.Message);
            }
        }
    }
}
