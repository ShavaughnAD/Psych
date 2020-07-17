using System.Collections.Generic;
using UnityEngine;

public class ShotgunHandler : WeaponShooting
{
    List<Quaternion> pellets = null;
    void Awake()
    {
        pellets = new List<Quaternion>(new Quaternion[pelletCount]);
        base.Awake();
    }

    public void Start()
    {
        base.Start();
    }

    private void Update()
    {
        base.Update();
    }
    public void ShotgunFire()
    {
        int i = 0;
        foreach (Quaternion quat in pellets)
        {
            //GameObject pellet = objectpooler.SpawnFromPool("Pellet", BarrelExit.transform.position, BarrelExit.transform.rotation);
            GameObject p = Instantiate(pellet, base.spawnPoint.position, base.spawnPoint.rotation);
            p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, Random.rotation, spreadAngle);
            p.GetComponent<Rigidbody>().AddForce(p.transform.forward * pelletTravelSpeed);
            i++;
        }
    }
}
