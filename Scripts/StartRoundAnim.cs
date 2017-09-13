using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoundAnim : MonoBehaviour {

    public Animator animMenu;
    public Animator animPass;

    public RoundController oppPanel;
    
    public void TriggerPhase()
    {
        animMenu.SetTrigger("NewRound");
    }

    public void TogglePass( bool pass )
    {
        animPass.SetBool("Pass", pass);
    }
    
}
