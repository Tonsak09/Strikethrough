using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detective : MonoBehaviour
{
    [Header("Zones")]
    [SerializeField] Vector3 dangerOffset;
    [SerializeField] Vector3 dangerHalfWidths;

    [SerializeField] List<SafeZone> safeZones;

    [Header("Light")]
    [SerializeField] GameObject lightObj;
    [SerializeField] LayerMask playerMask;

    [SerializeField] float dangerActiveTime;
    [Tooltip("Time that light is about to turn off")]
    [SerializeField] float dangerAlmostOutTime;

    [SerializeField] float dangerUnactiveTime;
    [Tooltip("Time that light is about to turn on")]
    [SerializeField] float dangerAlmostOnTime;

    [SerializeField] float maxTimeInLight;

    private LightState lightState;
    private float timer;
    private bool warningPlayed;

    [Header("Sounds")]
    [SerializeField] AK.Wwise.Event lightActivateEvent = null;
    [SerializeField] AK.Wwise.Event lightDeactivateEvent = null;
    [SerializeField] AK.Wwise.Event lightAboutTurnOnEvent = null;
    [SerializeField] AK.Wwise.Event lightAboutTurnOffEvent = null;
    [SerializeField] AK.Wwise.Event caughtEvent = null;

    private float timeInLight;

    private enum LightState
    {
        active,
        unactive
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        warningPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch(lightState)
        {
            case LightState.active:
                // Logic when light is active 

                Collider[] colliders = Physics.OverlapBox(this.transform.position + dangerOffset, dangerHalfWidths, Quaternion.identity, playerMask);
                if(colliders.Length > 0)
                {
                    // Player in light
                    timeInLight += Time.deltaTime;
                }

                if(timeInLight > maxTimeInLight)
                {
                    // Restart level 
                    caughtEvent.Post(this.gameObject);
                }

                if(timer >= dangerActiveTime)
                {
                    lightObj.SetActive(false);

                    lightActivateEvent.Post(this.gameObject);
                    lightState = LightState.unactive;

                    timer = 0;
                }
                else if(timer >= dangerAlmostOutTime && !warningPlayed)
                {
                    // Warning 
                    lightAboutTurnOffEvent.Post(this.gameObject);
                    warningPlayed = true;
                }

                break;
            case LightState.unactive:
                // Logic when light is not active 

                if (timer >= dangerUnactiveTime)
                {
                    lightObj.SetActive(true);

                    lightDeactivateEvent.Post(this.gameObject);
                    lightState = LightState.active;

                    timer = 0;
                }
                else if (timer >= dangerAlmostOnTime && !warningPlayed)
                {
                    // Warning 
                    lightAboutTurnOnEvent.Post(this.gameObject);
                    warningPlayed = true;
                }

                break;
        }

        timer += Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;   
        Gizmos.DrawWireCube(this.transform.position + dangerOffset, dangerHalfWidths);

        Gizmos.color = Color.green;
        for (int i = 0; i < safeZones.Count; i++)
        {
            Gizmos.DrawWireCube(this.transform.position + safeZones[i].position, safeZones[i].halfWidths);
        }
    }

    [System.Serializable]
    private class SafeZone
    {
        [SerializeField] public Vector3 position;
        [SerializeField] public Vector3 halfWidths;
    }
}
