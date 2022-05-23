using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;

    public event Action destroyed = default;

    private void Update() => transform.position += direction * speed * Time.deltaTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(destroyed != null)
            destroyed.Invoke();
        
        Destroy(gameObject);
    }
}
