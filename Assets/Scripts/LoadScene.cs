using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadS(string str)
	{
		SceneManager.LoadScene(str);
	}
	public void LoadRhythm()
	{
		if (Constant.ChoiceIngredientList.Count > 0)
		{
			SceneManager.LoadScene("RhythmScene");
		}
	}
}
