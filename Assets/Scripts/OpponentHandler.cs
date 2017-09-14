using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This script handles all the variables and UI elements for checking the other player's board states
/// 
/// Held in the opponentPanel game object in the GameplayMG canvas
/// Also on the opponentButton object, but that's mostly for the player variable
/// </summary>

public class OpponentHandler : MonoBehaviour {
    
    public static OpponentHandler boardState;

    public GameObject oppBoard;
    GameObject opponent;

    int cardsInHand;
    bool[] checkParts= new bool[11];

    //variables for the opponent tray
    public Transform healthUI;
    public Transform attackUI;
    public Transform cardUI;
    public Transform resourceUI;

    //public Transform resources;
    public GameObject[] prefab = new GameObject[6];


	// Use this for initialization
	void Start () {

        healthUI = oppBoard.GetComponent<Transform>().GetChild(0);
        attackUI = oppBoard.GetComponent<Transform>().GetChild(1);
        cardUI = oppBoard.GetComponent<Transform>().GetChild(2);

        oppBoard.SetActive(false);

    }

    void CheckAttack(bool[] parts, int player)
    {
        int attack = 0;

        if (parts[2]) { attack++; }
        if (parts[4]) { attack++; }
        if (parts[8]) { attack++; }
        if (parts[10]) { attack++; }

        GlobalScript.i.roster[player].attack = attack;
    }

    public void PopulateWindow(int player)
    {
        Player current = GlobalScript.i.roster[player];

        //GetChild refrences by index. Index 0 is the slider object, index 2 is the text part that goes to the output
        healthUI.GetChild(0).GetComponent<Slider>().value = current.health;
        healthUI.GetChild(2).GetComponent<Text>().text = current.health.ToString();


        //Counts all the values of the card arrays associated with each player
        cardsInHand = 0;
        checkParts = current.checkParts;
        for (int c = 0; c <= 7; c++) {
            cardsInHand += current.checkCards[c];
        }
        
        cardUI.GetChild(1).GetComponent<Text>().text = cardsInHand.ToString();


        //checks the attack stat of each enemy
        CheckAttack(checkParts, player);
        attackUI.GetChild(0).GetComponent<Slider>().value = current.attack;
        attackUI.GetChild(2).GetComponent<Text>().text = current.attack.ToString();


        //Creating the robody UI element for the opponents
        int whichRobot = 0;
        Transform parent = oppBoard.GetComponent<Transform>();
        //Currently hard coded for the 4th game object under oppBoard. Instantiated objects will be put on the bottom. Also, hierarchy count will have to be recalculated once resources are implamented.
        if (parent.GetChild(4)) { Destroy(parent.GetChild(4).gameObject); }

        switch (GlobalScript.i.roster[player].roboType)
        {
            case "ConstructorRobodyUI":
                whichRobot = 0;
                break;
            case "EsquireRobodyUI":
                whichRobot = 1;
                break;
            case "ExecutionerRobodyUI":
                whichRobot = 2;
                break;
            case "GuruRobodyUI":
                whichRobot = 3;
                break;
            case "ReforgeRobodyUI":
                whichRobot = 4;
                break;
            case "SyphonRobodyUI":
                whichRobot = 5;
                break;
            default:
                break;
            
        }

        // instantiates the appropriate robot from the prefab list. Exact same assets as the first player... 
        opponent = (GameObject) Instantiate(prefab[whichRobot], 
            new Vector3(parent.position.x, parent.position.y - (10f/48f), parent.position.z), 
            Quaternion.identity, 
            parent);
        
        // ...but smaller.
        opponent.transform.localScale = new Vector3(0.55f, 0.55f, 1f);
        
        Image[] robodySelect = opponent.GetComponent<RobodyArrayResource>().buttons;
                
        for (int i = 0; i < 11; i++)
        {
            if (checkParts[i].Equals(true))
            {
                robodySelect[i].GetComponent<Image>().color = GlobalScript.i.active;
            }
            else
            {
                robodySelect[i].color = GlobalScript.i.inactive;
            }
        }


        //for the resource numerations
        resourceUI.GetChild(0).GetChild(0).GetComponent<Text>().text = current.plastic.ToString();   //plastic
        resourceUI.GetChild(1).GetChild(0).GetComponent<Text>().text = current.coal.ToString();      //coal
        resourceUI.GetChild(2).GetChild(0).GetComponent<Text>().text = current.steel.ToString();     //steel
        resourceUI.GetChild(3).GetChild(0).GetComponent<Text>().text = current.gold.ToString();      //gold
    }
}
