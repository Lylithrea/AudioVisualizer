using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AV_Rotation : MonoBehaviour
{

    public float minSpeed;
    [Tooltip("0 = no limit")]
    public float maxSpeed = 0;

    public bool bpmSensitive = false;
    public float sensitivity = 1;

    public Tooling.Axis axis;

    private Vector3 axisVector = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        switch (axis)
        {
            case Tooling.Axis.x:
                axisVector = new Vector3(1,0,0);
                break;
            case Tooling.Axis.y:
                axisVector = new Vector3(0, 1, 0);
                break;
            case Tooling.Axis.z:
                axisVector = new Vector3(0, 0, 1);
                break;
            default:
                Debug.LogWarning("No axis has been resigned");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bpmSensitive)
        {
            float currentSpeed = (minSpeed + (AudioHelper.AudioSampler.bpm / 100)) * sensitivity * Time.deltaTime;
            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
            this.transform.Rotate(axisVector * currentSpeed);
        }
        else
        {
            this.transform.Rotate(axisVector * minSpeed);
        }
    }


}
