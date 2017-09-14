using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

    /// <summary>
    /// Script held in opponentPanel object
    /// This is the tracker for rounds
    /// The idea is to have an automated system of methods to reference each NPC's AI scripts (packaged elsewhere)
    /// To Begin:
    /// Call TurnCheck - it sets the newPhase boolian to false and opens the menu to choose the round's action
    /// Once the menu is up call a *Turn function depending on the button called
    /// 
    /// </summary>
public class RoundController : MonoBehaviour {

    //Stuff to update constantly; always useful and upto date info
    /// <summary>
    /// (GlobalScript.i.round - 1 + GlobalScript.i.phase + GlobalScript.i.turn) % GlobalScript.i.roster.Count
    /// </summary>
    public Player currentPlayer;
    
    int playerTurn = 0;

    public Text roundText;
    public Text phaseText;
    public Text turnText;
    public GameObject choice;
    public GameObject scavengeDouble;
    /// <summary> Capture, Fight, Upgrade, Gear, Scavenge, Mine, ScavengeOther </summary>
    public Button[] turnActions = new Button[7];    

    // for policing the fight menu
    public Sprite[] bodyRobot;      //icon used for opponent buttons
    public GameObject[] activateFight = new GameObject[5];  //button bank for fight menu
    public int attBonus = 0;        // static number added for first turn and card effects
    public float multiAtt = 1.0f;     //for chainsaws and force fields
    public Text attackText;         // bottom text giving current attack value
    GameObject butPress;
    public GameObject confirmFight; // references the big confirm button on the bottom

    // for policing the gear menu
    bool deluxe = false;
    public GameObject[] activateGear = new GameObject[9];
    /// <summary> [0] "Chainsaw", [1] "Force Field", [2] "Sticky Hands", [3] "Welders Kit", [4] "Repair Kit", [5] "Rocket Punch", [6] "Shovel", [7] "Twin Blasters" </summary>
    public Sprite[] gearTypes = new Sprite[8];
    public string[,] gearDeck = new string[32, 2] {
        { "cs" , "y" } , { "cs" , "y" } , { "cs" , "y" } ,      //chainsaw
        { "ff" , "y" } , { "ff" , "y" } , { "ff" , "y" } ,      //force field
        { "sh" , "y" } , { "sh" , "y" } , { "sh" , "y" } ,      //sticky hands
        { "wk" , "y" } , { "wk" , "y" } , { "wk" , "y" } ,      //welders kit
        { "rk" , "y" } , { "rk" , "y" } , { "rk" , "y" } , { "rk" , "y" } , { "rk" , "y" } ,    //repair kit
        { "rp" , "y" } , { "rp" , "y" } , { "rp" , "y" } , { "rp" , "y" } , { "rp" , "y" } ,    //rocket punch
        {  "s" , "y" } , {  "s" , "y" } , {  "s" , "y" } , {  "s" , "y" } , {  "s" , "y" } ,    //shovel
        { "tb" , "y" } , { "tb" , "y" } , { "tb" , "y" } , { "tb" , "y" } , { "tb" , "y" } };   //twin blasters

    // for policing the mine menu
    /// <summary> [0] "Plastic", [1] "Coal", [2] "Steel", [3] "Gold" </summary>
    public Sprite[] resourceType = new Sprite[4];
    public Transform quarryTab;
    public GameObject resTemplate; // prefab
    GameObject[] activateMine = new GameObject[8];
    public GameObject playerHealth;

    //for policing the upgrade functions. It's gonna be a mess
    public Button wildResource;
    public Button buildButton;
    public GameObject resTray;

    // for policing the scavenge menu
    public GameObject[] activateScavenge = new GameObject[4];

    // talk to other objects
    public GameObject actionMenu;

    // talk to other scripts
    public List<OppResource> oppHelper;
    public playCard cardHelper;
    public partSelect partHelper;
    public StartRoundAnim animHelper;

