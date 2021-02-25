using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPuzzleColliders : MonoBehaviour
{
    private bool glued = false;//jnc debug value
    private GameObject obj;//for ref values
    private Transform initPos;
    public bool Wrong = false; //default to "wrong"
    //enum check, for easy "yes/no" colliders
    public enum Polarity { Plus, Minus}
    public Polarity pol;
    public int order;
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform;//position
        Debug.Log("Script Set");
    }

    private void Update()
    {
        if (obj != null) {
            gameObject.transform.parent.
                gameObject.transform.position = 
                obj.transform.position;
        }
    }

    // Update is called once per frame

    //onTrigger
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object has trigger entered!");
        //if in windtunnel.
        //IF (In 
        if (other.tag == "WindTunnel")
        {
            //            for (int i = 0; i < 5; i++) { }//debug values
            //get "array" value out of object, starting at obj 0.

            //blah for-errata
            if (Wrong)
            {
                glued = false;
                //figure out how to "also" call the obj to 'reset itself', call
                obj = null;
                this.gameObject.transform.position
                    = initPos.position;
                //this.gameObject.transform.rotation = initPos.rotation;
            }//trigger fail script, break after
            else { }//trigger victory script
        }

        //start of "Figure out how to get A pos to = B pos line

        else if (!glued && other.gameObject.
            GetComponent<RocketPuzzleColliders>() != null)
        {
            RocketPuzzleColliders Enemy = other.gameObject.
            GetComponent<RocketPuzzleColliders>();
            //
            if (Enemy.pol == Polarity.Minus &&
                this.pol == Polarity.Plus)
            {

                /*REDACTED Script for now, this is bad code!
                 * as in, it works? But it bugs up. So, doesn't work.
                obj = Enemy.gameObject;//hack wise?
                //trigger fusion script
                if (Enemy.order != order++ || Wrong) {
                    Wrong = true;//new object fusion is wrong
                }//end if
                glued = true;
                */
            }//end if
        }//end if

    }//end trigger check

    public void ResetPos() { this.gameObject.transform.position = initPos.position; }
    //end easier resetPos function?
}
