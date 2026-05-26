using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            {
                Debug.Log("Coin berhasil di ambil");
                //playerStats.AddCoins(1);
            }
            // Destroy the coin after being collected
            Destroy(gameObject);
        }
    }   
}