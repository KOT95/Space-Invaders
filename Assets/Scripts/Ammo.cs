using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int count = 3;
    [SerializeField] private float speed = 5;
    
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _player.transform.position, speed * Time.deltaTime);
    }
}
