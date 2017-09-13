using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;


/// <summary>
/// What it does:
/// Creates the opponent tray at the top of the screen by creating a GUI Box and instantiating buttons with the player icons
/// Also animations probably.
/// 
/// Held in the opponentPanel game object in the GameplayMG canvas in the Gameplay scene
/// </summary>

public class GameplaySceneHandler : MonoBehaviour
{

    //activate the buttons that will open the opponent stat viewport
    public List<Button> oppBut;
    public Sprite[] headRobot;
    Sprite robotSprite;
    public GameObject oppTemplate;
    public List<GameObject> oppList;

    //Activate the viewpart that the opponent's stats are in
    public Transform panel;

    //for the off click function
    public GameObject partMenu;
    public GameObject cardMenu;
    public GameObject helpMenu;
    public GameObject oppMenu;
    public GameObject bgClick;

    // resource text assets
    public GameObject plasticText;
    public GameObject coalText;
    public GameObject steelText;
    public GameObject goldText;

    // Draws the player menu
    private void Start()
    {
        if (gameObject.name == "opponentPanel") { BuildRoster(); }

        //initialize resources based on load file
        UpdateResources();

        bgClick.SetActive(false);

    }

    void Update()
    {
        UpdateResources();
    }

    // Populates the opponent roster at the top of the screen.
    void BuildRoster()
    {
        int count = 0;

        //checking each string to add a button
        foreach (Player robot in GlobalScript.i.roster)
        {
            oppList.Add(oppTemplate);
            if (robot.roboType.Equals("ConstructorRobodyUI"))
            {
                robotSprite = headRobot[0];
            }
            if (robot.roboType.Equals("EsquireRobodyUI"))
            {
                robotSprite = headRobot[1];
            }
            if (robot.roboType.Equals("ExecutionerRobodyUI"))
            {
                robotSprite = headRobot[2];
            }
            if (robot.roboType.Equals("GuruRobodyUI"))
            {
                robotSprite = headRobot[3];
            }
            if (robot.roboType.Equals("ReforgeRobodyUI"))
            {
                robotSprite = headRobot[4];
            }
            if (robot.roboType.Equals("SyphonRobodyUI"))
            {
                robotSprite = headRobot[5];
            }

            if (count <= 5)
            {
                oppList[count] = (GameObject)Instantiate(oppTemplate,
                    new Vector3(transform.position.x + count, transform.position.y, transform.position.z),
                    Quaternion.identity, panel);
                oppList[count].GetComponent<AI>().roboName = "player";
            }
            else { break; }

            //Put the new buttons under the panel object
            oppList[count].transform.SetParent(panel);
            //Change the button reference to the sprite checked on above
            oppList[count].transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = robotSprite;
            oppList[count].GetComponent<OppResource>().playerNum = count;

            /////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////

            if (gameObject.GetComponent<RoundController>().oppHelper.Count <= count)
            {
                gameObject.GetComponent<RoundController>().oppHelper.Add(oppList[count].GetComponent<OppResource>());
            }
            else
            {
                gameObject.GetComponent<RoundController>().oppHelper[count] = oppList[count].GetComponent<OppResource>();
            }

            count++;
        }
        count = 1;
        //set the order appropriate for the round
        while(GlobalScript.i.round > count)
        {
            gameObject.transform.GetChild(0).SetAsLastSibling();
            count++;
        }
    }

    //Call it to change the readout.
    public void UpdateResources()
    {
        plasticText.GetComponent<Text>().text = GlobalScript.i.roster[0].plastic.ToString();
        coalText.GetComponent<Text>().text = GlobalScript.i.roster[0].coal.ToString();
        steelText.GetComponent<Text>().text = GlobalScript.i.roster[0].steel.ToString();
        goldText.GetComponent<Text>().text = GlobalScript.i.roster[0].gold.ToString();
    }

    // If you click the big invisible wall it closes the open menu
    public void OffClick()
    {
        partMenu.SetActive(false);
        cardMenu.SetActive(false);
        helpMenu.SetActive(false);
        oppMenu.SetActive(false);

        bgClick.SetActive(false);
    }

    // Creates a transparent wall between the foreground and midground
    public void ClickCatcher()
    {
        bgClick.SetActive(true);
    }
}