    /// <summary>
    /// The umbrella script for the rounds.
    /// Controls the animations for the round banner (banner), the selection menu (roundPanel), and the shuffling of the player order.
    /// Also handles the results of selecting the different kinds of turn actions.
    /// </summary>
    void Start () {

        // Populating the fight menu
        for (int c = 0; c < 5; c++)
        {
            if (c < GlobalScript.i.roster.Count - 1) {
                activateFight[c].SetActive(true);
                activateFight[c].transform.GetChild(2).GetComponent<Text>().text = GlobalScript.i.roster[c + 1].health.ToString();
                if (GlobalScript.i.roster[c + 1].roboType.Equals("ConstructorRobodyUI"))
                {
                    activateFight[c].transform.GetChild(1).GetComponent<Image>().sprite = bodyRobot[0];
                }
                if (GlobalScript.i.roster[c + 1].roboType.Equals("EsquireRobodyUI"))
                {
                    activateFight[c].transform.GetChild(1).GetComponent<Image>().sprite = bodyRobot[1];
                }
                if (GlobalScript.i.roster[c + 1].roboType.Equals("ExecutionerRobodyUI"))
                {
                    activateFight[c].transform.GetChild(1).GetComponent<Image>().sprite = bodyRobot[2];
                }
                if (GlobalScript.i.roster[c + 1].roboType.Equals("GuruRobodyUI"))
                {
                    activateFight[c].transform.GetChild(1).GetComponent<Image>().sprite = bodyRobot[3];
                }
                if (GlobalScript.i.roster[c + 1].roboType.Equals("ReforgeRobodyUI"))
                {
                    activateFight[c].transform.GetChild(1).GetComponent<Image>().sprite = bodyRobot[4];
                }
                if (GlobalScript.i.roster[c + 1].roboType.Equals("SyphonRobodyUI"))
                {
                    activateFight[c].transform.GetChild(1).GetComponent<Image>().sprite = bodyRobot[5];
                }
            }
            else { activateFight[c].SetActive(false); }
        }
        
//Populating the Mine menu and Gear menus. They have the same number of pieces
        for (int c = 0; c < 8; c++)
        {
            if (c < GlobalScript.i.roster.Count +2)
            {
                //first, set to true. Then, set a random element
            //    activateMine[c].SetActive(true);

                //set gear cards to true
                activateGear[c].SetActive(true);

            }
            else
            {
            //    activateMine[c].SetActive(false);
                activateGear[c].SetActive(false);
            }
        }

        FillQuarry();

        foreach (GameObject gear in activateGear) { gear.SetActive(false); }
        FillGear();

        foreach (GameObject resource in activateScavenge) { resource.SetActive(false); }

        //initializing variables
        GlobalScript.i.actionType = "";
        choice.SetActive(true);
        
        if (GlobalScript.i.roster.Count >= 6)
        {
            scavengeDouble.SetActive(true);
        }
        else
        {
            scavengeDouble.SetActive(false);
        }

        //dirty fix to prevent incrementing on Load
        GlobalScript.i.turn--;
        TurnCheck();

        currentPlayer = GlobalScript.i.roster[(GlobalScript.i.round - 1 + GlobalScript.i.phase + GlobalScript.i.turn) % GlobalScript.i.roster.Count];
    }

    /// <summary>
    /// Updates the current and first players always forever.
    /// </summary>
    void Update()
    {
        //adding the playerTurn variable (starts at 0) should give later player truns
        currentPlayer = GlobalScript.i.roster[(GlobalScript.i.round - 1 + GlobalScript.i.phase + GlobalScript.i.turn) % GlobalScript.i.roster.Count];
    }

