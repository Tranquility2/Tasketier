using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace tesketier
{
    public partial class TaskPanel : UserControl
    {
        public List<Button> buttonList { get; private set; } = new List<Button>();

        public TaskPanel()
        {
            InitializeComponent();
            
            this.AutoSize = true;
        }

        public void AddButton(string text, Image image)
        {
            Button newButton = new Button();

            newButton.Text = text;
            newButton.BackgroundImage = image;
            newButton.BackgroundImageLayout = ImageLayout.Center;
            newButton.Size = new Size(100, 100);
            
            newButton.Click += new EventHandler(NewButton_Click);
            newButton.FlatStyle = FlatStyle.Flat;

            buttonList.Add(newButton);
            this.Controls.Add(newButton);

            this.RearrangeButtonsLocation();
            this.Refresh();
        }

        public void RemoveButton(Button button)
        {
            buttonList.Remove(button);
            this.Controls.Remove(button);

            this.RearrangeButtonsLocation();
            this.Refresh();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            // TODO add real usage
            Button currentButton = sender as Button;

            Console.WriteLine("removing {0} @ {1}", buttonList.IndexOf(currentButton), currentButton.Location.ToString());

            RemoveButton(sender as Button);
        }

        public void RearrangeButtonsLocation()
        {
            Button previousButton = new Button();
            Point newPoint = new Point(0, 0);
            int size_counter = 0;
            int maxSize = (int) Math.Ceiling(Math.Sqrt(buttonList.Count()));

            Console.WriteLine("maxSize = {0}", maxSize);

            foreach (Button button in buttonList)
            {
                button.Location = newPoint;

                newPoint.Y += button.Size.Height;

                size_counter++;
                if (size_counter == maxSize)
                {
                    newPoint.Y = 0;
                    newPoint.X += button.Size.Width;
                    size_counter = 0;
                }
            }
        }
    }
}
