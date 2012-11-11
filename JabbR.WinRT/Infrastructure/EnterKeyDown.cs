using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace JabbR.WinRT.Infrastructure
{
    public sealed class EnterKeyDown
    {
        #region Properties

        #region Command

        /// <summary>
        /// GetCommand
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        /// <summary>
        /// SetCommand
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        /// <summary>
        /// DependencyProperty CommandProperty
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(EnterKeyDown), new PropertyMetadata(null, OnCommandChanged));

        #endregion Command

        #region CommandParameter

        /// <summary>
        /// GetCommandParameter
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object GetCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(CommandParameterProperty);
        }

        /// <summary>
        /// SetCommandParameter
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// DependencyProperty CommandParameterProperty
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(EnterKeyDown), new PropertyMetadata(null, OnCommandParameterChanged));

        #endregion CommandParameter

        #region HasCommandParameter

        private static bool GetHasCommandParameter(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasCommandParameterProperty);
        }

        private static void SetHasCommandParameter(DependencyObject obj, bool value)
        {
            obj.SetValue(HasCommandParameterProperty, value);
        }

        private static readonly DependencyProperty HasCommandParameterProperty =
            DependencyProperty.RegisterAttached("HasCommandParameter", typeof(bool), typeof(EnterKeyDown), new PropertyMetadata(false));

        #endregion HasCommandParameter

        #endregion Propreties

        #region Event Handling

        private static void OnCommandParameterChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            SetHasCommandParameter(o, true);
        }

        private static void OnCommandChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = o as FrameworkElement;
            if (element != null)
            {
                if (e.NewValue == null)
                {
                    element.KeyUp -= FrameworkElementKeyUp;
                }
                else if (e.OldValue == null)
                {
                    element.KeyUp += FrameworkElementKeyUp;
                }
            }
        }

        private static void FrameworkElementKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                var o = sender as DependencyObject;
                var command = GetCommand(sender as DependencyObject);

                var element = e.OriginalSource as FrameworkElement;
                if (element != null)
                {
                    // If the command argument has been explicitly set (even to NULL) 
                    if (GetHasCommandParameter(o))
                    {
                        var commandParameter = GetCommandParameter(o);

                        // Execute the command 
                        if (command.CanExecute(commandParameter))
                        {
                            command.Execute(commandParameter);
                        }
                    }
                    else if (command.CanExecute(element.DataContext))
                    {
                        command.Execute(element.DataContext);
                    }
                }
            }
        }

        #endregion
    }
}