    /// <summary>
    /// Pushes the player list. Refreshes Action hexes.
    /// </summary>
    void StartRound()
    {
        GlobalScript.i.round++;
        GlobalScript.i.phase = 0;
        GlobalScript.i.turn = 0;

        gameObject.transform.GetChild(0).SetAsLastSibling();
        
        foreach (OppResource opp in oppHelper) { opp.AnimRoster(); } // animator for new round stuff

        foreach (Button action in turnActions) { action.interactable = true; }  // turning on the buttons
        if (GlobalScript.i.roster.Count == 6) { scavengeDouble.GetComponent<Button>().interactable = true; }

        roundText.text = "Round " + GlobalScript.i.round;
    }

    /// <summary>
    /// Called through TurnCheck, TurnCapture, TurnScavenge.
    /// Takes care of the start of phase maintenance: displaying current round, populating Quarry and Gear
    /// </summary>
    void StartPhase()
    {
        animHelper.TogglePass(false);

        GlobalScript.i.phase++;
        GlobalScript.i.turn = 0;

        foreach (OppResource opp in oppHelper) { opp.AnimRoster(); } // animator for new phase stuff

        if (GlobalScript.i.phase % GlobalScript.i.roster.Count == 0
            && GlobalScript.i.phase > 1) { StartRound(); }

        // by the book this happens once that phase finishes.
        if (GlobalScript.i.actionType == "mine") { FillQuarry(); }
        if (GlobalScript.i.actionType == "gear") { FillGear(); }
        
        animHelper.TriggerPhase();

        phaseText.text = "Phase " + (GlobalScript.i.phase + 1);
    }

    /// <summary>
    /// Calls after confirming a turn action, checking if a new phase begins. If so, pass to StartPhase()
    /// </summary>
    public void TurnCheck()
    {
        GlobalScript.i.turn++;
        playerTurn = GlobalScript.i.turn;

        foreach (OppResource opp in oppHelper) { opp.AnimRoster(); } // animator for new phase stuff

        currentPlayer = GlobalScript.i.roster[(GlobalScript.i.round - 1 + GlobalScript.i.phase + GlobalScript.i.turn) % GlobalScript.i.roster.Count];

        // the Mine action gives the first player a second pass
        if (playerTurn == GlobalScript.i.roster.Count && GlobalScript.i.actionType == "mine") { TurnMine(); }
        // two actions only go off for first player
        else if (playerTurn >= GlobalScript.i.roster.Count
            || GlobalScript.i.actionType == "capture"
            || GlobalScript.i.actionType == "scavenge")
        { StartPhase(); }
        //check to see if the next player exists
        else if (playerTurn < GlobalScript.i.roster.Count)
        {
            switch (GlobalScript.i.actionType)
            {
                //Shouldn't be an option, but if it *does* happen I wanna know.
                case "capture":
                    StartPhase();
                    break;

                case "fight":
                    TurnFight();
                    break;

                case "gear":
                    TurnGear();
                    break;

                case "mine":    //gets an extra pass
                    TurnMine();
                    break;

                case "scavenge":
                    StartPhase();
                    break;

                case "upgrade":
                    TurnUpgrade();
                    break;

                default:
                    break;
            }
        }

        turnText.text = "Turn " + (GlobalScript.i.turn + 1);
    }
    
    /// <summary>
    /// Capture Logic:
    /// Give +2/+4 health to the current player.
    /// Take -1/-2 health to every other player.
    /// Alternately, heal 5 from broken.
    /// Pass turn.
    /// </summary>
    public void TurnCapture()
    {
        turnActions[0].GetComponent<Button>().interactable = false;
        GlobalScript.i.actionType = "capture";

        if (GlobalScript.i.turn == 0)
        {
            if (currentPlayer.holdHeart && !currentPlayer.broken)
            {
                foreach(Player robot in GlobalScript.i.roster)
                {
                    if(robot.roboType != currentPlayer.roboType)
                    {
                        robot.health -= 2;

                        //check for broken robots
                        if (robot.health <= 0)
                        {
                            robot.broken = true;
                            robot.health = 0;
                        }
                    }
                    else
                    {
                        robot.health += 4;
                    }
                }
            }
            else if (currentPlayer.broken)
            {
                currentPlayer.broken = false;
                currentPlayer.health = 5;
            }
            else
            {
                // Gain 2 health

                // Hit every other robot for 1
                foreach (Player robot in GlobalScript.i.roster)
                {
                    if (robot.roboType != currentPlayer.roboType)
                    {
                        robot.health -= 1;
                        
                        //check for broken robots
                        if (robot.health <= 0)
                        {
                            robot.broken = true;
                            robot.health = 0;
                        }
                    }
                    else { robot.health += 2; }
                }
            }

            //take it from everyone else
            foreach (Player robot in GlobalScript.i.roster)
            {
                robot.holdHeart = false;
                if (currentPlayer == robot)
                { robot.holdHeart = true; }
            }
            currentPlayer.holdHeart = true;
        }
        UpdateHealth();
        // Starts a new turn immediately 
        TurnCheck();
    }

