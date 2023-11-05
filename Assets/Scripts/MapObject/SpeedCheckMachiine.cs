using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성

public class SpeedCheckMachiine : MonoBehaviour
{
	[SerializeField] private FinePooling finePooling;
    [SerializeField] private Sprite greenSpr;
    [SerializeField] private Sprite redSpr;
    [SerializeField] private int limitSpeed;

	private SpriteRenderer sprRen;
	private bool isFine = false;

	// Start is called before the first frame update
	void Awake()
    {
        sprRen = this.GetComponent<SpriteRenderer>();
        sprRen.sprite = greenSpr;
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag.Equals("Player"))
		{
            if (collision.GetComponent<PlayerMove>().Speed >= limitSpeed)
			{
                sprRen.sprite = redSpr;
				if (!isFine)
                {
					isFine = true;
					finePooling.AddFine((int)(collision.GetComponent<PlayerMove>().Speed * 2000));
                }
			}
            else
			{
                sprRen.sprite = greenSpr;
			}
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		isFine = false;
		if (collision.tag.Equals("Player"))
		{
			sprRen.sprite = greenSpr;
		}
	}
}
