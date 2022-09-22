using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCreator : MonoBehaviour
{
    public int row, col;
    public int spacing;
    public GameObject obj;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                GameObject newObj = Instantiate(obj, this.transform);
                newObj.transform.position = new Vector3(i * spacing, 0, j * spacing);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
