using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "CreateScriptable/GameData", order = 2)]
public class GameData : ScriptableObject
{
    public float musicVolume;
    public float effectVolume;

}

