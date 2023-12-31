using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueShootStrategy : IShootStrategy
{
    private PlayerLaunchProjectile launchInteractor;
    private Transform shootPoint;

    //Constructor
    public BlueShootStrategy(PlayerLaunchProjectile launchInteractor)
    {
        Debug.Log("Changed To Blue Projectile Mode");
        this.launchInteractor = launchInteractor;
        shootPoint = launchInteractor.spawnPoint.transform;

        //change color
        launchInteractor.spawnPoint.gameObject.GetComponentInParent<MeshRenderer>().material.color = launchInteractor.blueProjectile.GetComponent<MeshRenderer>().sharedMaterial.color;
    }

    public void Shoot()
    {
        PooledObject projectile = launchInteractor.bulletPool2.GetPooledObject();
        projectile.gameObject.SetActive(true);
        projectile.transform.position = launchInteractor.spawnPoint.transform.position;
        projectile.transform.rotation = launchInteractor.spawnPoint.transform.rotation;
        projectile.GetComponent<Rigidbody>().AddForce(launchInteractor.spawnPoint.transform.forward * 1000);
        launchInteractor.bulletPool.DestroyPooledObject(projectile, 5.0f);
    }
}
