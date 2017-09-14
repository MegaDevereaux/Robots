using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/// <summary>
/// Runs the part UI
/// </summary>
public class partSelect : MonoBehaviour {

    //call buttons
    string partChoice;
    public Button backButton;

    public Button topChange;
    public Button upperLeftChange;
    public Button upperRightChange;
    public Button lowerLeftChange;
    public Button lowerRightChange;
    public Button bottomChange;

    //GameObject referencing the currently loaded Robody. Change it from the last scene.
    public GameObject robodyParent;
    public GameObject[] robody;

    public Color inactive;
    public Color active;

    public Image hexTarget;

    //things to get from the RAR (RobodyArrayResource)
    Sprite[] hexPartArray;
    Sprite[] microPartArray;
    Image[] robodySelect;

    //this is the menu to be enabled, currently referenced through the GameboardBG
    public GameObject PartMenu;
    int cPlastic;
    int cCoal;
    int cSteel;
    int cGold;
    int pPlastic;
    int pCoal;
    int pSteel;
    int pGold;
    public int partNum = 0;
    public Button[] resourceButtons;
    public Button buildButton;

    //references to other scripts
    public RoundController gameState; //get information from the current player
    public GameplaySceneHandler clickOff;
    public playCard cardHelper;
    public Part[] currentParts;     //references the RobodyArrayResource

	// Use this for initialization
	void Start ()
    {
        //Runs through all of the object names in the robody[] array and checks it against player 1. Needed only once, do not call twice.
        foreach (GameObject potential in robody)
        {
            //This section is to initialize the other player's information on load
            potential.GetComponent<RobodyArrayResource>().Audit();

            if (potential.GetComponent<RobodyArrayResource>().roboName == GlobalScript.i.roster[0].roboType)
            {
                robodyParent = potential;
            }
            else { potential.SetActive(false); }
        }
        

        robodyParent.SetActive(true);


        //initializing buttons
        backButton = backButton.GetComponent<Button>();
        
        DrawHexMap(robodyParent);

        //first is the close up of the part cost and function
        hexTarget = hexTarget.GetComponent<Image>();

        BuildABot();

        //initialize Menus
        PartMenu.SetActive(false);

        //have to instantiate a playCard object to use the funtions under the script
        cardHelper = cardHelper.GetComponent<playCard>();

    }


    public void BuildABot()
    {
        //goes piece by piece initializing what is built and what isn't from the save file
        for (int i = 0; i < 11; i++)
        {
            if (GlobalScript.i.roster[0].checkParts[i])
            {

                robodySelect[i].GetComponent<Image>().color = GlobalScript.i.active;
            }
            else { robodySelect[i].color = GlobalScript.i.inactive; }
        }
    }

    /// <summary>
    /// Called internally to reference the Robody Array Resource and color code the menu
    /// </summary>
    /// <param name="robot"></param>
    void DrawHexMap(GameObject robot)
    {
        //initialize the auxiliary select buttons and tint them to a specific robody based color
        topChange.GetComponent<Image>().color = robot.GetComponent<RobodyArrayResource>().theme;
        upperLeftChange.GetComponent<Image>().color = robot.GetComponent<RobodyArrayResource>().theme;
        upperRightChange.GetComponent<Image>().color = robot.GetComponent<RobodyArrayResource>().theme;
        lowerLeftChange.GetComponent<Image>().color = robot.GetComponent<RobodyArrayResource>().theme;
        lowerRightChange.GetComponent<Image>().color = robot.GetComponent<RobodyArrayResource>().theme;
        bottomChange.GetComponent<Image>().color = robot.GetComponent<RobodyArrayResource>().theme;

        //these three are dependent on the canvas group loaded giving a different suite of hexes, micros, and access to the interactive part buttons on the main screen
        hexPartArray = robot.GetComponent<RobodyArrayResource>().hex;
        microPartArray = robot.GetComponent<RobodyArrayResource>().micro;
        robodySelect = robot.GetComponent<RobodyArrayResource>().buttons;

    }
    
