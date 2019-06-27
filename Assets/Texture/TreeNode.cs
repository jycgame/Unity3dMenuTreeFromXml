using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoonSun {
public class TreeNode {

    public string Name { get; set; }
    public TreeNode Parent { get; set; }
    public List<TreeNode> Children { get; set; }
	public int Depth { get; set; }

	public TreeNode() {
		this.Children = new List<TreeNode> ();
	}
}
}
