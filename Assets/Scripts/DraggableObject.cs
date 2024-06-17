using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class DraggableObject : MonoBehaviour,  IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Image baseImage;
	public Transform parentAfterDrag;

    // Start is called before the first frame update
    void Start() {
		// Used to lock the size of the card, regardless of screen size.
		transform.localScale = new Vector3(1, 1, 1);
		baseImage = gameObject.GetComponent<Image>();
    }

	// Drag Event Handlers
	public virtual void OnBeginDrag(PointerEventData eventData) {
		parentAfterDrag = transform.parent;
		transform.SetParent(transform.root);
		// Moves the object to the bottom of the heirarchy which will render it on top of everything else.
		transform.SetAsLastSibling();
		baseImage.raycastTarget = false;
	}

	public void OnDrag(PointerEventData eventData) {
		transform.position = Input.mousePosition;
	}

	public virtual void OnEndDrag(PointerEventData eventData) {
		transform.SetParent(parentAfterDrag);
		baseImage.raycastTarget = true;
	}
}
