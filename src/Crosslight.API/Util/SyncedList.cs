using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Crosslight.API.Util
{
    public class SyncedList<TPub, TSub> : IList<TPub> where TPub : TSub
    {
        protected readonly IList<TPub> publisher;
        protected readonly IList<TSub> subscriber;

        /// <summary>
        /// Initializes a new instance of the <seealso cref="SyncedList{TPub, TSub}"/> class that
        /// is empty and has the default initial capacity.
        /// </summary>
        /// <param name="subscriber">
        /// The collection that subscribes to changes in <seealso cref="SyncedList{TPub, TSub}"/>.
        /// </param>
        public SyncedList(IList<TSub> subscriber) : this(new List<TPub>(), subscriber)
        { }

        /// <summary>
        /// Initializes a new instance of the <seealso cref="SyncedList{TPub, TSub}"/> class that
        /// contains elements copied from the specified collection and has sufficient capacity
        /// to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        /// <param name="subscriber">
        /// The collection that subscribes to changes in <seealso cref="SyncedList{TPub, TSub}"/>.
        /// </param>
        public SyncedList(IEnumerable<TPub> collection, IList<TSub> subscriber)
        {
            publisher = collection.ToList();
            this.subscriber = subscriber ?? throw new ArgumentNullException(nameof(subscriber));
        }

        /// <summary>
        /// Initializes a new instance of the <seealso cref="SyncedList{TPub, TSub}"/> class that
        /// is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">
        /// The number of elements that the new list can initially store.
        /// </param>
        /// <param name="subscriber">
        /// The collection that subscribes to changes in <seealso cref="SyncedList{TPub, TSub}"/>.
        /// </param>
        public SyncedList(int capacity, IList<TSub> subscriber) : this(new List<TPub>(capacity), subscriber)
        { }
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public TPub this[int index]
        {
            get => publisher[index];
            set
            {
                subscriber.Remove(publisher[index]);
                publisher[index] = value;
                subscriber.Add(value);
            }
        }

        public int Count => publisher.Count;

        public bool IsReadOnly => publisher.IsReadOnly;

        public void Add(TPub item)
        {
            publisher.Add(item);
            subscriber.Add(item);
        }

        public void Clear()
        {
            foreach (var item in publisher)
                subscriber.Remove(item);
            publisher.Clear();
        }

        public bool Contains(TPub item)
        {
            return publisher.Contains(item);
        }

        public void CopyTo(TPub[] array, int arrayIndex)
        {
            publisher.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TPub> GetEnumerator()
        {
            return publisher.GetEnumerator();
        }

        public int IndexOf(TPub item)
        {
            return publisher.IndexOf(item);
        }

        public void Insert(int index, TPub item)
        {
            publisher.Insert(index, item);
            subscriber.Add(item);
        }

        public bool Remove(TPub item)
        {
            subscriber.Remove(item);
            return publisher.Remove(item);
        }

        public void RemoveAt(int index)
        {
            TPub item = this[index];
            subscriber.Remove(item);
            publisher.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)publisher).GetEnumerator();
        }
    }
}
