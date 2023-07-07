using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateShowEntityMat : MonoBehaviour
{
    [SerializeField] Renderer rend;
    [SerializeField] Transform showField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rend.material.SetVector("_ShowZoneCenter", showField.position);
    }
}
