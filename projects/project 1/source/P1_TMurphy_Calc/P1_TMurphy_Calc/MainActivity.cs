using Android.App;
using Android.Widget;
using Android.OS;
using System;
//using System.Text.RegularExpressions;

namespace P1_TMurphy_Calc
{
    [Activity(Label = "P1_TMurphy_Calc", MainLauncher = true, Icon = "@mipmap/icon")] //@mipmap/icon //@drawable/icon
    public class MainActivity : Activity
    {
        //double outputValue = 0;
        int count = 1;
        bool clear = true;
        bool allClear = true;
        bool containsDec = false;
        string displayText = "0.000";
        string prevOperand = null;
        string currOperand = null;
        int lastOp = 0;


        protected override void OnCreate(Bundle savedInstanceState) //savedInstanceState //bundle
        {
            base.OnCreate(savedInstanceState); //savedInstanceState //bundle

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var gridView = FindViewById<GridView>(Resource.Id.gridView);
            gridView.Adapter = new ImageAdapter(this);
            gridView.ItemClick += GridView_ItemClick;


            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.calcButton);
            // button.AfterTextChanged += CalculateInput;
            // button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            // button.Click += delegate { CalculateInput; };
            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            button.AfterTextChanged += CalculateInput;
            //button.Click += CalculateInput;

            // Get our textfield from the layout resource, 
            // so we can pull values from it

            //TextView display = FindViewById<TextView>(Resource.Id.calcDisplay);
            //display.Text = 


        }

        private void GridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //throw new NotImplementedException();
            //Toast.MakeText(this, "Position: " + e.Position, ToastLength.Short).Show();

            TextView display = FindViewById<TextView>(Resource.Id.calcDisplay);

