/// <summary>
/// CharacterGenerator.cs
/// Oct 20, 2010
/// Peter Laliberte
/// 
/// This script is used to help the user generate a character.
/// 
/// This script can be attached to any game object in the scene.
/// You will need a GUI.skin to link to here as well as a player prefab.
/// 
/// TODO:
/// if a GUI.skin is not available, then create an defaul one and use that
/// </summary>
using UnityEngine;
using System.Collections;
using System;					//used for the Enum class

public class CharacterGenerator : MonoBehaviour {
//	private PlayerCharacter _toon;
	private const int STARTING_POINTS = 350;
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 10;
	private const int STARTING_VALUE = 50;
	private int pointsLeft;
	
	private const int OFFSET = 5;
	private const int LINE_HEIGHT = 32;
	
	private const int STAT_LABEL_WIDTH = 200;
	private const int BASEVALUE_LABEL_WIDTH = 90;
	private const int BUTTON_WIDTH = 60;
	private const int BUTTON_HEIGHT = 32;
	
	private Rect windowRect;
	
	private int statStartingPos = 30;
	
	public GUISkin mySkin;
	
	public float delayTimer = .25f;
	private float _lastClick = 0;
	
	void Awake() {
//		Debug.Log("***CharacterGenerator - Awake***");
//		PC.Instance.Initialize();
	}
	
	// Use this for initialization
	void Start () {
//		Debug.Log("***CharacterGenerator - Start***");
		
		pointsLeft = STARTING_POINTS;
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
			PC.Instance.GetPrimaryAttribute(cnt).BaseValue = STARTING_VALUE;
			pointsLeft -= (STARTING_VALUE - MIN_STARTING_ATTRIBUTE_VALUE);	
		}

