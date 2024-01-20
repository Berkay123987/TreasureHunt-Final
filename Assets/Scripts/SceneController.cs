using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Sahne yüklendiğinde çağrılacak metod ekle
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ana menüye dönüş kontrolü
        if (scene.name == "AnaSahne")
        {
            // Ana menüye dönüldüğünde çalışacak kod
            DestroyDontDestroyOnLoadObjects();
        }
    }

    private void DestroyDontDestroyOnLoadObjects()
    {
        // DontDestroyOnLoad ile korunan nesneleri bul ve yok et
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.scene.buildIndex == -1) // DontDestroyOnLoad nesneleri -1 indexine sahiptir
            {
                Destroy(obj);
            }
        }
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    IEnumerator LoadLevel()
    {
        int currentHealth = GameController.instance.GetCurrentHealth();
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        transitionAnim.SetTrigger("Start");
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            GameController.instance.SetCurrentHealth(currentHealth);
        };
    }

    private void OnDestroy()
    {
        // Bu script yok edildiğinde event'ten çıkar
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}


