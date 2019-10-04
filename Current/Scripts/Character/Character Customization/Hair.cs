using UnityEngine;

public class Hair {
	public int hairColorIndex;
	public int hairMeshIndex;
	public GameObject hairStyle;
	
	public Rect position;
	public int offset;
	
	private int _numberOfHairColors;
	private int _numberOfHairMeshes;
	
	private Vector2 _hairStyleButtonSize;
	private Vector2 _hairColorButtonSize;
	private Texture[] _hairColorTexture;
	
	
	
	public Hair() {
		hairColorIndex = 0;
		hairMeshIndex = 0;
		
		offset = 0;
		_numberOfHairColors = 6;
		_numberOfHairMeshes = 60;
		
		_hairColorTexture = new Texture[_numberOfHairColors];

		position = new Rect( 100, 10, 100, 50 );
		
		_hairColorButtonSize = new Vector2( ( position.width - ( offset * 2 ) ) / _numberOfHairColors, ( position.height - ( offset * 2 ) ) / 2 );
		_hairStyleButtonSize = new Vector2( ( position.width - ( offset * 2 ) ) / 2, ( position.height - ( offset * 2 ) ) / 2 );
		
	}
	
	public void OnGUI() {
		GUI.BeginGroup( position );

		HairColorButtons();

		NextHairStyle();
		PreviousHairStyle();
		GUI.EndGroup();
	}
	
	private void HairColorButtons() {
		if( _hairColorTexture[0] == null )
			LoadHairColorTextures();

		for( int cnt = 0; cnt < _numberOfHairColors; cnt++) {
			if( GUI.Button( new Rect( offset + (_hairColorButtonSize.x * cnt ), offset, _hairColorButtonSize.x, _hairColorButtonSize.y ), _hairColorTexture[cnt], "label") ) {
				hairColorIndex = cnt;
				hairStyle.renderer.material.mainTexture = _hairColorTexture[hairColorIndex];
			}
		}
	}
	
	private void PreviousHairStyle() {
		if( GUI.Button( new Rect( offset, position.height - _hairStyleButtonSize.y - offset, _hairStyleButtonSize.x, _hairStyleButtonSize.y ), "<") ) {
			hairMeshIndex--;
			
			if( hairMeshIndex < 0 )
				hairMeshIndex = _numberOfHairMeshes - 1;
			
			LoadHairMesh();
		}
	}

	private void NextHairStyle() {
		if (GUI.Button( new Rect( offset + _hairStyleButtonSize.x, position.height - _hairStyleButtonSize.y - offset, _hairStyleButtonSize.x, _hairStyleButtonSize.y ), ">") ) {
			hairMeshIndex++;
			
			if( hairMeshIndex > _numberOfHairMeshes - 1 )
				hairMeshIndex = 0;
			
			LoadHairMesh();
		}
	}
	
	private void LoadHairMesh() {
//		GameObject mount = PlayerModelCustomization.characterMesh.GetComponent<PlayerCharacter>().hairMount;
		
		if( PC.Instance.hairMount.transform.childCount > 0 )
			Object.Destroy( PC.Instance.hairMount.transform.GetChild(0).gameObject );
		
		int hairSet = hairMeshIndex / 5 + 1;
		int hairIndex = hairMeshIndex % 5 + 1;
		
//		Debug.Log(hairSet + "_" + hairIndex);
		hairStyle = Object.Instantiate( Resources.Load( GameSetting2.HUMAN_MALE_HAIR_MESH_PATH + "Hair" + " " + hairSet + "_" + hairIndex ), PC.Instance.hairMount.transform.position, PC.Instance.hairMount.transform.rotation ) as GameObject;
		
		//we want to adjust the scale of the hair mesh according to the global scale we are applying to our model
		hairStyle.transform.localScale = new Vector3(
		                                             hairStyle.transform.localScale.x * PlayerModelCustomization.scale.x,
		                                             hairStyle.transform.localScale.y * PlayerModelCustomization.scale.x,
		                                             hairStyle.transform.localScale.z * PlayerModelCustomization.scale.y
		                                             );
		
		hairStyle.transform.parent = PC.Instance.hairMount.transform;
		
		hairStyle.renderer.material.mainTexture = _hairColorTexture[hairColorIndex];
		
		MeshOffset mo = hairStyle.GetComponent<MeshOffset>();
		if( mo == null )
			return;
		
		hairStyle.transform.localPosition = mo.positionOffset;
		hairStyle.transform.localRotation = Quaternion.Euler( mo.rotationOffset );
		hairStyle.transform.localScale = mo.scaleOffset;
	}
	
	private void LoadHairColorTextures() {
		for( int cnt = 0; cnt < _hairColorTexture.Length; cnt++) 
			_hairColorTexture[cnt] = Resources.Load( GameSetting2.HUMAN_MALE_HAIR_COLOR_PATH + ((HairColorNames)cnt).ToString() ) as Texture;
	}
	
	public void LoadInitailHair() {
	if( _hairColorTexture[0] == null )
		LoadHairColorTextures();

		LoadHairMesh();
	}
}

public enum HairColorNames {
	Black = 0,
	Blonde = 1,
	Brown = 2,
	Light_Brown = 3,
	Red = 4,
	Grey = 5
}
