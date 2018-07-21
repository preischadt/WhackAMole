using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Avatar;
using UnityEngine.UI;

public class GunController : MonoBehaviour {

    public Text BulletsCountText;

    private int BulletsCount = 0;

    private float LastCannonFire = 0;
    public float CannonFireCadence = 1;

    private float LastBulletFire = 0;
    public float BulletFireCadence = 0.1f;

    public AudioSource FireAudio;

    public float BulletSpeed = 1000;
    public float CannonBulletSpeed = 1000;

    public Transform BulletSpawnPoint;
    public Transform CannonSpawnPoint;

    public GameObject BulletPrefab;
    public GameObject CannonBulletPrefab;

    OVRGrabbable ovrGrabbable;

	// Use this for initialization
	void Start () {
       this.ovrGrabbable =  this.gameObject.GetComponent<OVRGrabbable>();
	}
	
    void FireCannon()
    {
        if ((Time.time - LastCannonFire) < CannonFireCadence)
        {
            return;
        }

        FireAudio.Play();
        var bullet = Instantiate(CannonBulletPrefab, CannonSpawnPoint.position, CannonSpawnPoint.rotation, null);
        bullet.GetComponent<Rigidbody>().AddForce(CannonSpawnPoint.forward * CannonBulletSpeed);
        Destroy(bullet, 20);
        LastCannonFire = Time.time;
    }

    void Fire()
    {
        if ((Time.time - LastBulletFire) < BulletFireCadence)
        {
            return;
        }

        BulletsCount++;
        FireAudio.Play();
        var bullet = Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.rotation, null);
        bullet.GetComponent<Rigidbody>().AddForce(BulletSpawnPoint.forward * BulletSpeed);
        Destroy(bullet, 20);
        LastBulletFire = Time.time;
    }

    void UpdateHUD()
    {
        BulletsCountText.text = BulletsCount.ToString();
    }

	// Update is called once per frame
	void Update () {
        UpdateHUD();
        if (ovrGrabbable.isGrabbed)
        {
            if (ovrGrabbable.grabbedBy.tag == "Left")
            {
                if (OVRInput.Get(OVRInput.Button.Three))
                {
                    FireCannon();
                }

                if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                {
                    Fire();
                }

            }else if(ovrGrabbable.grabbedBy.tag == "Right")
            {
                if (OVRInput.Get(OVRInput.Button.One))
                {
                    FireCannon();
                }

                if(OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                {
                    Fire();
                }
            }
        }
        
	}
}
