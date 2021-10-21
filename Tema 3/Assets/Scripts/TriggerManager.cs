using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerManager : MonoBehaviour
{
    public GameObject cosRing;
    public TextMeshProUGUI scoreValue;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == cosRing.GetComponent<BoxCollider>().name)
        {
            int score = int.Parse(scoreValue.text);
            score++;
            scoreValue.text = score.ToString();
        }
    }
}
