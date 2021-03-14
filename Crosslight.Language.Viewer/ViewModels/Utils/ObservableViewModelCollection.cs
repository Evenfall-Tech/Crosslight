using Crosslight.Language.Viewer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Crosslight.Language.Viewer.ViewModels.Utils
{
    // From https://stackoverflow.com/a/15831128/2832341
    public class ObservableViewModelCollection<TVM, UM> : ObservableCollection<TVM>
        where TVM : ViewModelBase, IViewModelFor<UM>
        where UM : ModelBase
    {
        private readonly ICollection<UM> models;
        private readonly IViewModelFactory<TVM, UM> viewModelFactory;
        private bool synchDisabled;

        public ObservableViewModelCollection(
            ICollection<UM> models,
            IViewModelFactory<TVM, UM> viewModelFactory,
            bool autoFetch = true)
            : base()
        {
            this.models = models;
            this.viewModelFactory = viewModelFactory;

            CollectionChanged += ViewModelCollectionChanged;
            if (models is ObservableCollection<UM>)
            {
                var observableModels = models as ObservableCollection<UM>;
                observableModels.CollectionChanged += ModelCollectionChanged;
            }
            if (autoFetch) FetchFromModels();
        }

        public override sealed event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { base.CollectionChanged += value; }
            remove { base.CollectionChanged -= value; }
        }

        public void FetchFromModels()
        {
            synchDisabled = true;
            Clear();
            foreach (var model in models)
                AddForModel(model);
            synchDisabled = false;
        }

        private void ViewModelCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (synchDisabled) return;
            synchDisabled = true;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var m in e.NewItems.OfType<TVM>().Select(v => v.Model))
                        models.Add(m);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var m in e.OldItems.OfType<TVM>().Select(v => v.Model))
                        models.Remove(m);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    models.Clear();
                    foreach (var m in e.NewItems.OfType<TVM>().Select(v => v.Model))
                        models.Add(m);
                    break;
            }
            synchDisabled = false;
        }

        private void ModelCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (synchDisabled) return;
            synchDisabled = true;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var m in e.NewItems.OfType<UM>())
                        AddIfNotNull(CreateViewModel(m));
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var m in e.OldItems.OfType<UM>())
                        RemoveIfContains(GetViewModelOfModel(m));
                    break;

                case NotifyCollectionChangedAction.Reset:
                    Clear();
                    FetchFromModels();
                    break;
            }
            synchDisabled = false;
        }

        private void AddIfNotNull(TVM tVM)
        {
            if (tVM != null) Items.Add(tVM);
        }

        private void RemoveIfContains(TVM tVM)
        {
            if (tVM != null && Items.Contains(tVM)) Items.Remove(tVM);
        }

        private TVM CreateViewModel(UM model)
        {
            return viewModelFactory.Get(model);
        }

        private TVM GetViewModelOfModel(UM model)
        {
            return Items.FirstOrDefault(v => v.IsViewModelOf(model));
        }

        public void AddForModel(UM model)
        {
            Add(CreateViewModel(model));
        }
    }
}
