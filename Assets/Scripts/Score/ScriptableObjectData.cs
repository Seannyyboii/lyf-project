using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableObjectData : ScriptableObject
{
    [SerializeField] private int score;

    public int Score
    {
        // Get the score and sets it as the value of the Score integer
        get { return score; }
        set { score = value; }
    }
}
