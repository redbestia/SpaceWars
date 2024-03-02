using Game.Management;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;
using Game.Utility.Globals;
using System;

namespace Game.Room
{
    public class TestSceneManager : MonoBehaviour
    {
        [Inject] private PlayerManager _playerManager;
        [Inject] private ZenjectSceneLoader _sceneLoader;

        public Action OnRoomMainObjectiveCompleted;

        public void Load()
        {
            if (!SceneManager.GetSceneByName(Scenes.RoomTesting).isLoaded)
            {
                _sceneLoader.LoadScene(Scenes.RoomTesting, LoadSceneMode.Additive);
            }
        }

        public void RestartRoom()
        { 
            if(SceneManager.GetSceneByName(Scenes.RoomTesting).isLoaded)
            {
                SceneManager.UnloadSceneAsync(Scenes.RoomTesting);
            }

            _sceneLoader.LoadScene(Scenes.RoomTesting, LoadSceneMode.Additive);
            _playerManager.PlayerBody.position = Vector2.zero;
            _playerManager.PlayerBody.rotation = 0;
        }

        //_container.Resolve<ZenjectSceneLoader>().LoadSceneAsync("RoomTesting",
        //        UnityEngine.SceneManagement.LoadSceneMode.Additive, (container) =>
        //        {
        //    container.BindInstance(_playerManager);
        //}
        //    );
    }
}