    /// <summary>
    /// When pressing the back button
    /// </summary>
    public void BackPress()
    {
        //turn off the credit pop up
        PartMenu.SetActive(false);

        //turn on the standard buttons where approps
        cardHelper.ActivateCards();
    }

    /// <summary>
    /// Pops up the parts interface.
    /// Redraws the part selection menu to follow the robody being referenced currently.
    /// </summary>
    public void PartPress()
    {

        //initialize the partChoice Button here to run once something is pushed. 
        partChoice = EventSystem.current.currentSelectedGameObject.name;
        hexTarget = hexTarget.GetComponent<Image>();

        //check if switch statments can handle strings in C#
        if (partChoice == "head")
        {
            //changing the focus image
            hexTarget.GetComponent<Image>().sprite = hexPartArray[0];

            //initializing the images in the buttons and changing the names to use the same script
            topChange.GetComponent<Image>().sprite = microPartArray[11];        //nothing
            topChange.name = "nothing";
            upperLeftChange.GetComponent<Image>().sprite = microPartArray[11];  //nothing
            upperLeftChange.name = "nothing";
            upperRightChange.GetComponent<Image>().sprite = microPartArray[11]; //nothing
            upperRightChange.name = "nothing";
            lowerLeftChange.GetComponent<Image>().sprite = microPartArray[3];  //left shoulder
            lowerLeftChange.name = "shoulderLeft";
            lowerRightChange.GetComponent<Image>().sprite = microPartArray[1]; //right shoulder
            lowerRightChange.name = "shoulderRight";
            bottomChange.GetComponent<Image>().sprite = microPartArray[5];     //chest
            bottomChange.name = "chest";
        }
        else if (partChoice == "shoulderRight")
        {
            hexTarget.GetComponent<Image>().sprite = hexPartArray[1];

            //initializing the images in the buttons
            topChange.GetComponent<Image>().sprite = microPartArray[11];        //nothing
            topChange.name = "nothing";
            upperLeftChange.GetComponent<Image>().sprite = microPartArray[0];   //head
            upperLeftChange.name = "head";
            upperRightChange.GetComponent<Image>().sprite = microPartArray[11]; //nothing
            upperRightChange.name = "nothing";
            lowerLeftChange.GetComponent<Image>().sprite = microPartArray[5];   //chest
            lowerLeftChange.name = "chest";
            lowerRightChange.GetComponent<Image>().sprite = microPartArray[2];  //right arm
            lowerRightChange.name = "armRight";
            bottomChange.GetComponent<Image>().sprite = microPartArray[11];     //nothing
            bottomChange.name = "nothing";
        }
        else if (partChoice == "armRight")
        {
            hexTarget.GetComponent<Image>().sprite = hexPartArray[2];

            //initializing the images in the buttons
            topChange.GetComponent<Image>().sprite = microPartArray[11];        //nothing
            topChange.name = "nothing";
            upperLeftChange.GetComponent<Image>().sprite = microPartArray[1];   //right shoulder
            upperLeftChange.name = "shoulderRight";
            upperRightChange.GetComponent<Image>().sprite = microPartArray[11]; //nothing
            upperRightChange.name = "nothing";
            lowerLeftChange.GetComponent<Image>().sprite = microPartArray[11];  //nothing
            lowerLeftChange.name = "nothing";
            lowerRightChange.GetComponent<Image>().sprite = microPartArray[11]; //nothing
            lowerRightChange.name = "nothing";
            bottomChange.GetComponent<Image>().sprite = microPartArray[11];     //nothing
            bottomChange.name = "nothing";
        }
        else if (partChoice == "shoulderLeft")
        {
            hexTarget.GetComponent<Image>().sprite = hexPartArray[3];

            //initializing the images in the buttons
            topChange.GetComponent<Image>().sprite = microPartArray[11];        //nothing
            topChange.name = "nothing";
            upperLeftChange.GetComponent<Image>().sprite = microPartArray[11];  //nothing
            upperLeftChange.name = "nothing";
            upperRightChange.GetComponent<Image>().sprite = microPartArray[0];  //head
            upperRightChange.name = "head";
            lowerLeftChange.GetComponent<Image>().sprite = microPartArray[4];   //left arm
            lowerLeftChange.name = "armLeft";
            lowerRightChange.GetComponent<Image>().sprite = microPartArray[5];  //chest
            lowerRightChange.name = "chest";
            bottomChange.GetComponent<Image>().sprite = microPartArray[11];     //nothing
            bottomChange.name = "nothing";
        }
        else if (partChoice == "armLeft")
        {
            hexTarget.GetComponent<Image>().sprite = hexPartArray[4];

            //initializing the images in the buttons
            topChange.GetComponent<Image>().sprite = microPartArray[11];        //nothing
            topChange.name = "nothing";
            upperLeftChange.GetComponent<Image>().sprite = microPartArray[11];  //nothing
            upperLeftChange.name = "nothing";
            upperRightChange.GetComponent<Image>().sprite = microPartArray[3];  //left shoulder
            upperRightChange.name = "shoulderLeft";
            lowerLeftChange.GetComponent<Image>().sprite = microPartArray[11];  //nothing
            lowerLeftChange.name = "nothing";
            lowerRightChange.GetComponent<Image>().sprite = microPartArray[11]; //nothing
            lowerRightChange.name = "nothing";
            bottomChange.GetComponent<Image>().sprite = microPartArray[11];     //nothing
            bottomChange.name = "nothing";
        }
        else if (partChoice == "chest")
        {
            hexTarget.GetComponent<Image>().sprite = hexPartArray[5];

            //initializing the images in the buttons
            topChange.GetComponent<Image>().sprite = microPartArray[0];         //head
            topChange.name = "head";
            upperLeftChange.GetComponent<Image>().sprite = microPartArray[3];   //left shoulder
            upperLeftChange.name = "shoulderLeft";
            upperRightChange.GetComponent<Image>().sprite = microPartArray[1];  //right shoulder
            upperRightChange.name = "shoulderRight";
            lowerLeftChange.GetComponent<Image>().sprite = microPartArray[11];  //nothing
            lowerLeftChange.name = "nothing";
            lowerRightChange.GetComponent<Image>().sprite = microPartArray[11]; //nothing
            lowerRightChange.name = "nothing";
            bottomChange.GetComponent<Image>().sprite = microPartArray[6];      //core
            bottomChange.name = "core";
        }
        else if (partChoice == "core")
        {
            hexTarget.GetComponent<Image>().sprite = hexPartArray[6];

            //initializing the images in the buttons
            topChange.GetComponent<Image>().sprite = microPartArray[5];         //chest
            topChange.name = "chest";
            upperLeftChange.GetComponent<Image>().sprite = microPartArray[11];  //nothing
            upperLeftChange.name = "nothing";
            upperRightChange.GetComponent<Image>().sprite = microPartArray[11]; //nothing
            upperRightChange.name = "nothing";
            lowerLeftChange.GetComponent<Image>().sprite = microPartArray[9];   //left leg
            lowerLeftChange.name = "legLeft";
            lowerRightChange.GetComponent<Image>().sprite = microPartArray[7];  //right leg
            lowerRightChange.name = "legRight";
            bottomChange.GetComponent<Image>().sprite = microPartArray[11];     //nothing
            bottomChange.name = "nothing";
        }
        else if (partChoice == "legRight")
        {
            hexTarget.GetComponent<Image>().sprite = hexPartArray[7];

            //initializing the images in the buttons
            topChange.GetComponent<Image>().sprite = microPartArray[11];        //nothing
            topChange.name = "nothing";
            upperLeftChange.GetComponent<Image>().sprite = microPartArray[6];   //core
            upperLeftChange.name = "core";
            upperRightChange.GetComponent<Image>().sprite = microPartArray[11]; //nothing
            upperRightChange.name = "nothing";
            lowerLeftChange.GetComponent<Image>().sprite = microPartArray[11];  //nothing
            lowerLeftChange.name = "nothing";
            lowerRightChange.GetComponent<Image>().sprite = microPartArray[11]; //nothing
            lowerRightChange.name = "nothing";
            bottomChange.GetComponent<Image>().sprite = microPartArray[8];      //right foot
            bottomChange.name = "footRight";
        }
        else if (partChoice == "footRight")
        {
            hexTarget.GetComponent<Image>().sprite = hexPartArray[8];

            //initializing the images in the buttons
            topChange.GetComponent<Image>().sprite = microPartArray[7];        //right leg
            topChange.name = "legRight";
            upperLeftChange.GetComponent<Image>().sprite = microPartArray[11];  //nothing
            upperLeftChange.name = "nothing";
            upperRightChange.GetComponent<Image>().sprite = microPartArray[11]; //nothing
            upperRightChange.name = "nothing";
            lowerLeftChange.GetComponent<Image>().sprite = microPartArray[11];  //nothing
            lowerLeftChange.name = "nothing";
            lowerRightChange.GetComponent<Image>().sprite = microPartArray[11]; //nothing
            lowerRightChange.name = "nothing";
            bottomChange.GetComponent<Image>().sprite = microPartArray[11];     //nothing
            bottomChange.name = "nothing";
        }
        else if (partChoice == "legLeft")
        {
            hexTarget.GetComponent<Image>().sprite = hexPartArray[9];

            //initializing the images in the buttons
            topChange.GetComponent<Image>().sprite = microPartArray[11];        //nothing
            topChange.name = "nothing";
            upperLeftChange.GetComponent<Image>().sprite = microPartArray[11];  //nothing
            upperLeftChange.name = "nothing";
            upperRightChange.GetComponent<Image>().sprite = microPartArray[6]; //core
            upperRightChange.name = "core";
            lowerLeftChange.GetComponent<Image>().sprite = microPartArray[11];  //nothing
            lowerLeftChange.name = "nothing";
            lowerRightChange.GetComponent<Image>().sprite = microPartArray[11]; //nothing
            lowerRightChange.name = "nothing";
            bottomChange.GetComponent<Image>().sprite = microPartArray[10];     //left foot
            bottomChange.name = "footLeft";
        }
        else if (partChoice == "footLeft")
        {
            hexTarget.GetComponent<Image>().sprite = hexPartArray[10];

            //initializing the images in the buttons
            topChange.GetComponent<Image>().sprite = microPartArray[9];        //left leg
            topChange.name = "legLeft";
            upperLeftChange.GetComponent<Image>().sprite = microPartArray[11];  //nothing
            upperLeftChange.name = "nothing";
            upperRightChange.GetComponent<Image>().sprite = microPartArray[11]; //nothing
            upperRightChange.name = "nothing";
            lowerLeftChange.GetComponent<Image>().sprite = microPartArray[11];  //nothing
            lowerLeftChange.name = "nothing";
            lowerRightChange.GetComponent<Image>().sprite = microPartArray[11]; //nothing
            lowerRightChange.name = "nothing";
            bottomChange.GetComponent<Image>().sprite = microPartArray[11];     //nothing
            bottomChange.name = "nothing";
        }
        else if (partChoice == "nothing") { }
        else //Shouldn't be possible
        {
            hexTarget.GetComponent<Image>().sprite = hexPartArray[11];
        }

        LoadCost();

        //turn on the part menu
        PartMenu.SetActive( true );
        
        //turn off the standard buttons
        cardHelper.DisableCardButtons();

    }

