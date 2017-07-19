using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	public Texture2D cursor;
	public Texture2D coinTexture;
	private Player player;

	protected void Start () {
		Screen.showCursor = false;
		PlayerManager manager = GetComponent< PlayerManager > ();
		player = manager.activePlayer;
	}

	protected void OnGUI () {
		DrawCursor ();
		DrawCoinCollected ();
	}

	private void DrawCursor () {
		Vector2 cursorPoint = (Vector2)Camera.main.WorldToScreenPoint ((Vector3)player.cursor);
		Rect cursorCanvas = new Rect (cursorPoint.x - cursor.width / 2,
		                              Screen.height - cursorPoint.y - cursor.height / 2,
		                              cursor.width,
		                              cursor.height);
		GUI.Label (cursorCanvas, cursor);
	}

	private void DrawCoinCollected () {
		Rect coinCanvas = new Rect (0, 0, coinTexture.width, coinTexture.height);
		GUI.Label (coinCanvas, coinTexture);

		GUI.skin.label.alignment = TextAnchor.MiddleRight;
		GUI.Label (coinCanvas, PlayerManager.coinCollected.ToString ());
	}
}