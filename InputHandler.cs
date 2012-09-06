namespace InputHandler
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Linq;

 
    public delegate void InputDelegate(InputHelper sender);
    [Flags]
    public enum MouseButtons
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

    public class InputHelper  : GameComponent
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
        public Keys[] pressedKeys;
        public Keys[] Old_pressedKeys;
        public string[] StringKeys_Constant;
        public string[] StringKeys;
        public string[] Old_StringKeys;
        public int MouseScroll;
        public int Old_MouseScroll;
 
        public bool mouseLeftClicked_Continuous;
        public bool mouseRightClicked_Continuous;
  
        public bool mouseMiddleClicked_Single;
        public bool mouseButton1Clicked_Single;
        public bool mouseButton2Click_Single;
        public Vector2 Old_MouseNormal;
     
        public bool mouseMiddleClicked_Continuous;
        public bool mouseButton1Clicked_Continuous;
        public bool mouseButton2Click_Continuous;

        public MouseButtons MouseButtonsDown;
        public MouseButtons MouseButtonsDown_Constant;
        private MouseButtons Old_MouseButtonsDown_Constant;
 
        public MouseButtons MouseButtonsUp;
        public MouseButtons MouseButtonsUp_Constant;
        private MouseButtons Old_MouseButtonsUp_Constant;
        public InputHelper(Game game)
            : base(game)
        {
            this.MouseButtonsDown_Constant = new MouseButtons();
            this.MouseButtonsUp = new MouseButtons();

        }
        public static string GetEnum(object enumerator)
        {
            return Enum.GetName(enumerator.GetType(), Convert.ToUInt32(enumerator));

        }
        public string GetEnumName(object value)
        {
            bool shift = this.pressedKeys.Contains<Keys>(Keys.LeftShift) || this.pressedKeys.Contains<Keys>(Keys.RightShift);
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
        public override void Update(GameTime gametime)
        {

            this.Old_keyboardState = this.keyboardState;
            this.keyboardState = Keyboard.GetState();
            this.Old_pressedKeys = pressedKeys;
            this.pressedKeys = Keyboard.GetState().GetPressedKeys();
            if (this.pressedKeys.Contains<Keys>(Keys.CapsLock))
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
            this.Old_StringKeys = this.StringKeys_Constant;
            this.StringKeys_Constant = new string[this.pressedKeys.Length];
            for (int i = 0; i < this.pressedKeys.Length; i++)
            {
                this.StringKeys_Constant[i] = this.GetEnumName(this.pressedKeys[i]);
            }
            List<string> PressedKeysOnce = new List<string>();
            for (int i = 0; i < StringKeys_Constant.Length; i++)
            {
                string key = StringKeys_Constant[i];
                if (!Old_StringKeys.Contains<string>(key))
                {

                    PressedKeysOnce.Add(key);
                }
            }
            this.StringKeys = PressedKeysOnce.ToArray();

            this.Old_MouseState = this.MouseState;
            this.MouseState = Mouse.GetState();

            this.Old_MouseScroll = this.MouseScroll;
            this.MouseScroll = this.MouseState.ScrollWheelValue;
            this.MouseNormal = new Vector2((float)this.MouseState.X, (float)this.MouseState.Y);

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
            this.MouseButtonsDown_Constant = MouseButtons.None;
            this.MouseButtonsDown = MouseButtons.None;
            CheckButtom(this.MouseState.RightButton, ButtonState.Pressed, MouseButtons.Right, this.MouseButtonsDown_Constant, this.Old_MouseButtonsDown_Constant, this.MouseButtonsDown, out MouseButtonsDown_Constant, out MouseButtonsDown);
            CheckButtom(this.MouseState.LeftButton, ButtonState.Pressed, MouseButtons.Left, this.MouseButtonsDown_Constant, this.Old_MouseButtonsDown_Constant, this.MouseButtonsDown, out MouseButtonsDown_Constant, out MouseButtonsDown);
            CheckButtom(this.MouseState.MiddleButton, ButtonState.Pressed, MouseButtons.Middle, this.MouseButtonsDown_Constant, this.Old_MouseButtonsDown_Constant, this.MouseButtonsDown, out MouseButtonsDown_Constant, out MouseButtonsDown);
            CheckButtom(this.MouseState.XButton1, ButtonState.Pressed, MouseButtons.XButton1, this.MouseButtonsDown_Constant, this.Old_MouseButtonsDown_Constant, this.MouseButtonsDown, out MouseButtonsDown_Constant, out MouseButtonsDown);
            CheckButtom(this.MouseState.XButton2, ButtonState.Pressed, MouseButtons.XButton2, this.MouseButtonsDown_Constant, this.Old_MouseButtonsDown_Constant, this.MouseButtonsDown, out MouseButtonsDown_Constant, out MouseButtonsDown);
            FireDelegate(MouseButtonsDown_Constant != MouseButtons.None, this.MouseDown_Constant);
            FireDelegate(MouseButtonsDown != MouseButtons.None, this.MouseDown);








            #endregion

            #region mousebuttomsUp

            this.Old_MouseButtonsUp_Constant = this.MouseButtonsUp_Constant;
            this.MouseButtonsUp_Constant = MouseButtons.None;
            this.MouseButtonsUp = MouseButtons.None;
            CheckButtom(this.MouseState.RightButton, ButtonState.Released, MouseButtons.Right, this.MouseButtonsUp_Constant, this.Old_MouseButtonsUp_Constant, this.MouseButtonsUp, out MouseButtonsUp_Constant, out this.MouseButtonsUp);
            CheckButtom(this.MouseState.LeftButton, ButtonState.Released, MouseButtons.Left, this.MouseButtonsUp_Constant, this.Old_MouseButtonsUp_Constant, this.MouseButtonsUp, out MouseButtonsUp_Constant, out this.MouseButtonsUp);
            CheckButtom(this.MouseState.MiddleButton, ButtonState.Released, MouseButtons.Middle, this.MouseButtonsUp_Constant, this.Old_MouseButtonsUp_Constant, this.MouseButtonsUp, out MouseButtonsUp_Constant, out this.MouseButtonsUp);
            CheckButtom(this.MouseState.XButton1, ButtonState.Released, MouseButtons.XButton1, this.MouseButtonsUp_Constant, this.Old_MouseButtonsUp_Constant, this.MouseButtonsUp, out MouseButtonsUp_Constant, out this.MouseButtonsUp);
            CheckButtom(this.MouseState.XButton2, ButtonState.Released, MouseButtons.XButton2, this.MouseButtonsUp_Constant, this.Old_MouseButtonsUp_Constant, this.MouseButtonsUp, out MouseButtonsUp_Constant, out this.MouseButtonsUp);
            FireDelegate(MouseButtonsUp_Constant != MouseButtons.None, this.MouseUp_Constant);
            FireDelegate(MouseButtonsUp != MouseButtons.None, this.MouseUp);

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


        private void CheckButtom(ButtonState buttonState, ButtonState buttostatetockeck, MouseButtons buttomtockeck, MouseButtons constant, MouseButtons oldconstant, MouseButtons once, out MouseButtons Constant, out MouseButtons Once)
        {


            if (buttonState == buttostatetockeck)
            {
                //constant
                if (constant == MouseButtons.None)
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
                    if (once == MouseButtons.None)
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
        public bool IsTextPressed_Once(string key)
        {

            return this.StringKeys.Contains<string>(key);

        }



        public int X { get; set; }

        public int Y { get; set; }

        internal bool Contains(Keys keys)
        {
            return this.pressedKeys.Contains(keys);
        }
        internal bool Contains(Keys[] keys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                Keys key = keys[i];

                return this.pressedKeys.Contains(key);
                
            }
            return false;
        }
    }
}

