using System;

public class SwitcherLink
{
    private readonly Action<Action> _subscribe;
    private readonly Action<Action> _unsubscribe;
    private readonly Action _method;
    private bool _isLinked;
    public bool IsLinked
    {
        get => _isLinked;
        set
        {
            if (value == IsLinked) return;
            
            if (value)
                _subscribe(_method);
            else
                _unsubscribe(_method);

            _isLinked = value;
        }
    }

    public SwitcherLink(Action method, Action<Action> subscribe, Action<Action> unsubscribe)
    {
        _method = method ?? throw new ArgumentNullException(nameof(method));
        _subscribe = subscribe ?? throw new ArgumentNullException(nameof(subscribe));
        _unsubscribe = unsubscribe ?? throw new ArgumentNullException(nameof(unsubscribe));
    }
}