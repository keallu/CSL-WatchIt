using ColossalFramework;
using ColossalFramework.UI;
using System;
using System.Globalization;
using UnityEngine;

namespace WatchIt
{
    public class Limit
    {
        public string Name { get; set; }

        private UIPanel _panel;
        private UILabel _name;
        private UILabel _amount;
        private UILabel _capacity;
        private UILabel _consumption;
        private UISprite _modded;

        public void CreateLimit(UIComponent parent, string name, int index)
        {
            try
            {
                Name = name;

                _panel = UIUtils.CreatePanel(parent, name);
                _panel.anchor = UIAnchorStyle.Top | UIAnchorStyle.Left;
                _panel.backgroundSprite = "InfoviewPanel";
                _panel.color = new Color32(49, 52, 58, 255);
                _panel.opacity = index % 2 == 0 ? 1f : 0.5f;
                _panel.height = 25f;
                _panel.width = parent.width - 50f;
                _panel.relativePosition = new Vector3(25f, 75f + (25f * index));
                _panel.eventMouseEnter += (component, eventParam) =>
                {
                    _panel.color = new Color32(73, 78, 87, 255);
                };
                _panel.eventMouseLeave += (component, eventParam) =>
                {
                    _panel.color = new Color32(49, 52, 58, 255);
                    _panel.opacity = index % 2 == 0 ? 1f : 0.5f;
                };

                _name = UIUtils.CreateLabel(_panel, "LimitName", name);
                _name.textScale = 0.875f;
                _name.textColor = new Color32(185, 221, 254, 255);
                _name.textAlignment = UIHorizontalAlignment.Left;
                _name.verticalAlignment = UIVerticalAlignment.Middle;
                _name.autoSize = false;
                _name.height = 23f;
                _name.width = 150f;
                _name.relativePosition = new Vector3(5f, 2f);

                _amount = UIUtils.CreateLabel(_panel, "LimitAmount", "0");
                _amount.textScale = 0.875f;
                _amount.textColor = new Color32(185, 221, 254, 255);
                _amount.textAlignment = UIHorizontalAlignment.Right;
                _amount.verticalAlignment = UIVerticalAlignment.Middle;
                _amount.autoSize = false;
                _amount.height = 23f;
                _amount.width = 150f;
                _amount.relativePosition = new Vector3(165f, 2f);                

                _capacity = UIUtils.CreateLabel(_panel, "LimitCapacity", "0");
                _capacity.textScale = 0.875f;
                _capacity.textColor = new Color32(185, 221, 254, 255);
                _capacity.textAlignment = UIHorizontalAlignment.Right;
                _capacity.verticalAlignment = UIVerticalAlignment.Middle;
                _capacity.autoSize = false;
                _capacity.height = 23f;
                _capacity.width = 150f;
                _capacity.relativePosition = new Vector3(325f, 2f);

                _consumption = UIUtils.CreateLabel(_panel, "LimitConsumption", "0%");
                _consumption.textScale = 0.875f;
                _consumption.textColor = new Color32(185, 221, 254, 255);
                _consumption.textAlignment = UIHorizontalAlignment.Right;
                _consumption.verticalAlignment = UIVerticalAlignment.Middle;
                _consumption.autoSize = false;
                _consumption.height = 23f;
                _consumption.width = 150f;
                _consumption.relativePosition = new Vector3(485f, 2f);

                _modded = UIUtils.CreateSprite(_consumption, "LimitModded", "IconWarning");
                _modded.tooltip = "This capacity has been modded";
                _modded.opacity = 1f;
                _modded.size = new Vector2(23f, 23f);
                _modded.relativePosition = new Vector3(-10f, 0f);
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Limit:CreateLimit -> Exception: " + e.Message);
            }
        }

        public void UpdateLimit()
        {
            try
            {
                GetAmountAndCapacity(Name, out int amount, out int capacity, out bool modded);

                _amount.text = string.Format(CultureInfo.CurrentCulture, "{0:0,0}", amount);
                _capacity.text = string.Format(CultureInfo.CurrentCulture, "{0:0,0}", capacity);
                _consumption.text = GetConsumption(amount, capacity).ToString() + "%";
                _modded.isVisible = modded;
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Limit:UpdateLimit -> Exception: " + e.Message);
            }
        }

        public void DestroyLimit()
        {
            try
            {
                UnityEngine.Object.Destroy(_panel);
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Limit:DestroyLimit -> Exception: " + e.Message);
            }
        }

