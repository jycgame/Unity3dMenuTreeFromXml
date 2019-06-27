using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MoonSun;

public class Boot : MonoBehaviour
{
	GameObject buttonPanel;
	MoonSun.TreeNode currentNode;
	GameObject backButtonGo;

	private void Awake ()
	{
		MoonSun.Tree.Instance.Initialise ("MenuTree");

		buttonPanel = GameObject.Find ("Canvas/Page1/Scroll View/Viewport/Content/Panel");

		backButtonGo = GameObject.Find ("Canvas/BackButton");
		backButtonGo.GetComponent<Button> ().onClick.AddListener (BackButtonClick);
	}
	// Use this for initialization
	void Start ()
	{

		for (int i = 0; i < MoonSun.Tree.Instance.Root.Children.Count; ++i) {
			var res = Resources.Load ("Prefab/Button");
			GameObject go = Instantiate (res) as GameObject;
			go.transform.SetParent(buttonPanel.transform, false);

			var textObject = go.transform.GetChild (0).GetComponent<Text> ();
			textObject.text = MoonSun.Tree.Instance.Root.Children [i].Name;

			go.GetComponent<Button> ().onClick.AddListener (OnButtonClick);
		}

		currentNode = MoonSun.Tree.Instance.Root;
	}
	
	// Update is called once per frame
	void Update ()
	{
		backButtonGo.SetActive (currentNode.Parent != null);
	}

	void OnButtonClick ()
	{
		var button = EventSystem.current.currentSelectedGameObject;
		Debug.Log (button.transform.GetChild (0).GetComponent<Text> ().text);

		for (int i = 0; i < currentNode.Children.Count; ++i) {
			if (button.transform.GetChild (0).GetComponent<Text> ().text.Equals (currentNode.Children [i].Name)) {
				ShowChildMenu (currentNode.Children [i]);
			}
		}
	}

	void ShowChildMenu (TreeNode node)
	{

		DeleteAll ();

		// menu of this node from its children
		for (int i = 0; i < node.Children.Count; ++i) {
			var res = Resources.Load ("Prefab/Button");
			GameObject go = Instantiate (res) as GameObject;
			go.transform.SetParent (buttonPanel.transform, false);

			var textObject = go.transform.GetChild (0).GetComponent<Text> ();
			textObject.text = node.Children [i].Name;

			go.GetComponent<Button> ().onClick.AddListener (OnButtonClick);
		}

		currentNode = node;
	}

	void ShowParentMenu (TreeNode node)
	{
		DeleteAll ();

		// menu of this node from its children
		for (int i = 0; i < node.Children.Count; ++i) {
			var res = Resources.Load ("Prefab/Button");
			GameObject go = Instantiate (res) as GameObject;
			go.transform.SetParent (buttonPanel.transform, false);

			var textObject = go.transform.GetChild (0).GetComponent<Text> ();
			textObject.text = node.Children [i].Name;

			go.GetComponent<Button> ().onClick.AddListener (OnButtonClick);
		}

		currentNode = node;
	}

	void BackButtonClick ()
	{
		if (currentNode.Parent != null) {
			ShowChildMenu (currentNode.Parent);
		}
	}

	void DeleteAll ()
	{
		// delete those already exist 
		List<GameObject> goes = new List<GameObject> ();
		for (int i = 0; i < buttonPanel.transform.childCount; ++i) {
			goes.Add (buttonPanel.transform.GetChild (i).gameObject);
		}

		foreach (GameObject go in goes) {
			Destroy (go);
		}
	}
}
