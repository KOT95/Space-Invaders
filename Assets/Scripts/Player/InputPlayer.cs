using System;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    public float Horizontal { get; private set; }

    private bool _isFire;
    
    public event Action Fired = default;
        
    private void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        _isFire = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow);

        if (_isFire && Fired != null)
            Fired();
    }
}
