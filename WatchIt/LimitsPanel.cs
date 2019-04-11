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

        private List<Limit> _limits;

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

                CreateLimits();
                UpdateLimits();
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
                UpdateLimits();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitsPanel:UpdateUI -> Exception: " + e.Message);
            }
        }

        private void CreateLimits()
        {
            try
            {
                if (_limits == null)
                {
                    _limits = new List<Limit>();
                }

                _limits.Add(CreateLimit("Areas"));
                _limits.Add(CreateLimit("Buildings"));
                _limits.Add(CreateLimit("Citizens"));
                _limits.Add(CreateLimit("Citizen Units"));
                _limits.Add(CreateLimit("Citizen Instances"));
                _limits.Add(CreateLimit("Disasters"));
                _limits.Add(CreateLimit("Districts"));
                _limits.Add(CreateLimit("Events"));
                _limits.Add(CreateLimit("Loans"));
                _limits.Add(CreateLimit("Net Segments"));
                _limits.Add(CreateLimit("Net Nodes"));
                _limits.Add(CreateLimit("Net Lanes"));                
                _limits.Add(CreateLimit("Path Units"));
                _limits.Add(CreateLimit("Props"));
                _limits.Add(CreateLimit("Radio Channels"));
                _limits.Add(CreateLimit("Radio Contents"));
                _limits.Add(CreateLimit("Transport Lines"));
                _limits.Add(CreateLimit("Trees"));
                _limits.Add(CreateLimit("Vehicles"));
                _limits.Add(CreateLimit("Vehicles Parked"));
                _limits.Add(CreateLimit("Zoned Blocks"));
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitsPanel:CreateLimits -> Exception: " + e.Message);
            }
        }

        private Limit CreateLimit(string name)
        {
            Limit limit = new Limit();

            try
            {
                limit.CreateLimit(this, name, _limits.Count);
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitsPanel:CreateLimit -> Exception: " + e.Message);
            }

            return limit;
        }

        private void UpdateLimits()
        {
            try
            {
                foreach (Limit limit in _limits)
                {
                    limit.UpdateLimit();
                }

                _lastUpdated.text = "Updated at " + DateTime.Now.ToLongTimeString();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitsPanel:UpdateLimits -> Exception: " + e.Message);
            }
        }
    }
}
