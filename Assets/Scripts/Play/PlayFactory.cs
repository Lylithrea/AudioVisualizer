using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFactory : SequenceManager
{


    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public override void Play()
    {
        base.Play();
        isDone = true;
    }

}
