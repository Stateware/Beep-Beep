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
			if(!nodes[i].IsConnected)
				disconnected_nodes[index++]=nodes[i];
		}
			
	}

	public void check_for_connectedness (){

	}


}
