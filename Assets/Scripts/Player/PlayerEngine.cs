using UnityEngine;

[RequireComponent(typeof(InputPlayer))]
public class PlayerEngine : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private InputPlayer _inputPlayer;
    private Vector3 leftEdge;
    private Vector3 rightEdge;

    private void Awake()
    {
        _inputPlayer = GetComponent<InputPlayer>();
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    private void Update()
    {
        Vector3 pos = new Vector3(_inputPlayer.Horizontal, 0, 0);
        transform.position += pos * speed * Time.deltaTime;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftEdge.x + 1, rightEdge.x - 1), 
            transform.position.y, transform.position.z);
    }
}
