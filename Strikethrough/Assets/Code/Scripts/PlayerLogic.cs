using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed;
    [SerializeField] float slowDownRate;

    private float holdSpeed;
    private Vector3 holdDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;

        if(Input.GetKey(KeyCode.W))
        {
            dir += Vector3.forward;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            dir += -Vector3.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            dir += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir += Vector3.right;
        }

        dir = dir.normalized;

        if(dir.Equals(Vector3.zero))
        {
            holdSpeed = Mathf.Max(holdSpeed - slowDownRate * Time.deltaTime, 0);
        }
        else
        {
            holdSpeed = speed;
            holdDir = dir;
        }

        controller.Move(holdDir * holdSpeed * Time.deltaTime);
    }
}
