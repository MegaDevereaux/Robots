using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AI : MonoBehaviour {
    /// <summary>
    /// This is the general AI for now.
    /// 
    /// Capture has 1 possible course of action
    /// </summary>
    /// When able to choose he should prioritize Upgrade, then Gear if there is a Welding Kit or Mine if not. Fight if the opponent is low. Capture if threatening. Scavange if Mine doesn't have 2 viable options

    public string roboName = "";

    public RoundController rc;
    
    /// <summary>
    /// This is the meat of the AI function, deciding what action to use when first player.
    /// </summary>
    public void Decision(Player current)
    {
        //if deciding the action for the turn
        if (GlobalScript.i.turn == 0)
        {
            //capture route
            if (rc.turnActions[0].GetComponent<Button>().interactable == true && current.broken == true) { rc.TurnCapture(); }

            //fight route
            if (rc.turnActions[1].GetComponent<Button>().interactable == true)
            {
                rc.TurnFight();
            }

            //gear route
            if (rc.turnActions[3].GetComponent<Button>().interactable == true)
            {
                rc.TurnGear();
            }

            //mine route
            if (rc.turnActions[5].GetComponent<Button>().interactable == true)
            {
                rc.TurnMine();
            }

            //scavenge route
            if (rc.turnActions[4].GetComponent<Button>().interactable == true 
                || rc.turnActions[6].GetComponent<Button>().interactable == true)
            {
                rc.TurnScavenge();
            }

            //upgrade route
            if (rc.turnActions[2].GetComponent<Button>().interactable == true)
            {
                rc.TurnUpgrade();
            }
        }

        //if following already chosen actions
        switch (GlobalScript.i.actionType) { 
            case "fight":   //who to fight
                rc.ResolveFight();
                break;
            case "gear":    //what card to take
                rc.ResolveGear();
                break;
            case "mine":    //what resources are needed
                rc.ResolveMine();
                break;
            case "scavenge"://same as above, but guarnteed first player
                rc.ResolveScavenge("AI");
                break;
            case "upgrade": //what parts need building
                rc.ResolveFight();
                break;
            case "capture": //should just do it. Shouldn't hit this.
            default:
                Debug.Log("Wait what?");
                break;
        }
    }

    public void Capture()
    {
        rc.TurnCapture();
    }
    
    public void Fight()
    {

    }
    public void Gear()
    {

    }
    public void Mine()
    {

    }
    public void Scavenge()
    {

    }
    public void Upgrade()
    {

    }
}