    /// <summary>
    /// Changes GlobalScript.i.actionType to "fight". Increases attack value in the local and changes color. 
    /// Probably check for AI in the Turn functions.
    /// </summary>
    public void TurnFight()
    {
        Debug.Log("Putting dukes up");
        animHelper.TogglePass(true);

        turnActions[1].GetComponent<Button>().interactable = false;
        GlobalScript.i.actionType = "fight";

        if (GlobalScript.i.turn == 0) {
            attBonus = 2;
            if (currentPlayer == GlobalScript.i.roster[0]) { attackText.text = (GlobalScript.i.roster[0].attack + attBonus).ToString(); }
        }
        multiAtt = 1;
    }

    /// <summary>
    /// Choose and confirm target. Should only call if fight is chosen and if they pick a target in the fight tab.
    /// </summary>
    public void ResolveFight()
    {
        Debug.Log("Fighting...");
        //if selecting a different option it'll turn off the old one
        if (butPress != null && butPress.transform.GetSiblingIndex() != EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex())
        {
            butPress.transform.GetChild(0).GetComponent<Button>().interactable = false;
        }

        //initialize a new button
        butPress = EventSystem.current.currentSelectedGameObject;
        
        //what happens when selecting a target
        if (GlobalScript.i.actionType.Equals("fight")
            && !butPress.transform.GetChild(0).GetComponent<Button>().interactable)
        {            
            //dictate target
            butPress.transform.GetChild(0).GetComponent<Button>().interactable = true;
        }
        //what happens when confirming a target
        else if(butPress.transform.GetChild(0).GetComponent<Button>().interactable)
        {
            int damage = Mathf.FloorToInt((currentPlayer.attack + attBonus) * multiAtt);
            //assigns damage
            GlobalScript.i.roster[ butPress.transform.GetSiblingIndex() + 1 ].health -= damage;

            //initializing before passing
            attBonus = 0;
            multiAtt = 1;
            UpdateAttack();
            UpdateHealth();
            butPress.transform.GetChild(0).GetComponent<Button>().interactable = false;

            TurnCheck();
            actionMenu.GetComponent<Animator>().Play("FightOff");
        }

    }

    /// <summary>
    /// changes GlobalScript.i.actionType to "gear"
    /// </summary>
    public void TurnGear()
    {
        animHelper.TogglePass(true);
        

        foreach (GameObject gear in activateGear)
        {
            if (gear.GetComponent<Image>().sprite.name == "Repair Kit"
                || gear.GetComponent<Image>().sprite.name == "Rocket Punch"
                || gear.GetComponent<Image>().sprite.name == "Shovel"
                || gear.GetComponent<Image>().sprite.name == "Twin Blasters"
                || GlobalScript.i.turn == 0)
            {
                gear.GetComponent<Button>().interactable = true;
            }
            else
            {
                gear.GetComponent<Button>().interactable = false;
            }
        }

        turnActions[3].GetComponent<Button>().interactable = false;
        GlobalScript.i.actionType = "gear";
    }

