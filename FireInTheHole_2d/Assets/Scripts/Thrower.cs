using UnityEngine;
using System.Collections;

public class Thrower : MonoBehaviour {

    public Transform muzzle;
    public Flame projectile;
    public float msBetweenShots = 70;
    public float muzzleVelocity = 5;

    float nextShotTime;

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Flame newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Flame;
            Flame newProjectile2 = Instantiate(projectile, muzzle.position, muzzle.rotation) as Flame;
            newProjectile.setSpeed(muzzleVelocity);
            newProjectile2.setSpeed(muzzleVelocity);
        }
    }
}
