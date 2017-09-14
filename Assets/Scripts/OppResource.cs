using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OppResource : MonoBehaviour {

    public Animator anim;

    public int playerNum;
    public int order;

    public int roundTemp = 1;
    public int phaseTemp = 0;
    public int turnTemp = 0;

    public bool attackMe = false;
    
    void Update()
    {
        order = gameObject.transform.GetSiblingIndex();
    }

    /// <summary>
    /// Time for yellow? Y/N
    /// </summary>
    public void NextTurn()
    {
        if ((GlobalScript.i.phase + GlobalScript.i.turn) % GlobalScript.i.roster.Count == order
            && GlobalScript.i.turn > 0)
        {
            anim.Play("Yellow");
        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Yellow"))
        {
            anim.Play("YellowOff");
        }
    }

    /// <summary>
    /// Chooses which is red and when to turn not red.
    /// </summary>
    public void NextPhase()
    {
        if (GlobalScript.i.phase == order)      //Turns it red
        {
            anim.Play("ActivePhase");
        }
        else if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("ActivePhase"))
        {
            anim.Play("No Phase");
        }
        phaseTemp = GlobalScript.i.phase;
    }

    /// <summary>
    /// Does all the stuff a new round needs. Namely, moving shit around.
    /// </summary>
    public void NextRound()
    {
        //this only animates things.
        if (GlobalScript.i.round != roundTemp)
        {
            Transform root = gameObject.transform.parent.transform;
            gameObject.transform.Translate( root.position.x + order, root.position.y, root.position.z );
        }

        roundTemp = GlobalScript.i.round;
    }

    IEnumerable HoldOn(float seconds)
    {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(seconds);
        Debug.Log(Time.time);
    }

    public void AnimRoster()
    {
        order = gameObject.transform.GetSiblingIndex();

        NextRound();
        NextPhase();
        NextTurn();
    }

    // Update is called once per frame
    public void PressOpp()
    {
        GetComponentInParent<GameplaySceneHandler>().ClickCatcher();

        //write new info on the window
        GetComponentInParent<OpponentHandler>().PopulateWindow(playerNum);

        //turn on the tray interface
        GetComponentInParent<OpponentHandler>().oppBoard.SetActive(true);
    }
}
