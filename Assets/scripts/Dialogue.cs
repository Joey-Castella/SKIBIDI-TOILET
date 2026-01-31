using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // <--- SUPER IMPORTANT!
public class Dialogue {

    public string name;

    [TextArea(3, 10)] // Optional: Makes the box bigger in Unity to type easily
    public string[] sentences;

}