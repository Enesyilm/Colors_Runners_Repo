using System;
using System.Collections.Generic;
using Extentions.Interfaces;

namespace Extentions
{
    public class StateMachine
    {
        private IState _currentState;
        private List<Transition> _currentTransitions = new List<Transition>();
        private List<Transition> _anyTransitions = new List<Transition>();
        private static  List<Transition> _emptyTransitions = new List<Transition>();

        private class Transition
    {
        public Func<bool> Condition { get; }
        public IState To { get; }
        
        public Transition(Func<bool> condition,IState to)
        {
            Condition = condition;
            To=to;
        }

        public void Setup()
        {
            
        }
        public void SetState()
        {
            
        }
        public void AddTranssition()
        {
            
        }
        public void AddAnyTransation()
        {
            
        }
        public Transition GetTransation()
        {
            return null;
        }
        
    }
    }
}