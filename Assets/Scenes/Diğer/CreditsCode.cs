using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsCode : MonoBehaviour
{
    public void LoadSceneIndex8()
    {
        // Direkt olarak 8. indeksteki sahneye geç
        SceneManager.LoadSceneAsync(8);
    }
}

