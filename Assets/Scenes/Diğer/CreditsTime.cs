using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    void Start()
    {
        // 15 saniye sonra LevelChange fonksiyonunu çağır
        Invoke("LevelChange", 10f);
    }

    void LevelChange()
    {
        // Sahneyi index 0 ile değiştir
        SceneManager.LoadScene(0);
    }
}

