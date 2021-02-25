// Becca's score script for virus count
// Adapted from Becca's coronavirus game last semester
using UnityEngine;
using TMPro;

public class VirusCount : MonoBehaviour
{
    // Makes reference for counting virus hits in game
    TMP_Text virusHits;
    //TMP_Text debrisHits;
    public int virusCount;

    public static VirusCount instance;

    void Start()
    {
        virusHits = GetComponent<TMP_Text>();

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the score depending on the number of viruses clicked
        virusHits.text = "Virus count: " + virusCount;
    }
}
