﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MFAAvalonia.Helper.ValueType;

public partial class ObservableQueue<T> : ObservableObject
{
    private readonly Queue<T> _queue = new();

    [ObservableProperty] private int _count;

    public EventHandler<CountChangedEventArgs>? CountChanged;

    public ObservableQueue()
    {
        Count = _queue.Count;
    }
    partial void OnCountChanged(int oldValue, int newValue)
    {
        CountChanged?.Invoke(this, new CountChangedEventArgs(oldValue, newValue));
    }
    
    public void Enqueue(T task)
    {
        _queue.Enqueue(task);
        Count = _queue.Count;
    }

    public T Dequeue()
    {
        var task = _queue.Dequeue();
        Count = _queue.Count;
        return task;
    }

    public void Clear()
    {
        _queue.Clear();
        Count = _queue.Count;
    }
    public bool Any()
    {
        return _queue.Any();
    }
    
    public bool Any(Func<T, bool> predicate)
    {
        return _queue.Any(predicate);
    }
    
    public class CountChangedEventArgs(int oldValue, int newValue) : EventArgs
    {
        public int OldValue => oldValue;
        public int NewValue => newValue;
    }
}
