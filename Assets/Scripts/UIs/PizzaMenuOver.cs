using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//한석호 작성
public class PizzaMenuOver : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
	[SerializeField] private RectTransform menuRect;
	[SerializeField] private RectTransform menuListRect;

	public int Speed = 3;
	public int SuperSpeed = 5;

	private UnityEngine.UI.Image img;

	private Color alpha = new Color(0, 0, 0, 185 / 255f);

	private bool isDown = false;
	private bool isEnter = false;
	public void Awake()
	{
		img = this.GetComponent<UnityEngine.UI.Image>();
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		isDown = true;
		img.color = Color.red - alpha;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isEnter = true;
		img.color = Color.magenta - alpha;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isDown = false;
		img.color = Color.magenta - alpha;
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		isEnter = false;
		img.color = Color.white - alpha;
	}

	public void Update()
	{
		if (!isEnter && !isDown) { return; }

		if (isEnter)
		{
			if (menuListRect.rect.height - menuListRect.localPosition.y <= 540)
			{
				if (menuRect.localPosition.y < 200)
				{
					menuRect.localPosition += new Vector3(0, Speed);
				}
				else
				{
					menuRect.localPosition = new Vector3(0, 200);
				}
			}
			else if (menuRect.localPosition.y < 0)
			{
				menuRect.localPosition += new Vector3(0, Speed);
			}
			else
			{
				menuRect.localPosition = new Vector3(0, 0);
				if (menuListRect.rect.height - menuListRect.localPosition.y > 540)
				{
					menuListRect.localPosition += new Vector3(0, Speed);
				}
				else
				{
					menuListRect.localPosition = new Vector3(0, menuListRect.rect.height - 540);
				}
			}

		}

		if (isDown)
		{
			if (menuListRect.rect.height - menuListRect.localPosition.y <= 540)
			{
				if (menuRect.localPosition.y < 200)
				{
					menuRect.localPosition += new Vector3(0, SuperSpeed);
				}
				else
				{
					menuRect.localPosition = new Vector3(0, 200);
				}
			}
			else if (menuRect.localPosition.y < 0)
			{
				menuRect.localPosition += new Vector3(0, SuperSpeed);
			}
			else
			{
				menuRect.localPosition = new Vector3(0, 0);
				if (menuListRect.rect.height - menuListRect.localPosition.y > 540)
				{
					menuListRect.localPosition += new Vector3(0, SuperSpeed);
				}
				else
				{
					menuListRect.localPosition = new Vector3(0, menuListRect.rect.height - 540);
				}
			}
		}
	}
}
