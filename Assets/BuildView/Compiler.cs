using UnityEngine;
using System.Collections;

/*
this compiler should carry out the following tasks:
-> check if the graph is connected
--> if not, change sprite of nodes that are disconnected
-> start simulation view by spawning cars
*/

public class Compiler : MonoBehaviour {
    public BuildViewNode[] nodes;

    void compile()
    {
        nodes = UnityEngine.Object.FindObjectsOfType<BuildViewNode>();
        
    }
}
