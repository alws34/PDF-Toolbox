using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PDFToolbox.Common
{
    internal static class UIHelper
    {
        internal static UIElement GetUIElement(ItemsControl container, Point position)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (position == null) throw new ArgumentNullException("position");

            UIElement elementAtPosition = (UIElement)container.InputHitTest(position);
            object testElement;

            if(elementAtPosition!=null)
            {
                while(elementAtPosition!=null)
                {
                    testElement = container.ItemContainerGenerator.ItemFromContainer(elementAtPosition);
                    if(testElement != DependencyProperty.UnsetValue)
                    {
                        break;
                    }
                    else
                    {
                        elementAtPosition = (UIElement) VisualTreeHelper.GetParent(elementAtPosition);
                    }
                }
            }
            return elementAtPosition;
        }

        internal static bool IsPositionAboveElement(UIElement element, Point relativePosition)
        {
            if (element == null) throw new ArgumentNullException("element");
            if (relativePosition == null) throw new ArgumentNullException("relativePosition");

            return relativePosition.Y < ((FrameworkElement)element).ActualHeight / 2;
        }
    }
}
