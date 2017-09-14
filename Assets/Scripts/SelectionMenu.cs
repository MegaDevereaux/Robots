using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/// <summary>
/// Selection Menu is held in GameboardBG in the ModeMenu scene.
/// 
/// Designed to handle the caracter slection for the game with the following functionality:
/// Minimum 3 players; maximum 6 players
/// First robot confirmed is the player's avatar to control
/// Further confirmed robots are listed out in order
/// Any robot confirmed cannot be later chosen
/// Until the Add Player button is selected the currently chosen robot may be changed
/// If Add Player is pressed and no robot selected it will trim the empty element from the roster variable in the GlobalScript
/// </summary>

public class SelectionMenu : MonoBehaviour {

    //initialize character select buttons. On click they change the preview robot. Press confirm to continue to gameplay
    public Button selectSy;
    public Button selectRe;
    public Button selectCon;
    public Button selectEsq;
    public Button selectGuru;
    public Button selectEx;
    Button buttonTurnOff;

    public Button selectContinue;
    public Button selectBack;
    public Button selectAddRobot;
    int playerIndex;

    //initialize the sprite bank
    public Sprite robCon;
    public Sprite robEsq;
    public Sprite robEx;
    public Sprite robGuru;
    public Sprite robRe;
    public Sprite robSy;

    //initializing the name plate at the top
    public Image namePlate;
    public Sprite namePlateCo;
    public Sprite namePlateEs;
    public Sprite namePlateEx;
    public Sprite namePlateGu;
    public Sprite namePlateRe;
    public Sprite namePlateSy;

    public GameObject robody;
    public List<GameObject> lineUp;
    public Sprite[] lineSprites;

    // Use this for initialization
    void Start()
    {

        selectRe = selectRe.GetComponent<Button>();
        selectEsq = selectEsq.GetComponent<Button>();
        selectGuru = selectGuru.GetComponent<Button>();
        //still need work
        selectCon = selectCon.GetComponent<Button>();
        selectEx = selectEx.GetComponent<Button>();
        selectSy = selectSy.GetComponent<Button>();

        //Gray out buttons I don't have the sprite maps finished for

        selectContinue = selectContinue.GetComponent<Button>();
        selectContinue.interactable = false;
        selectAddRobot.interactable = false;
        selectBack = selectBack.GetComponent<Button>();

        namePlate = namePlate.GetComponent<Image>();
        playerIndex = 0;

        //get that element 0 I need so much
        GlobalScript.i.roster.Add(new Player());
        GlobalScript.i.roster[playerIndex].attack = 0;
        GlobalScript.i.roster[playerIndex].health = 20;
        GlobalScript.i.roster[playerIndex].broken = false;
        GlobalScript.i.roster[playerIndex].coal = 0;
        GlobalScript.i.roster[playerIndex].plastic = 0;
        GlobalScript.i.roster[playerIndex].steel = 0;
        GlobalScript.i.roster[playerIndex].gold = 0;
        GlobalScript.i.roster[playerIndex].checkCards = new int[8];
        GlobalScript.i.roster[playerIndex].checkParts = new bool[11];
        GlobalScript.i.roster[playerIndex].holdHeart = false;

        //turn off all the buttons
        for (int i = 1; i <= 5; i++) { lineUp[i].SetActive(false); }

    }

    /// <summary>
    /// Back out to start menu
    /// </summary>
    public void PressBack()
    {
        GlobalScript.i.roster.Clear();
        //load start screen again
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

    /// <summary>
    /// Go to the Gameplay scene. Saves the load out. Minimum 3 players, maximum 6
    /// </summary>
    public void PressGo()
    {
        GlobalScript.i.roster.TrimExcess();

        if (GlobalScript.i.roster[playerIndex].roboType.Equals("")) {
            GlobalScript.i.roster.RemoveAt(playerIndex) ;
        }

        GlobalScript.i.Save();

        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);  //pass to the next scene

    }

