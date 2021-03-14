using System;
using System.Collections.Generic;

namespace Crosslight.API.Util
{
    public class SyncedProperty<TPub, TSub> where TPub : TSub
    {
        private TPub property;
        private readonly IList<TSub> subscriber;

        /// <summary>
        /// Initializes a new instance of the <seealso cref="SyncedProperty{TPub, TSub}"/> class that
        /// is empty and has the default initial value.
        /// </summary>
        /// <param name="subscriber">
        /// The collection that subscribes to changes in <seealso cref="SyncedProperty{TPub, TSub}"/>.
        /// </param>
        public SyncedProperty(IList<TSub> subscriber) : this(default, subscriber)
        { }

        /// <summary>
        /// Initializes a new instance of the <seealso cref="SyncedProperty{TPub, TSub}"/> class that
        /// contains <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The initial property value.</param>
        /// <param name="subscriber">
        /// The collection that subscribes to changes in <seealso cref="SyncedProperty{TPub, TSub}"/>.
        /// </param>
        public SyncedProperty(TPub value, IList<TSub> subscriber)
        {
            property = value;
            this.subscriber = subscriber ?? throw new ArgumentNullException(nameof(subscriber));
            if (property != null) this.subscriber.Add(property);
        }

        public TPub Value
        {
            get => property;
            set
            {
                subscriber.Remove(property);
                property = value;
                subscriber.Add(property);
            }
        }
    }
}
