using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAS_View : MonoBehaviour, IView {
    private DAS_HumbleView HumbleView = null;

	public void Awake() {
		HumbleView = new DAS_HumbleView();
		
		//DAS_Controller Controller = new DAS_Controller();
		//Example.SetController(Controller);

		//Cross reference the Command ID's from the Controller class near the bottom of this page.
		//Example.SetCommandID(0); 
	}

	public void Activate() {
		HumbleView.ConstructView(new DAS_ModelCollection());
		//Add any Listeners needed here.
		Debug.Log("DAS_View Activated.");
	}

	public void Refresh() => HumbleView.RefreshView(new DAS_ModelCollection());

	public void Deactivate() {
		HumbleView.DeconstructView();
		//Remove any Listeners needed here.
		Debug.Log("DAS_View Deactivated.");
	}
}

public class DAS_HumbleView {
	public void ConstructView(DAS_ModelCollection modelCollection) {
	
	}

	public void RefreshView(DAS_ModelCollection modelCollection) {
	
	}

	public void DeconstructView() {
	
	}
}

public class DAS_Controller : IController {
	public void TriggerCommand(int commandID, string input) {
        switch(commandID) {
            case 0: break;
            default: Debug.Log("[WARNING][DAS_Controller] CommandID not recognized! "); return;
        }
    }
}

public class DAS_ModelCollection {
	// Put Model Collections Here
}