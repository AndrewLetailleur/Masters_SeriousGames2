// Becca's score script for virus count
// Adapted from Becca's coronavirus game last semester
using UnityEngine;
using TMPro;

public class DebrisCount : MonoBehaviour
{
    // Makes reference to counting debris hits in game
    TMP_Text debrisHits;
    public int debrisCount;

    public static DebrisCount instance;

    void Start()
    {
        debrisHits = GetComponent<TMP_Text>();

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the score depending on the number of viruses clicked
        debrisHits.text = "Cell debris count: -" + debrisCount;
    }
}
