using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Controller.Object;
namespace Controller
{

    public class GetItemEvent : UnityEvent<GameObject> { }
    public class ShowDescription : UnityEvent<Objects> { }
    public class ObjectInteraction : UnityEvent<Objects> { }
    public class SelectUtilitySlot : UnityEvent<Slot> { }
    public class GameEvents 
    {
        public static GetItemEvent onGetItemTest = new GetItemEvent();
        public static ShowDescription onShowDescription = new ShowDescription();
        public static ObjectInteraction onObjectInteraction = new ObjectInteraction();
        public static SelectUtilitySlot onSelectUtilitySlot = new SelectUtilitySlot();
    }
}
