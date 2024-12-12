using UnityEngine;

public class Player : MonoSingleton<Player>
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            Debug.Log("Player died");
        }
    }
}
