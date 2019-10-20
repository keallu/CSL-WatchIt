using System;
using UnityEngine;

namespace WatchIt
{
    public class ModProperties
    {
        public float WarningPanelDefaultPositionX;
        public float WarningPanelDefaultPositionY;
        public float PanelDefaultPositionX;
        public float PanelDefaultPositionY;

        private static ModProperties instance;

        public static ModProperties Instance
        {
            get
            {
                return instance ?? (instance = new ModProperties());
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
                Debug.Log("[Hide It!] ModProperties:ResetWarningPanelPosition -> Exception: " + e.Message);
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
                Debug.Log("[Hide It!] ModProperties:ResetPanelPosition -> Exception: " + e.Message);
            }
        }
    }
}
