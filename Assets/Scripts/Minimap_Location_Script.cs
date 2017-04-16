using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_Location_Script : MonoBehaviour {

	// upper left coords (120 & bway): 40.810489, -73.961964 
	// upper right coords (120 & morningside): 40.810489, -73.957637
	// lower left coords (114 & bway): 40.806677, -73.964718
	// lower right coords (114 & morningside): 40.804711, -73.9601014
	private float[] UPPER_LEFT_COORDS = new float[]{ (float)40.810497, (float)-73.961964 };
	private float[] UPPER_RIGHT_COORDS = new float[]{ (float)40.808666, (float)-73.957640 };
	private float[] LOWER_LEFT_COORDS = new float[]{ (float)40.806676, (float)-73.964743 };
	private float[] LOWER_RIGHT_COORDS = new float[]{ (float)40.804743, (float)-73.960183 };

	float[][][] coordinates_grid;
	private int LAT_IDX = 0;
	private int LONG_IDX = 0;

	private Location_Script locationScript;

	void Start () {
		locationScript = GetComponent<Location_Script> ();
		initCoordinateGrid ();
	}
	
	private void initCoordinateGrid() {
		// initialize the grid
		coordinates_grid = new float[50][][];

		/* iterate over the grid and use an
		 * interpolation scheme to fill
		 * the grid in with coordinates
		*/
		for (int latitude_iter = 0; latitude_iter < 50; latitude_iter++) {
			float[][] row = new float[50][];
			for (int longitude_iter = 0; longitude_iter < 50; longitude_iter++) {
				float[] coords = new float[2];
				// check for the known locations
				if (latitude_iter == 0 && longitude_iter == 0) { // upper left corner
					coords [LAT_IDX] = UPPER_LEFT_COORDS [LAT_IDX];
					coords [LONG_IDX] = UPPER_LEFT_COORDS [LONG_IDX];
				} else if (latitude_iter == 0 && longitude_iter == 49) { // upper right corner
					coords [LAT_IDX] = UPPER_RIGHT_COORDS [LAT_IDX];
					coords [LONG_IDX] = UPPER_RIGHT_COORDS [LONG_IDX];
				} else if (latitude_iter == 49 && longitude_iter == 0) { // lower left corner
					coords[LAT_IDX] = LOWER_LEFT_COORDS[LAT_IDX];
					coords [LONG_IDX] = LOWER_LEFT_COORDS [LONG_IDX];
				} else if (latitude_iter == 49 && longitude_iter == 49) { // lower right corner
					coords[LAT_IDX] = LOWER_RIGHT_COORDS[LAT_IDX];
					coords [LONG_IDX] = LOWER_RIGHT_COORDS [LONG_IDX];
				} else { // use interpolation method

				}

				row[longitude_iter] = coords;
			}
			coordinates_grid [latitude_iter] = row;
		}
	}

	/*
	 * Given coordinates, find the cell in the coordinate that contains coordinates closest to
	 * the params
	 */ 
	private int[] getClosestCellToCoords(float latitude, float longitude) {
		Debug.Log ("getting closest cell to " + latitude.ToString () + ", " + longitude.ToString ());
		int[] closestCell = new int[2];



		return closestCell;
	}

	/* visually indicate on the map where the user is
	 * @param mapCell - the indices of the location on the coordinate grid where the user currently is
	 */ 
	private void showUserOnMap(int[] mapCell) {
		if (mapCell == null || mapCell.Length != 2)
			return;


	}

	// want to optimize this to not check every frame
	void Update() {
		showUserOnMap (
			getClosestCellToCoords (
				locationScript.getCurrLatitute (),
				locationScript.getCurrLongitude ()));
	}
}