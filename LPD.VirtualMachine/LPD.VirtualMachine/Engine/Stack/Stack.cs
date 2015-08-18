using System;

namespace LPD.VirtualMachine.Engine
{
    /// <summary>
    /// Represents the region where the program data will be stored.
    /// </summary>
    public sealed class Stack
    {
        /// <summary>
        /// The top of the stack.
        /// </summary>
        private int _top;
        /// <summary>
        /// The size of the stack.
        /// </summary>
        private int _size;
        /// <summary>
        /// The internal stack.
        /// </summary>
        private int[] _array;

        /// <summary>
        /// Initializes a new instance of the <see cref="Stack"/> class with the specified size, in bytes.
        /// </summary>
        /// <param name="size">The size, in bytes, of the stack.</param>
        public Stack(int size)
        {
            _top = 0;
            _size = size;
            _array = new int[size];
        }

        /// <summary>
        /// Raised when the stack changes.
        /// </summary>
        public event EventHandler<StackChangedEventArgs> Changed;

        /// <summary>
        /// Gets the current position of the stack.
        /// </summary>
        public int Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Clears all the contents of the stack.
        /// </summary>
        public void Clear()
        {
            _array = null;
            _array = new int[_size];
            NotifyStackChanged(StackChangedReason.Cleared);
        }

        /// <summary>
        /// Gets an item from the current position.
        /// </summary>
        /// <returns>The item stored at the current position.</returns>
        public int Load()
        {
            //TODO - tratar o erro e não deixar o programa quebrar...
            //-1 is the lowest stack position.
            if (_top == -1)
            {                
                throw new ExecutionException("The top of the stack is already at the lowest position.");
            }
            return _array[_top];
        }

        /// <summary>
        /// Gets an item from the specified position.
        /// </summary>
        /// <param name="position">The position to load the item from.</param>
        /// <returns>The item stored at the current position.</returns>
        public int LoadFrom(int position)
        {
            return _array[position];
        }

        /// <summary>
        /// Stores an item on the specified position.
        /// </summary>
        /// <param name="position">The position to store the value.</param>
        /// <param name="item">The item to store at the current position.</param>
        public void StoreAt(int position, int item)
        {
            _array[position] = item;
            NotifyStackChanged(StackChangedReason.Pushed);
        }

        /// <summary>
        /// Stores an item on the current position.
        /// </summary>
        /// <param name="item">The item to store at the current position.</param>
        public void Store(int item)
        {
            _array[_top] = item;
            NotifyStackChanged(StackChangedReason.Pushed);
        }

        /// <summary>
        /// Sets the stack to the specified position.
        /// </summary>
        /// <param name="position">The position to set the top of the stack.</param>
        /// <returns>The position before seeking.</returns>
        public int Seek(int position)
        {
            int oldValue = _top;

            ThrownOnIndexOutOfRange(position);
            _top = position;
            return oldValue;
        }

        /// <summary>
        /// Increments the top of the stack by one position.
        /// </summary>
        public void Up()
        {
            if(_top == _size)
            {
                throw new InvalidOperationException("The top of the stack is already at the highest position.");
            }

            _top++;
        }

        /// <summary>
        /// Decrements the top of the stack by one position.
        /// </summary>
        public void Down()
        {
            //-1 is the lowest stack position.
            if (_top == -1)
            {
                throw new InvalidOperationException("The top of the stack is already at the lowest position.");
            }

            NotifyStackChanged(StackChangedReason.Popped);
            _top--;
        }

        /// <summary>
        /// Gets the stack's top position and the value at this position as a string;
        /// </summary>
        /// <returns>A string that represents the stack's top position and the value at this position.</returns>
        public override string ToString()
        {
            return $"[{_top.ToString()}] = {_array[_top]}";
        }

        /// <summary>
        /// Throws an exception if a given position is greater than the maximum stack size 
        /// or less than the minimum stack size.
        /// </summary>
        /// <param name="position">The stack's top position.</param>
        private void ThrownOnIndexOutOfRange(int position)
        {
            if (position < -1 || position > _size)
            {
                throw new IndexOutOfRangeException(nameof(position));
            }
        }

        /// <summary>
        /// Notifies this stack has changed.
        /// </summary>
        /// <param name="reason">Why the stack changed.</param>
        private void NotifyStackChanged(StackChangedReason reason)
        {
            if (Changed != null)
            {
                Changed(this, new StackChangedEventArgs(reason));
            }
        }
    }
}
