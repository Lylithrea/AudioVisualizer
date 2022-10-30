using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVV_ChangeMatValue : PlayFactory
{

    private Material mat;
    public float speed;
    public float intensity;
    public string valueName;
    private bool isFalling;

    private bool isRunning;
    private float timer;
    private float startValue;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        mat = GetComponent<SpriteRenderer>().material;
        startValue = mat.GetFloat(valueName);
    }


    public override void Update()
    {
        base.Update();
        if (isRunning)
        {
            if (!isFalling)
            {
                timer += Time.deltaTime * speed;
                if (timer >= 1)
                {
                    isFalling = true;
                    timer = 1;
                }
            }
            else
            {
                timer -= Time.deltaTime * speed;
                if (timer <= 0)
                {
                    isFalling = false;
                    isRunning = false;
                    timer = 0;
                }
            }
            mat.SetFloat(valueName, Mathf.Lerp(startValue, intensity, timer));
        }
    }


    public override bool Test()
    {
        timer = 0;
        isFalling = false;
        isRunning = true;
        return true;
    }





}
