using UnityEngine;
using System.Collections;

public class LootWindow : MonoBehaviour {
	public static int numberOfRows = 3;
	public static int slotsPerRow = 3;
	
	private UIHorizontalLayout[] row;
	private UIButton[] slot;
	

	void Start() {
		row = new UIHorizontalLayout[numberOfRows];
		slot = new UIButton[slotsPerRow];

		for (int y = 0; y < row.Length; y++) {
			row[y] = new UIHorizontalLayout( 3 );
			row[y].position = new Vector3( 5, -5 - ( y * 50 ), 0 );
			
			for (int i = 0; i < slot.Length; i++) {
				slot[i] = UIButton.create( "cbDown.png", "cbUnchecked.png", i * 50, 0 );
				slot[i].onTouchUpInside += OnSlotClicked;
				row[y].addChild( slot[i] );
			}
		}
	}
	
	public void Setup( int rows, int slots ) {
		numberOfRows = rows;
		slotsPerRow = slots;
	}
	
	void OnSlotClicked( UIButton sender ) {
		Debug.Log( sender.index );
	}
}
