using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UIController : MonoBehaviour {

	public List<UIButton> generalButtons = new List<UIButton>();
	public List<NinJaController> entityControllers = new List<NinJaController>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void test(){
		Debug.Log("test");
	}

	public void initUIWithGeneralControllers(List<NinJaController> generalList){
		for (int i = 0; i < generalList.Count; i++) {
			generalButtons[i].gameObject.SetActive(true);	
			generalButtons[i].isEnabled = false;
			entityControllers.Add(generalList[i]);
		}
	}

	public void onClickButton1(){
		Debug.Log("1");
		entityControllers [0].activeSkillButtonPressed = true;
		generalButtons [0].isEnabled = false;
	}

	public void onClickButton2(){
		Debug.Log("2");
		entityControllers [1].activeSkillButtonPressed = true;
		generalButtons [1].isEnabled = false;
	}

	public void onClickButton3(){
		Debug.Log("3");
		generalButtons [2].isEnabled = false;
	}

	public void onClickButton4(){
		Debug.Log("4");
		generalButtons [3].isEnabled = false;
	}

	public void onClickButton5(){
		Debug.Log("5");
		generalButtons [4].isEnabled = false;
	}

	public void readyToCast(NinJaController entity){
		int idx = entityControllers.IndexOf (entity);
		if (idx == -1) {
			return;		
		}
		generalButtons [idx].isEnabled = true;
	}
	
}
