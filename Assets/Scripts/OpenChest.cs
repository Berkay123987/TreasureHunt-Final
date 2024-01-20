using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public Animator chestAnimator; // Sandığın Animator komponentine referans

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // Eğer temas eden obje oyuncu ise
        {
            chestAnimator.SetTrigger("Open"); // Sandık animasyonunu tetikle
        }
    }
}
