using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성

public class SpeedCheckMachiine : MonoBehaviour
{
    [SerializeField] private Sprite greenSpr;
    [SerializeField] private Sprite redSpr;
    [SerializeField] private int limitSpeed;

    private SpriteRenderer sprRen;
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
			}
            else
			{
                sprRen.sprite = greenSpr;
			}
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag.Equals("Player"))
		{
			sprRen.sprite = greenSpr;
		}
	}
}