    /// <summary>
    /// 
    /// </summary>
    public void PressHeroSelect()
    {
        //hold the name of the cardButton
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        
        if (buttonName == "maskDrCon")      //Doctor Constructor check
        {
            namePlate.GetComponent<Image>().sprite = namePlateCo;                   //changes name plate to correspond to the choice
            robody.GetComponent<SpriteRenderer>().sprite = robCon;                  //changes the large displayed robot with a static image
            buttonTurnOff = selectCon;
            
            GlobalScript.i.roster[playerIndex].roboType = "ConstructorRobodyUI";                        //adds to the List<Player> of enemies            
            lineUp[playerIndex].GetComponent<Image>().sprite = lineSprites[0];    //rosterSprite holds the head sprite, 0 being the index for Doctor Constructor
        }

        else if (buttonName == "maskEsq")    //Extractor Esquire check
        {
            namePlate.GetComponent<Image>().sprite = namePlateEs;
            robody.GetComponent<SpriteRenderer>().sprite = robEsq;
            buttonTurnOff = selectEsq;

            GlobalScript.i.roster[playerIndex].roboType = "EsquireRobodyUI";                        //adds to the List<Player> of enemies
            lineUp[playerIndex].GetComponent<Image>().sprite = lineSprites[1];
        }
        else if (buttonName == "maskExecut")    //Executioner check
        {
            namePlate.GetComponent<Image>().sprite = namePlateEx;
            robody.GetComponent<SpriteRenderer>().sprite = robEx;
            buttonTurnOff = selectEx;

            GlobalScript.i.roster[playerIndex].roboType = "ExecutionerRobodyUI";                        //adds to the List<Player> of enemies
            lineUp[playerIndex].GetComponent<Image>().sprite = lineSprites[2];
        }

        else if (buttonName == "maskGuru")      //Gizmo Guru check
        {
            namePlate.GetComponent<Image>().sprite = namePlateGu;
            robody.GetComponent<SpriteRenderer>().sprite = robGuru;
            buttonTurnOff = selectGuru;

            GlobalScript.i.roster[playerIndex].roboType = "GuruRobodyUI";                        //adds to the List<Player> of enemies
            lineUp[playerIndex].GetComponent<Image>().sprite = lineSprites[3];
        }

        else if (buttonName == "maskReforge")   //Lord Reforge check
        {
            namePlate.GetComponent<Image>().sprite = namePlateRe;
            robody.GetComponent<SpriteRenderer>().sprite = robRe;
            buttonTurnOff = selectRe;

            GlobalScript.i.roster[playerIndex].roboType = "ReforgeRobodyUI";                        //adds to the List<Player> of enemies
            lineUp[playerIndex].GetComponent<Image>().sprite = lineSprites[4];
        }

        else if (buttonName == "maskSyphon")    //Professor Syphon check
        {
            namePlate.GetComponent<Image>().sprite = namePlateSy;
            robody.GetComponent<SpriteRenderer>().sprite = robSy;
            buttonTurnOff = selectSy;

            GlobalScript.i.roster[playerIndex].roboType = "SyphonRobodyUI";                        //adds to the List<Player> of enemies
            lineUp[playerIndex].GetComponent<Image>().sprite = lineSprites[5];
        }
        
        lineUp[playerIndex].GetComponent<Image>().preserveAspect = true;
        lineUp[playerIndex].GetComponent<Image>().color = GlobalScript.i.active;

        //give starting resources based on chosen hero
        if(GlobalScript.i.roster[playerIndex].roboType == "ExecutionerRobodyUI"
            || GlobalScript.i.roster[playerIndex].roboType == "GuruRobodyUI"
            || GlobalScript.i.roster[playerIndex].roboType == "SyphonRobodyUI")
        { GlobalScript.i.roster[playerIndex].plastic = 1; }
        if (GlobalScript.i.roster[playerIndex].roboType == "EsquireRobodyUI"
            || GlobalScript.i.roster[playerIndex].roboType == "ReforgeRobodyUI"
            || GlobalScript.i.roster[playerIndex].roboType == "SyphonRobodyUI")
        { GlobalScript.i.roster[playerIndex].coal = 1; }
        if (GlobalScript.i.roster[playerIndex].roboType == "ConstructorRobodyUI"
            || GlobalScript.i.roster[playerIndex].roboType == "ReforgeRobodyUI"
            || GlobalScript.i.roster[playerIndex].roboType == "ExecutionerRobodyUI")
        { GlobalScript.i.roster[playerIndex].steel = 1; }
        if (GlobalScript.i.roster[playerIndex].roboType == "ConstructorRobodyUI"
            || GlobalScript.i.roster[playerIndex].roboType == "EsquireRobodyUI"
            || GlobalScript.i.roster[playerIndex].roboType == "GuruRobodyUI")
        { GlobalScript.i.roster[playerIndex].gold = 1; }

        //turn on the buttons
        selectAddRobot.interactable = true;
        if (playerIndex >= 2)
        {
            selectContinue.interactable = true;
        }



    }

    /// <summary>
    /// Minimum 3 players, maximum 6. Will trim when they progress
    /// </summary>
    public void AddEnemy()
    {
        //disable the buttons to not double up on robots
        buttonTurnOff.interactable = false;

        if (GlobalScript.i.roster[playerIndex].roboType == "")
        {
            Debug.Log("Cheeky monkey");
        }
        else
        {
            //changing the choosing method and pushing variables into 
            playerIndex++;
            if (playerIndex > 5) { playerIndex = 5; }

            lineUp[playerIndex].SetActive(true);
            GlobalScript.i.roster.Add(new Player());
            GlobalScript.i.roster[playerIndex].attack = 0;
            GlobalScript.i.roster[playerIndex].health = 20;
            GlobalScript.i.roster[playerIndex].broken = false;
            GlobalScript.i.roster[playerIndex].coal = 0;
            GlobalScript.i.roster[playerIndex].plastic = 0;
            GlobalScript.i.roster[playerIndex].steel = 0;
            GlobalScript.i.roster[playerIndex].gold = 0;
            GlobalScript.i.roster[playerIndex].checkCards = new int[8];
            GlobalScript.i.roster[playerIndex].checkParts = new bool[11];
            GlobalScript.i.roster[playerIndex].holdHeart = false;
        }
        
    }

    /// <summary>
    /// Will incorperate later.
    /// </summary>
    public void RemoveEnemy()
    {

    }

}
