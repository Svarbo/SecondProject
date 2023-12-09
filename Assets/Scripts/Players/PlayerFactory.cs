using CameraFollowing;
using ConstantValues;
using Data;
using Infrastructure.Inputs;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Players
{
    public class PlayerFactory
    {
        private readonly bool _isMobile;
        private readonly Type _difficult;

        private Vector3 _startSpawnPosition;
        private Vector3 _cameraOffset = new Vector3(0, 0, -5);

        public PlayerFactory(Vector3 startSpawnPosition, bool IsMobile, Type difficult)
        {
            _startSpawnPosition = startSpawnPosition;
            _isMobile = IsMobile;
            _difficult = difficult;
        }

        public Player Create()
        {
            Player personage = Object.Instantiate
            (
                original: Resources.Load<Player>(ResourcesPath.PlayerPath),
                position: _startSpawnPosition,
                rotation: Quaternion.identity
            );
            
            Camera.main.GetComponent<TargetFollower>().Construct(personage.transform, _cameraOffset);

            SetAttemptsCount(personage);
            SetInputService(personage);

            return personage;
        }

        private void SetAttemptsCount(Player personage)
        {
            int count = LevelsProgress.Instance.GetDifficultByType(_difficult).GetCountTryBySceneName(SceneManager.GetActiveScene().name);
            personage.SetAttemptsCount(count);
        }

        private void SetInputService(Player personage)
        {
            if (_isMobile)
            {
                personage.InputServiceView.Activate();
                personage.Input.SetInputService(new MobileInputService());
            }
            else
            {
                personage.InputServiceView.Deactivate();
                personage.Input.SetInputService(new StandaloneInputService());
            }
        }
    }
}