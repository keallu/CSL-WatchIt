using ColossalFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WatchIt.Managers
{
    public class ProblemManager
    {
        private bool isUpdatingData;
        public bool IsUpdatingData => isUpdatingData;
        public List<ProblemType> ProblemTypes { get; } = new List<ProblemType>();
        public List<ushort> BuildingsWithProblems { get; } = new List<ushort>();
        public List<ushort> NetNodesWithProblems { get; } = new List<ushort>();
        public List<ushort> NetSegmentsWithProblems { get; } = new List<ushort>();
        private enum SourceType { Building, Network };

        private static readonly PositionData<Notification.Problem1>[] keyNotificationsNormal = Utils.GetOrderedEnumData<Notification.Problem1>("Normal");

        private static readonly PositionData<Notification.Problem1>[] keyNotificationsMajor = Utils.GetOrderedEnumData<Notification.Problem1>("Major");

        private static readonly PositionData<Notification.Problem1>[] keyNotificationsFatal = Utils.GetOrderedEnumData<Notification.Problem1>("Fatal");

        private static readonly PositionData<Notification.Problem1>[] keyNotifications = Utils.GetOrderedEnumData<Notification.Problem1>("Text");

        private static ProblemManager instance;

        public static ProblemManager Instance
        {
            get
            {
                return instance ?? (instance = new ProblemManager());
            }
        }

        public InstanceID GetBuildingInstanceID(ushort buildingID)
        {
            InstanceID id = default(InstanceID);
            id.Building = buildingID;
            return id;
        }
        public InstanceID GetNetNodeInstanceID(ushort netNodeID)
        {
            InstanceID id = default(InstanceID);
            id.NetNode = netNodeID;
            return id;
        }

        public InstanceID GetNetSegmentInstanceID(ushort netSegmentID)
        {
            InstanceID id = default(InstanceID);
            id.NetSegment = netSegmentID;
            return id;
        }

        public string GetBuildingName(ushort buildingID)
        {
            return Singleton<BuildingManager>.instance.GetBuildingName(buildingID, InstanceID.Empty);
        }

        public string GetNetNodeName(ushort netNodeID)
        {
            return Singleton<NetManager>.instance.m_nodes.m_buffer[netNodeID].Info.GetUncheckedLocalizedTitle();
        }

        public string GetNetSegmentName(ushort netSegmentID)
        {
            return Singleton<NetManager>.instance.m_segments.m_buffer[netSegmentID].Info.GetUncheckedLocalizedTitle();
        }

        public string GetNetNodeName(NetNode netNode)
        {
            return netNode.Info.GetUncheckedLocalizedTitle();
        }

        public string GetNetSegmentName(NetSegment netSegment)
        {
            return netSegment.Info.GetUncheckedLocalizedTitle();
        }

        public string[] GetSprites(Building building)
        {
            List<string> sprites = new List<string>();

            Notification.Problem1 enumValue;

            for (int i = 0; i < keyNotifications.Length; i++)
            {
                enumValue = keyNotifications[i].enumValue;

                if ((building.m_problems & enumValue).IsNotNone)
                  {
                    if ((building.m_problems & Notification.Problem1.FatalProblem).IsNotNone)
                    {
                        sprites.Add(keyNotificationsFatal[i].enumName);
                    }
                    else if ((building.m_problems & Notification.Problem1.MajorProblem).IsNotNone)
                    {
                        sprites.Add(keyNotificationsMajor[i].enumName);
                    }
                    else
                    {
                        sprites.Add(keyNotificationsNormal[i].enumName);
                    }
                }
            }

            return sprites.ToArray();
        }

        public string[] GetSprites(NetNode netNode)
        {
            List<string> sprites = new List<string>();

            Notification.Problem1 enumValue;

            for (int i = 0; i < keyNotifications.Length; i++)
            {
                enumValue = keyNotifications[i].enumValue;

                if ((netNode.m_problems & enumValue).IsNotNone)
                {
                    if ((netNode.m_problems & Notification.Problem1.FatalProblem).IsNotNone)
                    {
                        sprites.Add(keyNotificationsFatal[i].enumName);
                    }
                    else if ((netNode.m_problems & Notification.Problem1.MajorProblem).IsNotNone)
                    {
                        sprites.Add(keyNotificationsMajor[i].enumName);
                    }
                    else
                    {
                        sprites.Add(keyNotificationsNormal[i].enumName);
                    }
                }
            }

            return sprites.ToArray();
        }

        public string[] GetSprites(NetSegment netSegment)
        {
            List<string> sprites = new List<string>();

            Notification.Problem1 enumValue;

            for (int i = 0; i < keyNotifications.Length; i++)
            {
                enumValue = keyNotifications[i].enumValue;

                if ((netSegment.m_problems & enumValue).IsNotNone)
                {
                    if ((netSegment.m_problems & Notification.Problem1.FatalProblem).IsNotNone)
                    {
                        sprites.Add(keyNotificationsFatal[i].enumName);
                    }
                    else if ((netSegment.m_problems & Notification.Problem1.MajorProblem).IsNotNone)
                    {
                        sprites.Add(keyNotificationsMajor[i].enumName);
                    }
                    else
                    {
                        sprites.Add(keyNotificationsNormal[i].enumName);
                    }
                }
            }

            return sprites.ToArray();
        }

        public Vector3 GetPosition(Building building)
        {
            return building.m_position;
        }

        public Vector3 GetPosition(NetNode netNode)
        {
            return netNode.m_position;
        }

        public Vector3 GetPosition(NetSegment netSegment)
        {
            return netSegment.m_middlePosition;
        }

        public void UpdateData()
        {
            try
            {
                isUpdatingData = true;

                ProblemTypes.Clear();
                BuildingsWithProblems.Clear();
                NetNodesWithProblems.Clear();
                NetSegmentsWithProblems.Clear();

                Notification.Problem1 problems;

                BuildingManager buildingManager = Singleton<BuildingManager>.instance;
                Building building;

                for (ushort i = 0; i < buildingManager.m_buildings.m_buffer.Length; i++)
                {
                    building = buildingManager.m_buildings.m_buffer[i];

                    problems = building.m_problems;

                    if (problems != Notification.Problem1.None)
                    {
                        if (buildingManager.m_buildings.m_buffer[i].m_flags != Building.Flags.None)
                        {
                            UpdateProblemTypes(SourceType.Building, problems);
                            BuildingsWithProblems.Add(i);
                        }
                    }
                }

                NetManager netManager = Singleton<NetManager>.instance;
                NetNode netNode;
                NetSegment netSegment;

                for (ushort i = 0; i < netManager.m_nodes.m_buffer.Length; i++)
                {
                    netNode = netManager.m_nodes.m_buffer[i];

                    problems = netNode.m_problems;

                    if (problems != Notification.Problem1.None)
                    {
                        if (netManager.m_nodes.m_buffer[i].m_flags != NetNode.Flags.None && (netManager.m_nodes.m_buffer[i].m_flags & NetNode.Flags.Temporary) == 0)
                        {
                            UpdateProblemTypes(SourceType.Network, problems);
                            NetNodesWithProblems.Add(i);
                        }
                    }
                }

                for (ushort i = 0; i < netManager.m_segments.m_buffer.Length; i++)
                {
                    netSegment = netManager.m_segments.m_buffer[i];

                    problems = netSegment.m_problems;

                    if (problems != Notification.Problem1.None)
                    {
                        if (netManager.m_segments.m_buffer[i].m_flags != NetSegment.Flags.None)
                        {
                            UpdateProblemTypes(SourceType.Network, problems);
                            NetSegmentsWithProblems.Add(i);
                        }
                    }
                }

                ProblemTypes.Sort();
                ProblemTypes.Reverse();

                isUpdatingData = false;
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemManager:Update -> Exception: " + e.Message);
            }
        }

        private void UpdateProblemTypes(SourceType sourceType, Notification.Problem1 problems)
        {
            try
            {
                Notification.Problem1 enumValue;

                for (int i = 0; i < keyNotifications.Length; i++)
                {
                    enumValue = keyNotifications[i].enumValue;

                    if ((problems & enumValue) != 0)
                    {
                        if ((problems & Notification.Problem1.FatalProblem) != 0)
                        {
                            string sprite = keyNotificationsFatal[i].enumName;

                            AddOrUpdateProblemType(sourceType, sprite);
                        }
                        else if ((problems & Notification.Problem1.MajorProblem) != 0)
                        {
                            string sprite = keyNotificationsMajor[i].enumName;

                            AddOrUpdateProblemType(sourceType, sprite);
                        }
                        else
                        {
                            string sprite = keyNotificationsNormal[i].enumName;

                            AddOrUpdateProblemType(sourceType, sprite);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemManager:UpdateProblemTypes -> Exception: " + e.Message);
            }
        }

        private void AddOrUpdateProblemType(SourceType sourceType, string sprite)
        {
            try
            {
                ProblemType problemType = ProblemTypes.Find(x => x.Sprite == sprite);

                if (problemType == null)
                {
                    problemType = new ProblemType
                    {
                        Sprite = sprite
                    };

                    ProblemTypes.Add(problemType);
                }

                switch (sourceType)
                {
                    case SourceType.Building:
                        problemType.TotalBuildings++;
                        break;
                    case SourceType.Network:
                        problemType.TotalNetworks++;
                        break;
                    default:
                        break;
                }

                problemType.Total++;
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ProblemManager:AddOrUpdateProblemType -> Exception: " + e.Message);
            }
        }
    }
}
