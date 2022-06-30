public void ActivateEffects()
{
    _enable = true;
    _effects.StartEnableAnimation();
}

public void DeactivateEffects()
{
    _enable = false;
    _pool.Free(this);
}