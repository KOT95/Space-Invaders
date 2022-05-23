using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleDeadPrefab;
    [SerializeField] private Ammo ammo;

    public event Action<GameObject> killed = default;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            killed.Invoke(gameObject);
            ParticleSystem particle = Instantiate(particleDeadPrefab, transform.position, Quaternion.identity);
            Destroy(particle.gameObject, 1f);
            int random = Random.Range(0, 2);
            if (random == 1)
                Instantiate(ammo, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
