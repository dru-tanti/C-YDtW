using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class DraggableObject : MonoBehaviour,  IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Image baseImage;
	public Transform parentAfterDrag;

    // Start is called before the first frame update
    void Start() {
		baseImage = this.gameObject.GetComponent<Image>();
    }

	// Drag Event Handlers
	public void OnBeginDrag(PointerEventData eventData) {
		parentAfterDrag = this.transform.parent;
		this.transform.SetParent(this.transform.root);
		// Moves the object to the bottom of the heirarchy which will render it on top of everything else.
		this.transform.SetAsLastSibling();
		baseImage.raycastTarget = false;
	}

	public void OnDrag(PointerEventData eventData) {
		this.transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData) {
		transform.SetParent(parentAfterDrag);
		baseImage.raycastTarget = true;
	}
}
