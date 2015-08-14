using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameJam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Gui : Window
    {
        private GameLogic logic;

        public Gui()
        {
            InitializeComponent();
            logic = new GameLogic();
        }

        public void displayText(string toDisplay)
        {
            AppendLineToChatBox(toDisplay);
        }

        /// <summary>
        /// Append the provided message to the chatBox text box.
        /// </summary>
        /// <param name="message"></param>
        private void AppendLineToChatBox(string message)
        {
            //To ensure we can successfully append to the text box from any thread
            //we need to wrap the append within an invoke action.
            outputBox.Dispatcher.BeginInvoke(new Action<string>((messageToAdd) =>
            {
                outputBox.AppendText(messageToAdd + "\n");
                outputBox.ScrollToEnd();
            }), new object[] { message });
        }

        /// <summary>
        /// Process the entered message
        /// </summary>
        private void ProcessMessage()
        {
            string message = inputText.Text;
            inputText.Text = string.Empty;

            displayText("> " + message);

            string output = logic.processMessage(message);

            displayText(output);
        }

        /// <summary>
        /// Send any entered message when we click the send button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessMessage();
        }

        /// <summary>
        /// Send any entered message when we press enter or return
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                ProcessMessage();
            }
        }
    }
}
