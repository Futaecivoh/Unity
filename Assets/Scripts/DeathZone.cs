using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Игрок вошёл в зону смерти: {collision.gameObject.name}");
        if (collision.CompareTag("Player"))
        {
            CheckpointManager check = collision.GetComponent<CheckpointManager>();
            if (check != null)
            {
                check.Respawn();
            }
        }
    }
}
