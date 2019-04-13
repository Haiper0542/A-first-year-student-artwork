using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour
{
    public Transform muzzle;
    public Thrown_hook projectile;
    public float msBetweenShots = 5;
    float muzzleVelocity = 50;

    float nextShotTime;

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots;
            Thrown_hook newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Thrown_hook;
            newProjectile.setSpeed(muzzleVelocity);
        }
    }
}