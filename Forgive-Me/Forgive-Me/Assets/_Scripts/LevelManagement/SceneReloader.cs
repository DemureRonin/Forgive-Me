using System.Collections;
using _Scripts.Model;
using _Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.LevelManagement
{
    public class SceneReloader : MonoBehaviour
    {
        public delegate void SceneLoad();

        public static event SceneLoad OnLoadNextScene;

        private WaitForSeconds _delay;
        private GameSession _session;

        private void Awake()
        {
            _delay = new WaitForSeconds(4f);
        }

        public void ReloadSceneTime(float time)
        {
            _delay = new WaitForSeconds(time);
            StartCoroutine(WaitForReload());
        }

        private IEnumerator WaitForReload()
        {
            yield return _delay;
            Time.timeScale = 1;
            var currentScene = SceneManager.GetActiveScene().name;
            // if (_session != null)Destroy(_session.gameObject);
            OnLoadNextScene?.Invoke();
            SceneManager.LoadScene(currentScene);
        }
    }
}