            if (e.Position == 0) // "ALL CLEAR"
            {
                displayText = "0";
                display.Text = displayText;
                clear = true;
                allClear = true;
                containsDec = false;
                lastOp = 0;
                prevOperand = null;
                currOperand = null;
                //Toast.MakeText(this, "ALL CLEAR", ToastLength.Short).Show();
            }
            else if (e.Position == 1) // "CLEAR"
            {
                displayText = "0";
                display.Text = displayText;
                clear = true;
                containsDec = false;
                currOperand = null;
                //Toast.MakeText(this, "CLEAR", ToastLength.Short).Show();
            }
            else if (e.Position == 2) // "PERCENTAGE"
            {
                Toast.MakeText(this, "PERCENTAGE", ToastLength.Short).Show();
            }
            else if (e.Position == 3) // "DIVISION"
            {
                lastOp = 1; // div
                if (prevOperand == null) // 1st operand not yet set
                {
                    prevOperand = displayText;
                }
                else if (currOperand == null) // 1st operand already set, 2nd operand not yet set
                {
                    currOperand = displayText;
                }
                else // 1st and 2nd operands are set, calculate the result
                {
                    Toast.MakeText(this, "DIVISION", ToastLength.Short).Show();
                }
                clear = true;
            }
            else if (e.Position == 4) // 7
            {
                if (clear == true) // replace current display w/ '7'
                {
                    clear = false;
                    displayText = "7";
                    display.Text = displayText;
                }
                else // append '7' to current number
                {
                    displayText = displayText + "7";
                    display.Text = displayText;
                }

                //Toast.MakeText(this, "7", ToastLength.Short).Show();
            }
            else if (e.Position == 5) // 8
            {
                if (clear == true) // replace current display w/ '8'
                {
                    clear = false;
                    displayText = "8";
                    display.Text = displayText;
                }
                else // append '8' to current number
                {
                    displayText = displayText + "8";
                    display.Text = displayText;
                }

                //Toast.MakeText(this, "8", ToastLength.Short).Show();
            }
            else if (e.Position == 6) // 9
            {
                if (clear == true) // replace current display w/ '9'
                {
                    clear = false;
                    displayText = "9";
                    display.Text = displayText;
                }
                else // append '9' to current number
                {
                    displayText = displayText + "9";
                    display.Text = displayText;
                }

                //Toast.MakeText(this, "9", ToastLength.Short).Show();
            }
            else if (e.Position == 7) // "MULTIPLICATION"
            {
                lastOp = 2; // multi
                if (prevOperand == null) // 1st operand not yet set
                {
                    prevOperand = displayText;
                }
                else if (currOperand == null) // 1st operand already set, 2nd operand not yet set
                {
                    currOperand = displayText;
                }
                else // 1st and 2nd operands are set, calculate the result
                {
                    Toast.MakeText(this, "MULTIPLICATION", ToastLength.Short).Show();
                }
                clear = true;
            }
            else if (e.Position == 8) // 4
            {
                if (clear == true) // replace current display w/ '4'
                {
                    clear = false;
                    displayText = "4";
                    display.Text = displayText;
                }
                else // append '4' to current number
                {
                    displayText = displayText + "4";
                    display.Text = displayText;
                }

                //Toast.MakeText(this, "4", ToastLength.Short).Show();
            }
            else if (e.Position == 9) // 5
            {
                if (clear == true) // replace current display w/ '5'
                {
                    clear = false;
                    displayText = "5";
                    display.Text = displayText;
                }
                else // append '5' to current number
                {
                    displayText = displayText + "5";
                    display.Text = displayText;
                }

                //Toast.MakeText(this, "5", ToastLength.Short).Show();
            }
            else if (e.Position == 10) // 6
            {
                if (clear == true) // replace current display w/ '6'
                {
                    clear = false;
                    displayText = "6";
                    display.Text = displayText;
                }
                else // append '6' to current number
                {
                    displayText = displayText + "6";
                    display.Text = displayText;
                }

                //Toast.MakeText(this, "6", ToastLength.Short).Show();
            }
            else if (e.Position == 11) // "SUBTRACTION"
            {
                lastOp = 3; // sub
                if (prevOperand == null) // 1st operand not yet set
                {
                    prevOperand = displayText;
                }
                else if (currOperand == null) // 1st operand already set, 2nd operand not yet set
                {
                    currOperand = displayText;
                }
                else // 1st and 2nd operands are set, calculate the result
                {
                    Toast.MakeText(this, "SUBTRACTION", ToastLength.Short).Show();
                }
                clear = true;
            }
            else if (e.Position == 12) // 1
            {
                if (clear == true) // replace current display w/ '1'
                {
                    clear = false;
                    displayText = "1";
                    display.Text = displayText;
                }
                else // append '1' to current number
                {
                    displayText = displayText + "1";
                    display.Text = displayText;
                }

                //Toast.MakeText(this, "1", ToastLength.Short).Show();
            }
            else if (e.Position == 13) // 2
            {
                if (clear == true) // replace current display w/ '2'
                {
                    clear = false;
                    displayText = "2";
                    display.Text = displayText;
                }
                else // append '2' to current number
                {
                    displayText = displayText + "2";
                    display.Text = displayText;
                }

                //Toast.MakeText(this, "2", ToastLength.Short).Show();
            }
            else if (e.Position == 14) // 3
            {
                if (clear == true) // replace current display w/ '3'
                {
                    clear = false;
                    displayText = "3";
                    display.Text = displayText;
                }
                else // append '3' to current number
                {
                    displayText = displayText + "3";
                    display.Text = displayText;
                }

                //Toast.MakeText(this, "3", ToastLength.Short).Show();
            }
            else if (e.Position == 15) // "ADDITION"
            {
                lastOp = 4; // add
                if (prevOperand == null) // 1st operand not yet set
                {
                    prevOperand = displayText;
                }
                else if (currOperand == null) // 1st operand already set, 2nd operand not yet set
                {
                    currOperand = displayText;
                }
                else // 1st and 2nd operands are set, calculate the result
                {
                    Toast.MakeText(this, "ADDITION", ToastLength.Short).Show();
                }
                clear = true;
            }
            else if (e.Position == 16) // "POSITIVE / NEGATIVE"
            {
                Toast.MakeText(this, "POSITIVE / NEGATIVE", ToastLength.Short).Show();
            }
            else if (e.Position == 17) // 0
            {
                if (clear == true) // replace current display w/ '0'
                {
                    clear = false;
                    displayText = "0";
                    display.Text = displayText;
                }
                else // append '0' to current number
                {
                    displayText = displayText + "0";
                    display.Text = displayText;
                }

                //Toast.MakeText(this, "0", ToastLength.Short).Show();
            }
            else if (e.Position == 18) // "DECIMAL POINT"
            {
                if (clear == true) // replace current display w/ '0.'
                {
                    clear = false;
                    containsDec = true;
                    displayText = "0.";
                    display.Text = displayText;
                }
                else if (containsDec == false) // append '.' to current number
                {
                    containsDec = true;
                    displayText = displayText + ".";
                    display.Text = displayText;
                }

                //Toast.MakeText(this, "DECIMAL POINT", ToastLength.Short).Show();
            }
            else if (e.Position == 19) // "CALCULATE"
            {
                currOperand = displayText;
                Toast.MakeText(this, "Prev = " + prevOperand + ", Curr = " + currOperand, ToastLength.Short).Show();
                prevOperand = currOperand;
                //currOperand = result;

                //Toast.MakeText(this, "CALCULATE", ToastLength.Short).Show();
            }
            else // Error
            {
                // The grid item clicked does not match one of the 20 initialized: {0-19}
                Toast.MakeText(this.ApplicationContext, "AN ERROR HAS OCCURED.", ToastLength.Short).Show();
            }
        }

        //private void CalculateInput(object sender, Android.Text.AfterTextChangedEventArgs e)
        private void CalculateInput(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            // throw new System.NotImplementedException();

            // https://forums.xamarin.com/discussion/71735/how-to-display-a-message-box-or-alert-message-in-c-xamarin-android
            // Toast.MakeText(this.ApplicationContext, "Let's try to calc stuff..", ToastLength.Short).Show();
            EditText inputText = FindViewById<EditText>(Resource.Id.inputTextBox);
            //inputText.Text
            //Toast.MakeText(this.ApplicationContext, "Let's try to calculate (" + inputText.Text + ")", ToastLength.Short).Show();
            int firstOperand;
            int secondOperand;
            if (inputText.Text.Contains(" ") || inputText.Text.Contains("."))
            {
                //Console.WriteLine("Please removes any spaces or decimals from query.");
                Toast.MakeText(this.ApplicationContext, "Please removes any spaces or decimals from query.", ToastLength.Short).Show();
            }
            else if (inputText.Text.Contains("+"))
            {
                int opIndex = inputText.Text.IndexOf(@"+");
                Int32.TryParse(inputText.Text.Substring(0, opIndex), out firstOperand);
                Int32.TryParse(inputText.Text.Substring((opIndex + 1), (inputText.Text.Length - opIndex - 1)), out secondOperand);
                //Console.WriteLine(inputText.Text + " = " + (firstOperand + secondOperand));
                Toast.MakeText(this.ApplicationContext, inputText.Text + " = " + (firstOperand + secondOperand), ToastLength.Short).Show();
            }
            else if (inputText.Text.Contains("-"))
            {
                int opIndex = inputText.Text.IndexOf(@"-");
                Int32.TryParse(inputText.Text.Substring(0, opIndex), out firstOperand);
                Int32.TryParse(inputText.Text.Substring((opIndex + 1), (inputText.Text.Length - opIndex - 1)), out secondOperand);
                //Console.WriteLine(inputText.Text + " = " + (firstOperand - secondOperand));
                Toast.MakeText(this.ApplicationContext, inputText.Text + " = " + (firstOperand - secondOperand), ToastLength.Short).Show();
            }
            else if (inputText.Text.Contains("*"))
            {
                int opIndex = inputText.Text.IndexOf(@"*");
                Int32.TryParse(inputText.Text.Substring(0, opIndex), out firstOperand);
                Int32.TryParse(inputText.Text.Substring((opIndex + 1), (inputText.Text.Length - opIndex - 1)), out secondOperand);
                //Console.WriteLine(inputText.Text + " = " + (firstOperand * secondOperand));
                Toast.MakeText(this.ApplicationContext, inputText.Text + " = " + (firstOperand * secondOperand), ToastLength.Short).Show();
            }
            else if (inputText.Text.Contains("/"))
            {
                int opIndex = inputText.Text.IndexOf(@"/");
                Int32.TryParse(inputText.Text.Substring(0, opIndex), out firstOperand);
                Int32.TryParse(inputText.Text.Substring((opIndex + 1), (inputText.Text.Length - opIndex - 1)), out secondOperand);
                //Console.WriteLine(inputText.Text + " = " + (firstOperand / secondOperand));
                Toast.MakeText(this.ApplicationContext, inputText.Text + " = " + (firstOperand / secondOperand), ToastLength.Short).Show();
            }
            else
            {
                // None of the folllowing operators were found: {+, -, *, /}
                //Console.WriteLine("None of the accepted operators were found: + - * /");
                Toast.MakeText(this.ApplicationContext, "None of the accepted operators were found: + - * /", ToastLength.Short).Show();
            }
        }
    }
}

