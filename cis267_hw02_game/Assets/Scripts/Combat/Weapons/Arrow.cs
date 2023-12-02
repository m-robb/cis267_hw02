using static Utilities;
using UnityEngine;


[RequireComponent(typeof(Projectile))]
public class Arrow : MonoBehaviour {
	public GameObject head;
	public GameObject shaft;
	public GameObject fletching;
	[SerializeField] private float shaftLengthMultiplier;

	private SpriteRenderer shaftRenderer;
	private SpriteRenderer fletchingRenderer;


	void Start() {
		shaftRenderer = shaft.GetComponent<SpriteRenderer>();
		fletchingRenderer = fletching.GetComponent<SpriteRenderer>();

		shaftLength(shaftLengthMultiplier);
	}


	/*
	 * Changes the arrow shaft's length.
	 */
	public void shaftLength(float multiplier) {
		Vector3 shaftSize;
		Vector3 fletchingSize;

		/* Set new size for shaft */
		shaftLengthMultiplier = multiplier;
		shaft.transform.localScale = v3Set(shaft.transform.localScale,
				0, shaftLengthMultiplier);

		/* Get bounds to match edges. */
		shaftSize = Vector3.Scale(shaftRenderer.sprite.bounds.size,
				shaft.transform.localScale);
		fletchingSize = Vector3.Scale(
				fletchingRenderer.sprite.bounds.size,
				fletching.transform.localScale);

		fletching.transform.localPosition = Vector3.zero;
		shaft.transform.localPosition = v3Inc(
				fletching.transform.localPosition, 0,
				fletchingSize.x);
		head.transform.localPosition = v3Inc(
				shaft.transform.localPosition, 0,
				shaftSize.x);
	}
}
