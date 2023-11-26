using static Utilities;
using UnityEngine;


[RequireComponent(typeof(Projectile))]
public class Arrow : MonoBehaviour {
	public GameObject head;
	public GameObject shaft;
	public GameObject fletching;
	[SerializeField] private float shaftLengthMultiplier;

	private Renderer shaftRenderer;
	private Renderer fletchingRenderer;


	void Start() {
		shaftRenderer = shaft.GetComponent<Renderer>();
		fletchingRenderer = fletching.GetComponent<Renderer>();

		shaftLength(shaftLengthMultiplier);
	}


	/*
	 * Changes the arrow shaft's length.
	 */
	public void shaftLength(float multiplier) {
		Bounds shaftBounds; /* Not cacheable. */
		Bounds fletchingBounds;

		/* Set new size for shaft */
		shaftLengthMultiplier = multiplier;
		shaft.transform.localScale = v3Set(shaft.transform.localScale,
				0, shaftLengthMultiplier);

		/* Get bounds to match edges. */
		shaftBounds = shaftRenderer.bounds;
		fletchingBounds = fletchingRenderer.bounds;

		fletching.transform.localPosition = Vector3.zero;
		shaft.transform.localPosition = v3Inc(
				fletching.transform.localPosition, 0,
				fletchingBounds.size.x);
		head.transform.localPosition = v3Inc(
				shaft.transform.localPosition, 0,
				shaftBounds.size.x);
	}
}
