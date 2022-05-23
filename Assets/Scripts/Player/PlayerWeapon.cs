using TMPro;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Projectile prefab;
    [SerializeField] private int ammo;
    [SerializeField] private TextMeshProUGUI ammoText;

    private bool _laserAction;
    
    private void OnEnable()
    {
        ammoText.text = ammo.ToString();
        GetComponent<InputPlayer>().Fired += OnFired;
        GetComponent<TriggerPlayer>().Reload += OnReload;
    }

    private void OnDisable()
    {
        GetComponent<InputPlayer>().Fired -= OnFired;
        GetComponent<TriggerPlayer>().Reload -= OnReload;
    }

    private void OnFired()
    {
        if (!_laserAction && ammo > 0)
        {
            Projectile projectile = Instantiate(prefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            projectile.destroyed += LaserDestroyed;
            ammo--;
            ammoText.text = ammo.ToString();
            _laserAction = true;
        }
    }

    private void LaserDestroyed() => _laserAction = false;

    private void OnReload(int count)
    {
        ammo += count;
        ammoText.text = ammo.ToString();
    }
}
