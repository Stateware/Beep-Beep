using UnityEngine;
using System.Collections;
using System.Collections.Generic;	

public class Compiler : MonoBehaviour {
	public Node[] nodes;
	public Node[] disconnected_nodes=new Node[1000];

	void compiler()
	{
		List<Node> diconnected_nodes;
		nodes=Node.FindObjectsOfType<Node> ();
		int index = 0;
		for(int i=0; i<nodes.GetLength(1); i++) {
			if(!nodes[i].isConnected())
				disconnected_nodes[index++]=nodes[i];
		}
			
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

	public void check_for_connectedness (){

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
