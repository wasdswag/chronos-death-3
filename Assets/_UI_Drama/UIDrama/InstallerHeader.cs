using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIDrama
{
    public class InstallerHeader : MonoBehaviour
    {
        [SerializeField] private GameObject field;
        [SerializeField] private GameObject headerFloor;
        private Rigidbody2D body;

        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject installHeaderText;
        [SerializeField] private int nextSceneIndex;
        [SerializeField] private Button startButton;
        private bool _isComplete;

        private void Awake() => startButton.onClick.AddListener(SetCompleted);
        

        public void OnInstallationSuccess()
        {
            body = GetComponent<Rigidbody2D>();
            var rbs = FindObjectsOfType<RbColliderDragger>();
            foreach (var rb in rbs) 
                rb.Release();
                
            field.SetActive(false);
            headerFloor.SetActive(true);
            body.isKinematic = false;
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            var collider = other.collider;
            if (collider.TryGetComponent(out Rigidbody2D otherbody))
                otherbody.isKinematic = false;
            
        }

        public void StartChronOS()
        {
            installHeaderText.SetActive(false);
            startMenu.SetActive(true);
            StartCoroutine(Preload());
        }

        private void SetCompleted() => _isComplete = true;
        
        
        private IEnumerator Preload()
        {
            yield return null;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextSceneIndex);
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    if (_isComplete)
                        asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }


      
    }
}