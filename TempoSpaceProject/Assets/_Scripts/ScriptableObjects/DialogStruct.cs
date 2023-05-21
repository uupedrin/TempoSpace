using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialog", menuName = "ScriptableOBJ/DialogOBJ/Dialog", order = 1)]
public class DialogStruct : ScriptableObject
{
    public Image characterImage;
    public string characterName;
    public string characterQuote;
}
