using ColossalFramework.UI;
using ICities;
using System;
using UnityEngine;
using WatchIt.Panels;

namespace WatchIt
{

    public class Loading : LoadingExtensionBase
    {
        private LoadMode _loadMode;
        private GameObject _modManagerGameObject;

        private GameObject _warningPanelameObject;
        private GameObject _gaugePanelGameObject;
        private GameObject _limitPanelGameObject;
        private GameObject _problemPanelGameObject;

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
                    _warningPanelameObject = new GameObject("WatchItWarningPanel");
                    _warningPanelameObject.transform.parent = uiView.transform;
                    _warningPanelameObject.AddComponent<WarningPanel>();

                    _gaugePanelGameObject = new GameObject("WatchItGaugePanel");
                    _gaugePanelGameObject.transform.parent = uiView.transform;
                    _gaugePanelGameObject.AddComponent<GaugePanel>();

                    _limitPanelGameObject = new GameObject("WatchItLimitPanel");
                    _limitPanelGameObject.transform.parent = uiView.transform;
                    _limitPanelGameObject.AddComponent<LimitPanel>();

                    _problemPanelGameObject = new GameObject("WatchItProblemPanel");
                    _problemPanelGameObject.transform.parent = uiView.transform;
                    _problemPanelGameObject.AddComponent<ProblemPanel>();
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

                if (_problemPanelGameObject != null)
                {
                    UnityEngine.Object.Destroy(_problemPanelGameObject.gameObject);
                }
                if (_limitPanelGameObject != null)
                {
                    UnityEngine.Object.Destroy(_limitPanelGameObject.gameObject);
                }
                if (_gaugePanelGameObject != null)
                {
                    UnityEngine.Object.Destroy(_gaugePanelGameObject.gameObject);
                }
                if (_warningPanelameObject != null)
                {
                    UnityEngine.Object.Destroy(_warningPanelameObject.gameObject);
                }
                if (_modManagerGameObject != null)
                {
                    UnityEngine.Object.Destroy(_modManagerGameObject.gameObject);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Loading:OnLevelUnloading -> Exception: " + e.Message);
            }
        }
    }
}