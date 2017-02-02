using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//http://stackoverflow.com/questions/37473802/unity3d-ui-calculation-for-position-dragging-an-item/37473953#37473953

public class UNCDraggable:MonoBehaviour,
IBeginDragHandler, IDragHandler, IEndDragHandler //, IDropHandler
{
	public Image image;
	// note DON'T try to drag the actual item: it's not worth the hassle.
	// a problem arises where you can't have it on top (as you would want
	// visually), and still easily get the drops. always use a ghost.
	// even if you want the "original invisible" while dragging,
	// simply hide it and use a ghost. everything is tremendously
	// easier if you do not move the originals.

	void Awake()
	{
		//image.raycastTarget = false;
		// (just in case you forgot to do that in the Editor)
		//image.enabled = false;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		image.transform.position = transform.position;
		Debug.Log ("initial Pos: " + image.transform.position.x + ", " + image.transform.position.y);
	}

	public void OnDrag(PointerEventData eventData)
	{
		image.transform.position += (Vector3)eventData.delta;
		Debug.Log ("dragging Pos: " + image.transform.position.x + ", " + image.transform.position.y);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log ("final Pos: " + image.transform.position.x + ", " + image.transform.position.y);
	}

	/*public void OnDrop(PointerEventData data)
	{
		GameObject fromItem = data.pointerDrag;
		if (data.pointerDrag == null) return; // (will never happen)

		UNCDraggable d = fromItem.GetComponent<UNCDraggable>();
		if (d == null)
		{
			// means something unrelated to our system was dragged from.
			// for example, just an unrelated scrolling area, etc.
			// simply completely ignore these.
			return;
			// note, if very unusually you have more than one "system"
			// of UNCDraggable items on the same screen, be careful to
			// distinguish them! Example solution, check parents are same.
		}

		Debug.Log ("dropped  " + fromItem.name +" onto " +gameObject.name);

		// your code would look probably like this:
		//YourThings fromThing = fromItem.GetComponent<YourButtons>().info;
		//YourThings untoThing = gameObject.GetComponent<YourButtons>().info;

		//yourBossyObject.dragHappenedFromTo(fromThing, untoThing);
	}*/
}