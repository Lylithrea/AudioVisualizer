using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Shaders")]
public class ShaderSettings : ScriptableObject
{
    [SerializeField]
    public ShaderSetting[] settings;
}

[Serializable]
public class ShaderSetting
{
    public string name;
    public float value;
}