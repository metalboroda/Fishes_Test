using Assets.__Game.Scripts.Fish;
using Assets.__Game.Scripts.StateMachine;

namespace Assets.__Game.Scripts.EventBus
{
  public class EventStructs
  {
    #region Infrastructure

    public struct StateChanged : IEvent
    {
      public State State;
    }

    #endregion

    #region Fish
    public struct FishUiEvent : IEvent
    {
      public int FishId;
      public int FishNumber;
    }

    public struct FishClickEvent : IEvent
    {
      public int FishNumber;
      public FishHandler FishHandler;
    }

    public struct FishDestroyEvent : IEvent
    {
      public int FishId;
    }
    #endregion
  }
}