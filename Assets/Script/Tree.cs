using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.IO;

namespace MoonSun
{

	public class Tree
	{

		private Tree ()
		{

		}

		private static Tree instance;

		public static Tree Instance {
			get {
				if (instance == null) {
					instance = new Tree ();
				}
				return instance;
			}
		}

		public TreeNode Root { get; set; }

		public void Initialise (string path)
		{
			var file = Resources.Load (path) as TextAsset;
			string content = file.text;

			var stringReader = new StringReader (content);
			XDocument r = XDocument.Load (stringReader);

			XElement bodyElement = r.Root.Elements ("body").First ();

			XElement rootOutline = bodyElement.Element ("outline");

			this.Root = new TreeNode ();
			this.Root.Name = "@";
			this.Root.Parent = null;
			this.Root.Depth = 0;
			Process (rootOutline, this.Root);

			stringReader.Dispose ();

			//test
			Display (this.Root);

		}

		private void Process (XElement current, TreeNode parent)
		{
			IEnumerable<XElement> list = current.Elements ("outline");

			foreach (XElement el in list) {

				var node = new TreeNode ();
				node.Parent = parent;
				node.Name = el.Attribute ("text").Value;
				node.Depth = parent.Depth + 1;
				parent.Children.Add (node);

				Process (el, node);
			}
		}

		private void Display (TreeNode node)
		{
			for (int i = 0; i < node.Children.Count; ++i) {
				Debug.Log (node.Children [i].Name);
			}

			for (int i = 0; i < node.Children.Count; ++i) {
				Display (node.Children [i]);
			}
		}
	}
}
