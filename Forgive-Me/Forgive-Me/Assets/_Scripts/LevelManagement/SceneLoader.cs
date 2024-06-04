using System.Collections;
using _Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.LevelManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private float _delayTime = 4f;
        private WaitForSeconds _delay;
        private GameSession _session;

        public delegate void SceneLoad();

        public static event SceneLoad OnLoadNextScene;


        private void Start()
        {
            _delay = new WaitForSeconds(_delayTime);
            _session = FindAnyObjectByType<GameSession>();
        }

        public void LoadScene()
        {
            StartCoroutine(WaitForReload());
        }

        private IEnumerator WaitForReload()
        {
            OnLoadNextScene?.Invoke();
            yield return _delay;
            if (_session != null)Destroy(_session.gameObject);
            
            
            SceneManager.LoadScene(_sceneName);
        }
        
    }
}