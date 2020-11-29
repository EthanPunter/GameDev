﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject gun;
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
        if (Input.GetMouseButtonDown(0) && gun.GetComponent<GunBehaviour>().canShoot) {
            if (gun.GetComponent<GunBehaviour>().ammoCount > 0) {
                Project(mouse_pos);
            }
            gun.GetComponent<GunBehaviour>().Fire(angle);
        }

        /*Reload method if ammo is less than max*/
        if ((gun.GetComponent<GunBehaviour>().ammoCount < gun.GetComponent<GunBehaviour>().ammoMax) && Input.GetKeyDown(KeyCode.R)) {
            gun.GetComponent<GunBehaviour>().Reload();
        }
    }

    private void Project(Vector3 mouse_pos) {
        angle = Mathf.Atan2(mouse_pos.x, mouse_pos.y);
        float xcomponent = Mathf.Cos(angle);
        float ycomponent = Mathf.Sin(angle);
        Vector3 direction = new Vector3(-1 * ycomponent, -1 * xcomponent);
        gameObject.GetComponent<Rigidbody>().AddForce(direction*gun.GetComponent<GunBehaviour>().propulsion, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Collision!");
        if (other.tag == "Instakill") {
            Die();
        }
    }

    private void Die() {
        SceneManager.LoadScene("Playtest");
    }

}