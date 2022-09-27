using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVS_AnimFloat : MonoBehaviour
{

    public SPAnimationSelector[] animation;
    private Material mat;
    private int currentAnimation = -1;
    public int currentFrame;
    private int maxFrames;

    // Start is called before the first frame update
    void Start()
    {
        mat = this.GetComponent<MeshRenderer>().material;
        AudioSampleTooling.onBeat += handleOnBeat;
    }

    void handleOnBeat()
    {
        if (currentAnimation == -1)
        {
            pickRandomAnimation();
        }

        if (currentFrame < maxFrames)
        {
            updateShader();
            currentFrame++;
        }
        else
        {
            pickRandomAnimation();
        }

    }
    
    void pickRandomAnimation()
    {
        currentAnimation = Random.Range(0, animation.Length);
        currentFrame = 0;
        setupShader();
    }


    void updateShader()
    {
        mat.SetFloat("_currentTile", currentFrame);
    }

    void setupShader()
    {
        mat.SetTexture("_spriteSheet", animation[currentAnimation].spritesheet);
        mat.SetFloat("_col", animation[currentAnimation].col);
        mat.SetFloat("_row", animation[currentAnimation].row);
        mat.SetFloat("_currentTile", 0);
        maxFrames = animation[currentAnimation].col * animation[currentAnimation].row;
    }


}
