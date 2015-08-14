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
        private Queue<string> messageLog;
        private int maxLogSize;
        private int logPointer;

        public Gui()
        {
            InitializeComponent();
            logic = new GameLogic();
            inputText.KeyUp += new KeyEventHandler(MessageText_KeyUp);

            messageLog = new Queue<string>();
            maxLogSize = 10;
            logPointer = 0;

            string intro = logic.getIntro();
            displayText(intro);

            inputText.Focus();
        }

        public void displayText(string toDisplay)
        {
            AppendLineToChatBox(toDisplay);
        }

        /// <summary>
        /// Add the provided message to the message log
        /// </summary>
        /// <param name="message"></param>
        private void AddMessageToLog(string message)
        {
            messageLog.Enqueue(message);

            while (messageLog.Count > maxLogSize)
            {
                messageLog.Dequeue();
            }

            logPointer = messageLog.Count - 1;
        }

        /// <summary>
        /// Get the previously selected message in the log
        /// </summary>
        /// <param name="message"></param>
        private void RestorePreviousMessage()
        {
            if (messageLog.Count == 0)
            {
                return;
            }

            string previousMessage = messageLog.ElementAt(logPointer);
            inputText.Text = previousMessage;

            logPointer--;
            if (logPointer < 0)
            {
                logPointer = 0;
            }
        }

        /// <summary>
        /// Get the following selected message in the log
        /// </summary>
        /// <param name="message"></param>
        private void RestoreNextMessage()
        {
            if (messageLog.Count == 0)
            {
                return;
            }

            logPointer++;
            if (logPointer >= messageLog.Count)
            {
                logPointer = messageLog.Count - 1;
            }

            string nextMessage = messageLog.ElementAt(logPointer);
            inputText.Text = nextMessage;
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

            AddMessageToLog(message);

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

            if (e.Key == Key.Up)
            {
                RestorePreviousMessage();
            }

            if (e.Key == Key.Down)
            {
                RestoreNextMessage();
            }
        }
    }
}
