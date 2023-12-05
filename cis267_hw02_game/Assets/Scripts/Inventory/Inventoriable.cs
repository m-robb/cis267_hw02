using static Utilities;
using UnityEngine;


/*
 * A class that contains a nested InventoryItem class to allow exposing it
 * 		to the inspector.
 */
public class Inventoriable : MonoBehaviour {
	public GameObject inventory; /* TEMPORARY! */

	public InventoryItem item;

	[ContextMenu("addToInventory()")]
	public void addToInventory() {
		Debug.Log(inventory.GetComponent<Inventory>().add(item));
		if (item.amount == 0) {
			Destroy(gameObject);
		}
	}
}