    /// <summary>
    /// On piece press and further moving about the UI displays the cost for parts.
    /// </summary>
    public void LoadCost()
    {
        currentParts = robodyParent.GetComponent<RobodyArrayResource>().parts;
        
        switch (partChoice)
        {
            case "head":
                partNum = 0;
                break;
            case "shoulderRight":
                partNum = 1;
                break;
            case "armRight":
                partNum = 2;
                break;
            case "shoulderLeft":
                partNum = 3;
                break;
            case "armLeft":
                partNum = 4;
                break;
            case "chest":
                partNum = 5;
                break;
            case "core":
                partNum = 6;
                break;
            case "legRight":
                partNum = 7;
                break;
            case "footRight":
                partNum = 8;
                break;
            case "legLeft":
                partNum = 9;
                break;
            case "footLeft":
                partNum = 10;
                break;
            default:
                Debug.Log("Nope. Try again.");
                break;
        }

        // transcribes the resources necessary for building the part in question
        resourceButtons[0].interactable = (currentParts[partNum].plastic > 0) ? true : false;
        resourceButtons[0].transform.GetChild(0).GetComponent<Text>().text = currentParts[partNum].plastic.ToString();
        resourceButtons[1].interactable = (currentParts[partNum].coal > 0) ? true : false;
        resourceButtons[1].transform.GetChild(0).GetComponent<Text>().text = currentParts[partNum].coal.ToString();
        resourceButtons[2].interactable = (currentParts[partNum].steel > 0) ? true : false;
        resourceButtons[2].transform.GetChild(0).GetComponent<Text>().text = currentParts[partNum].steel.ToString();
        resourceButtons[3].interactable = (currentParts[partNum].gold > 0) ? true : false;
        resourceButtons[3].transform.GetChild(0).GetComponent<Text>().text = currentParts[partNum].gold.ToString();

        cPlastic = gameState.currentPlayer.plastic;
        cCoal = gameState.currentPlayer.coal;
        cSteel = gameState.currentPlayer.steel;
        cGold = gameState.currentPlayer.gold;

        pPlastic = currentParts[partNum].plastic;
        pCoal = currentParts[partNum].coal;
        pSteel = currentParts[partNum].steel;
        pGold = currentParts[partNum].gold;

        buildButton.interactable = false;
    }

