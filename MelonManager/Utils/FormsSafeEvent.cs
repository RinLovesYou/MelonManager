using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonManager.Managers;

namespace MelonManager.Utils
{
    public class FormsSafeEventBase<T> where T : Delegate
    {
        private List<T> subscribtions = new List<T>();
        private List<Tuple<T, Control>> formsSubscribtions = new List<Tuple<T, Control>>();
        private List<T> subscribtionsToRemove = new List<T>();
        private List<Tuple<T, Control>> formsSubscribtionsToRemove = new List<Tuple<T, Control>>();
        private bool invoking;

        public virtual void Invoke(params object[] args)
        {
            invoking = true;
            lock (subscribtions)
                foreach (var s in subscribtions)
                {
                    s.DynamicInvoke(args);
                }

            lock (formsSubscribtions)
                foreach (var s in formsSubscribtions)
                {
                    s.Item2.Invoke(s.Item1, args);
                }
            invoking = false;

            foreach (var s in subscribtionsToRemove)
                Unsubscribe(s);
            foreach (var s in formsSubscribtionsToRemove)
                Unsubscribe(s.Item1, s.Item2);
            subscribtionsToRemove.Clear();
            formsSubscribtionsToRemove.Clear();
        }

        public void Subscribe(T action)
        {
            lock (subscribtions)
            {
                if (subscribtions.Contains(action))
                    return;

                subscribtions.Add(action);
            }
        }

        public void Subscribe(T action, Control control)
        {
            lock (formsSubscribtions)
            {
                var tuple = new Tuple<T, Control>(action, control);
                if (formsSubscribtions.Contains(tuple))
                    return;

                if (control is Form form)
                    form.FormClosed += (s, e) => Unsubscribe(action, control); // had to implement this cuz for some reason some forms are too lazy to dispose themselves
                else
                    control.Disposed += (s, e) => Unsubscribe(action, control);
                formsSubscribtions.Add(tuple);
            }
        }

        public void Unsubscribe(T action, Control control)
        {
            lock (formsSubscribtions)
            {
                var tuple = new Tuple<T, Control>(action, control);
                if (invoking)
                {
                    formsSubscribtionsToRemove.Add(tuple);
                    return;
                }
                formsSubscribtions.Remove(tuple);
            }
        }

        public void Unsubscribe(T action)
        {
            lock (subscribtions)
            {
                if (invoking)
                {
                    subscribtionsToRemove.Add(action);
                    return;
                }
                subscribtions.Remove(action);
            }
        }
    }

    public sealed class FormsSafeEvent : FormsSafeEventBase<Action> 
    {
        public void Invoke()
        {
            base.Invoke();
        }
    }
    public sealed class FormsSafeEvent<T> : FormsSafeEventBase<Action<T>> 
    {
        public void Invoke(T arg)
        {
            base.Invoke(arg);
        }
    }
    public sealed class FormsSafeEvent<T, T2> : FormsSafeEventBase<Action<T, T2>> 
    {
        public void Invoke(T arg, T2 arg2)
        {
            base.Invoke(arg, arg2);
        }
    }
    public sealed class FormsSafeEvent<T, T2, T3> : FormsSafeEventBase<Action<T, T2, T3>>
    {
        public void Invoke(T arg, T2 arg2, T3 arg3)
        {
            base.Invoke(arg, arg2, arg3);
        }
    }
}