		PC.Instance.StatUpdate();
	}
		
	//update the GUI
	void OnGUI() {
		GUI.skin = mySkin;
		
		windowRect = GUI.Window( 0, windowRect, MyWindow, "Character Creation" );
		
//		if(_toon.Name == "" || pointsLeft > 0)
//			DisplayCreateLabel();
//		else
//			DisplayCreateButton();
	}
	
	public void Update() {
		windowRect	= new Rect( -5, -23, Screen.width + 10, Screen.height + 46 );
	}
	
	
	private void MyWindow( int id ) {
		DisplayName();
		DisplayPointsLeft();
		DisplayAttributes();
		DisplayVitals();
		DisplaySkills();

		if(PC.Instance.Name != "" && pointsLeft < 1)
			DisplayCreateButton();

	}
	
	//display the name lable as well as the textbox for them to enter the name
	private void DisplayName() {
		GUI.BeginGroup( new Rect( 40, 90, 400, LINE_HEIGHT ) );
			GUI.Label(new Rect( 0, 0, 120, LINE_HEIGHT), "Name:");
			PC.Instance.Name = GUI.TextField(new Rect(120, 0, 200, LINE_HEIGHT), PC.Instance.Name);
		GUI.EndGroup();
	}
	
	//display all of the attributes as well as the +/- boxes for the user to alter the values
	private void DisplayAttributes() {
		GUI.BeginGroup(new Rect( 40, 90 + LINE_HEIGHT, ( Screen.width - 80 ) / 2, LINE_HEIGHT * ( Enum.GetValues(typeof(AttributeName)).Length + 1 ) ), "Attributes", "box" );
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++) {
			GUI.Label(new Rect(	OFFSET,									//x
								statStartingPos + (cnt * LINE_HEIGHT),	//y
								STAT_LABEL_WIDTH,						//width
								LINE_HEIGHT								//height
					), ((AttributeName)cnt).ToString());

			GUI.Label(new Rect(	STAT_LABEL_WIDTH + OFFSET,				//x
								statStartingPos + (cnt * LINE_HEIGHT),	//y
								BASEVALUE_LABEL_WIDTH,					//width
								LINE_HEIGHT								//height
					 ), PC.Instance.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString());

			if(GUI.RepeatButton(new Rect(	OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH,	//x
									statStartingPos + (cnt * BUTTON_HEIGHT),			//y
									BUTTON_WIDTH,										//width
									BUTTON_HEIGHT										//height
						 ), "-")) {
				
				if(Time.time - _lastClick > delayTimer) {
					if(PC.Instance.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE) {
						PC.Instance.GetPrimaryAttribute(cnt).BaseValue--;
						pointsLeft++;
						PC.Instance.StatUpdate();
					}
					_lastClick = Time.time;
				}
			}
			if(GUI.RepeatButton(new Rect( OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH,	//x
									statStartingPos + (cnt * BUTTON_HEIGHT),							//y
									BUTTON_WIDTH,														//width
									BUTTON_HEIGHT														//height
						 ), "+")) {
				
				if(Time.time - _lastClick > delayTimer) {
					if(pointsLeft > 0) {
						PC.Instance.GetPrimaryAttribute(cnt).BaseValue++;
						pointsLeft--;
						PC.Instance.StatUpdate();
					}
					_lastClick = Time.time;
				}
			}
		}
		GUI.EndGroup();
	}
	
	//display all of the vitals
	private void DisplayVitals() {
		GUI.BeginGroup(new Rect( 40, 90 + LINE_HEIGHT * (Enum.GetValues(typeof(AttributeName)).Length + 2), ( Screen.width - 80 ) / 2, LINE_HEIGHT * ( Enum.GetValues(typeof(VitalName)).Length + 1 ) ), "Vitals", "box" );
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) {
			GUI.Label(new Rect( OFFSET,											//x
								statStartingPos + (cnt * LINE_HEIGHT),			//y
								STAT_LABEL_WIDTH,								//width
								LINE_HEIGHT										//height
					 ), ((VitalName)cnt).ToString());
			
			GUI.Label(new Rect( OFFSET + STAT_LABEL_WIDTH,						//x
								statStartingPos + (cnt * LINE_HEIGHT),			//y
								BASEVALUE_LABEL_WIDTH,							//width
								LINE_HEIGHT										//height
					 ), PC.Instance.GetVital(cnt).AdjustedBaseValue.ToString());
		}
		GUI.EndGroup();
	}	
	
	//display all of the skills
	private void DisplaySkills() {
		GUI.BeginGroup(new Rect( Screen.width - ( Screen.width - 20 ) / 2, 90 + LINE_HEIGHT, ( Screen.width - 80 ) / 2, LINE_HEIGHT * ( Enum.GetValues(typeof(SkillName)).Length + 1 ) ), "Attributes", "box" );
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++) {
			GUI.Label(new Rect( 0,	//x
								statStartingPos + (cnt * LINE_HEIGHT),												//y
								STAT_LABEL_WIDTH,																	//width
								LINE_HEIGHT																			//height
					 ), ((SkillName)cnt).ToString());

			GUI.Label(new Rect( STAT_LABEL_WIDTH,	//x
								statStartingPos + (cnt * LINE_HEIGHT),																	//y
								BASEVALUE_LABEL_WIDTH,																					//width
								LINE_HEIGHT																								//height
					 ), PC.Instance.GetSkill(cnt).AdjustedBaseValue.ToString());
		}
		GUI.EndGroup();
	}
	
	//show them how many points they have left to spend
	private void DisplayPointsLeft() {
		GUI.Label(new Rect(Screen.width - 150 - 30, 90, 150, LINE_HEIGHT), "Points Left: " + pointsLeft.ToString());
	}
	
	//display label that looks like a button until they have all the requiements filled out to create a character
	private void DisplayCreateLabel() {
//		GUI.Label(new Rect( Screen.width/ 2 - 50, statStartingPos + (10 * LINE_HEIGHT), 100, LINE_HEIGHT), "Creating...", "Button");
	}

	//display the button that will call the save method when pressed, and then load the first level
	private void DisplayCreateButton() {
		if(GUI.Button(new Rect( Screen.width/ 2 - 50, statStartingPos + (10 * LINE_HEIGHT), 100, LINE_HEIGHT), "Next")) {
			//change the cur value of the vitals to the max modified value of that vital
			UpdateCurVitalValues();
			
			GameSetting2.SaveName( PC.Instance.Name );
			GameSetting2.SaveAttributes( PC.Instance.primaryAttribute );
			GameSetting2.SaveVitals( PC.Instance.vital );
			GameSetting2.SaveSkills( PC.Instance.skill );

			Application.LoadLevel(GameSetting2.levelNames[2]);
		}
	}
	
	//update the curValue for each vital to be the max value it can be so we do not start with a  zero in any vital
	private void UpdateCurVitalValues() {
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++) {
			PC.Instance.GetVital(cnt).CurValue = PC.Instance.GetVital(cnt).AdjustedBaseValue;
		}
	}
}
