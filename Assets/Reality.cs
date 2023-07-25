using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reality : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Bagage playerBagage;
    private Mind _playerMind;
    [SerializeField] private int bullets;


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Mind player))
        {
            _playerMind = player;
            bullets = playerBagage.ammo;
            InvokeRepeating(nameof(ShootThem), 0, 0.3f);
        }
    }


    private void ShootThem()
    {
        if (bullets > 0)
        {
            var bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
            bullet.Launch(_playerMind, deathScreen);
            playerBagage.RemoveFlower();
            bullets--;
        }
        else
        {
            CancelInvoke();
        }

    }


}
