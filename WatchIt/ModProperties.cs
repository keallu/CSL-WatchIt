using System;
using UnityEngine;

namespace WatchIt
{
    public class ModProperties
    {        
        public float PanelDefaultPositionX;
        public float PanelDefaultPositionY;
        public float WarningPanelDefaultPositionX;
        public float WarningPanelDefaultPositionY;

        private static ModProperties instance;

        public static ModProperties Instance
        {
            get
            {
                return instance ?? (instance = new ModProperties());
            }
        }

        public void ResetPanelPosition()
        {
            try
            {
                ModConfig.Instance.PositionX = PanelDefaultPositionX;
                ModConfig.Instance.PositionY = PanelDefaultPositionY;
                ModConfig.Instance.Save();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ModProperties:ResetPanelPosition -> Exception: " + e.Message);
            }
        }

        public void ResetWarningPanelPosition()
        {
            try
            {
                ModConfig.Instance.WarningPositionX = WarningPanelDefaultPositionX;
                ModConfig.Instance.WarningPositionY = WarningPanelDefaultPositionY;
                ModConfig.Instance.Save();
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] ModProperties:ResetWarningPanelPosition -> Exception: " + e.Message);
            }
        }
    }
}
