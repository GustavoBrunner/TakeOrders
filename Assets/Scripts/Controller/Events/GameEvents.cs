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
    public class TurnMovementOn : UnityEvent { }
    public class ShowFadeWalls : UnityEvent { }

    public class TurnInventoryIcons: UnityEvent<bool> { }
    public class TurnAllUi: UnityEvent<bool> { }

    public class ChangeCam: UnityEvent<string> { }

    public class OpenPauseMenu : UnityEvent<bool> { }
    public class TurnBlackEffect : UnityEvent<bool> { }
    public class TurnWhiteEffect : UnityEvent<bool> { }

    public class UpdateCamPos : UnityEvent<Vector3> { }
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

        public static ChangeCam onChangeCam = new ChangeCam();

        public static OpenPauseMenu onOpenPauseMenu = new OpenPauseMenu();

        public static TurnBlackEffect onTurnBlackEffect = new TurnBlackEffect();

        public static TurnWhiteEffect onTurnWhiteEffect = new TurnWhiteEffect();

        public static TurnWhiteEffect onTurnWhiteEffectOn = new TurnWhiteEffect();

        public static TurnMovementOn onTurnMovementOn = new TurnMovementOn();

        public static UpdateCamPos onUpdateCamPos = new UpdateCamPos();
    }
}
