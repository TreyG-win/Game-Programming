using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCam : MonoBehaviour
{
    [SerializeField] Transform target;

    public float rotSpeed = 1.5f;

    private float rotY;
    private Vector3 offset;

    void Start()
    {
        rotY = transform.eulerAngles.y;
        offset = target.position - transform.position;
    }

    void LateUpdate()
    {
        float horizonInput = Input.GetAxis("Horizontal");
        if (!Mathf.Approximately(horizonInput, 0))
        {
            rotY += horizonInput * rotSpeed;
        }
        else
        {
            rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
        }

        Quaternion rot = Quaternion.Euler(0, rotY, 0);
        transform.position = target.position - (rot * offset);
        transform.LookAt(target);

    }
}
