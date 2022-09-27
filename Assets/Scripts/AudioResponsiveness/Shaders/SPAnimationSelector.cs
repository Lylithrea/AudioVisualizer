using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Animation/Spritesheet")]
public class SPAnimationSelector : ScriptableObject
{
    public Texture2D spritesheet;
    public int col;
    public int row;
}
