using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class playCard : MonoBehaviour {

    //confirm and deny buttons
    public Button useButton;
    public Button cancelButton;
    
    /// <summary>
    /// Reference to each of the 8 types of cards: chainsaw, forceField, stickyHands, weldersKit, shovel, repairKit, rocketPunch, twinBlasters
    /// </summary>
    public Button[] bank;
        
    //the empty card view port
    public Sprite[] cardType;
    public Image cardDisplay;
    public Text cardTitle;
    public Text cardRules;
    public Text cardTips;
    public Text cardClarify;

    //card effect bank
    public GameObject[] shovelBut = new GameObject[4];

    //this is the menu to be enabled, currently referenced through the GameboardBG
    public GameObject CardAbility;

    //references to other scripts
    public RoundController helpRC;
    
    // Use this for initialization
    void Start()
    {

        //starting up the cancel buttons
        cancelButton = cancelButton.GetComponent<Button>();
        useButton = useButton.GetComponent<Button>();
        foreach (GameObject res in shovelBut)
        {
            res.SetActive(false);
        }

        cardDisplay = cardDisplay.GetComponent<Image>();

        //initializing canvas
        CardAbility.SetActive(false);

        //turn on/off all the card link buttons where appropriate
        ActivateCards();
    }

    // The cancel function is handled by the gamplay script under the like named Canvas

    /// <summary>
    /// When pressing the cancel button
    /// </summary>
    public void BackPress()
    {
        //turn on the main suite of buttons
        ActivateCards();

        //turn off the card details pop up
        CardAbility.SetActive(false);
    }

    /// <summary>
    /// When pressing the card button in the tray
    /// </summary>
    public void CardButtonPress()
    {
        //hold the name of the cardButton
        string buttonName = EventSystem.current.currentSelectedGameObject.name;

        //prototype foreach loop
        string[] cardName = new string[] { "cardChainsaw", "cardForceField", "cardStickyHands",
            "cardWeldersKit", "cardRepairKit", "cardRocketPunch",
            "cardShovel" , "cardTwinBlasters" , "cardBack"};
        string[] cardText1 = new string[] { "Chainsaw", "Force Field", "Sticky Hands",
            "Welders Kit", "Repair Kit", "Rocket Punch",
            "Shovel" , "Twin Blasters" , "Mystery Card"};
        //references the rules
        string[] cardText2 = new string[] { "Doubles the attack value during a Fight",
            "Halves the damage dealt from an attack",
            "Takes the Coal Heart from either another player or from the bank. Then, use the Coal Heart affect as if you chose Capture",
            "Use this Deluxe Gear to build a robot piece for free",
            "Gain 2 health. Only playable on your turn.",
            "Upon attacking an opponent you can take one of their resources of your choice",
            "Take a resource of your choice from the supply" ,
            "Add 2 to your attack value fo this Fight" ,
            "Oh no!" };
        //references the tips and stuff
        string[] cardText3 = new string[] { "Chainsaw always applies after any other attack value increases from other gear or parts.",
            "Force Field is only playable when you are the target of an attack.",
            "Professor Syphon has many synergies with the Coal Heart. Using Sticky Hands while you already have the Coal Heart will tigger the second tier effect.",
            "Chest and Head of robots tend to be the most powerful and the most expensive. Choose wisely.",
            "Repair Kit cannot be used in reaction to an attack.",
            "Taking a resource your opponents may need could be mor useful than taking one you need.",
            "Lord Reforge's abilities work heavily off taking resources from the supply.",
            "This number adds onto the base of arm and foot bonuses." ,
            "You missed a reference" };
        string[] cardText4 = new string[] { "Can only be used when you attack",
            "Can only to be used when attacked",
            "Can only be used on your turn",
            "Can only be used on your turn",
            "Can only be used on your turn",
            "Can only be used when you attack",
            "Can only be used on your turn",
            "Can only be used when you attack",
            "cardBack" };

        int count = 0;

        //checks every card name (cardName[]) against the name of the button pressed (buttonName).
        foreach (string card in cardName)
        {
            if (card.Equals(buttonName))
            {
                //diplay the card pressed
                cardDisplay.GetComponent<Image>().sprite = cardType[count];
                //display the card title
                cardTitle.GetComponent<Text>().text     = cardText1[count];
                //change the text for the rules
                cardRules.GetComponent<Text>().text     = cardText2[count];
                //change the text for the tips and tricks section
                cardTips.GetComponent<Text>().text      = cardText3[count];
                //To clarify when to use the card
                cardClarify.GetComponent<Text>().text   = cardText4[count];
            }
            count++;
        }
        
        //turn on the card details pop up
        CardAbility.SetActive(true);

        useButton.interactable = false;

        // Can the player use the card right now?
        switch (cardTitle.GetComponent<Text>().text)
        {
            case "Chainsaw":
                if (GlobalScript.i.actionType == "fight" && helpRC.currentPlayer == GlobalScript.i.roster[0]) { useButton.interactable = true; }
                useButton.transform.GetChild(0).GetComponent<Text>().text = "x2 Attack";
                break;
            case "Force Field":
                if (GlobalScript.i.actionType == "fight" && helpRC.currentPlayer != GlobalScript.i.roster[0]) { useButton.interactable = true; }
                useButton.transform.GetChild(0).GetComponent<Text>().text = "Block 1/2";
                break;
            case "Sticky Hands":
                if (helpRC.currentPlayer == GlobalScript.i.roster[0]) { useButton.interactable = true; }
                useButton.transform.GetChild(0).GetComponent<Text>().text = "Steal Heart";
                break;
            case "Welders Kit":
                if (helpRC.currentPlayer == GlobalScript.i.roster[0]) { useButton.interactable = true; }
                useButton.transform.GetChild(0).GetComponent<Text>().text = "Build Part";
                break;
            case "Repair Kit":
                if (helpRC.currentPlayer == GlobalScript.i.roster[0]) { useButton.interactable = true; }
                useButton.transform.GetChild(0).GetComponent<Text>().text = "+2 Health";
                break;
            case "Rocket Punch":
                if (GlobalScript.i.actionType == "fight" && helpRC.currentPlayer == GlobalScript.i.roster[0]) { useButton.interactable = true; }
                useButton.transform.GetChild(0).GetComponent<Text>().text = "Steal 1 Resource";
                break;
            case "Shovel":
                if (helpRC.currentPlayer == GlobalScript.i.roster[0]) { useButton.interactable = true; }
                useButton.transform.GetChild(0).GetComponent<Text>().text = "+1 Resource";
                break;
            case "Twin Blasters":
                if (GlobalScript.i.actionType == "fight" )
                { useButton.interactable = true; }
                useButton.transform.GetChild(0).GetComponent<Text>().text = "+2 Attack";
                break;
            case "Mystery Card":
                break;
            default:
                break;
        }

        //turn off the main suite of buttons
        DisableCardButtons();
    }

    /// <summary>
    /// Nested in the Use button. Points the program to a specific effect
    /// </summary>
    public void UseCard()
    {
        //turn off the card details pop up
        CardAbility.SetActive(false);
        string option = cardTitle.GetComponent<Text>().text;

        switch (option)
        {
            case "Chainsaw":
                if (GlobalScript.i.actionType == "fight") { GoChainsaw(); }
                break;
            case "Force Field":
                if (GlobalScript.i.actionType == "fight") { GoForcefield(); }
                break;
            case "Sticky Hands":
                GoSticky();
                break;
            case "Welders Kit":
                GoWelder();
                break;
            case "Repair Kit":
                GoRepair();
                break;
            case "Rocket Punch":
                if (GlobalScript.i.actionType == "fight") { GoPunch(); }
                break;
            case "Shovel":
                GoShovel();
                break;
            case "Twin Blasters":
                if (GlobalScript.i.actionType == "fight") { GoBlaster(); }
                break;
            case "Mystery Card":
                break;
            default:
                break;
        }

        //turn on the main suite of buttons
        ActivateCards();
        //autosave before closing, and soon to be before using the card (Save should be called after resolving the effects of the card)
        GlobalScript.i.Save();

    }

    /// <summary>
    /// Does the chainsaw effect: x2 to total attack after all modifiers
    /// </summary>
    void GoChainsaw()
    {
        useButton.transform.GetChild(0).GetComponent<Text>().text = "*chainsaw noises*";
        helpRC.multiAtt *= 2;
    }

    /// <summary>
    /// Does the Forcefield effect: 1/2 total attack after all modifiers
    /// </summary>
    void GoForcefield()
    {
        useButton.transform.GetChild(0).GetComponent<Text>().text = "Shields Actived";
        helpRC.multiAtt /= 2;
    }

    /// <summary>
    /// Does the Sticky Hands effect: Takes the coal heart and does the coal heart effect
    /// </summary>
    void GoSticky()
    {
        useButton.transform.GetChild(0).GetComponent<Text>().text = "Yoink!";
        if (GlobalScript.i.turn == 0)
        {
            if (helpRC.currentPlayer.holdHeart && !helpRC.currentPlayer.broken)
            {
                foreach (Player robot in GlobalScript.i.roster)
                {
                    if (robot.roboType != helpRC.currentPlayer.roboType)
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
            else if (helpRC.currentPlayer.broken)
            {
                helpRC.currentPlayer.broken = false;
                helpRC.currentPlayer.health = 5;
            }
            else
            {
                // Gain 2 health

                // Hit every other robot for 1
                foreach (Player robot in GlobalScript.i.roster)
                {
                    if (robot.roboType != helpRC.currentPlayer.roboType)
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
                if (helpRC.currentPlayer == robot)
                { robot.holdHeart = true; }
            }
            helpRC.currentPlayer.holdHeart = true;
        }
        helpRC.UpdateHealth();
    }

    /// <summary>
    /// Does the Welder's Kit effect: Build one part for free
    /// </summary>
    void GoWelder()
    {
        useButton.transform.GetChild(0).GetComponent<Text>().text = "Warming spot welder...";
    }

    /// <summary>
    /// Does the Repair Kit effect: +2 health
    /// </summary>
    void GoRepair()
    {
        useButton.transform.GetChild(0).GetComponent<Text>().text = "Putting blood back";
        if (helpRC.currentPlayer.health != 0) { helpRC.currentPlayer.health += 2; }

        helpRC.UpdateHealth();
    }

    /// <summary>
    /// Does the Rocket Punch effect: steals 1 resource on attack
    /// </summary>
    void GoPunch()
    {
        useButton.transform.GetChild(0).GetComponent<Text>().text = "Get over there!";
    }

    /// <summary>
    /// Does the Shovel effect: gain 1 resource of your choice (like scavenge)
    /// </summary>
    void GoShovel()
    {
        useButton.transform.GetChild(0).GetComponent<Text>().text = "Diggin' for Gold";

        // turns the resource tray into buttons
        foreach(GameObject res in shovelBut)
        {
            res.SetActive(true);
        }
    }

    /// <summary>
    /// turning off the tray and adding the resource
    /// </summary>
    public void EndShovel( string resource)
    {
        // turns the buttons off
        foreach (GameObject res in shovelBut)
        {
            res.SetActive(false);
        }

        switch (resource) {
            case "coal":
                helpRC.currentPlayer.coal++;
                break;
            case "plastic":
                helpRC.currentPlayer.plastic++;
                break;
            case "steel":
                helpRC.currentPlayer.steel++;
                break;
            case "gold":
                helpRC.currentPlayer.gold++;
                break;
            default:
                Debug.Log("But why?");
                break;
        }

        helpRC.UpdateResources();
    }

    /// <summary>
    /// Does the Twin Blaster effect: +2 to attack
    /// </summary>
    void GoBlaster()
    {
        helpRC.attBonus += 2;
    }

    /// <summary>
    /// Renders the tray of card buttons as interactable or not based on roster.cardCheck[x]
    /// </summary>
    public void ActivateCards()
    {
        int c = 0;
        foreach (Button card in bank)
        {
            if (GlobalScript.i.roster[0].checkCards[c] > 0) {
                card.interactable = true;
            }
            else { card.interactable = false; }
            c++;
        }
    }

    /// <summary>
    /// Turns off the card tray buttons
    /// </summary>
    public void DisableCardButtons()
    {
        foreach(Button card in bank)
        {
            card.interactable = false;
        }
    }
}
