using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Player Stats�")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float maxYSpeed, gravity;


}