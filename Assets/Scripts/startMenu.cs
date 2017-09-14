using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

public class startMenu : MonoBehaviour {

    public Button newButton;
    public Button loadButton;
    public Button creditsButton;
    public Button settingButton;
    public Button backButton;

    public GameObject CreditMenu;
    public GameObject LoadMenu;
    //New file menu, three stacked
    public GameObject saveSelect;
    public GameObject[] saveFile1; //only need an array of 6 for this
    public GameObject[] saveFile2; //only need an array of 6 for this
    public GameObject[] saveFile3; //only need an array of 6 for this
    //The reference for wrting the load buttons with a 15 length array
    public GameObject[] loadFileInfo1;
    public GameObject[] loadFileInfo2;
    public GameObject[] loadFileInfo3;

    public Sprite[] namePlates;
    public Sprite[] onlyRobot;

    // Use this for initialization
    void Start () {

        //Button initializing
        newButton = newButton.GetComponent<Button> ();
        loadButton = loadButton.GetComponent<Button>();
        creditsButton = creditsButton.GetComponent<Button>();
        settingButton = settingButton.GetComponent<Button>();
        backButton = backButton.GetComponent<Button>();

        //menu initializing
        CreditMenu.SetActive(false);
        
        LoadMenu.SetActive(false);

        //save file selection panel is invisible until you hit new. Then it always is I guess.
        saveSelect.SetActive(false);
    }

