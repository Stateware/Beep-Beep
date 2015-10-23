using UnityEngine;
using System.Collections;
using System.Collections.Generic;	

public class Compiler : MonoBehaviour {
	public BuildViewNode[] nodes;
	
	public BuildViewLink[] links;
	void compiler()
	{
		nodes = UnityEngine.Object.FindObjectsOfType<BuildViewNode>();
		
		List< List<bool>> node_adj_matrix=new List< List<bool>>();
		int index = 0;
		foreach(object node in nodes)
		{
			node_adj_matrix.Add(new List<bool>());
			foreach(object node_other in nodes)
			{
				node_adj_matrix[index].Add(false);
			}
			//Console.WriteLine(node.ToString());
			++index;
		}
		
		links = UnityEngine.Object.FindObjectsOfType<BuildViewLink>();
		foreach (object link in links)
		{
			
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
