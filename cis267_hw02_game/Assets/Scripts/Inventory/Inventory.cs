using static Utilities;
using UnityEngine;


/*
 * A stack of inventory items.
 */
[System.Serializable]
public class InventoryItem {
	/*
	 * A fake enum used for item flags.
	 * Use as a enum that may be implicitly converted to an uint.
	 */
	public static class ItemFlags {
		public const uint NONE       = 0U;
		public const uint EQUIPABLE  = 1U << 0;
		public const uint CONSUMABLE = 1U << 1;
		public const uint SPAWNABLE  = 1U << 2;
	};


	[Tooltip("The GameObject the item should be when entering the world.")]
	public GameObject item;
	[Tooltip("The sprite to be used when being displayed in an inventory.")]
	public Sprite sprite;
	[Tooltip("The ID of the item. It must be unique amongst items.")]
	public string id; /* Must be unique amongst items! */
	[Tooltip("The descriptive name of the item.")]
	public string name;
	[Tooltip("The description of the item.")]
	[TextArea]
	public string description;
	[Tooltip("Cost in gold.")]
	public int value;
	[Tooltip("The maximum amount of this item allowed to be in one slot.")]
	public int amountMax;
	[Tooltip("The current amount of this item in this slot.")]
	public int amount;
	[Tooltip("Miscellaneous flags for items.\n"
			+ "Consult the table in the script.")]
	public uint flags;


	/*
	 * Default constructor.
	 */
	public InventoryItem() {
		item = null;
		sprite = null;
		id = "";
		name = "";
		description = "";
		value = 0;
		amountMax = 0;
		amount = 0;
		flags = ItemFlags.NONE;
	}

	/*
	 * Copy constructor.
	 */
	public InventoryItem(InventoryItem item) {
		this.item        = item.item;
		this.sprite      = item.sprite;
		this.id          = item.id;
		this.name        = item.name;
		this.description = item.description;
		this.value       = item.value;
		this.amountMax   = item.amountMax;
		this.amount      = item.amount;
		this.flags       = item.flags;
	}


	/*
	 * Returns the remaining amount of space.
	 */
	public int remaining() {
		return amountMax - amount;
	}
}


/*
 * An inventory that can hold many InventoryItems.
 * Does not display anything, use a different class for that.
 * In most cases, do not change the capacity of an Inventory after creation.
 */
public class Inventory : MonoBehaviour {
	[SerializeField] private GameObject inventoryDisplayPrefab;
	[Space(64)]
	[Tooltip("Should only be true on one inventory.")]
	public bool isMainInventory;
	[Space(64)]
	[Tooltip("The number of items able to be stored in the inventory.")]
	[Min(1)]
	[SerializeField] private int capacity;

	[HideInInspector] public int used;
	private InventoryItem[] inventory;

	private GameObject inventoryDisplay;


	void Awake() {
		int i;

		/* Create the actual inventory as soon as possible. */
		inventory = new InventoryItem[capacity];

		/* Call default constructor on every item. Used for display. */
		for (i = 0; i < capacity; ++i) {
			inventory[i] = new InventoryItem();
		}

		used = 0;

		inventoryDisplay = Instantiate(inventoryDisplayPrefab);
		inventoryDisplay.GetComponent<InventoryUI>().inventory = this;
		inventoryDisplay.SetActive(false);
	}

	void Update() {
		if (isMainInventory && Input.GetButtonDown(BUTTON_INVENTORY)) {
			/* Toggle the inventory's visibility. */
			inventoryDisplay.SetActive(
					!inventoryDisplay.activeSelf);
		}
	}


	public void OnTriggerEnter2D(Collider2D collision) {
		/* If we bump into an item that can be picked up, do that. */
		if (collision.gameObject.tag == TAG_INVENTORIABLE) {
			add(collision.GetComponent<Inventoriable>().item);
		}
	}


	/*
	 * Returns the amount of capacity remaining in the inventory.
	 */
	public int capacityRemaining() {
		return capacity - used;
	}

	/*
	 * Returns the total capacity of the inventory.
	 */
	public int getCapacity() {
		return capacity;
	}

	/*
	 * Returns a copy of the InventoryItem at index i.
	 */
	public InventoryItem peek(int i) {
		return new InventoryItem(inventory[i]);
	}

	/*
	 * Add an item to the inventory.
	 * Returns true if all items were able to be added to the inventory.
	 * If only some, or even none, of the items were unable to be added,
	 * 		returns false.
	 */
	public bool add(InventoryItem item) {
		int i;
		int j;


		/* See if this item is already in the inventory. */
		for (i = j = 0; j < used && item.amount > 0; ++i) {
			/* Try to find match that has space remaining. */
			if (inventory[i].id == item.id
					&& inventory[i].remaining() > 0) {
				Debug.Log("Found existing and added to it.");
				inventory[i].amount += item.amount;
				item.amount = -inventory[i].remaining();
				inventory[i].amount = System.Math.Min(
						inventory[i].amount,
						inventory[i].amountMax);
			}

			++j;
		}

		/* Find and fill the first available space if there is one. */
		for (i = 0; i < capacity && item.amount > 0
				&& capacityRemaining() > 0; ++i) {
			if (inventory[i].id == "") {
				inventory[i] = new InventoryItem(item);
				item.amount = System.Math.Max(0,
						-inventory[i].remaining());
				inventory[i].amount = System.Math.Min(
						inventory[i].amount,
						inventory[i].amountMax);
				++used;
			}
		}

		return item.amount == 0;
	}

	/*
	 * Removes an item from the inventory at index index.
	 * Returns the GameObject associated with the inventory item.
	 * Returns null if there is no item found.
	 */
	public GameObject remove(int index) {
		GameObject go;

		/* Guard clause that hopefully shortcircuits...? */
		if (index >= capacity) {
			return null;
		}

		go = inventory[index].item;

		inventory[index] = new InventoryItem();

		--used;

		return go;
	}

	/*
	 * Swaps the items in the inventory at index i and j.
	 */
	public void swap(int i, int j) {
		if (i > capacity || j > capacity) {
			Debug.LogWarning("Swap IOOB: capacity = " + capacity
					+ "; provided = " + i + ", " + "j",
					this);
			return;
		}

		InventoryItem tmp;

		tmp = inventory[i];
		inventory[i] = inventory[j];
		inventory[j] = tmp;
	}

	/*
	 * Displays info and the contents of the inventory.
	 */
	[ContextMenu("debugDisplayContents()")]
	public void debugDisplayContents() {
		string log;
		int i;

		log = "";
		log += "Capacity  = " + capacity + "\n";
		log += "Used      = " + used + "\n";
		log += "Remaining = " + capacityRemaining() + "\n";

		log += "\n";

		for (i = 0; i < capacity; ++i) {
			log += "\n";
			log += "--------ITEM #" + i + "--------\n";

			log += "ID             = " + inventory[i].id + "\n";
			log += "Name           = " + inventory[i].name + "\n";
			log += "Description    = " + inventory[i].description
					+ "\n";
			log += "Maximum Amount = " + inventory[i].amountMax
					+ "\n";
			log += "Amount         = " + inventory[i].amount + "\n";
			log += "Remaining      = " + inventory[i].remaining()
					+ "\n";
			log += "Flags          = " + inventory[i].flags + "\n";
		}

		Debug.Log(log);
	}
}