    //It will open the save file window and read out information to the file GUI
    public void NewPress()
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Array of the loadFileInfo objects by numbers:                                                   //
        //  0: *use the name of the object for save file reference*                                         //
        //  1: activePlayer - Image file to preview who is the active player                                //
        //  2: numberOfPlayers - The total number of players                                                //
        //  3: health# - The health value of the main player                                                //
        //  4: attack# - The attack value of the main player                                                //
        //  5: timePlayed - the time value of the last time you played                                      //
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        GameObject[] saveHolder = saveFile1;
        for (int i = 0; i <= 2; i++)
        {
            if (i == 0) {   saveHolder = saveFile1; }
            if (i == 1) {   saveHolder = saveFile2; }
            if (i == 2) {   saveHolder = saveFile3; }
            if (File.Exists(Application.persistentDataPath + "/" + saveHolder[0].name + ".dat"))
            {

                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.OpenRead(Application.persistentDataPath + "/" + saveHolder[0].name + ".dat");
                PlayerData data = (PlayerData)bf.Deserialize(file);

                //take out the data and put it into the local variables
                if (data.roboType[0].Equals("ConstructorRobodyUI")) { saveHolder[1].GetComponent<Image>().sprite = onlyRobot[0]; }
                if (data.roboType[0].Equals("EsquireRobodyUI")) { saveHolder[1].GetComponent<Image>().sprite = onlyRobot[1]; }
                if (data.roboType[0].Equals("ExecutionerRobodyUI")) { saveHolder[1].GetComponent<Image>().sprite = onlyRobot[2]; }
                if (data.roboType[0].Equals("GuruRobodyUI")) { saveHolder[1].GetComponent<Image>().sprite = onlyRobot[3]; }
                if (data.roboType[0].Equals("ReforgeRobodyUI")) { saveHolder[1].GetComponent<Image>().sprite = onlyRobot[4]; }
                if (data.roboType[0].Equals("SyphonRobodyUI")) { saveHolder[1].GetComponent<Image>().sprite = onlyRobot[5]; }

                int c;
                for (c = 0; c<6; c++) { if (data.roboType[c] == null) { break; } }
                saveHolder[2].GetComponent<Text>().text = c.ToString();
                saveHolder[3].GetComponent<Text>().text = data.health[0].ToString();
                saveHolder[4].GetComponent<Text>().text = data.attack[0].ToString();
                saveHolder[5].GetComponent<Text>().text = data.lastPlayed;

                file.Close();
            }
        }
        saveSelect.SetActive(true);

    }

    //By "SaveFile" it means start a new game under the new file. 
    public void SaveFilePress()
    {
        GlobalScript.i.saveFile = EventSystem.current.currentSelectedGameObject.name;
        
        //clear out robodyHandler variables
        GlobalScript.i.Initialize();

        //loads the mentioned scene. The "single" mode closes all other scenes.
        SceneManager.LoadScene("ModeMenu", LoadSceneMode.Single);
    }

    // The button pressed here activates the load file menu, not loading an actual game. That would be LoadFilePress(). Fool.
    public void LoadPress()
    {

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Array of the loadFileInfo objects by numbers:                                                   //
        //  0: playerName - Text object of player's robot's name                                            //
        //  1-4: plasticText, steelText, coalText, goldText - Text objects referencing a number             //
        //  5: built - Text object of the number of pieces built in the robot                               //
        //  6-7: healthText and attackText - Text object that references the player's health and attack     //
        //  8-12: challenger1-5 - Image objects that hold the opposing robot's title plaques                //
        //  13: playerRobot - Image object of the player's robot a la "only*something*"                     //
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        WriteLoadButtons(loadFileInfo1);
        WriteLoadButtons(loadFileInfo2);
        WriteLoadButtons(loadFileInfo3);

        //reveal the credit pop up
        LoadMenu.SetActive(true);

        //turn off the start menu buttons
        newButton.enabled = false;
        loadButton.enabled = false;
        creditsButton.enabled = false;
        settingButton.enabled = false;

    }

    //By "LoadFile" it means start a new game under the selected file information. 
    public void LoadFilePress()
    {
        //selecting the object in here in case I want to load level from somewhere not in the start menu
        GlobalScript.i.saveFile = EventSystem.current.currentSelectedGameObject.name;

        GlobalScript.i.Load();
    }

    //For the GUI white out for the load menu
    void WriteLoadButtons(GameObject[] loadGUI) //probaly schould be in Global Script but I don't know how to add classes and shit during runtime
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Array of the loadFileInfo objects by numbers:                                                   //
        //  0: playerName - Text object of player's robot's name                                            //
        //  1-4: plasticText, steelText, coalText, goldText - Text objects referencing a number             //
        //  5: built - Text object of the number of pieces built in the robot                               //
        //  6-7: healthText and attackText - Text object that references the player's health and attack     //
        //  8-12: challenger1-5 - Image objects that hold the opposing robot's title plaques                //
        //  13: playerRobot - Image object of the player's robot a la "only*something*"                     //
        //////////////////////////////////////////////////////////////////////////////////////////////////////

        if (File.Exists(Application.persistentDataPath + "/" + loadGUI[14].name + ".dat"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "/" + loadGUI[14].name + ".dat");
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            loadGUI[1].GetComponent<Text>().text = data.plastic[0].ToString();
            loadGUI[2].GetComponent<Text>().text = data.steel[0].ToString();
            loadGUI[3].GetComponent<Text>().text = data.coal[0].ToString();
            loadGUI[4].GetComponent<Text>().text = data.gold[0].ToString();
            
            //it runs through every element checking if parts are built or not.
            int count = 0;
            for (int i = 0; i<=10; i++) { if (data.checkParts[0][i]) { count++; } }
            loadGUI[5].GetComponent<Text>().text = count.ToString();

            loadGUI[6].GetComponent<Text>().text = data.health[0].ToString();
            loadGUI[7].GetComponent<Text>().text = data.attack[0].ToString();

            //deals with 
            count = 8;
            foreach (string robot in data.roboType)
            {
                if (robot == null) { break; }
                else if (robot.Equals(data.roboType[0]))
                {
                    if (robot.Equals("ConstructorRobodyUI"))    //robot references the names (strings) in the player array
                    {
                        loadGUI[13].GetComponent<Image>().sprite = onlyRobot[0];
                        loadGUI[0].GetComponent<Text>().text = "Dr. Constructor";
                    }
                    if (robot.Equals("EsquireRobodyUI"))
                    {
                        loadGUI[13].GetComponent<Image>().sprite = onlyRobot[1];
                        loadGUI[0].GetComponent<Text>().text = "Extractor Esquire";
                    }
                    if (robot.Equals("ExecutionerRobodyUI"))
                    {
                        loadGUI[13].GetComponent<Image>().sprite = onlyRobot[2];
                        loadGUI[0].GetComponent<Text>().text = "The Executioner";
                    }
                    if (robot.Equals("GuruRobodyUI"))
                    {
                        loadGUI[13].GetComponent<Image>().sprite = onlyRobot[3];
                        loadGUI[0].GetComponent<Text>().text = "Gizmo Guru";

                    }
                    if (robot.Equals("ReforgeRobodyUI"))
                    {
                        loadGUI[13].GetComponent<Image>().sprite = onlyRobot[4];
                        loadGUI[0].GetComponent<Text>().text = "Lord Reforge";

                    }
                    if (robot.Equals("SyphonRobodyUI"))
                    {
                        loadGUI[13].GetComponent<Image>().sprite = onlyRobot[5];
                        loadGUI[0].GetComponent<Text>().text = "Professor Syphon";
                    }
                    continue;
                }
                else if (robot.Equals("ConstructorRobodyUI"))    //robot references the names (strings) in the player array
                {
                    loadGUI[count].GetComponent<Image>().sprite = namePlates[0];
                }
                else if (robot.Equals("EsquireRobodyUI"))
                {
                    loadGUI[count].GetComponent<Image>().sprite = namePlates[1];
                }
                else if (robot.Equals("ExecutionerRobodyUI"))
                {
                    loadGUI[count].GetComponent<Image>().sprite = namePlates[2];
                }
                else if (robot.Equals("GuruRobodyUI"))
                {
                    loadGUI[count].GetComponent<Image>().sprite = namePlates[3];
                }
                else if (robot.Equals("ReforgeRobodyUI"))
                {
                    loadGUI[count].GetComponent<Image>().sprite = namePlates[4];

                }
                else if (robot.Equals("SyphonRobodyUI"))
                {
                    loadGUI[count].GetComponent<Image>().sprite = namePlates[5];
                }

                count++;
            }

        }
        else
        {
            for(int i = 0; i<=14; i++)
            {
                if (i <= 7)
                {
                    loadGUI[i].GetComponent<Text>().text = "-";
                }
                else if (i >7 && i < 13)
                {
                    loadGUI[i].GetComponent<Image>().sprite = namePlates[6];
                }
                else if (i == 13)
                {
                    loadGUI[13].GetComponent<Image>().sprite = onlyRobot[6];
                }
                else { }
            }
        }
    }

    // When pressing the credits button. Pretty much just text.
    public void CreditPress() {
       
        //reveal the credit pop up
        CreditMenu.SetActive(true);

        //turn off the start menu buttons
        newButton.enabled = false;
        loadButton.enabled = false;
        creditsButton.enabled = false;
        settingButton.enabled = false;

    }

    //when pressing the back button
    public void BackPress()
    {

        //turn off the credit pop up
        CreditMenu.SetActive(false);
        LoadMenu.SetActive(false);

        //turn on the start menu buttons
        newButton.enabled = true;
        loadButton.enabled = true;
        creditsButton.enabled = true;
        settingButton.enabled = true;

    }

}