    /// <summary>
    /// Goes after you choose a card to draw.
    /// </summary>
    public void ResolveGear()
    {
        if (GlobalScript.i.actionType == "gear")
        {
            if (currentPlayer.checkParts[6] || GlobalScript.i.turn == 0)
            {
                deluxe = true;
            }

            switch (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name)
            {
                case "Chainsaw":
                    if (deluxe)
                    {
                        currentPlayer.checkCards[0]++;
                        goto default;
                    }
                    break;
                case "Force Field":
                    if (deluxe)
                    {
                        currentPlayer.checkCards[1]++;
                        goto default;
                    }
                    break;
                case "Sticky Hands":
                    if (deluxe)
                    {
                        currentPlayer.checkCards[2]++;
                        goto default;
                    }
                    break;
                case "Welders Kit":
                    if (deluxe)
                    {
                        currentPlayer.checkCards[3]++;
                        goto default;
                    }
                    break;
                case "Repair Kit":
                    currentPlayer.checkCards[4]++;
                    goto default;
                case "Rocket Punch":
                    currentPlayer.checkCards[5]++;
                    goto default;
                case "Shovel":
                    currentPlayer.checkCards[6]++;
                    goto default;
                case "Twin Blasters":
                    currentPlayer.checkCards[7]++;
                    goto default;
                default:
                    EventSystem.current.currentSelectedGameObject.SetActive(false);
                    break;
            }

            // write in the new card values
            UpdateResources();
            // set the new buttons active
            cardHelper.ActivateCards();
            // References the turn check function for the other player's actions
            TurnCheck();
            actionMenu.GetComponent<Animator>().Play("GearOff");
        }
    }

    /// <summary>
    /// Creates the deck and deals the cards. Nothing taken out of the deck should be played again.
    /// </summary>
    void FillGear()
    {
        foreach(GameObject gear in activateGear)
        {
            gear.GetComponent<Button>().interactable = false;
        }

        for (int c = 0; c < GlobalScript.i.roster.Count + 2; c++)
        {
            if (activateGear[c].activeInHierarchy)
            {
                continue;
            }
            else
            {
                activateGear[c].SetActive(true);
                GetRandom:
                int ranGear = Random.Range(0, 32);

                // has the card already been played?
                if (gearDeck[ranGear, 1] == "n") { goto GetRandom; }

                else
                {
                    switch (ranGear)
                    {
                        case 0:     // chainsaw
                        case 1:
                        case 2:
                            activateGear[c].GetComponent<Image>().sprite = gearTypes[0];
                            break;
                        case 3:     // force field
                        case 4:
                        case 5:
                            activateGear[c].GetComponent<Image>().sprite = gearTypes[1];
                            break;
                        case 6:     // sticky hands
                        case 7:
                        case 8:
                            activateGear[c].GetComponent<Image>().sprite = gearTypes[2];
                            break;
                        case 9:     // welding kit
                        case 10:
                        case 11:
                            activateGear[c].GetComponent<Image>().sprite = gearTypes[3];
                            break;
                        case 12:     // repair kit
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                            activateGear[c].GetComponent<Image>().sprite = gearTypes[4];
                            break;
                        case 17:     // rocket punch
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                            activateGear[c].GetComponent<Image>().sprite = gearTypes[5];
                            break;
                        case 22:     // shovel
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                            activateGear[c].GetComponent<Image>().sprite = gearTypes[6];
                            break;
                        case 27:     // twin blasters
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                            activateGear[c].GetComponent<Image>().sprite = gearTypes[7];
                            break;
                        default:
                            Debug.Log("waitwhat");
                            break;
                    }

                    gearDeck[c, 1] = "n";
                }
            }
        }
    }

    /// <summary>
    /// Sets GlobalScript.i.actionType to mine. Turns off the button.
    /// </summary>
    public void TurnMine()
    {
        foreach(GameObject mine in activateMine) { if (mine != null) { mine.GetComponent<Button>().interactable = true; } }
        turnActions[5].GetComponent<Button>().interactable = false;
        GlobalScript.i.actionType = "mine";

    }

