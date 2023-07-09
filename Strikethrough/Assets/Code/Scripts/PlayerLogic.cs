using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] CharacterController controller;
    [SerializeField] float speed;
    [SerializeField] float slowDownRate;

    [Header("Visual")]
    [SerializeField] Transform playerMesh;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Texture2D idle;
    [SerializeField] Texture2D idleEmissive;
    [SerializeField] Texture2D walk;
    [SerializeField] Texture2D walkEmissive;

    public bool CanMove { get; set; }
    private float holdSpeed;
    private Vector3 holdDir;

    // Start is called before the first frame update
    void Start()
    {
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanMove)
            return;

        Movement();
        if(holdSpeed > 0.001f)
        {
            meshRenderer.material.SetTexture("_Texture", walk);
            meshRenderer.material.SetTexture("_Emission", walkEmissive);
        }
        else
        {
            meshRenderer.material.SetTexture("_Texture", idle);
            meshRenderer.material.SetTexture("_Emission", idleEmissive);
        }

        if(Vector3.Dot(holdDir, Vector3.right) > 0)
        {
            playerMesh.localScale = new Vector3(1, 2, 1);
        }
        else
        {
            playerMesh.localScale = new Vector3(-1, 2, 1);
        }
    }

    private void Movement()
    {
        Vector3 dir = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            dir += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
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

        if (dir.Equals(Vector3.zero))
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
