using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    [SerializeField] Portal _destinationPortal = null;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (_destinationPortal != null) {
            Vector2 landingPos = new Vector2(_destinationPortal.transform.position.x, collider.transform.position.y);
            collider.transform.position = landingPos;
        }
    }
}
