using ColossalFramework;
using ColossalFramework.UI;
using System;
using System.Globalization;
using UnityEngine;
using EManagersLib.API;

namespace WatchIt.Panels
{
    public class LimitItem
    {
        public string Name { get; set; }

        private UIPanel _panel;
        private UILabel _name;
        private UILabel _amount;
        private UILabel _capacity;
        private UILabel _consumption;
        private UISprite _modded;

        public void CreateLimitItem(UIComponent parent, string name, int index)
        {
            try
            {
                Name = name;

                _panel = UIUtils.CreatePanel(parent, name);
                _panel.anchor = UIAnchorStyle.Top | UIAnchorStyle.Left;
                _panel.backgroundSprite = "InfoviewPanel";
                _panel.color = index % 2 != 0 ? new Color32(56, 61, 63, 255) : new Color32(49, 52, 58, 255);
                _panel.height = 25f;
                _panel.width = parent.width - 50f;
                _panel.relativePosition = new Vector3(25f, 75f + (25f * index));
                _panel.eventMouseEnter += (component, eventParam) =>
                {
                    _panel.color = new Color32(73, 78, 87, 255);
                };
                _panel.eventMouseLeave += (component, eventParam) =>
                {
                    _panel.color = index % 2 != 0 ? new Color32(56, 61, 63, 255) : new Color32(49, 52, 58, 255);
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

                _modded = UIUtils.CreateSprite(_consumption, "LimitModded", "CityInfo");
                _modded.tooltip = "This capacity has been modded";
                _modded.opacity = 1f;
                _modded.height = 20f;
                _modded.width = 20f;
                _modded.relativePosition = new Vector3(0f, 0f);
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitItem:CreateLimitItem -> Exception: " + e.Message);
            }
        }

        public void DestroyLimitItem()
        {
            try
            {
                if (_name != null)
                {
                    UnityEngine.Object.Destroy(_name.gameObject);
                }
                if (_amount != null)
                {
                    UnityEngine.Object.Destroy(_amount.gameObject);
                }
                if (_capacity != null)
                {
                    UnityEngine.Object.Destroy(_capacity.gameObject);
                }
                if (_consumption != null)
                {
                    UnityEngine.Object.Destroy(_consumption.gameObject);
                }
                if (_modded != null)
                {
                    UnityEngine.Object.Destroy(_modded.gameObject);
                }
                if (_panel != null)
                {
                    UnityEngine.Object.Destroy(_panel.gameObject);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitItem:DestroyLimitItem -> Exception: " + e.Message);
            }
        }

        public void UpdateLimitItem()
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
                Debug.Log("[Watch It!] LimitItem:UpdateLimitItem -> Exception: " + e.Message);
            }
        }

