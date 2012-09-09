namespace InputHelp
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Linq;

 
    public delegate void InputDelegate(InputHelper sender);
    [Flags]
    public enum Mouse_Buttoms
    {
        // Summary:
        //     No mouse button was pressed.
        None = 0,
        //
        // Summary:
        //     The left mouse button was pressed.
        Left = 1048576,
        //
        // Summary:
        //     The right mouse button was pressed.
        Right = 2097152,
        //
        // Summary:
        //     The middle mouse button was pressed.
        Middle = 4194304,
        //
        // Summary:
        //     The first XButton was pressed.
        XButton1 = 8388608,
        //
        // Summary:
        //     The second XButton was pressed.
        XButton2 = 16777216,
    }

    public class InputHelper   
    {
        /// <summary>
        /// Contant Fires at the end of all checks
        /// </summary>
        public InputDelegate ConstantCall;
        /// <summary>
        /// Fires when the mouse position changes
        /// </summary>
        public InputDelegate MouseMouse;
        /// <summary>
        /// Fires when any mouse key is PRESSED once
        /// </summary>
        public InputDelegate MouseDown;
        /// <summary>
        /// Fires when any mouse key is PRESSED constantly
        /// </summary>
        public InputDelegate MouseDown_Constant;
        /// <summary>
        /// Fires when any mouse key is NOT PRESSED once
        /// </summary>
        public InputDelegate MouseUp;
        /// <summary>
        /// Fires when any mouse key is NOT PRESSED constantly
        /// </summary>
        public InputDelegate MouseUp_Constant;
        /// <summary>
        /// Fires when the mouse wheel value changes
        /// </summary>
        public InputDelegate MouseWheel;
        /// <summary>
        /// Fires when the Keyboard Keys Changes 
        /// </summary>
        public InputDelegate KeyboardKeys_Once;
        /// <summary>
        /// Fires when the Keyboard Keys Changes contantly
        /// </summary>
        public InputDelegate KeyboardKeys_Contant;

        public bool CapsLock;
        public KeyboardState keyboardState;
        public bool mouseLeftClicked_Single;
        public Vector2 MouseNormal;
        public bool mouseRightClicked_Single;
        public Microsoft.Xna.Framework.Input.MouseState MouseState;
        public Vector2 mouseWorld;
        public KeyboardState Old_keyboardState;
        public Microsoft.Xna.Framework.Input.MouseState Old_MouseState;
        public int Old_Scroll;
        public Keys[] PressedKeys;
        public Keys[] pressedKeys_Constant;
        public Keys[] Old_pressedKeys;
        public string[] StringKeys_Constant;
        public string[] StringKeys;
         
        public int New_MouseScroll_Value;
        public int Old_MouseScroll_Value;
 
        public bool mouseLeftClicked_Continuous;
        public bool mouseRightClicked_Continuous;
  
        public bool mouseMiddleClicked_Single;
        public bool mouseButton1Clicked_Single;
        public bool mouseButton2Click_Single;
        public Vector2 Old_MouseNormal;
     
        public bool mouseMiddleClicked_Continuous;
        public bool mouseButton1Clicked_Continuous;
        public bool mouseButton2Click_Continuous;

        public Mouse_Buttoms MouseButtonsDown;
        public Mouse_Buttoms MouseButtonsDown_Constant;
        private Mouse_Buttoms Old_MouseButtonsDown_Constant;
 
        public Mouse_Buttoms MouseButtonsUp;
        public Mouse_Buttoms MouseButtonsUp_Constant;
        private Mouse_Buttoms Old_MouseButtonsUp_Constant;
        public bool MouseScrollValueUP;
        public bool MouseScrollValueDown;
        public bool MouseScrollValueConstant;
        
        public InputHelper(   )
         
        {
            this.MouseButtonsDown_Constant = new Mouse_Buttoms();
            this.MouseButtonsDown = new Mouse_Buttoms();

            this.MouseButtonsUp_Constant = new Mouse_Buttoms();
            this.MouseButtonsUp = new Mouse_Buttoms();

        }
        public static string GetEnum(object enumerator)
        {
            return Enum.GetName(enumerator.GetType(), Convert.ToUInt32(enumerator));

        }
        public string GetEnumName(object value)
        {
            bool shift = this.pressedKeys_Constant.Contains<Keys>(Keys.LeftShift) || this.pressedKeys_Constant.Contains<Keys>(Keys.RightShift);
            string OEM = GetEnum(value);
            if (!shift)
            {
                switch (OEM)
                {
                    case "Space":
                        return " ";

                    case "Divide":
                        return "/";

                    case "Multiply":
                        return "*";

                    case "Subtract":
                        return "-";

                    case "Add":
                        return "+";

                    case "Decimal":
                        return ".";

                    case "OemMinus":
                        return "-";

                    case "OemPlus":
                        return "=";

                    case "OemOpenBrackets":
                        return "[";

                    case "OemCloseBrackets":
                        return "]";

                    case "OemPipe":
                        return @"\";

                    case "OemSemicolon":
                        return ";";

                    case "OemQuotes":
                        return "\"";

                    case "OemComma":
                        return ",";

                    case "OemPeriod":
                        return ".";

                    case "OemQuestion":
                        return "/";

                    case "D0":
                        return "0";

                    case "D1":
                        return "1";

                    case "D2":
                        return "2";

                    case "D3":
                        return "3";

                    case "D4":
                        return "4";

                    case "D5":
                        return "5";

                    case "D6":
                        return "6";

                    case "D7":
                        return "7";

                    case "D8":
                        return "8";

                    case "D9":
                        return "9";
                }
            }
            else
            {
                switch (OEM)
                {
                    case "Space":
                        return " ";

                    case "OemOpenBrackets":
                        return "{";

                    case "OemCloseBrackets":
                        return "}";

                    case "OemMinus":
                        return "_";

                    case "OemPlus":
                        return "+";

                    case "OemPipe":
                        return "|";

                    case "OemSemicolon":
                        return ":";

                    case "OemQuotes":
                        return "\"";

                    case "OemComma":
                        return "<";

                    case "OemPeriod":
                        return ">";

                    case "OemQuestion":
                        return "?";

                    case "D0":
                        return ")";

                    case "D1":
                        return "!";

                    case "D2":
                        return "@";

                    case "D3":
                        return "#";

                    case "D4":
                        return "$";

                    case "D5":
                        return "%";

                    case "D6":
                        return "^";

                    case "D7":
                        return "&";

                    case "D8":
                        return "*";

                    case "D9":
                        return "(";
                }
            }
            if (OEM.Contains("NumPad"))
            {
                OEM.Replace("NumPad", "");
                return OEM;
            }
            if (!this.CapsLock && !shift)
            {
                OEM = OEM.ToLower();
            }
            return OEM;
        }
        public void Update()
        {

            this.Old_keyboardState = this.keyboardState;
            this.keyboardState = Keyboard.GetState();
            this.Old_pressedKeys = pressedKeys_Constant;
            this.pressedKeys_Constant = keyboardState.GetPressedKeys();
            List<Keys> tempkeys = new List<Keys>();
            List<string> stringtempkeys = new List<string>();
            List<string> stringtempkeys2 = new List<string>();
            for (int i = 0; i < pressedKeys_Constant.Length; i++)
            {
                var stringkey = this.GetEnumName(this.pressedKeys_Constant[i]);
                var key = pressedKeys_Constant[i];

                stringtempkeys2.Add(stringkey);
                if (!this.Old_pressedKeys.Contains(key))
                {
                    stringtempkeys.Add(stringkey);
                    tempkeys.Add(key);
                }
            }
            this.PressedKeys = tempkeys.ToArray();
            this.StringKeys = stringtempkeys.ToArray();
            this.StringKeys_Constant = stringtempkeys2.ToArray();







            if (this.pressedKeys_Constant.Contains<Keys>(Keys.CapsLock))
            {
                if (this.CapsLock)
                {
                    this.CapsLock = false;
                }
                else
                {
                    this.CapsLock = true;
                }
            }


            FireDelegate(StringKeys_Constant.Length > 0, this.KeyboardKeys_Contant);
            FireDelegate(StringKeys.Length > 0, this.KeyboardKeys_Once);


            this.Old_MouseState = this.MouseState;
            this.MouseState = Mouse.GetState();

            this.Old_MouseScroll_Value = this.New_MouseScroll_Value;
            this.New_MouseScroll_Value = this.MouseState.ScrollWheelValue;
            this.MouseNormal = new Vector2((float)this.MouseState.X, (float)this.MouseState.Y);

            this.MouseScrollValueUP = Old_MouseScroll_Value < New_MouseScroll_Value;
            this.MouseScrollValueDown = Old_MouseScroll_Value > New_MouseScroll_Value;
            this.MouseScrollValueConstant = Old_MouseScroll_Value == New_MouseScroll_Value;
            FireDelegate(MouseScrollValueConstant == false, MouseWheel);

            this.X = (int)MouseNormal.X;
            this.Y = (int)MouseNormal.Y;

            this.mouseLeftClicked_Single = (this.MouseState.LeftButton == ButtonState.Pressed) && (this.Old_MouseState.LeftButton != this.MouseState.LeftButton);
            this.mouseMiddleClicked_Single = (this.MouseState.MiddleButton == ButtonState.Pressed) && (this.Old_MouseState.MiddleButton != this.MouseState.MiddleButton);
            this.mouseRightClicked_Single = (this.MouseState.RightButton == ButtonState.Pressed) && (this.Old_MouseState.RightButton != this.MouseState.RightButton);
            this.mouseButton1Clicked_Single = (this.MouseState.XButton1 == ButtonState.Pressed) && (this.Old_MouseState.XButton1 != this.MouseState.XButton1);
            this.mouseButton2Click_Single = (this.MouseState.XButton2 == ButtonState.Pressed) && (this.Old_MouseState.XButton2 != this.MouseState.XButton2);

            this.mouseLeftClicked_Continuous = (this.MouseState.LeftButton == ButtonState.Pressed);
            this.mouseMiddleClicked_Continuous = (this.MouseState.MiddleButton == ButtonState.Pressed);
            this.mouseRightClicked_Continuous = (this.MouseState.RightButton == ButtonState.Pressed);
            this.mouseButton1Clicked_Continuous = (this.MouseState.XButton1 == ButtonState.Pressed);
            this.mouseButton2Click_Continuous = (this.MouseState.XButton2 == ButtonState.Pressed);








            #region mousebuttomsDown


            this.Old_MouseButtonsDown_Constant = this.MouseButtonsDown_Constant;
            this.MouseButtonsDown_Constant = Mouse_Buttoms.None;
            this.MouseButtonsDown = Mouse_Buttoms.None;
            CheckButtom(this.MouseState.RightButton, ButtonState.Pressed, Mouse_Buttoms.Right, this.MouseButtonsDown_Constant, this.Old_MouseButtonsDown_Constant, this.MouseButtonsDown, out MouseButtonsDown_Constant, out MouseButtonsDown);
            CheckButtom(this.MouseState.LeftButton, ButtonState.Pressed, Mouse_Buttoms.Left, this.MouseButtonsDown_Constant, this.Old_MouseButtonsDown_Constant, this.MouseButtonsDown, out MouseButtonsDown_Constant, out MouseButtonsDown);
            CheckButtom(this.MouseState.MiddleButton, ButtonState.Pressed, Mouse_Buttoms.Middle, this.MouseButtonsDown_Constant, this.Old_MouseButtonsDown_Constant, this.MouseButtonsDown, out MouseButtonsDown_Constant, out MouseButtonsDown);
            CheckButtom(this.MouseState.XButton1, ButtonState.Pressed, Mouse_Buttoms.XButton1, this.MouseButtonsDown_Constant, this.Old_MouseButtonsDown_Constant, this.MouseButtonsDown, out MouseButtonsDown_Constant, out MouseButtonsDown);
            CheckButtom(this.MouseState.XButton2, ButtonState.Pressed, Mouse_Buttoms.XButton2, this.MouseButtonsDown_Constant, this.Old_MouseButtonsDown_Constant, this.MouseButtonsDown, out MouseButtonsDown_Constant, out MouseButtonsDown);
            FireDelegate(MouseButtonsDown_Constant != Mouse_Buttoms.None, this.MouseDown_Constant);
            FireDelegate(MouseButtonsDown != Mouse_Buttoms.None, this.MouseDown);








            #endregion

            #region mousebuttomsUp

            this.Old_MouseButtonsUp_Constant = this.MouseButtonsUp_Constant;
            this.MouseButtonsUp_Constant = Mouse_Buttoms.None;
            this.MouseButtonsUp = Mouse_Buttoms.None;
            CheckButtom(this.MouseState.RightButton, ButtonState.Released, Mouse_Buttoms.Right, this.MouseButtonsUp_Constant, this.Old_MouseButtonsUp_Constant, this.MouseButtonsUp, out MouseButtonsUp_Constant, out this.MouseButtonsUp);
            CheckButtom(this.MouseState.LeftButton, ButtonState.Released, Mouse_Buttoms.Left, this.MouseButtonsUp_Constant, this.Old_MouseButtonsUp_Constant, this.MouseButtonsUp, out MouseButtonsUp_Constant, out this.MouseButtonsUp);
            CheckButtom(this.MouseState.MiddleButton, ButtonState.Released, Mouse_Buttoms.Middle, this.MouseButtonsUp_Constant, this.Old_MouseButtonsUp_Constant, this.MouseButtonsUp, out MouseButtonsUp_Constant, out this.MouseButtonsUp);
            CheckButtom(this.MouseState.XButton1, ButtonState.Released, Mouse_Buttoms.XButton1, this.MouseButtonsUp_Constant, this.Old_MouseButtonsUp_Constant, this.MouseButtonsUp, out MouseButtonsUp_Constant, out this.MouseButtonsUp);
            CheckButtom(this.MouseState.XButton2, ButtonState.Released, Mouse_Buttoms.XButton2, this.MouseButtonsUp_Constant, this.Old_MouseButtonsUp_Constant, this.MouseButtonsUp, out MouseButtonsUp_Constant, out this.MouseButtonsUp);
            FireDelegate(MouseButtonsUp_Constant != Mouse_Buttoms.None, this.MouseUp_Constant);
            FireDelegate(MouseButtonsUp != Mouse_Buttoms.None, this.MouseUp);

            #endregion

            if (Old_MouseNormal != MouseNormal)
            {
                if (this.MouseMouse != null)
                {
                    this.MouseMouse(this);
                }
                Old_MouseNormal = MouseNormal;
            }
            if (this.ConstantCall != null)
            {
                this.ConstantCall(this);
            }
        }


        private void CheckButtom(ButtonState buttonState, ButtonState buttostatetockeck, Mouse_Buttoms buttomtockeck, Mouse_Buttoms constant, Mouse_Buttoms oldconstant, Mouse_Buttoms once, out Mouse_Buttoms Constant, out Mouse_Buttoms Once)
        {


            if (buttonState == buttostatetockeck)
            {
                //constant
                if (constant == Mouse_Buttoms.None)
                {
                    constant = buttomtockeck;
                }
                else
                {
                    constant |= buttomtockeck;
                }
                //once
                if (!oldconstant.HasFlag(buttomtockeck))
                {
                    if (once == Mouse_Buttoms.None)
                    {
                        once = buttomtockeck;
                    }
                    else
                    {
                        once |= buttomtockeck;
                    }
                }
            }

            Once = once;
            Constant = constant;

        }


        private void FireDelegate(bool fire, InputDelegate handle )
        {
            if (fire && handle != null)
            {
                handle(this);

            }
        }

        public bool IsTextPressed_Continuous(string key)
        {

            return this.StringKeys_Constant.Contains<string>(key);
           
        }
        public bool IsTextPressed(string key)
        {

            return this.StringKeys.Contains<string>(key);

        }

        public bool IsKeypressed_Continuous(Keys key)
        {

            return this.pressedKeys_Constant.Contains(key);

        }
        public bool IsKeypressed(Keys key)
        {

            return this.PressedKeys.Contains(key);

        }


        public int X { get; set; }

        public int Y { get; set; }

        internal bool Contains(Keys keys)
        {
            return this.pressedKeys_Constant.Contains(keys);
        }
        internal bool Contains(Keys[] keys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                Keys key = keys[i];

                return this.pressedKeys_Constant.Contains(key);
                
            }
            return false;
        }
    }
}

