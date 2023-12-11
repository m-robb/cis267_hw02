using static Utilities;
using System;
using UnityEngine;


public class InventoryUI : MonoBehaviour {
	[SerializeField] private GameObject itemCardPrefab;
	/* public GameObject objectInventory; */
	[System.NonSerialized] public Inventory inventory;

	[Header("Display Options")]
	public Color hazeColor;
	[Range(0.00f, 1.00f)]
	public float hazeStrength;
	public Color glowColor;

	private GameObject[] itemCards;
	private RectTransform displayArea;

	private int selected;

	private bool allowScroll;
	private float inputVerticalLast;


	void Start() {
		displayArea = (RectTransform)GetComponent<RectTransform>()
				.Find("Display");

		selected = 0;
		allowScroll = true;
		inputVerticalLast = 0.00f;

		draw();
		fill();
	}

	void Update() {
		float inputVertical;
		bool inputUse;

		inputVertical = Input.GetAxisRaw(AXIS_Y);
		inputUse = Input.GetButtonDown(BUTTON_ATTACK);

		/* If the input is 0.00f or has switched sign, allow scroll. */
		if (inputVertical == 0.00f || Mathf.Sign(inputVerticalLast)
				!= Mathf.Sign(inputVertical)) {
			allowScroll = true;
		}

		if (allowScroll && inputVertical != 0.00f
				&& inventory != null) {
			selected += inputVertical > 0.00f ? -1 : 1;
			allowScroll = false;
			fill();
			Debug.Log("New selected: " + selected + ".");
		}

		inputVerticalLast = inputVertical;

		if (inputUse) {
			inventory.use(selected);
		}
	}

	void OnEnable() {
		draw();
		fill();
	}


	/*
	 * Redraws the entire inventory UI.
	 * A fill call after is likely always desirable.
	 */
	public void draw() {
		float itemHeight;
		float itemsShowable;
		int i;

		if (inventory == null) { return; }

		itemHeight = itemCardPrefab.GetComponent<RectTransform>()
				.rect.size.y;
		itemsShowable = displayArea.rect.size.y / itemHeight;

		/* Destroy any previously created item cards. */
		for (i = 0; itemCards != null && i < itemCards.Length; ++i) {
			Destroy(itemCards[i]);
		}

		/* Always needs to be odd. This is a dumb way to do that. */
		itemCards = new GameObject[Mathf.Ceil(itemsShowable) % 2 == 0
				? Mathf.CeilToInt(itemsShowable) + 1
				: Mathf.CeilToInt(itemsShowable)];

		for (i = 0; i < itemCards.Length; ++i) {
			RectTransform rect;
			ItemCardEdit itemCardEdit;
			int relative;

			/* Index relative to the centermost item card. */
			relative = i - (int)(itemCards.Length / 2.00f);
			Debug.Log("Accessing index: " + (selected + relative)
					+ ". Relative to selection: "
					+ relative + ".");

			itemCards[i] = Instantiate(itemCardPrefab, displayArea);

			/* Set the height of the card. */
			rect = itemCards[i].GetComponent<RectTransform>();
			rect.anchoredPosition = rect.anchoredPosition
					- Vector2.up * itemHeight * relative;

			/* Set haze and glow. */
			itemCardEdit = itemCards[i]
					.GetComponent<ItemCardEdit>();
				hazeColor.a = 0.00f; /* Force. */
			itemCardEdit.setHaze(hazeColor + Color.black
					* hazeStrength * Math.Abs(relative));
		}
	}


	/*
	 * Fills the already drawn UI with appropriate information.
	 */
	public void fill() {
		int i;

		if (inventory == null) { return; }

		for (i = 0; i < itemCards.Length; ++i) {
			ItemCardEdit itemCardEdit;
			int relative;

			relative = i - (int)(itemCards.Length / 2.00f);

			/* Set item details. */
			itemCardEdit = itemCards[i]
					.GetComponent<ItemCardEdit>();

			if (selected + relative < inventory.getCapacity()
					&& selected + relative >= 0) {
				itemCardEdit.setItem(inventory
						.peek(selected + relative));
			}
			else {
				itemCardEdit.setItem(new InventoryItem());
			}			
		}
	}
}