        private void GetAmountAndCapacity(string name, out int amount, out int capacity, out bool modded)
        {
            amount = 0;
            capacity = 0;
            modded = false;

            GameAreaManager gameAreaManager = null;
            BuildingManager buildingManager = null;
            CitizenManager citizenManager = null;
            DisasterManager disasterManager = null;
            DistrictManager districtManager = null;
            EventManager eventManager = null;
            EconomyManager economyManager = null;
            NetManager netManager = null;
            PathManager pathManager = null;
            PropManager propManager = null;
            AudioManager audioManager = null;
            TransportManager transportManager = null;
            TreeManager treeManager = null;
            VehicleManager vehicleManager = null;
            ZoneManager zoneManager = null;

            switch (name)
            {
                case "Areas":
                    gameAreaManager = Singleton<GameAreaManager>.instance;
                    amount = gameAreaManager.m_areaCount;
                    capacity = gameAreaManager.m_maxAreaCount;
                    modded = gameAreaManager.m_maxAreaCount > 9 ? true : false;
                    break;
                case "Buildings":
                    buildingManager = Singleton<BuildingManager>.instance;
                    amount = buildingManager.m_buildingCount;
                    capacity = BuildingManager.MAX_BUILDING_COUNT;
                    modded = false;
                    break;
                case "Citizens":
                    citizenManager = Singleton<CitizenManager>.instance;
                    amount = citizenManager.m_citizenCount;
                    capacity = CitizenManager.MAX_CITIZEN_COUNT;
                    modded = false;
                    break;
                case "Citizen Units":
                    citizenManager = Singleton<CitizenManager>.instance;
                    amount = citizenManager.m_unitCount;
                    capacity = CitizenManager.MAX_UNIT_COUNT;
                    modded = false;
                    break;
                case "Citizen Instances":
                    citizenManager = Singleton<CitizenManager>.instance;
                    amount = citizenManager.m_instanceCount;
                    capacity = CitizenManager.MAX_INSTANCE_COUNT;
                    modded = false;
                    break;
                case "Disasters":
                    disasterManager = Singleton<DisasterManager>.instance;
                    amount = disasterManager.m_disasterCount;
                    capacity = DisasterManager.MAX_DISASTER_COUNT;
                    modded = false;
                    break;
                case "Districts":
                    districtManager = Singleton<DistrictManager>.instance;
                    amount = districtManager.m_districtCount;
                    capacity = DistrictManager.MAX_DISTRICT_COUNT;
                    modded = false;
                    break;
                case "Events":
                    eventManager = Singleton<EventManager>.instance;
                    amount = eventManager.m_eventCount;
                    capacity = EventManager.MAX_EVENT_COUNT;
                    modded = false;
                    break;
                case "Loans":
                    economyManager = Singleton<EconomyManager>.instance;
                    amount = economyManager.CountLoans();
                    capacity = EconomyManager.MAX_LOANS;
                    modded = false;
                    break;
                case "Net Segments":
                    netManager = Singleton<NetManager>.instance;
                    amount = netManager.m_segmentCount;
                    capacity = NetManager.MAX_SEGMENT_COUNT;
                    modded = false;
                    break;
                case "Net Nodes":
                    netManager = Singleton<NetManager>.instance;
                    amount = netManager.m_nodeCount;
                    capacity = NetManager.MAX_NODE_COUNT;
                    modded = false;
                    break;
                case "Net Lanes":
                    netManager = Singleton<NetManager>.instance;
                    amount = netManager.m_laneCount;
                    capacity = NetManager.MAX_LANE_COUNT;
                    modded = false;
                    break;
                case "Path Units":
                    pathManager = Singleton<PathManager>.instance;
                    amount = pathManager.m_pathUnitCount;
                    capacity = PathManager.MAX_PATHUNIT_COUNT;
                    modded = false;
                    break;
                case "Props":
                    propManager = Singleton<PropManager>.instance;
                    amount = propManager.m_propCount;
                    capacity = PropManager.MAX_PROP_COUNT;
                    modded = false;
                    break;
                case "Radio Channels":
                    audioManager = Singleton<AudioManager>.instance;
                    amount = audioManager.m_radioChannelCount;
                    capacity = AudioManager.MAX_RADIO_CHANNEL_COUNT;
                    modded = false;
                    break;
                case "Radio Contents":
                    audioManager = Singleton<AudioManager>.instance;
                    amount = audioManager.m_radioContentCount;
                    capacity = AudioManager.MAX_RADIO_CONTENT_COUNT;
                    modded = false;
                    break;
                case "Transport Lines":
                    transportManager = Singleton<TransportManager>.instance;
                    amount = transportManager.m_lineCount;
                    capacity = TransportManager.MAX_LINE_COUNT;
                    modded = false;
                    break;
                case "Trees":
                    treeManager = Singleton<TreeManager>.instance;
                    amount = treeManager.m_treeCount;
                    capacity = treeManager.m_trees.m_size > 262144 ? (int)treeManager.m_trees.m_size : TreeManager.MAX_TREE_COUNT;
                    modded = treeManager.m_trees.m_size > 262144 ? true : false;
                    break;
                case "Vehicles":
                    vehicleManager = Singleton<VehicleManager>.instance;
                    amount = vehicleManager.m_vehicleCount;
                    capacity = VehicleManager.MAX_VEHICLE_COUNT;
                    modded = false;
                    break;
                case "Vehicles Parked":
                    vehicleManager = Singleton<VehicleManager>.instance;
                    amount = vehicleManager.m_parkedCount;
                    capacity = VehicleManager.MAX_PARKED_COUNT;
                    modded = false;
                    break;
                case "Zoned Blocks":
                    zoneManager = Singleton<ZoneManager>.instance;
                    amount = zoneManager.m_blockCount;
                    capacity = ZoneManager.MAX_BLOCK_COUNT;
                    modded = false;
                    break;
                default:
                    amount = 0;
                    capacity = 0;
                    modded = false;
                    break;
            }
        }

        private int GetConsumption(int amount, int capacity)
        {
            return (int)((float)amount / (float)capacity * 100);
        }
    }
}
