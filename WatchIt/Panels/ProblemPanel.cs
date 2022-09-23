using ColossalFramework;
using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using WatchIt.Managers;

namespace WatchIt.Panels
{
    public class ProblemPanel : UIPanel
    {
        private bool _initialized;
        private float _timer;

        private UITextureAtlas _notificationsAtlas;
        private UILabel _title;
        private UIButton _close;
        private UIDragHandle _dragHandle;

        private UITabstrip _tabstrip;
        private UITabContainer _tabContainer;
        private UIButton _templateButton;

        private UIPanel _problemPanel;
        private UIScrollablePanel _problemScrollablePanel;
        private UIScrollbar _problemScrollbar;
        private UISlicedSprite _problemScrollbarTrack;
        private UISlicedSprite _problemScrollbarThumb;
        private UILabel _lastUpdated;
        private UILabel _counter;

        private List<ProblemItem> _problemItems;

        public override void Awake()
        {
            base.Awake();

            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemPanel:Awake -> Exception: " + e.Message);
            }
        }

        public override void Start()
        {
            base.Start();

            try
            {
                _notificationsAtlas = ResourceLoader.GetAtlas("Notifications");

                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemPanel:Start -> Exception: " + e.Message);
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

                    if (isVisible && ModConfig.Instance.ProblemAutoRefresh)
                    {
                        RefreshProblems();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemPanel:Update -> Exception: " + e.Message);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            try
            {
                foreach (ProblemItem problemItem in _problemItems)
                {
                    problemItem.DestroyProblemItem();
                }
                if (_counter != null)
                {
                    Destroy(_counter.gameObject);
                }
                if (_lastUpdated != null)
                {
                    Destroy(_lastUpdated.gameObject);
                }
                if (_problemScrollbarThumb != null)
                {
                    Destroy(_problemScrollbarThumb.gameObject);
                }
                if (_problemScrollbarTrack != null)
                {
                    Destroy(_problemScrollbarTrack.gameObject);
                }
                if (_problemScrollbar != null)
                {
                    Destroy(_problemScrollbar.gameObject);
                }
                if (_problemScrollablePanel != null)
                {
                    Destroy(_problemScrollablePanel.gameObject);
                }
                if (_problemPanel != null)
                {
                    Destroy(_problemPanel.gameObject);
                }
                if (_templateButton != null)
                {
                    Destroy(_templateButton.gameObject);
                }
                if (_tabContainer != null)
                {
                    Destroy(_tabContainer.gameObject);
                }
                if (_tabstrip != null)
                {
                    Destroy(_tabstrip.gameObject);
                }
                if (_dragHandle != null)
                {
                    Destroy(_dragHandle.gameObject);
                }
                if (_close != null)
                {
                    Destroy(_close.gameObject);
                }
                if (_title != null)
                {
                    Destroy(_title.gameObject);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemPanel:OnDestroy -> Exception: " + e.Message);
            }
        }
        public void ForceUpdateUI()
        {
            UpdateUI();
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

                _title = UIUtils.CreateMenuPanelTitle(this, "Problems - Overview");
                _close = UIUtils.CreateMenuPanelCloseButton(this);
                _dragHandle = UIUtils.CreateMenuPanelDragHandle(this);

                _tabstrip = UIUtils.CreateTabStrip(this);
                _tabstrip.width = width - 40f;
                _tabstrip.relativePosition = new Vector3(20f, 50f);
                _tabstrip.eventSelectedIndexChanged += (UIComponent component, int value) =>
                {
                    RefreshProblems();
                };

                _tabContainer = UIUtils.CreateTabContainer(this);
                _tabContainer.width = width - 40f;
                _tabContainer.height = 0f;
                _tabContainer.relativePosition = new Vector3(20f, 85f);

                _templateButton = UIUtils.CreateTabButton(this);

                _tabstrip.tabPages = _tabContainer;

                UIPanel panel = null;

                _tabstrip.AddTab("Buildings", _templateButton, true);
                _tabstrip.selectedIndex = 0;
                panel = _tabstrip.tabContainer.components[0] as UIPanel;
                if (panel != null)
                {

                }

                _tabstrip.AddTab("Networks", _templateButton, true);
                _tabstrip.selectedIndex = 1;
                panel = _tabstrip.tabContainer.components[1] as UIPanel;
                if (panel != null)
                {

                }

                _problemPanel = UIUtils.CreatePanel(this, "ProblemList");
                _problemPanel.width = width - 40f;
                _problemPanel.height = height - 145f;
                _problemPanel.relativePosition = new Vector3(20f, 85f);

                _problemScrollablePanel = UIUtils.CreateScrollablePanel(_problemPanel, "ProblemScrollablePanel");
                _problemScrollablePanel.autoLayout = true;
                _problemScrollablePanel.autoLayoutDirection = LayoutDirection.Vertical;
                _problemScrollablePanel.scrollWheelDirection = UIOrientation.Vertical;
                _problemScrollablePanel.builtinKeyNavigation = true;
                _problemScrollablePanel.clipChildren = true;
                _problemScrollablePanel.width = _problemScrollablePanel.parent.width - 25f;
                _problemScrollablePanel.height = _problemScrollablePanel.parent.height;
                _problemScrollablePanel.relativePosition = new Vector3(0f, 0f);

                _problemScrollbar = UIUtils.CreateScrollbar(_problemPanel, "ProblemScrollbar");
                _problemScrollbar.orientation = UIOrientation.Vertical;
                _problemScrollbar.incrementAmount = 25f;
                _problemScrollbar.width = 20f;
                _problemScrollbar.height = _problemScrollbar.parent.height;
                _problemScrollbar.relativePosition = new Vector3(_problemScrollbar.parent.width - 20f, 0f);

                _problemScrollbarTrack = UIUtils.CreateSlicedSprite(_problemScrollbar, "ProblemScrollbarTrack");
                _problemScrollbarTrack.spriteName = "ScrollbarTrack";
                _problemScrollbarTrack.fillDirection = UIFillDirection.Vertical;
                _problemScrollbarTrack.width = _problemScrollbarTrack.parent.width;
                _problemScrollbarTrack.height = _problemScrollbarTrack.parent.height;
                _problemScrollbarTrack.relativePosition = new Vector3(0f, 0f);

                _problemScrollbarThumb = UIUtils.CreateSlicedSprite(_problemScrollbar, "ProblemScrollbarThumb");
                _problemScrollbarThumb.spriteName = "ScrollbarThumb";
                _problemScrollbarThumb.fillDirection = UIFillDirection.Vertical;
                _problemScrollbarThumb.width = _problemScrollbarThumb.parent.width - 5f;
                _problemScrollbarThumb.relativePosition = new Vector3(0f, 0f);

                _problemScrollablePanel.verticalScrollbar = _problemScrollbar;
                _problemScrollbar.trackObject = _problemScrollbarTrack;
                _problemScrollbar.thumbObject = _problemScrollbarThumb;

                _lastUpdated = UIUtils.CreateLabel(this, "LastUpdated", "Updated: Never");
                _lastUpdated.textScale = 0.7f;
                _lastUpdated.textAlignment = UIHorizontalAlignment.Left;
                _lastUpdated.verticalAlignment = UIVerticalAlignment.Middle;
                _lastUpdated.relativePosition = new Vector3(30f, height - 38f);

                _counter = UIUtils.CreateLabel(this, "Counter", "0 of 0");
                _counter.autoSize = false;
                _counter.width = 200f;
                _counter.textScale = 0.7f;
                _counter.textAlignment = UIHorizontalAlignment.Right;
                _counter.verticalAlignment = UIVerticalAlignment.Middle;
                _counter.relativePosition = new Vector3(width - _counter.width - 30f, height - 38f);

                CreateProblems();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemPanel:CreateUI -> Exception: " + e.Message);
            }
        }

        private void UpdateUI()
        {
            try
            {
                UpdateProblems();
                RefreshProblems();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemPanel:UpdateUI -> Exception: " + e.Message);
            }
        }

        private void CreateProblems()
        {
            try
            {
                if (_problemItems == null)
                {
                    _problemItems = new List<ProblemItem>();
                }

                for (int i = 0; i < ModConfig.Instance.ProblemMaxItems; i++)
                {
                    _problemItems.Add(CreateProblem("Problem" + i));
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemPanel:CreateProblems -> Exception: " + e.Message);
            }
        }

        private void UpdateProblems()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemPanel:UpdateProblems -> Exception: " + e.Message);
            }
        }

        private ProblemItem CreateProblem(string name)
        {
            ProblemItem problemItem = new ProblemItem();

            try
            {
                problemItem.CreateProblemItem(_problemScrollablePanel, name, _problemItems.Count, _notificationsAtlas);
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemPanel:CreateProblem -> Exception: " + e.Message);
            }

            return problemItem;
        }

        private void RefreshProblems()
        {
            try
            {
                ProblemManager problemManager = ProblemManager.Instance;

                if (!problemManager.IsUpdatingData)
                {
                    BuildingManager buildingManager = Singleton<BuildingManager>.instance;
                    NetManager netManager = Singleton<NetManager>.instance;
                    Building building;
                    NetNode netNode;
                    NetSegment netSegment;

                    int buildingShown = 0;
                    int netNodesShown = 0;
                    int netSegmentShown = 0;

                    if (_problemItems != null)
                    {
                        for (int i = 0; i < _problemItems.Count; i++)
                        {
                            if (_tabstrip.selectedIndex == 0)
                            {
                                if (i < problemManager.BuildingsWithProblems.Count)
                                {
                                    building = buildingManager.m_buildings.m_buffer[problemManager.BuildingsWithProblems[buildingShown]];

                                    _problemItems[i].UpdateProblemItem(problemManager.GetBuildingName(problemManager.BuildingsWithProblems[buildingShown]), problemManager.GetSprites(building), problemManager.GetBuildingInstanceID(problemManager.BuildingsWithProblems[buildingShown]), problemManager.GetPosition(building));

                                    _problemItems[i].Show();
                                    buildingShown++;
                                }
                                else
                                {
                                    _problemItems[i].Hide();
                                }

                                _counter.text = $"{buildingShown} of {problemManager.BuildingsWithProblems.Count} buildings";
                            }
                            else if (_tabstrip.selectedIndex == 1)
                            {
                                if (i < problemManager.NetNodesWithProblems.Count)
                                {
                                    netNode = netManager.m_nodes.m_buffer[problemManager.NetNodesWithProblems[netNodesShown]];

                                    _problemItems[i].UpdateProblemItem(problemManager.GetNetNodeName(netNode), problemManager.GetSprites(netNode), problemManager.GetNetNodeInstanceID(problemManager.NetNodesWithProblems[netNodesShown]), problemManager.GetPosition(netNode));

                                    _problemItems[i].Show();
                                    netNodesShown++;
                                }
                                else if (i < problemManager.NetSegmentsWithProblems.Count)
                                {
                                    netSegment = netManager.m_segments.m_buffer[problemManager.NetSegmentsWithProblems[netSegmentShown]];

                                    _problemItems[i].UpdateProblemItem(problemManager.GetNetSegmentName(netSegment), problemManager.GetSprites(netSegment), problemManager.GetNetSegmentInstanceID(problemManager.NetSegmentsWithProblems[netSegmentShown]), problemManager.GetPosition(netSegment));

                                    _problemItems[i].Show();
                                    netSegmentShown++;
                                }
                                else
                                {
                                    _problemItems[i].Hide();
                                }

                                _counter.text = $"{netNodesShown + netSegmentShown} of {problemManager.NetNodesWithProblems.Count + problemManager.NetSegmentsWithProblems.Count} networks";
                            }
                        }

                        _lastUpdated.text = "Updated at " + DateTime.Now.ToLongTimeString();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemPanel:RefreshProblems -> Exception: " + e.Message);
            }
        }
    }
}
