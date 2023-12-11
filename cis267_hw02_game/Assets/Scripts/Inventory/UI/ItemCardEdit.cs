using static Utilities;
using UnityEngine;
using UnityEngine.UI;


public class ItemCardEdit : MonoBehaviour {
	[SerializeField] GameObject objectItemName;
	[SerializeField] GameObject objectItemDescription;
	[SerializeField] GameObject objectItemValue;
	[SerializeField] GameObject objectItemImage;
	[SerializeField] GameObject objectFrameGlow;
	[SerializeField] GameObject objectHaze;

	private Text itemName;
	private Text itemDescription;
	private Text itemValue;
	private Image itemImage;
	private Image frameGlow;
	private Image haze;


	void Awake() {
		itemName = objectItemName.GetComponent<Text>();
		itemDescription = objectItemDescription.GetComponent<Text>();
		itemValue = objectItemValue.GetComponent<Text>();
		itemImage = objectItemImage.GetComponent<Image>();
		frameGlow = objectFrameGlow.GetComponent<Image>();
		haze = objectHaze.GetComponent<Image>();
	}


	/*
	 * Sets the item's name in the inventory UI.
	 */
	public void setName(string name) {
		itemName.text = name;
	}

	/*
	 * Sets the item's descriptionh in the inventory UI.
	 */
	public void setDescription(string description) {
		itemDescription.text = description;
	}

	/*
	 * Sets the item's value in the inventory UI.
	 */
	public void setValue(string value) {
		itemValue.text = value;
	}

	/*
	 * Sets the item's sprite in the inventory UI.
	 */
	public void setSprite(Sprite sprite) {
		itemImage.sprite = sprite;
	}

	/*
	 * Sets the item frame's glow color in the inventory UI.
	 * Set alpha to zero to disable glow.
	 */
	public void setFrameGlow(Color color) {
		frameGlow.color = color;
	}

	/*
	 * Sets the item entry's haze color in the inventory UI.
	 * Set alpha to zero to disable haze.
	 */
	public void setHaze(Color color) {
		haze.color = color;
	}

	/*
	 * Sets all attributes contained in an InventoryItem.
	 * haze and frameGlow will need to be set separately.
	 */
	public void setItem(InventoryItem item) {
		if (item == null) { Debug.Log("bad item"); }
		itemName.text = item.name;
		itemDescription.text = item.description;
		itemValue.text = "" + item.value;
		itemImage.sprite = item.sprite;

		itemImage.enabled = itemImage.sprite != null;
	}
}
