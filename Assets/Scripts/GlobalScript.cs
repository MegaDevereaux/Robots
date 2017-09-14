using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// This class MUST exist in every scene. It holds every value for every player. It uses them to save and load values.
/// 
/// Is a resource for StartMenu, ModeMenu, and Gameplay scenes.
/// Without it pretty much every scene will throw errors.
/// </summary>
public class GlobalScript : MonoBehaviour {

    public static GlobalScript i;
    public List<Player> roster = new List<Player>();

    //stuff not being saved
    public Color inactive;
    public Color active;
    public string saveFile = "";
    string temp = "";
    public string actionType = "";
    /// <summary>
    /// Round: Complete a series of phases until it comes around to the first player again. First player pushes to last, new round begins.
    /// </summary>
    public int round = 1; //Starts on round 1
    /// <summary>
    /// Phase: Player selects an action from the Round Menu, each other player repeats said action.
    /// </summary>
    public int phase = 0;
    /// <summary>
    /// Turn: When a player can change his board state. Primarily used during opponent's phases.
    /// </summary>
    public int turn = 0;

    // This is something called Singleton. It should keep only one instance of the object and keep it persistant across all scenes.
    void Awake () {
        if (!i){
            i = this;
            //make the object IMMORTAL
            DontDestroyOnLoad(gameObject);
        }
        else{   Destroy(gameObject);    }

    }
    
    //Here in case it opens in a scene when a game should be loaded
    void Start()
    {
        //test case for the Gameplay scene
        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            Load();
        }
    }

    //it save shit. What do you want from me? Pushy.
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/" + saveFile + ".dat");
        
        //the data variable is going to hold the content of the file
        PlayerData data = new PlayerData();

        //set local variables to the PlayerData object
        //take out the data and put it into the local variables
        data.actionType = GlobalScript.i.actionType;
        data.round = GlobalScript.i.round;
        data.phase = GlobalScript.i.phase;
        data.turn = GlobalScript.i.turn;

        int count = 0;
        foreach (Player robot in GlobalScript.i.roster)
        {
            data.roboType[count] = robot.roboType;
            data.health[count] = robot.health;
            data.attack[count] = robot.attack;
            data.plastic[count] = robot.plastic;
            data.steel[count] = robot.steel;
            data.coal[count] = robot.coal;
            data.gold[count] = robot.gold;
            data.checkParts[count] = robot.checkParts;
            data.checkCards[count] = robot.checkCards;
            data.holdHeart[count] = robot.holdHeart;

            count++;
        }

        data.lastPlayed = DateTime.Now.ToString();
        
        bf.Serialize(file, data);
        file.Close();
        
    }

    //loads from a serialized function held locally on the device. Will not work for web based games
    //fileName is to use multiple load files that the player can choose from
    public void Load()
    {
        Initialize();
        temp = GlobalScript.i.saveFile;

        if (File.Exists(Application.persistentDataPath + "/missingFile.dat")) { File.Delete(Application.persistentDataPath + "/missingFile.dat"); }
        if (File.Exists(Application.persistentDataPath + "/.dat")) { File.Delete(Application.persistentDataPath + "/.dat"); }
        

        if (File.Exists(Application.persistentDataPath + "/" + GlobalScript.i.saveFile + ".dat")) {
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + GlobalScript.i.saveFile + ".dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            //take out the data and put it into the local variables

            GlobalScript.i.actionType = data.actionType;
            GlobalScript.i.round = data.round;
            GlobalScript.i.phase = data.phase;
            GlobalScript.i.turn = data.turn;
            for (int c = 0; c <= 5; c++)
            {
                if (data.roboType[c] == null) { break; }
                if (data.roboType[c].Contains("RobodyUI"))
                {

                    GlobalScript.i.roster.Add(new Player());
                    GlobalScript.i.roster[c].roboType = data.roboType[c];
                    GlobalScript.i.roster[c].health = data.health[c];
                    GlobalScript.i.roster[c].attack = data.attack[c];
                    GlobalScript.i.roster[c].plastic = data.plastic[c];
                    GlobalScript.i.roster[c].steel = data.steel[c];
                    GlobalScript.i.roster[c].coal = data.coal[c];
                    GlobalScript.i.roster[c].gold = data.gold[c];
                    GlobalScript.i.roster[c].checkParts = data.checkParts[c];
                    GlobalScript.i.roster[c].checkCards = data.checkCards[c];
                    GlobalScript.i.roster[c].holdHeart = data.holdHeart[c];

                    //broken is dealt with in game for now
                    if (GlobalScript.i.roster[c].health <= 0) { GlobalScript.i.roster[c].broken = true; }
                    else { GlobalScript.i.roster[c].broken = false; }
                }
            }

            if (SceneManager.GetActiveScene().name != "Gameplay")
            {
                SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
            }
        }

        else { Debug.Log("file missing"); GlobalScript.i.saveFile = temp; }

    }

    public void Initialize()
    {
        //initialize the holders for future variables
        roster.Clear();
    }
    
}

[Serializable]
class PlayerData
{
    //handlers for the variables that need to be sheparded scene to scene.
    public string[] roboType = new string[6];
    public bool[][] checkParts = new bool[6][];
    public int[][] checkCards = new int[6][];

    public int[] health = new int[6];
    public int[] attack = new int[6];

    public int[] plastic = new int[6];
    public int[] coal = new int[6];
    public int[] steel = new int[6];
    public int[] gold = new int[6];

    public bool[] holdHeart = new bool[6];

    public string actionType;
    public int round;
    public int phase;
    public int turn;

    public string lastPlayed;

}

//holder for each player variable. Embarassing amount of time before I thought of this.
public class Player{

    public string roboType = "";
    /// <summary> [0] "head", [1] "rightShoulder", [2] "rightArm", [3] "leftShoulder", [4] "leftArm", [5] "chest", [6] "core", [7] "rightLeg", [8] "rightFoot", [9] "leftLeg", [10] "leftFoot" </summary>
    public bool[] checkParts = new bool[11];
    /// <summary> Counts number of cards in hand: [0] chainsaw, [1] force field, [2] sticky hands, [3] welders kit, [4] repair kit, [5] rocket punch, [6] shovel, [7] twin blasters, [8] card back</summary>
    public int[] checkCards = new int[9];

    public int attack;
    public int health;
    public bool broken;

    public int plastic;
    public int coal;
    public int steel;
    public int gold;

    public bool holdHeart;
}
