using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_V2_View : MonoBehaviour, IView {
    private Goal_V2_HumbleView HumbleView = null;

	public void Awake() {
		HumbleView = new Goal_V2_HumbleView();
		
		//Goal_V2_Controller Controller = new Goal_V2_Controller();
		//Example.SetController(Controller);

		//Cross reference the Command ID's from the Controller class near the bottom of this page.
		//Example.SetCommandID(0); 
	}

	public void Activate() {
		HumbleView.ConstructView(new Goal_V2_ModelCollection());
		//Add any Listeners needed here.
		Debug.Log("Goal_V2_View Activated.");
	}

	public void Refresh() => HumbleView.RefreshView(new Goal_V2_ModelCollection());

	public void Deactivate() {
		HumbleView.DeconstructView();
		//Remove any Listeners needed here.
		Debug.Log("Goal_V2_View Deactivated.");
	}
}

public class Goal_V2_HumbleView {
	public void ConstructView(Goal_V2_ModelCollection modelCollection) {
	
	}

	public void RefreshView(Goal_V2_ModelCollection modelCollection) {
	
	}

	public void DeconstructView() {
	
	}
}

public class Goal_V2_Controller : IController {
	public void TriggerCommand(int commandID, string input) {
        switch(commandID) {
            case 0: break;
            default: Debug.Log("[WARNING][Goal_V2_Controller] CommandID not recognized! "); return;
        }
    }
}

public class Goal_V2_ModelCollection {
	// Put Model Collections Here
}