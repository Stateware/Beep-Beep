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

    public void compileToActionPoint()
    {
        this.GetComponent<SpriteRenderer>().sprite = this.getAssetForNode();
        //new collision area must be redefined
    }

    private Sprite getAssetForNode()
    {
        string assetPathVar = this.generateAssetPath();
        return (Sprite)Resources.Load(assetPathVar, typeof(Sprite));
    }

    private string generateAssetPath()
    {
        BuildViewNode allNodeProperties = GameObject.FindObjectOfType<BuildViewNode>();
        if (allNodeProperties.getNodeProperty() == BuildViewNode.NodeType.TrafficLight)
        {
            return BuildViewNode.NodeType.TrafficLight.ToString();
        }
        else if (allNodeProperties.getNodeProperty() == BuildViewNode.NodeType.StopSign)
        {
            return BuildViewNode.NodeType.StopSign.ToString();
        }
        else if (allNodeProperties.getNodeGateTypeProperty() == BuildViewNode.NodeGateType.Source)
        {
            return BuildViewNode.NodeGateType.Source.ToString();
        }
        return BuildViewNode.NodeGateType.Sink.ToString();
    }
}