    /// <summary>
    /// Gives the current player the chosen resource
    /// </summary>
    public void ResolveMine()
    {
        Debug.Log("boop" + currentPlayer.roboType);
        if (GlobalScript.i.actionType == "mine")
        {            
            switch (EventSystem.current.currentSelectedGameObject.transform.GetChild(1).GetComponent<Text>().text)
            {
                case "gold":
                    currentPlayer.gold++;
                    break;
                case "steel":
                    currentPlayer.steel++;
                    break;
                case "coal":
                    currentPlayer.coal++;
                    break;
                case "plastic":
                    currentPlayer.plastic++;
                    break;
                default:
                    Debug.Log("Check your spelling, yo.");
                    break;
            }

            EventSystem.current.currentSelectedGameObject.SetActive(false);

            // First update it in the player objects
            UpdateResources();
            // Reflect it on the game state
            gameObject.GetComponent<GameplaySceneHandler>().UpdateResources();
            
            //References the turn check function for the other player's actions
            TurnCheck();
            actionMenu.GetComponent<Animator>().Play("MineOff");
        }
    }

    /// <summary>
    /// Activates and launches new mining buttons
    /// </summary>
    void FillQuarry()
    {
        for (int c = 0; c <= GlobalScript.i.roster.Count + 1; c++)
        {
            activateMine[c] = Instantiate(resTemplate,
                    new Vector3(transform.position.x , transform.position.y, transform.position.z),
                    Quaternion.identity, quarryTab);

            int ranItem = Random.Range( 0, 4 );
            switch (ranItem)
            {
                case 0:     //plastic
                    activateMine[c].GetComponent<Image>().color = new Color(155f / 255f, 1f, 155f/255f, 1f);
                    activateMine[c].transform.GetChild(1).GetComponent<Text>().text = "plastic";
                    break;
                case 1:     //coal
                    activateMine[c].GetComponent<Image>().color = new Color(135 / 255f, 135f / 255f, 135f/255f, 1f);
                    activateMine[c].transform.GetChild(1).GetComponent<Text>().text = "coal";
                    break;
                case 2:     //steel
                    activateMine[c].GetComponent<Image>().color = new Color(146f/255f, 219f/255f, 1f,1f);
                    activateMine[c].transform.GetChild(1).GetComponent<Text>().text = "steel";
                    break;
                case 3:     //gold
                    activateMine[c].GetComponent<Image>().color = new Color(1f, 1f, 155f/255f, 1f);
                    activateMine[c].transform.GetChild(1).GetComponent<Text>().text = "gold";
                    break;
            }

        }

        foreach (GameObject mine in activateMine) { if (mine != null) { mine.GetComponent<Button>().interactable = false; } }

    }

    /// <summary>
    /// Sets turntype and turns off the scavenge button pressed.
    /// </summary>
    public void TurnScavenge()
    {
        //turn off the button pressed
        if (EventSystem.current.currentSelectedGameObject.name == "scavengeOtherButton")
        {
            turnActions[6].GetComponent<Button>().interactable = false;
        }
        else
        {
            turnActions[4].GetComponent<Button>().interactable = false;

        }

        foreach (GameObject button in activateScavenge) { button.SetActive(true); }

        GlobalScript.i.actionType = "scavenge";
    }

    /// <summary>
    /// Returns whatever type of resource the player selects in the Scavenge menu
    /// </summary>
    public void ResolveScavenge(string resource)
    {
        foreach (GameObject button in activateScavenge) { button.SetActive(false); }
        if(GlobalScript.i.actionType == "scavenge")
        {
            switch (resource)
            {
                case "gold":
                    currentPlayer.gold++;
                    break;
                case "steel":
                    currentPlayer.steel++;
                    break;
                case "coal":
                    currentPlayer.coal++;
                    break;
                case "plastic":
                    currentPlayer.plastic++;
                    break;
                default:
                    Debug.Log("Check your spelling, yo.");
                    break;
            }
            
            // First update it in the player objects
            UpdateResources();
            // Reflect it on the game state
            gameObject.GetComponent<GameplaySceneHandler>().UpdateResources();


            // Turn check should pass it to the next round immediately.
            TurnCheck();
            actionMenu.GetComponent<Animator>().Play("ScavengeOff");
        }
    }

