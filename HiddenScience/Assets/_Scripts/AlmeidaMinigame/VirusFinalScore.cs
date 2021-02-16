// Becca's script for final score of virus game
// Adapted from Becca's coronavirus game last semester
using UnityEngine;
using TMPro;

public class VirusFinalScore : MonoBehaviour
{
    public int finalScore;
    TMP_Text yourScore;
    public static VirusFinalScore instance;

    void Start()
    {
        yourScore = GetComponent<TMP_Text>();

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        finalScore = VirusCount.instance.virusCount - DebrisCount.instance.debrisCount; 

        yourScore.text = "Your final score is " + finalScore;
    }
}
