using UnityEngine;
using UnityEngine.SceneManagement;

//only purpose of this script, is to function as a "on Trigger enter" condition, to entering new scenes. Modularity wise?

//expanded/tweaked by "Portrait Interaction" script. Consider scrapping/deleting?
public class NewSceneTrigger : MonoBehaviour
{
    public string scene_name;
    private void OnTriggerEnter(Collider other)
    { if (other.tag == "Player") GenericScene(scene_name); }
    //Andrew L Edit = a bit inefficient? So instead, going to try and make a "generic name X" Scene manager, in case it 'just works'.
    private void GenericScene(string scene_name)
    { SceneManager.LoadScene(scene_name); }
}//end NewSceneTrigger class