    /// <summary>
    /// Inputs a resource type to invest. Decrements the current player's resource type and the visible cost. 
    /// Once all resources assigned the build button turns on.
    /// </summary>
    /// <param name="resource"></param>
    public void Invest(string resource)
    {
        if (resource == "plastic" && cPlastic > 0) {
            cPlastic--;
            pPlastic--;
            Debug.Log("Plastic to offer: " + cPlastic);
        }
        if (resource == "coal" && cCoal > 0) {
            cCoal--;
            pCoal--;
            Debug.Log("Coal in hand: " + gameState.currentPlayer.coal);
            Debug.Log("Coal to offer: " + cCoal);
        }
        if (resource == "steel" && cSteel > 0) {
            cSteel--;
            pSteel--;
            Debug.Log("Steel to offer: " + cSteel);
        }
        if (resource == "gold" && cGold > 0) {
            cGold--;
            pGold--;
            Debug.Log("Gold to offer: " + cGold);
        }

        // simliar but different to above.
        resourceButtons[0].interactable = (pPlastic > 0) ? true : false;
        resourceButtons[0].transform.GetChild(0).GetComponent<Text>().text = pPlastic.ToString();
        resourceButtons[1].interactable = (pCoal > 0) ? true : false;
        resourceButtons[1].transform.GetChild(0).GetComponent<Text>().text = pCoal.ToString();
        resourceButtons[2].interactable = (pSteel > 0) ? true : false;
        resourceButtons[2].transform.GetChild(0).GetComponent<Text>().text = pSteel.ToString();
        resourceButtons[3].interactable = (pGold > 0) ? true : false;
        resourceButtons[3].transform.GetChild(0).GetComponent<Text>().text = pGold.ToString();

        if (!resourceButtons[0].interactable 
            && !resourceButtons[1].interactable
            && !resourceButtons[2].interactable
            && !resourceButtons[3].interactable) { buildButton.interactable = true; }

    }
}