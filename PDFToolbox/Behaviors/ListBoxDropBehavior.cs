using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Controls;

using PDFToolbox.Common;

namespace PDFToolbox.Behaviors
{
    public class ListBoxDropBehavior : Behavior<ItemsControl>
    {
        private Type _droppableType;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.AllowDrop = true;
            AssociatedObject.DragEnter += new DragEventHandler(AssociatedObject_DragEnter);
            AssociatedObject.DragOver += new DragEventHandler(AssociatedObject_DragOver);
            AssociatedObject.DragLeave += new DragEventHandler(AssociatedObject_DragLeave);
            AssociatedObject.Drop += new DragEventHandler(AssociatedObject_Drop);
        }

        void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            ItemsControl dropContainer;
            UIElement droppedOverElement;
            int dropIndex;
            IDragable source;
            IDropable target;

            if(_droppableType!=null)
            {
                if(e.Data.GetDataPresent(_droppableType))
                {
                    dropContainer = (ItemsControl)sender;
                    droppedOverElement = UIHelper.GetUIElement(dropContainer, e.GetPosition(dropContainer));
                    dropIndex = dropContainer.ItemContainerGenerator.IndexFromContainer(droppedOverElement) + 1;

                    source = (IDragable)e.Data.GetData(_droppableType);
                    source.Remove(e.Data.GetData(_droppableType));

                    target = (IDropable)AssociatedObject.DataContext;
                    target.Drop(e.Data.GetData(_droppableType), dropIndex);
                }

                e.Handled = true;
            }
        }

        void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            if (_droppableType != null)
            {
                if(e.Data.GetDataPresent(_droppableType))
                {
                    SetDragDropEffects(e);
                    
                }
            }
            e.Handled = true;

        }

        void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            if(_droppableType == null)
            {
                if(AssociatedObject.DataContext!=null)
                {
                    if(AssociatedObject.DataContext as IDropable != null)
                    {
                        _droppableType = ((IDropable)AssociatedObject.DataContext).DataType;
                    }
                }
            }

            e.Handled = true;
        }

        private void SetDragDropEffects(DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;  //default to None

            if (e.Data.GetDataPresent(_droppableType))
            {
                e.Effects = DragDropEffects.Move;
            }
        }

    }
}
