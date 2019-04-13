using UnityEngine;
using System.Collections;

public class FireThrower : MonoBehaviour
{

    public Transform weaponHold;
    public Thrower StartingFire;
    Thrower equippedFire;
    Animator shootCast;

    void Start()
    {
        if (StartingFire != null)
        {
            EquipGun(StartingFire);
        }

    }
    public void EquipGun(Thrower gunToEquip)
    {
        if (equippedFire != null)
        {
           // Destroy(equippedFire.gameObject);
        }
        equippedFire = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation) as Thrower;
        equippedFire.transform.parent = weaponHold;
    }
    public void Shoot()
    {
        if (equippedFire != null)
        {
            shootCast.
            equippedFire.Shoot();
        }
    }
}