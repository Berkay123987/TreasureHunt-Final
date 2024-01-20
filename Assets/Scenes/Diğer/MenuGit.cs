using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGit : MonoBehaviour
{
    public void LoadSceneIndex0()
    {
        if (TimeDevam.Instance != null)
        {
            TimeDevam.Instance.ResetTimer();
        }
        // Direkt olarak 0. indeksteki sahneye geç
        SceneManager.LoadSceneAsync(0);
    }
}