    /// <summary>
    /// Sets the gameplay scene for Upgrading
    /// </summary>
    public void TurnUpgrade()
    {
        animHelper.TogglePass(true);

        turnActions[2].GetComponent<Button>().interactable = false;

        if (GlobalScript.i.turn == 0)
        {
            wildResource.interactable = true;
        }

        resTray.SetActive(true);
        buildButton.interactable = false;
        
        GlobalScript.i.actionType = "upgrade";
    }

    /// <summary>
    /// Sets the bool array element to true and builds the main player's thingy.
    /// </summary>
    public void ShopUpgrade()
    {
        if ( GlobalScript.i.actionType == "upgrade" )
        {
            currentPlayer.checkParts[partHelper.partNum] = true;
            partHelper.BuildABot();
            UpdateResources();
        }
    }
    
    /// <summary>
    /// Does clean up for the end of the turn.
    /// </summary>
    public void Pass()
    {
        if (GlobalScript.i.actionType == "upgrade")
        {
            resTray.SetActive(false);
        }
        else {
            UpdateAttack();
            UpdateHealth();
            TurnCheck();
        }
    }


//helper functions
    /// <summary>
    /// Sets attack to zero and then builds it up again
    /// </summary>
    public void UpdateAttack()
    {
        foreach(Player robot in GlobalScript.i.roster)
        {
            robot.attack = 0;
            if ( robot.checkParts[2] )  robot.attack++;
            if (robot.checkParts[4]) robot.attack++;
            if (robot.checkParts[8]) robot.attack++;
            if (robot.checkParts[10]) robot.attack++;

        }
        attackText.GetComponent<Text>().text = GlobalScript.i.roster[0].attack.ToString();
    }

    /// <summary>
    /// Updates the current player then changes the text displays
    /// </summary>
    public void UpdateHealth()
    {
        // currentPlayer is (GlobalScript.i.round - 1 + GlobalScript.i.phase + GlobalScript.i.turn) % GlobalScript.i.roster.Count
        GlobalScript.i.roster[(GlobalScript.i.round - 1 + GlobalScript.i.phase + GlobalScript.i.turn) % GlobalScript.i.roster.Count].health = currentPlayer.health;
        
        //Updates the new health values in the fight window
        int c = 0;
        foreach (Player robot in GlobalScript.i.roster)
        {
            if (robot.health <= 0) { robot.broken = true; robot.health = 0; }
            if (robot.health >= 20) { robot.health = 20; }
            if (robot.roboType != GlobalScript.i.roster[0].roboType) {
                activateFight[c-1].transform.GetChild(2).GetComponent<Text>().text = robot.health.ToString();
            }
            c++;
        }

        playerHealth.transform.GetChild(0).GetComponent<Slider>().value = GlobalScript.i.roster[0].health;
        playerHealth.transform.GetChild(2).GetComponent<Text>().text = GlobalScript.i.roster[0].health.ToString();
    }

    /// <summary>
    /// Imports current materials and cards
    /// </summary>
    public void UpdateResources()
    {
        foreach (Player robot in GlobalScript.i.roster) {
            if (robot.roboType == currentPlayer.roboType)
            {
                robot.gold = currentPlayer.gold;
                robot.steel = currentPlayer.steel;
                robot.plastic = currentPlayer.plastic;
                robot.coal = currentPlayer.coal;

                int c = 0;
                foreach (int part in currentPlayer.checkCards)
                {
                    robot.checkCards[c] = part;
                    c++;
                }
            }
        }

    }

}