        private void GetAmountAndCapacity(string name, out int amount, out int capacity, out bool modded)
        {
            try
            {
                CitizenManager citizenManager;
                NetManager netManager;
                AudioManager audioManager;
                VehicleManager vehicleManager;

                switch (name)
                {
                    case "Areas":
                        GameAreaManager gameAreaManager = Singleton<GameAreaManager>.instance;
                        amount = gameAreaManager.m_areaCount;
                        capacity = gameAreaManager.m_maxAreaCount;
                        modded = gameAreaManager.m_maxAreaCount > 9;
                        break;
                    case "Buildings":
                        BuildingManager buildingManager = Singleton<BuildingManager>.instance;
                        amount = buildingManager.m_buildingCount;
                        capacity = (int)buildingManager.m_buildings.m_size;
                        modded = buildingManager.m_buildings.m_size > BuildingManager.MAX_BUILDING_COUNT;
                        break;
                    case "Citizens":
                        citizenManager = Singleton<CitizenManager>.instance;
                        amount = citizenManager.m_citizenCount;
                        capacity = (int)citizenManager.m_citizens.m_size;
                        modded = citizenManager.m_citizens.m_size > CitizenManager.MAX_CITIZEN_COUNT;
                        break;
                    case "Citizen Units":
                        citizenManager = Singleton<CitizenManager>.instance;
                        amount = citizenManager.m_unitCount;
                        capacity = (int)citizenManager.m_units.m_size;
                        modded = citizenManager.m_units.m_size > CitizenManager.MAX_UNIT_COUNT;
                        break;
                    case "Citizen Instances":
                        citizenManager = Singleton<CitizenManager>.instance;
                        amount = citizenManager.m_instanceCount;
                        capacity = (int)citizenManager.m_instances.m_size;
                        modded = citizenManager.m_instances.m_size > CitizenManager.MAX_INSTANCE_COUNT;
                        break;
                    case "Disasters":
                        DisasterManager disasterManager = Singleton<DisasterManager>.instance;
                        amount = disasterManager.m_disasterCount;
                        capacity = disasterManager.m_disasters.m_size;
                        modded = disasterManager.m_disasters.m_size > DisasterManager.MAX_DISASTER_COUNT;
                        break;
                    case "Districts":
                        DistrictManager districtManager = Singleton<DistrictManager>.instance;
                        amount = districtManager.m_districtCount;
                        capacity = (int)districtManager.m_districts.m_size;
                        modded = districtManager.m_districts.m_size > DistrictManager.MAX_DISTRICT_COUNT;
                        break;
                    case "Events":
                        EventManager eventManager = Singleton<EventManager>.instance;
                        amount = eventManager.m_eventCount;
                        capacity = eventManager.m_events.m_size;
                        modded = eventManager.m_events.m_size > EventManager.MAX_EVENT_COUNT;
                        break;
                    case "Loans":
                        EconomyManager economyManager = Singleton<EconomyManager>.instance;
                        amount = economyManager.CountLoans();
                        capacity = EconomyManager.MAX_LOANS;
                        modded = false;
                        break;
                    case "Net Segments":
                        netManager = Singleton<NetManager>.instance;
                        amount = netManager.m_segmentCount;
                        capacity = (int)netManager.m_segments.m_size;
                        modded = netManager.m_segments.m_size > NetManager.MAX_SEGMENT_COUNT;
                        break;
                    case "Net Nodes":
                        netManager = Singleton<NetManager>.instance;
                        amount = netManager.m_nodeCount;
                        capacity = (int)netManager.m_nodes.m_size;
                        modded = netManager.m_nodes.m_size > NetManager.MAX_NODE_COUNT;
                        break;
                    case "Net Lanes":
                        netManager = Singleton<NetManager>.instance;
                        amount = netManager.m_laneCount;
                        capacity = (int)netManager.m_lanes.m_size;
                        modded = netManager.m_lanes.m_size > NetManager.MAX_LANE_COUNT;
                        break;
                    case "Path Units":
                        PathManager pathManager = Singleton<PathManager>.instance;
                        amount = pathManager.m_pathUnitCount;
                        capacity = (int)pathManager.m_pathUnits.m_size;
                        modded = pathManager.m_pathUnits.m_size > PathManager.MAX_PATHUNIT_COUNT;
                        break;
                    case "Props":
                        PropManager propManager = Singleton<PropManager>.instance;
                        amount = propManager.m_propCount;

                        if (propManager.m_props == null && PropAPI.m_isEMLInstalled)
                        {
                            capacity = PropAPI.PropBufferLen;
                            modded = true;
                        }
                        else
                        {
                            capacity = (int)propManager.m_props.m_size;
                            modded = false;
                        }                        

                        break;
                    case "Radio Channels":
                        audioManager = Singleton<AudioManager>.instance;
                        amount = audioManager.m_radioChannelCount;
                        capacity = audioManager.m_radioChannels.m_size;
                        modded = audioManager.m_radioChannels.m_size > AudioManager.MAX_RADIO_CHANNEL_COUNT;
                        break;
                    case "Radio Contents":
                        audioManager = Singleton<AudioManager>.instance;
                        amount = audioManager.m_radioContentCount;
                        capacity = audioManager.m_radioContents.m_size;
                        modded = audioManager.m_radioContents.m_size > AudioManager.MAX_RADIO_CONTENT_COUNT;
                        break;
                    case "Transport Lines":
                        TransportManager transportManager = Singleton<TransportManager>.instance;
                        amount = transportManager.m_lineCount;
                        capacity = (int)transportManager.m_lines.m_size;
                        modded = transportManager.m_lines.m_size > TransportManager.MAX_LINE_COUNT;
                        break;
                    case "Trees":
                        TreeManager treeManager = Singleton<TreeManager>.instance;
                        amount = treeManager.m_treeCount;
                        capacity = (int)treeManager.m_trees.m_size;
                        modded = treeManager.m_trees.m_size > TreeManager.MAX_TREE_COUNT;
                        break;
                    case "Vehicles":
                        vehicleManager = Singleton<VehicleManager>.instance;
                        amount = vehicleManager.m_vehicleCount;
                        capacity = (int)vehicleManager.m_vehicles.m_size;
                        modded = vehicleManager.m_vehicles.m_size > VehicleManager.MAX_VEHICLE_COUNT;
                        break;
                    case "Vehicles Parked":
                        vehicleManager = Singleton<VehicleManager>.instance;
                        amount = vehicleManager.m_parkedCount;
                        capacity = (int)vehicleManager.m_parkedVehicles.m_size;
                        modded = vehicleManager.m_parkedVehicles.m_size > VehicleManager.MAX_PARKED_COUNT;
                        break;
                    case "Zoned Blocks":
                        ZoneManager zoneManager = Singleton<ZoneManager>.instance;
                        amount = zoneManager.m_blockCount;
                        capacity = (int)zoneManager.m_blocks.m_size;
                        modded = zoneManager.m_blocks.m_size > ZoneManager.MAX_BLOCK_COUNT;
                        break;
                    default:
                        amount = 0;
                        capacity = 0;
                        modded = false;
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] LimitItem:GetAmountAndCapacity -> Exception: " + e.Message);

                amount = 0;
                capacity = 0;
                modded = false;
            }
        }

        private int GetConsumption(int amount, int capacity)
        {
            return (int)((float)amount / (float)capacity * 100);
        }
    }
}
