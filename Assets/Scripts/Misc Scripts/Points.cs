using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] private Text pointText;

    private void Start()
    {
        pointText.text = "Score: " + points;
    }

    public void AddPoints(int amount)
    {
        points += amount;
        pointText.text = "Score: " + points;
    }
}
