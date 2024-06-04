using System.Linq;
using _Scripts.LevelManagement;
using _Scripts.Model.Data;
using _Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private string _defaultCheckpoint;
        [SerializeField] private PlayerData _data;
        [SerializeField] private InventoryItemModel _inventory;
        [SerializeField] private PlayerPerksModel _perks;
        [SerializeField] private SavedData _saveData;

        [SerializeField] private CheckpointsData _checkpointsData;
        [SerializeField] private bool _spawnPlayer = true;

        private const string UI = "UI";
        private const string SavedData = "SavedData";

        public PlayerData Data => _data;

        public delegate void SessionStart();

        public static event SessionStart OnSessionStart;


        private void Awake()
        {
            var existingSessions = GetExistingSessions();
            if (existingSessions != null)
            {
                existingSessions.StartSession(_defaultCheckpoint);
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(this);
                StartSession(_defaultCheckpoint);
            }

            InitializeModels();

            OnSessionStart?.Invoke();
        }

        private GameSession GetExistingSessions()
        {
            var sessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None);
            foreach (var gameSession in sessions)
            {
                if (gameSession != this)
                {
                    return gameSession;
                }
            }

            return null;
        }

        private void InitializeModels()
        {
            if (_saveData == null)
            {
                _saveData = Resources.Load<SavedData>(SavedData);
            }

            _data.Initialize();
        }

        private void StartSession(string defaultCheckpoint)
        {
            SetChecked(defaultCheckpoint);
            SpawnHero();
            if (_spawnPlayer)
                SceneManager.LoadScene(UI, LoadSceneMode.Additive);
        }

        private void SpawnHero()
        {
            if (!_spawnPlayer) return;
            var checkpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);


            foreach (var checkpoint in checkpoints)
            {
                if (checkpoint.ID != _checkpointsData.Checkpoints.Last()) continue;
                checkpoint.SpawnHero();
                break;
            }
        }

        public void SetChecked(string id)
        {
            if (_checkpointsData.Checkpoints.Count > 0 && id == _defaultCheckpoint)
                return;

            _checkpointsData.Checkpoints.Add(id);
        }

        private void EnableSphere()
        {
            _data.PlayerPerks.EnableSphere();
        }

        private void EnableDashing()
        {
            _data.PlayerPerks.EnableDashing();
        }

        private void OnEnable()
        {
            InputToggle.OnDashingEnabled += EnableDashing;
            InputToggle.OnSphereEnabled += EnableSphere;
            InputToggle.OnSphereDisabled += DisableSphere;
            SceneLoader.OnLoadNextScene += ClearCheckpoints;
            DestroySessionObserver.OnDestroySession += DestroySession;
        }

        private void DestroySession()
        {
            Destroy(gameObject);
        }

        private void DisableSphere()
        {
            _data.PlayerPerks.DisableSphere();
        }


        private void OnDisable()
        {
            InputToggle.OnDashingEnabled -= EnableDashing;
            InputToggle.OnSphereEnabled -= EnableSphere;
            SceneLoader.OnLoadNextScene -= ClearCheckpoints;
            InputToggle.OnSphereDisabled -= DisableSphere;
            DestroySessionObserver.OnDestroySession -= DestroySession;
        }

        private void ClearCheckpoints()
        {
            _checkpointsData.Checkpoints.Clear();
        }


        private void OnApplicationQuit()
        {
            ClearCheckpoints();
        }
    }
}