namespace Plarium.Assets.GameCore.Events
{
    public class PlayerInputEventParams : BaseEventParams
    {
        public string PlayerInput { get; }

        public PlayerInputEventParams(string playerInput)
        {
            PlayerInput = playerInput;
        }
    }
}
