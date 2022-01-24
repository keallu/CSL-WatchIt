using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WatchIt.Panels
{
    public class LimitPanel : UIPanel
    {
        private bool _initialized;
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

            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitPanel:Awake -> Exception: " + e.Message);
            }
        }

        public override void Start()
        {
            base.Start();

            try
            {
                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitPanel:Start -> Exception: " + e.Message);
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

                    if (isVisible && ModConfig.Instance.LimitAutoRefresh)
                    {
                        RefreshLimits();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitPanel:Update -> Exception: " + e.Message);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            try
            {
                foreach (LimitItem limitItem in _limitItems)
                {
                    limitItem.DestroyLimitItem();
                }

                if (_title != null)
                {
                    Destroy(_title.gameObject);
                }
                if (_close != null)
                {
                    Destroy(_close.gameObject);
                }
                if (_dragHandle != null)
                {
                    Destroy(_dragHandle.gameObject);
                }
                if (_header != null)
                {
                    Destroy(_header.gameObject);
                }
                if (_headerName != null)
                {
                    Destroy(_headerName.gameObject);
                }
                if (_headerAmount != null)
                {
                    Destroy(_headerAmount.gameObject);
                }
                if (_headerCapacity != null)
                {
                    Destroy(_headerCapacity.gameObject);
                }
                if (_headerComsumption != null)
                {
                    Destroy(_headerComsumption.gameObject);
                }
                if (_lastUpdated != null)
                {
                    Destroy(_lastUpdated.gameObject);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitPanel:OnDestroy -> Exception: " + e.Message);
            }
        }

        private void CreateUI()
        {
            try
            {
                backgroundSprite = "MenuPanel2";
                isVisible = false;
                canFocus = true;
                isInteractive = true;
                width = 750f;
                height = 650f;
                relativePosition = new Vector3(Mathf.Floor((GetUIView().fixedWidth - width) / 2f), Mathf.Floor((GetUIView().fixedHeight - height) / 2f));

                _title = UIUtils.CreateMenuPanelTitle(this, "Limits - Overview");
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

                CreateLimits();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitPanel:CreateUI -> Exception: " + e.Message);
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
                UpdateLimits();
                RefreshLimits();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitPanel:UpdateUI -> Exception: " + e.Message);
            }
        }

        private void CreateLimits()
        {
            try
            {
                if (_limitItems == null)
                {
                    _limitItems = new List<LimitItem>();
                }

                _limitItems.Add(CreateLimit("Areas"));
                _limitItems.Add(CreateLimit("Buildings"));
                _limitItems.Add(CreateLimit("Citizens"));
                _limitItems.Add(CreateLimit("Citizen Units"));
                _limitItems.Add(CreateLimit("Citizen Instances"));
                _limitItems.Add(CreateLimit("Disasters"));
                _limitItems.Add(CreateLimit("Districts"));
                _limitItems.Add(CreateLimit("Events"));
                _limitItems.Add(CreateLimit("Loans"));
                _limitItems.Add(CreateLimit("Net Segments"));
                _limitItems.Add(CreateLimit("Net Nodes"));
                _limitItems.Add(CreateLimit("Net Lanes"));
                _limitItems.Add(CreateLimit("Path Units"));
                _limitItems.Add(CreateLimit("Props"));
                _limitItems.Add(CreateLimit("Radio Channels"));
                _limitItems.Add(CreateLimit("Radio Contents"));
                _limitItems.Add(CreateLimit("Transport Lines"));
                _limitItems.Add(CreateLimit("Trees"));
                _limitItems.Add(CreateLimit("Vehicles"));
                _limitItems.Add(CreateLimit("Vehicles Parked"));
                _limitItems.Add(CreateLimit("Zoned Blocks"));
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitPanel:CreateLimits -> Exception: " + e.Message);
            }
        }

        private void UpdateLimits()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitPanel:UpdateLimits -> Exception: " + e.Message);
            }
        }

        private LimitItem CreateLimit(string name)
        {
            LimitItem limitItem = new LimitItem();

            try
            {
                limitItem.CreateLimitItem(this, name, _limitItems.Count);
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitPanel:CreateLimit -> Exception: " + e.Message);
            }

            return limitItem;
        }

        private void RefreshLimits()
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
                Debug.Log("[Watch It!] LimitPanel:UpdateLimits -> Exception: " + e.Message);
            }
        }
    }
}
