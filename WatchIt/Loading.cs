using ColossalFramework.UI;
using ICities;
using System;
using UnityEngine;

namespace WatchIt
{

    public class Loading : LoadingExtensionBase
    {
        private LoadMode _loadMode;
        private GameObject _gameObject1;
        private GameObject _gameObject2;

        public override void OnLevelLoaded(LoadMode mode)
        {
            try
            {
                _loadMode = mode;

                if (_loadMode != LoadMode.LoadGame && _loadMode != LoadMode.NewGame && _loadMode != LoadMode.NewGameFromScenario)
                {
                    return;
                }

                UIView objectOfType = UnityEngine.Object.FindObjectOfType<UIView>();
                if (objectOfType != null)
                {
                    _gameObject1 = new GameObject("WatchItWatcher");
                    _gameObject1.transform.parent = objectOfType.transform;
                    _gameObject1.AddComponent<Watcher>();

                    _gameObject2 = new GameObject("WatchItLimitsPanel");
                    _gameObject2.transform.parent = objectOfType.transform;
                    _gameObject2.AddComponent<LimitsPanel>();
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

                if (_gameObject1 != null)
                {
                    UnityEngine.Object.Destroy(_gameObject1);
                }

                if (_gameObject2 != null)
                {
                    UnityEngine.Object.Destroy(_gameObject2);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Watch It!] Loading:OnLevelUnloading -> Exception: " + e.Message);
            }
        }
    }
}