using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Input;

namespace PDFToolbox.Behaviors
{
    public class DropBehavior : Behavior<FrameworkElement>
    {
        private Type dataType;

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
            if (dataType == null) return;

            IDropable target;
            IDragable source;

            
            if (e.Data.GetDataPresent(dataType))
            {
                target = (IDropable)AssociatedObject.DataContext;
                source = (IDragable)e.Data.GetData(dataType);
                source.Remove(e.Data.GetData(dataType));
            }
            
            e.Handled = true;
        }

        void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            if (dataType == null) return;

            if(e.Data.GetDataPresent(dataType))
            {
                this.SetDragDropEffects(e);

            }
            e.Handled = true;
        }

        void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            IDropable dropObject;

            if(dataType==null)
            {
                if(AssociatedObject.DataContext!=null)
                {
                    dropObject = (IDropable)AssociatedObject.DataContext;
                    if(dropObject!=null)
                    {
                        dataType = dropObject.DataType;
                    }
                }
            }
            e.Handled = true;
        }

        private void SetDragDropEffects(DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;

            if (dataType == null) return;

            if (e.Data.GetDataPresent(dataType))
            {
                e.Effects = DragDropEffects.Move;
            }
        }
    }
}
