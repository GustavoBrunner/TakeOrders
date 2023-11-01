using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Controller.Object;
namespace Controller
{

    public class GetItemEvent : UnityEvent<Items> { }
    public class ShowDescription : UnityEvent<Items> { }
    public class ObjectInteraction : UnityEvent<Objects> { }
    public class SelectUtilitySlot : UnityEvent<Slot> { }
    public class LetPlayerMove : UnityEvent<bool> { }
    public class ShowFadeWalls : UnityEvent { }

    public class TurnInventoryIcons: UnityEvent<bool> { }
    public class TurnAllUi: UnityEvent<bool> { }
    public class GameEvents 
    {
        public static GetItemEvent onGetItemTest = new GetItemEvent();

        public static ShowDescription onShowDescription = new ShowDescription();

        public static ObjectInteraction onObjectInteraction = new ObjectInteraction();

        public static SelectUtilitySlot onSelectUtilitySlot = new SelectUtilitySlot();

        public static ShowFadeWalls onShowFadeWall = new ShowFadeWalls();

        public static LetPlayerMove onLetPlayerMove = new LetPlayerMove();

        public static TurnInventoryIcons onTurnInventoryIconOn = new TurnInventoryIcons();

        public static TurnAllUi onTurnAllUi = new TurnAllUi();
    }
}
