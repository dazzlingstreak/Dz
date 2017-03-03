using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Pattern.Observer
{
    abstract class AbstractSubject
    {
        public abstract void Accept(AbstractObserver observer);

        public abstract void Remove(AbstractObserver observer);

        public abstract void Notify();
    }

    class ConcreteSubject : AbstractSubject
    {
        private List<AbstractObserver> observers = new List<AbstractObserver>();

        public override void Accept(AbstractObserver observer)
        {
            observers.Add(observer);
        }

        public override void Remove(AbstractObserver observer)
        {
            observers.Remove(observer);
        }

        public override void Notify()
        {
            foreach (var item in observers)
            {
                item.Update();
            }
        }
    }
}
