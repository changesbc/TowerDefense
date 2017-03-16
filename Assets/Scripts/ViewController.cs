using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour {

    public float speed = 25;
    public float mouseSpeed = 600;

	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");

        transform.Translate(new Vector3(v * speed, mouse * mouseSpeed, -h * speed) * Time.deltaTime, Space.World);

	}
}
