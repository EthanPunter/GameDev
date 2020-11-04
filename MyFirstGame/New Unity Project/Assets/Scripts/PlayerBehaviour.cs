using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject gun;
    private bool canShoot = true;
    private float angle;

    void Update() {
        /*Getting position of mouse for gun rotation/aiming*/
        Vector3 mouse_pos = Input.mousePosition;
        mouse_pos.z = 15f;
        Vector3 player_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - player_pos.x-10;
        mouse_pos.y = mouse_pos.y - player_pos.y-10;
        /*Trig to find angle*/
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        gun.GetComponent<GunBehaviour>().RotateGun(-1 * angle);
        
        /*Shoot method in the gun script as different guns have different attributes*/
        if ((canShoot) && (Input.GetMouseButtonDown(0))) {
            canShoot = false;
            gun.GetComponent<GunBehaviour>().Fire(angle);
            Project(mouse_pos);
            canShoot=true;
        }
    }

    private void Project(Vector3 mouse_pos) {
        angle = Mathf.Atan2(mouse_pos.x, mouse_pos.y);
        float xcomponent = Mathf.Cos(angle);
        float ycomponent = Mathf.Sin(angle);
        Vector3 direction = new Vector3(-1 * ycomponent, -1 * xcomponent);
        gameObject.GetComponent<Rigidbody>().AddForce(direction*5, ForceMode.Impulse);
    }

}
