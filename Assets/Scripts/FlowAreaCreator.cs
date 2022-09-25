using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowAreaCreator : MonoBehaviour
{
    public int row, col;
    public float spacing;
    public GameObject obj;
    [Range(0f, 1f)]
    public float falloff = 0.5f;
    public float intensity;
    public float riseSpeed;
    public float fallSpeed;

    private List<GameObject> objs = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                GameObject newObj = Instantiate(obj, this.transform);
                newObj.transform.localPosition = new Vector3(i * spacing, 0, j * spacing);
                newObj.GetComponent<FlowHelper>().riseSpeed = riseSpeed;
                newObj.GetComponent<FlowHelper>().fallSpeed = fallSpeed;
                objs.Add(newObj);
            }
        }
        AudioHelper.AudioSampler.onBeat += UpdateObjects;
    }

    public void UpdateObjects()
    {
        StartCoroutine(SetVariables());
    }

    IEnumerator SetVariables()
    {

        for(int i = 0; i < objs.Count; i++)
        {
            objs[i].GetComponent<FlowHelper>().SetVariable(new Vector3(1, intensity * ( 1- (i * (falloff / objs.Count))), 1));

            yield return new WaitForSecondsRealtime(0.05f);
        }


    }


}
