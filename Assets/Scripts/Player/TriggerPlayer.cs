using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerPlayer : MonoBehaviour
{
    public event Action<int> Reload = default;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Missile") || col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            PlayerPrefs.DeleteKey("Score");
        }

        if (col.gameObject.layer == LayerMask.NameToLayer("Ammo"))
        {
            if (Reload != null)
            {
                Reload(col.gameObject.GetComponent<Ammo>().count);
                Destroy(col.gameObject);
            }
        }
    }
}
