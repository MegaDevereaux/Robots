using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// A simple resource dump to access in the objects it's tied to.
/// Currently only holding the following:
/// hex[]: an array of references to the hex images of each robot's specific parts
/// micro[]: an array used while displaying the hexes to show robot navagation
/// buttons[]: actually the part map of the robot shown in the main viewer. Irregular sizes
/// color: simple thematic changes depending on which player is chosen.
/// </summary>
public class RobodyArrayResource : MonoBehaviour {
    
    public string roboName = "";
    public Sprite[] hex;
    public Sprite[] micro;
    public Image[] buttons;

    public Color theme;

    public Part[] parts = new Part[11];

    public void Audit()
    {
        for(int c = 0; c < 11; c++) { parts[c] = new Part(); }
        
        switch (roboName)
        {
            case "ConstructorRobodyUI":
                parts[0].plastic = 0;  parts[0].coal = 1;  parts[0].steel = 2;  parts[0].gold = 0;  parts[0].partName = "head";     //
                parts[1].plastic = 1;  parts[1].coal = 0;  parts[1].steel = 0;  parts[1].gold = 2;  parts[1].partName = "shoulderR";//
                parts[2].plastic = 0;  parts[2].coal = 1;  parts[2].steel = 0;  parts[2].gold = 1;  parts[2].partName = "armR";     //
                parts[3].plastic = 1;  parts[3].coal = 1;  parts[3].steel = 0;  parts[3].gold = 0;  parts[3].partName = "shoulderL";//
                parts[4].plastic = 1;  parts[4].coal = 0;  parts[4].steel = 1;  parts[4].gold = 0;  parts[4].partName = "armL";     //
                parts[5].plastic = 0;  parts[5].coal = 0;  parts[5].steel = 2;  parts[5].gold = 2;  parts[5].partName = "chest";    //
                parts[6].plastic = 0;  parts[6].coal = 0;  parts[6].steel = 1;  parts[6].gold = 1;  parts[6].partName = "core";     //
                parts[7].plastic = 1;  parts[7].coal = 0;  parts[7].steel = 0;  parts[7].gold = 1;  parts[7].partName = "legR";     //
                parts[8].plastic = 0;  parts[8].coal = 0;  parts[8].steel = 0;  parts[8].gold = 1;  parts[8].partName = "footR";    //
                parts[9].plastic = 0;  parts[9].coal = 1;  parts[9].steel = 1;  parts[9].gold = 0;  parts[9].partName = "legL";     //
                parts[10].plastic = 0; parts[10].coal = 0; parts[10].steel = 1; parts[10].gold = 0; parts[10].partName = "footL";   //
                break;
            case "EsquireRobodyUI":
                parts[0].plastic = 1;  parts[0].coal = 0;  parts[0].steel = 1;  parts[0].gold = 1;  parts[0].partName = "head";     //
                parts[1].plastic = 0;  parts[1].coal = 2;  parts[1].steel = 0;  parts[1].gold = 1;  parts[1].partName = "shoulderR";//
                parts[2].plastic = 1;  parts[2].coal = 1;  parts[2].steel = 0;  parts[2].gold = 0;  parts[2].partName = "armR";     //
                parts[3].plastic = 1;  parts[3].coal = 0;  parts[3].steel = 0;  parts[3].gold = 1;  parts[3].partName = "shoulderL";//
                parts[4].plastic = 0;  parts[4].coal = 1;  parts[4].steel = 1;  parts[4].gold = 0;  parts[4].partName = "armL";     //
                parts[5].plastic = 0;  parts[5].coal = 2;  parts[5].steel = 0;  parts[5].gold = 2;  parts[5].partName = "chest";    //
                parts[6].plastic = 0;  parts[6].coal = 1;  parts[6].steel = 0;  parts[6].gold = 1;  parts[6].partName = "core";     //
                parts[7].plastic = 0;  parts[7].coal = 0;  parts[7].steel = 1;  parts[7].gold = 1;  parts[7].partName = "legR";     //
                parts[8].plastic = 0;  parts[8].coal = 0;  parts[8].steel = 0;  parts[8].gold = 1;  parts[8].partName = "footR";    //
                parts[9].plastic = 1;  parts[9].coal = 0;  parts[9].steel = 1;  parts[9].gold = 0;  parts[9].partName = "legL";     //
                parts[10].plastic = 0; parts[10].coal = 1; parts[10].steel = 0; parts[10].gold = 0; parts[10].partName = "footL";   //
                break;
            case "ExecutionerRobodyUI":
                parts[0].plastic = 1;  parts[0].coal = 1;  parts[0].steel = 1;  parts[0].gold = 0;  parts[0].partName = "head";     //
                parts[1].plastic = 0;  parts[1].coal = 0;  parts[1].steel = 1;  parts[1].gold = 1;  parts[1].partName = "shoulderR";//
                parts[2].plastic = 0;  parts[2].coal = 1;  parts[2].steel = 1;  parts[2].gold = 0;  parts[2].partName = "armR";     //
                parts[3].plastic = 1;  parts[3].coal = 1;  parts[3].steel = 0;  parts[3].gold = 0;  parts[3].partName = "shoulderL";//
                parts[4].plastic = 1;  parts[4].coal = 0;  parts[4].steel = 1;  parts[4].gold = 0;  parts[4].partName = "armL";     //
                parts[5].plastic = 2;  parts[5].coal = 0;  parts[5].steel = 2;  parts[5].gold = 0;  parts[5].partName = "chest";    //
                parts[6].plastic = 1;  parts[6].coal = 0;  parts[6].steel = 1;  parts[6].gold = 0;  parts[6].partName = "core";     //
                parts[7].plastic = 0;  parts[7].coal = 1;  parts[7].steel = 0;  parts[7].gold = 1;  parts[7].partName = "legR";     //
                parts[8].plastic = 1;  parts[8].coal = 0;  parts[8].steel = 0;  parts[8].gold = 0;  parts[8].partName = "footR";    //
                parts[9].plastic = 1;  parts[9].coal = 0;  parts[9].steel = 1;  parts[9].gold = 1;  parts[9].partName = "legL";     //
                parts[10].plastic = 0; parts[10].coal = 0; parts[10].steel = 1; parts[10].gold = 0; parts[10].partName = "footL";   //
                break;
            case "GuruRobodyUI":
                parts[0].plastic = 1;  parts[0].coal = 2;  parts[0].steel = 0;  parts[0].gold = 0;  parts[0].partName = "head";     //
                parts[1].plastic = 1;  parts[1].coal = 0;  parts[1].steel = 1;  parts[1].gold = 1;  parts[1].partName = "shoulderR";//
                parts[2].plastic = 1;  parts[2].coal = 0;  parts[2].steel = 1;  parts[2].gold = 0;  parts[2].partName = "armR";     //
                parts[3].plastic = 0;  parts[3].coal = 0;  parts[3].steel = 1;  parts[3].gold = 0;  parts[3].partName = "shoulderL";//
                parts[4].plastic = 0;  parts[4].coal = 1;  parts[4].steel = 0;  parts[4].gold = 1;  parts[4].partName = "armL";     //
                parts[5].plastic = 2;  parts[5].coal = 0;  parts[5].steel = 0;  parts[5].gold = 2;  parts[5].partName = "chest";    //
                parts[6].plastic = 1;  parts[6].coal = 0;  parts[6].steel = 0;  parts[6].gold = 1;  parts[6].partName = "core";     //
                parts[7].plastic = 1;  parts[7].coal = 1;  parts[7].steel = 0;  parts[7].gold = 1;  parts[7].partName = "legR";     //
                parts[8].plastic = 0;  parts[8].coal = 0;  parts[8].steel = 0;  parts[8].gold = 1;  parts[8].partName = "footR";    //
                parts[9].plastic = 0;  parts[9].coal = 0;  parts[9].steel = 1;  parts[9].gold = 1;  parts[9].partName = "legL";     //
                parts[10].plastic = 1; parts[10].coal = 0; parts[10].steel = 0; parts[10].gold = 0; parts[10].partName = "footL";   //
                break;
            case "ReforgeRobodyUI":
                parts[0].plastic = 0;  parts[0].coal = 1;  parts[0].steel = 2; parts[0].gold = 0;  parts[0].partName = "head";      //
                parts[1].plastic = 1;  parts[1].coal = 0;  parts[1].steel = 1;  parts[1].gold = 0;  parts[1].partName = "shoulderR";//
                parts[2].plastic = 1;  parts[2].coal = 1;  parts[2].steel = 0;  parts[2].gold = 0;  parts[2].partName = "armR";     //
                parts[3].plastic = 0;  parts[3].coal = 1;  parts[3].steel = 0;  parts[3].gold = 1;  parts[3].partName = "shoulderL";//
                parts[4].plastic = 1;  parts[4].coal = 0;  parts[4].steel = 0;  parts[4].gold = 1;  parts[4].partName = "armL";     //
                parts[5].plastic = 0;  parts[5].coal = 2;  parts[5].steel = 2;  parts[5].gold = 0;  parts[5].partName = "chest";    //
                parts[6].plastic = 0;  parts[6].coal = 1;  parts[6].steel = 1;  parts[6].gold = 0;  parts[6].partName = "core";     //
                parts[7].plastic = 0;  parts[7].coal = 0;  parts[7].steel = 1;  parts[7].gold = 1;  parts[7].partName = "legR";     //
                parts[8].plastic = 0;  parts[8].coal = 0;  parts[8].steel = 1;  parts[8].gold = 0;  parts[8].partName = "footR";    //
                parts[9].plastic = 1;  parts[9].coal = 1;  parts[9].steel = 0;  parts[9].gold = 1;  parts[9].partName = "legL";     //
                parts[10].plastic = 0; parts[10].coal = 1; parts[10].steel = 0; parts[10].gold = 0; parts[10].partName = "footL";   //
                break;
            case "SyphonRobodyUI":
                parts[0].plastic = 1;  parts[0].coal = 1;  parts[0].steel = 0;  parts[0].gold = 1;  parts[0].partName = "head";     //
                parts[1].plastic = 1;  parts[1].coal = 0;  parts[1].steel = 0;  parts[1].gold = 1;  parts[1].partName = "shoulderR";//
                parts[2].plastic = 1;  parts[2].coal = 0;  parts[2].steel = 0;  parts[2].gold = 1;  parts[2].partName = "armR";     //
                parts[3].plastic = 1;  parts[3].coal = 0;  parts[3].steel = 1;  parts[3].gold = 0;  parts[3].partName = "shoulderL";//
                parts[4].plastic = 0;  parts[4].coal = 1;  parts[4].steel = 1;  parts[4].gold = 0;  parts[4].partName = "armL";     //
                parts[5].plastic = 2;  parts[5].coal = 2;  parts[5].steel = 0;  parts[5].gold = 0;  parts[5].partName = "chest";    //
                parts[6].plastic = 1;  parts[6].coal = 1;  parts[6].steel = 0;  parts[6].gold = 0;  parts[6].partName = "core";     //
                parts[7].plastic = 0;  parts[7].coal = 2;  parts[7].steel = 1;  parts[7].gold = 0;  parts[7].partName = "legR";     //
                parts[8].plastic = 1;  parts[8].coal = 0;  parts[8].steel = 0;  parts[8].gold = 0;  parts[8].partName = "footR";    //
                parts[9].plastic = 0;  parts[9].coal = 0;  parts[9].steel = 1;  parts[9].gold = 1;  parts[9].partName = "legL";     //
                parts[10].plastic = 0; parts[10].coal = 1; parts[10].steel = 0; parts[10].gold = 0; parts[10].partName = "footL";   //
                break;
            default:
                Debug.Log("Shit");
                break;
        }
    }
}

public class Part
{

    public int plastic = 0;
    public int coal = 0;
    public int steel = 0;
    public int gold = 0;
    
    public string partName = "";
}