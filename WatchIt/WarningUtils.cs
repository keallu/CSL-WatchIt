using ColossalFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WatchIt
{
    public static class WarningUtils
    {
        public static List<Warning> GetWarnings(bool buildings, bool networks, int threshold)
        {
            try
            {
                List<Warning> warnings = new List<Warning>();

                Notification.Problem problems;

                if (buildings)
                {
                    foreach (Building building in Singleton<BuildingManager>.instance.m_buildings.m_buffer)
                    {
                        problems = building.m_problems;

                        if (problems != Notification.Problem.None)
                        {
                            CheckForAllProblems(warnings, problems);
                        }
                    }
                }

                if (networks)
                {
                    foreach (NetNode node in Singleton<NetManager>.instance.m_nodes.m_buffer)
                    {
                        problems = node.m_problems;

                        if (problems != Notification.Problem.None)
                        {
                            CheckForAllProblems(warnings, problems);
                        }
                    }
                }

                warnings.RemoveAll(x => x.Count < threshold);
                warnings.Sort();
                warnings.Reverse();

                return warnings;
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] WarningUtils:GetWarnings -> Exception: " + e.Message);
                return null;
            }
        }

        private static List<Warning> CheckForAllProblems(List<Warning> warnings, Notification.Problem problems)
        {
            CheckForProblem(warnings, problems, Notification.Problem.Garbage);
            CheckForProblem(warnings, problems, Notification.Problem.Electricity);
            CheckForProblem(warnings, problems, Notification.Problem.Water);
            CheckForProblem(warnings, problems, Notification.Problem.Fire);
            CheckForProblem(warnings, problems, Notification.Problem.DirtyWater);
            CheckForProblem(warnings, problems, Notification.Problem.Crime);
            CheckForProblem(warnings, problems, Notification.Problem.Pollution);
            CheckForProblem(warnings, problems, Notification.Problem.TurnedOff);
            CheckForProblem(warnings, problems, Notification.Problem.TooFewServices);
            CheckForProblem(warnings, problems, Notification.Problem.LandValueLow);
            CheckForProblem(warnings, problems, Notification.Problem.ElectricityNotConnected);
            CheckForProblem(warnings, problems, Notification.Problem.NoFuel);
            CheckForProblem(warnings, problems, Notification.Problem.RoadNotConnected);
            CheckForProblem(warnings, problems, Notification.Problem.WaterNotConnected);
            CheckForProblem(warnings, problems, Notification.Problem.Sewage);
            CheckForProblem(warnings, problems, Notification.Problem.Death);
            CheckForProblem(warnings, problems, Notification.Problem.LandfillFull);
            CheckForProblem(warnings, problems, Notification.Problem.LineNotConnected);
            CheckForProblem(warnings, problems, Notification.Problem.NoCustomers);
            CheckForProblem(warnings, problems, Notification.Problem.NoResources);
            CheckForProblem(warnings, problems, Notification.Problem.NoGoods);
            CheckForProblem(warnings, problems, Notification.Problem.NoPlaceforGoods);
            CheckForProblem(warnings, problems, Notification.Problem.NoWorkers);
            CheckForProblem(warnings, problems, Notification.Problem.NoEducatedWorkers);
            CheckForProblem(warnings, problems, Notification.Problem.Noise);
            CheckForProblem(warnings, problems, Notification.Problem.Emptying);
            CheckForProblem(warnings, problems, Notification.Problem.TaxesTooHigh);
            CheckForProblem(warnings, problems, Notification.Problem.EmptyingFinished);
            CheckForProblem(warnings, problems, Notification.Problem.Flood);
            CheckForProblem(warnings, problems, Notification.Problem.Snow);
            CheckForProblem(warnings, problems, Notification.Problem.DepotNotConnected);
            CheckForProblem(warnings, problems, Notification.Problem.HeatingNotConnected);
            CheckForProblem(warnings, problems, Notification.Problem.Heating);
            CheckForProblem(warnings, problems, Notification.Problem.TrackNotConnected);
            CheckForProblem(warnings, problems, Notification.Problem.StructureDamaged);
            CheckForProblem(warnings, problems, Notification.Problem.StructureVisited);
            CheckForProblem(warnings, problems, Notification.Problem.NoFood);
            CheckForProblem(warnings, problems, Notification.Problem.Evacuating);
            CheckForProblem(warnings, problems, Notification.Problem.StructureVisitedService);
            CheckForProblem(warnings, problems, Notification.Problem.NoPark);
            CheckForProblem(warnings, problems, Notification.Problem.TooLong);
            CheckForProblem(warnings, problems, Notification.Problem.NoMainGate);
            CheckForProblem(warnings, problems, Notification.Problem.PathNotConnected);
            CheckForProblem(warnings, problems, Notification.Problem.NotInIndustryArea);
            CheckForProblem(warnings, problems, Notification.Problem.WrongAreaType);
            CheckForProblem(warnings, problems, Notification.Problem.ResourceNotSelected);
            CheckForProblem(warnings, problems, Notification.Problem.NoNaturalResources);
            CheckForProblem(warnings, problems, Notification.Problem.NoInputProducts);
            CheckForProblem(warnings, problems, Notification.Problem.WrongCampusAreaType);
            CheckForProblem(warnings, problems, Notification.Problem.NotInCampusArea);
            CheckForProblem(warnings, problems, Notification.Problem.PathNotConnectedCampus);

            return warnings;
        }

        private static List<Warning> CheckForProblem(List<Warning> warnings, Notification.Problem problems, Notification.Problem problem)
        {
            if ((problems & problem) != Notification.Problem.None)
            {
                Warning warning = warnings.Find(x => x.Problem == problem);

                if (warning == null)
                {
                    warning = new Warning
                    {
                        Problem = problem,
                        Name = TextUtils.AddSpacesBeforeCapitalLetters(Utils.GetNameByValue(problem, "Text")),
                        SpriteName = Utils.GetNameByValue(problem, "Normal")
                };

                    warnings.Add(warning);
                }

                warning.Count++;
            }

            return warnings;
        }
    }
}
