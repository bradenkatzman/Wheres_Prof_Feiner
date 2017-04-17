using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap_Location_Script : MonoBehaviour {

	// upper left coords (120 & bway): 40.810489, -73.961964 
	// upper right coords (120 & morningside): 40.810489, -73.957637
	// lower left coords (114 & bway): 40.806677, -73.964718
	// lower right coords (114 & morningside): 40.804711, -73.9601014
	private float[] UPPER_LEFT_COORDS = new float[]{ 40.81049f, -73.96196f }; /* 120, bway */
	private float[] UPPER_RIGHT_COORDS = new float[]{ 40.80866f, -73.95764f }; /* 120, amstrdm */
	private float[] LOWER_LEFT_COORDS = new float[]{ 40.806676f, -73.96474f }; /* 114, bway */
	private float[] LOWER_RIGHT_COORDS = new float[]{ 40.804743f, -73.96018f }; /* 114, amstrdm */

	private float[] BWAY_1 = new float[]{ 40.81022f, -73.96218f }; /* bw 120 and 119, bway */
	private float[] BWAY_2 = new float[]{ 40.80991f, -73.96241f }; /* 119, bway */
	private float[] BWAY_3 = new float[]{ 40.80969f, -73.96260f }; /* bw 119 and 118, bway */
	private float[] BWAY_4 = new float[]{ 40.80929f, -73.96284f }; /* 118, bway */
	private float[] BWAY_5 = new float[]{ 40.80897f, -73.96308f }; /* bw 118 and 117, bway */
	private float[] BWAY_6 = new float[]{ 40.80875f, -73.96326f }; /* 117, bway */
	private float[] BWAY_7 = new float[]{ 40.80839f, -73.96352f }; /* bw 117 and 116, bway */
	private float[] BWAY_8 = new float[]{ 40.80799f, -73.96383f }; /* 116, bway */
	private float[] BWAY_9 = new float[]{ 40.80766f, -73.96405f }; /* bw 116 and 115, bway */
	private float[] BWAY_10 = new float[]{ 40.80733f, -73.96429f }; /* 115, bway*/
	private float[] BWAY_11 = new float[]{ 40.80700f, -73.96451f }; /* bw 115 and 114, bway */
	private float[] AMSTRDM_1 = new float[]{ 40.80903f, -73.95946f }; /* bw 120 and 119, amstrdm */
	private float[] AMSTRDM_2 = new float[]{ 40.80874f, -73.95965f }; /* 119, amstrdm */
	private float[] AMSTRDM_3 = new float[]{ 40.80846f, -73.95985f }; /* bw 119 and 118, amstrdm */
	private float[] AMSTRDM_4 = new float[]{ 40.80812f, -73.96010f}; /* 118, amstrdm */
	private float[] AMSTRDM_5 = new float[]{ 40.80772f, -73.96040f}; /* bw 118 and 117, amstrdm */
	private float[] AMSTRDM_6 = new float[]{ 40.80742f, -73.96063f}; /* 117, amstrdm */
	private float[] AMSTRDM_7 = new float[]{ 40.80712f, -73.96084f}; /* bw 117 and 116, amstrdm */
	private float[] AMSTRDM_8 = new float[]{ 40.80683f, -73.96107f}; /* 116, amstrdm */
	private float[] AMSTRDM_9 = new float[]{ 40.80649f, -73.96130f}; /* bw 116 and 115, amstrdm */
	private float[] AMSTRDM_10 = new float[]{ 40.80614f, -73.96157f}; /* 115, amstrdm */
	private float[] AMSTRDM_11 = new float[]{ 40.80584f, -73.96177f}; /* bw 115 and 114, amstrdm */

	private float[][][] coordinates_grid_preInterp;
	private float[][][] coordinates_grid_interp;

	/* the increment to be used when interpolating coordinates across the grid
	 * the distance between broadway and amsterdam is approx. 300 meters and our
	 * location services update when the user has moved 10 meters so we use a step of
	 * 300/10 = 30
	 * for best acuracy
	 */ 
	private int INTERP_STEP = 30;

	private int LAT_IDX = 0;
	private int LONG_IDX = 1;

	private Location_Script locationScript;

	public Image Map_Image;
	private RectTransform mapImageRectTransform;
	private float mapImageW;
	private float mapImageH;

	public Image User_Indicator_Image;
	private RectTransform userIndicatorImageRectTransform;

	private bool initialized = false;

	void Start () {
		locationScript = GetComponent<Location_Script> ();

		this.mapImageRectTransform = Map_Image.rectTransform;
		this.mapImageW = mapImageRectTransform.rect.width;
		this.mapImageH = mapImageRectTransform.rect.height;

		this.userIndicatorImageRectTransform = User_Indicator_Image.rectTransform;
		initCoordinateGrid ();
	}

	private void initCoordinateGridPreInterp() {
		// first fill in the non interpolated coordinate map
		coordinates_grid_preInterp = new float[][][]{
			new float[][]{ UPPER_LEFT_COORDS, UPPER_RIGHT_COORDS},
			new float[][]{ BWAY_1, AMSTRDM_1},
			new float[][]{ BWAY_2, AMSTRDM_2},
			new float[][]{ BWAY_3, AMSTRDM_3},
			new float[][]{ BWAY_4, AMSTRDM_4},
			new float[][]{ BWAY_5, AMSTRDM_5},
			new float[][]{ BWAY_6, AMSTRDM_6},
			new float[][]{ BWAY_7, AMSTRDM_7},
			new float[][]{ BWAY_8, AMSTRDM_8},
			new float[][]{ BWAY_9, AMSTRDM_9},
			new float[][]{ BWAY_10, AMSTRDM_10},
			new float[][]{ BWAY_11, AMSTRDM_11},
			new float[][]{ LOWER_LEFT_COORDS, LOWER_RIGHT_COORDS},
		};
	}
	
	private void initCoordinateGrid() {
		// first initialize the grid pre interpolation
		initCoordinateGridPreInterp();


		// initialize the grid that will be interpolated
		coordinates_grid_interp = new float[coordinates_grid_preInterp.Length][][];
		for (int i = 0; i < coordinates_grid_preInterp.Length; i++) {
			float[][] row = new float[INTERP_STEP][];

			float latIncrement = ((coordinates_grid_preInterp [i] [0] [LAT_IDX] - coordinates_grid_preInterp [i] [1] [LAT_IDX]) / ((float) INTERP_STEP));
			float longIncrement = ((coordinates_grid_preInterp [i] [0] [LONG_IDX] - coordinates_grid_preInterp [i] [1] [LONG_IDX]) / ((float) INTERP_STEP));
			for (int k = 0; k < INTERP_STEP; k++) {
				float[] coords = new float[2];

				if (latIncrement > 0.0f) {
					// increment
					coords[0] = coordinates_grid_preInterp [i] [0] [LAT_IDX] + (latIncrement * INTERP_STEP);
				} else {
					// decrement
					coords[0] = coordinates_grid_preInterp [i] [0] [LAT_IDX] - (latIncrement * INTERP_STEP);
				}


				if (longIncrement > 0.0f) {
					coords [1] = coordinates_grid_preInterp [i] [0] [LONG_IDX] + (longIncrement * INTERP_STEP);
				} else {
					coords [1] = coordinates_grid_preInterp [i] [0] [LONG_IDX] - (longIncrement * INTERP_STEP);
				}
				row [k] = coords;
			}
			coordinates_grid_interp [i] = row;
		}


		initialized = true;
		showUserOnMap (
			getClosestCellToCoords (
				LOWER_RIGHT_COORDS[LAT_IDX],
				LOWER_RIGHT_COORDS[LONG_IDX]));
	}

	/*
	 * Given coordinates, find the cell in the coordinate that contains coordinates closest to
	 * the params
	 */ 
	private int[] getClosestCellToCoords(float latitude, float longitude) {
		int[] closestCell = new int[2];

		float[] minDifference = new float[]{1000.0f, 1000.0f};
		for (int i = 0; i < coordinates_grid_interp.Length; i++) {
			float[][] row = coordinates_grid_interp [i];
			for (int k = 0; k < row.Length; k++) {
				float latDifference = Mathf.Abs (row [k] [LAT_IDX] - latitude);
				float longDifference = Mathf.Abs (row [k] [LONG_IDX] - longitude);

				if (latDifference <= minDifference [LAT_IDX] && longDifference <= minDifference [LONG_IDX]) {
					minDifference [LAT_IDX] = latDifference;
					minDifference [LONG_IDX] = longDifference;
					closestCell [0] = i; // y --> down/up bway, amstrdm
					closestCell [1] = k; // x --> across bway, amstrdm
				}
			}
		}

		Debug.Log ("closest cell (y, x): " + closestCell [0].ToString () + ", " + closestCell [1].ToString () + " - with difference (lat, long): " + minDifference [0].ToString () + ", " + minDifference [1].ToString ());

		return closestCell;
	}

	/* visually indicate on the map where the user is
	 * @param mapCell - the indices of the location on the coordinate grid where the user currently is
	 * 
	 * 
	 * here's the image source: http://piq.codeus.net/picture/83857/jump_2_green_stick_figure_j_u_m_p_
	 * here's the the checkpoint image source: https://www.iconfinder.com/icons/882808/checkpoint_direction_navigation_point_icon
	 */ 
	private void showUserOnMap(int[] mapCell) {
		if (mapCell == null || mapCell.Length != 2)
			return;

		// move the User_Indicator_Image to the location corresponding to the closest cell

		// y,x directions
		int pixY = Mathf.RoundToInt (
			(((float)mapCell [0]) / (coordinates_grid_interp.Length - 1.0f))
				* mapImageH);
		int pixX = Mathf.RoundToInt (
				(((float)mapCell [1]) / (((float)INTERP_STEP)-1.0f))
				* mapImageW);

		Debug.Log ("(pixY, pixX): " + pixY.ToString () + ", " + pixX.ToString ());
		if (pixY <= mapImageH && pixY > -1 && pixX <= mapImageW && pixX > -1) {
			/* move the user image to this pixel location
			 * this is relative to the center of the panel
			 */ 
			userIndicatorImageRectTransform.anchoredPosition = new Vector3 ((pixX - (mapImageW/2.0f)), ((mapImageH/2.0f) - pixY), 0);
		} else {
			Debug.Log ("Couldn't locate user on map");
		}

	}

	// want to optimize this to not check every frame
	void Update() {
		if (initialized) {
//			showUserOnMap (
//				getClosestCellToCoords (
//					locationScript.getCurrLatitute (),
//					locationScript.getCurrLongitude ()));
		}
	}
}