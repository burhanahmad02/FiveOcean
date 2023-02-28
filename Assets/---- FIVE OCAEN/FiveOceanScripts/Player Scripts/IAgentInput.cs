using System;

internal interface IAgentInput
{
    event Action OnAttack;
    event Action OnJumpPressed;
    event Action OnJumpReleased;
    event Action OnWeaponChange;
}