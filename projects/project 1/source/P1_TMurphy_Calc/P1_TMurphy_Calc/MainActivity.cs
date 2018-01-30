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
        bool clear = true;
        bool allClear = true;
        bool containsDec = false;
        string displayText = "0";
        string prevOperand = null;
        string currOperand = null;
        int lastOp = 0;
        decimal result = 0;

        protected override void OnCreate(Bundle savedInstanceState) //savedInstanceState //bundle
        {
            base.OnCreate(savedInstanceState); //savedInstanceState //bundle

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var gridView = FindViewById<GridView>(Resource.Id.gridView);
            gridView.Adapter = new ImageAdapter(this);
            gridView.ItemClick += GridView_ItemClick;
        }

        private void GridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
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
                result = 0;
            }
            else if (e.Position == 1) // "CLEAR"
            {
                displayText = "0";
                display.Text = displayText;
                clear = true;
                containsDec = false;
                currOperand = null;
            }
            else if (e.Position == 2) // "PERCENTAGE"
            {
                result = Convert.ToDecimal(displayText) / 100;
                displayText = result.ToString("0.00");
                if (displayText.Substring(displayText.Length - 3) == ".00")
                {
                    displayText = displayText.Substring(0, displayText.Length - 3);
                }
                display.Text = displayText;
                prevOperand = displayText;
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
                if(clear == true)
                {
                    //
                }
                else if(displayText[0] == '-') // is neg, set to pos
                {
                    displayText = displayText.Substring(1);
                    display.Text = displayText;
                }
                else // is pos, set to neg
                {
                    displayText = "-" + displayText;
                    display.Text = displayText;
                }
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
                CalculateResult();
            }
            else // Error
            {
                // The grid item clicked does not match one of the 20 initialized: {0-19}
                Toast.MakeText(this.ApplicationContext, "AN ERROR HAS OCCURED.", ToastLength.Short).Show();
            }
        }

        //
        private void CalculateResult() //Decimal
        {
            decimal firstOprnd;
            decimal secondOprnd;
            Decimal.TryParse(prevOperand, out firstOprnd);
            if(currOperand != null) // is second operand set? if so tryparse
            {
                Decimal.TryParse(currOperand, out secondOprnd);
            }
            else // second operand is not set, set equal to first operand
            {
                secondOprnd = firstOprnd;
            }
            TextView display = FindViewById<TextView>(Resource.Id.calcDisplay);

            if (lastOp == 0) // "No operator has been set yet"
            {
                Toast.MakeText(this.ApplicationContext, "No operator has been set yet.", ToastLength.Short).Show();
            }
            else if (lastOp == 1) // "Division"
            {
                result = firstOprnd / secondOprnd;
                displayText = result.ToString("0.00");
                if(displayText.Substring(displayText.Length - 3) == ".00")
                {
                    displayText = displayText.Substring(0, displayText.Length - 3);
                }
                display.Text = displayText;
                prevOperand = displayText;
            }
            else if (lastOp == 2) // "Multiplication"
            {
                result = firstOprnd * secondOprnd;
                displayText = result.ToString("0.00");
                if (displayText.Substring(displayText.Length - 3) == ".00")
                {
                    displayText = displayText.Substring(0, displayText.Length - 3);
                }
                display.Text = displayText;
                prevOperand = displayText;
            }
            else if (lastOp == 3) // "Subraction"
            {
                result = firstOprnd - secondOprnd;
                displayText = result.ToString("0.00");
                if (displayText.Substring(displayText.Length - 3) == ".00")
                {
                    displayText = displayText.Substring(0, displayText.Length - 3);
                }
                display.Text = displayText;
                prevOperand = displayText;
            }
            else if (lastOp == 4) // "Addition"
            {
                result = firstOprnd + secondOprnd;
                displayText = result.ToString("0.00");
                if (displayText.Substring(displayText.Length - 3) == ".00")
                {
                    displayText = displayText.Substring(0, displayText.Length - 3);
                }
                display.Text = displayText;
                prevOperand = displayText;
            }
            else // Error
            {
                // ...
                Toast.MakeText(this.ApplicationContext, "AN ERROR HAS OCCURED.", ToastLength.Short).Show();
            }
        }
    }
}

