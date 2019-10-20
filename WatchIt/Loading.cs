using ColossalFramework.UI;
using ICities;
using System;
using UnityEngine;

namespace WatchIt
{

    public class Loading : LoadingExtensionBase
    {
        private LoadMode _loadMode;
        private GameObject _modManagerGameObject;
        private GameObject _warningsPanelGameObject;
        private GameObject _limitsPanelGameObject;

        public override void OnLevelLoaded(LoadMode mode)
        {
            try
            {
                _loadMode = mode;

                if (_loadMode != LoadMode.LoadGame && _loadMode != LoadMode.NewGame && _loadMode != LoadMode.NewGameFromScenario)
                {
                    return;
                }

                _modManagerGameObject = new GameObject("WatchItModManager");
                _modManagerGameObject.AddComponent<ModManager>();

                UIView uiView = UnityEngine.Object.FindObjectOfType<UIView>();
                if (uiView != null)
                {
                    _warningsPanelGameObject = new GameObject("WatchItWarningsPanel");
                    _warningsPanelGameObject.transform.parent = uiView.transform;
                    _warningsPanelGameObject.AddComponent<WarningsPanel>();

                    _limitsPanelGameObject = new GameObject("WatchItLimitsPanel");
                    _limitsPanelGameObject.transform.parent = uiView.transform;
                    _limitsPanelGameObject.AddComponent<LimitsPanel>();
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Loading:OnLevelLoaded -> Exception: " + e.Message);
            }
        }

        public override void OnLevelUnloading()
        {
            try
            {
                if (_loadMode != LoadMode.LoadGame && _loadMode != LoadMode.NewGame && _loadMode != LoadMode.NewGameFromScenario)
                {
                    return;
                }

                if (_modManagerGameObject != null)
                {
                    UnityEngine.Object.Destroy(_modManagerGameObject);
                }

                if (_limitsPanelGameObject != null)
                {
                    UnityEngine.Object.Destroy(_limitsPanelGameObject);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Loading:OnLevelUnloading -> Exception: " + e.Message);
            }
        }
    }
}