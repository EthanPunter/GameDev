using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour {
    
    public AudioSource shotSound;
    public float bulletLifeTime;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed;
    public float recoilIntensity;
    public float recoilDuration;
    private float angle; 
    public float recoilAngle;
    public float shotDelay;
    public int ammoCount;
    public int ammoMax;
    public float reloadDuration;

    void Start() {
        ammoCount = ammoMax;
    }

    public void RotateGun(float angle) {
        Vector3 flippedScale = transform.localScale;
        if (angle > 90f || (angle > -180f && angle < -90f)) {
            flippedScale.y = -1;
            recoilAngle = 1;
        }
        else {
            flippedScale.y = 1;
            recoilAngle = -1;
        }
        transform.localScale = flippedScale;
        transform.rotation = Quaternion.Euler(angle, 90f, 0f);
    }
    public void Fire(float angle) {
        if (ammoCount-- > 0) {
            reload(reloadDuration, ammoCount, ammoMax);
        }
        GameObject bullet = Instantiate(bulletPrefab);
        shotSound.Play();
        bullet.transform.position = bulletSpawn.position;
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);
        bullet.transform.rotation = Quaternion.Euler(-1*angle, 90f, 0f);
        StartCoroutine(Recoil(recoilIntensity, recoilDuration));
        StartCoroutine(DestroyBulletAfterDelay(bullet, bulletLifeTime));
        StartCoroutine(PauseAfterShot(shotDelay));
    }

    private IEnumerator DestroyBulletAfterDelay(GameObject bullet, float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    private IEnumerator Recoil(float recoilIntensity, float recoilDuration) {
        Quaternion rotate = transform.rotation * Quaternion.Euler(recoilAngle * recoilIntensity, 0, 0);
        Quaternion initial = transform.rotation;
        for(float t=0;t<recoilDuration;t+=Time.deltaTime) {
            transform.rotation = Quaternion.Lerp(rotate, initial, t / recoilDuration);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(angle, 0f, 0f);
    }

    private IEnumerator PauseAfterShot(float shotDelay){
        yield return new WaitForSeconds(shotDelay);
    }

    private IEnumerator reload(float reloadDuration, int ammoCount, int ammoMax) {
        yield return new WaitForSeconds(reloadDuration);
        ammoCount = ammoMax;
    }

}
