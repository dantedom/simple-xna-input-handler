namespace InputHandler
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Linq;

    public delegate void LeftClick(object sender);
    public delegate void Continuous_LeftClick(object sender);
    public delegate void RightClick(object sender);
    public delegate void Continuous_RightClick(object sender);
    public delegate void MouseMove(InputH sender);
    public delegate void MouseUp(InputH sender);
    public delegate void MouseDown(InputH sender);

    public delegate void PressedKeys(Microsoft.Xna.Framework.Input.Keys[] keys);
 
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

    public class InputH  : GameComponent
    {

        public PressedKeys PressedKeysArray;
        public MouseMove mouseMove;
        public MouseDown mouseDown;
        public MouseUp mouseUp;
        public LeftClick leftclick;
        public Continuous_LeftClick continuous_leftclick;
        public RightClick rightclick;
        public Continuous_RightClick continuous_rightclick;
        public bool CapsLock;
        public KeyboardState keyboardState;
        public bool mouseLeftClicked;
        public Vector2 MouseNormal;
        public bool mouseRightClicked;
        public Microsoft.Xna.Framework.Input.MouseState MouseState;
        public Vector2 mouseWorld;
        public KeyboardState Old_keyboardState;
        public Microsoft.Xna.Framework.Input.MouseState Old_MouseState;
        public int Old_Scroll;
        public Keys[] pressedKeys;
        public Keys[] Old_pressedKeys;
        public string[] StringKeys;
        public string[] Old_StringKeys;
        public int MouseScroll;
        public int Old_MouseScroll;
        public string[] StringKeys_Once;
        public bool mouseLeftClicked_Continuous;
        public bool mouseRightClicked_Continuous;
        public MouseButtons MouseButtonsDown;
        public bool mouseMiddlelicked;
        public bool mouseButton1;
        public bool mouseButton2;
        public Vector2 Old_MouseNormal;
        public MouseButtons MouseButtonsUp;
        public InputH(Game game)
            : base(game)
        {
            this.MouseButtonsDown = new MouseButtons();
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
            this.Old_StringKeys = this.StringKeys;
            this.StringKeys = new string[this.pressedKeys.Length];
            for (int i = 0; i < this.pressedKeys.Length; i++)
            {
                this.StringKeys[i] = this.GetEnumName(this.pressedKeys[i]);
            }
            this.MouseState = Mouse.GetState();
            this.Old_MouseScroll = this.MouseScroll;
            this.MouseScroll = this.MouseState.ScrollWheelValue;
            this.MouseNormal = new Vector2((float)this.MouseState.X, (float)this.MouseState.Y);
            this.mouseLeftClicked = (this.MouseState.LeftButton == ButtonState.Pressed) && (this.Old_MouseState.LeftButton != this.MouseState.LeftButton);
            this.mouseMiddlelicked = (this.MouseState.MiddleButton == ButtonState.Pressed) && (this.Old_MouseState.MiddleButton != this.MouseState.MiddleButton);
            this.mouseRightClicked = (this.MouseState.RightButton == ButtonState.Pressed) && (this.Old_MouseState.RightButton != this.MouseState.RightButton);
            this.mouseButton1 = (this.MouseState.XButton1 == ButtonState.Pressed) && (this.Old_MouseState.XButton1 != this.MouseState.XButton1);
            this.mouseButton2 = (this.MouseState.XButton2 == ButtonState.Pressed) && (this.Old_MouseState.XButton2 != this.MouseState.XButton2);



            if (this.MouseState.LeftButton == ButtonState.Pressed)
            {
                 mouseLeftClicked_Continuous = true;
                if (this.continuous_leftclick != null)
                {
                    this.continuous_leftclick(this);
                }
            }
            else { mouseLeftClicked_Continuous = false; }

            if (this.mouseLeftClicked)
            {
                
                if (this.leftclick != null)
                {
                    this.leftclick(this);
                }
            }
            if (this.MouseState.RightButton == ButtonState.Pressed)
            {


               
                mouseRightClicked_Continuous = true;

                if (this.continuous_rightclick != null)
                {
                    this.continuous_rightclick(this);
                }

            }
            else { mouseRightClicked_Continuous = false; }
            if (this.mouseRightClicked)
            {
          
                if (this.rightclick != null)
                {
                    this.rightclick(this);
                }
            }

            this.Old_MouseState = this.MouseState;
            List<string> PressedKeysOnce = new List<string>();
            for (int i = 0; i < StringKeys.Length; i++)
            {
                string key = StringKeys[i];
                if (!Old_StringKeys.Contains<string>(key))
                {

                    PressedKeysOnce.Add(key);
                }
            }
            this.StringKeys_Once = PressedKeysOnce.ToArray();

        

            #region mousebuttomsDown

    this.MouseButtonsDown = new MouseButtons();
    bool firedown = false;
            if (this.MouseState.RightButton == ButtonState.Pressed)
            {

                firedown = true;
                if (this.MouseButtonsDown == MouseButtons.None)
                {
                    this.MouseButtonsDown = MouseButtons.Right;
                }
                else
                {
                    this.MouseButtonsDown |= MouseButtons.Right;
                }
            }


            if (this.MouseState.LeftButton == ButtonState.Pressed)
            {
                firedown = true;             
                if (this.MouseButtonsDown == MouseButtons.None)
                {
                    this.MouseButtonsDown = MouseButtons.Left;
                }
                else
                {
                    this.MouseButtonsDown |= MouseButtons.Left;
                }
            }


            if (this.MouseState.MiddleButton == ButtonState.Pressed)
            {

                firedown = true;
                if (this.MouseButtonsDown == MouseButtons.None)
                {
                    this.MouseButtonsDown = MouseButtons.Middle;
                }
                else
                {
                    this.MouseButtonsDown |= MouseButtons.Middle;
                }
            }


            if (this.MouseState.XButton1 == ButtonState.Pressed)
            {
                firedown = true;
                 
             
                if (this.MouseButtonsDown == MouseButtons.None)
                {
                    this.MouseButtonsDown = MouseButtons.XButton1;
                }
                else
                {
                    this.MouseButtonsDown |= MouseButtons.XButton1;
                }
            }


            if (this.MouseState.XButton2 == ButtonState.Pressed)
            {
                firedown = true;
             
                if (this.MouseButtonsDown == MouseButtons.None)
                {
                    this.MouseButtonsDown = MouseButtons.XButton2;
                }
                else
                {
                    this.MouseButtonsDown |= MouseButtons.XButton2;
                }
            }
            if (firedown) { firemousedown(); }
            #endregion

            #region mousebuttomsUp
            bool fireup = false;
            this.MouseButtonsUp = new MouseButtons();
            if (this.MouseState.RightButton != ButtonState.Pressed)
            {
                fireup = true;
                if (this.MouseButtonsUp == MouseButtons.None)
                {
                    this.MouseButtonsUp = MouseButtons.Right;
                }
                else
                {
                    this.MouseButtonsUp |= MouseButtons.Right;
                }
            }


            if (this.MouseState.LeftButton != ButtonState.Pressed)
            {
                fireup = true;
                if (this.MouseButtonsUp == MouseButtons.None)
                {
                    this.MouseButtonsUp = MouseButtons.Left;
                }
                else
                {
                    this.MouseButtonsUp |= MouseButtons.Left;
                }
            }


            if (this.MouseState.MiddleButton != ButtonState.Pressed)
            {
                fireup = true;
                if (this.MouseButtonsUp == MouseButtons.None)
                {
                    this.MouseButtonsUp = MouseButtons.Middle;
                }
                else
                {
                    this.MouseButtonsUp |= MouseButtons.Middle;
                }
            }


            if (this.MouseState.XButton1 != ButtonState.Pressed)
            {
                fireup = true;
                if (this.MouseButtonsUp == MouseButtons.None)
                {
                    this.MouseButtonsUp = MouseButtons.XButton1;
                }
                else
                {
                    this.MouseButtonsUp |= MouseButtons.XButton1;
                }
            }


            if (this.MouseState.XButton2 != ButtonState.Pressed)
            {
                fireup = true;
                if (this.MouseButtonsUp == MouseButtons.None)
                {
                    this.MouseButtonsUp = MouseButtons.XButton2;
                }
                else
                {
                    this.MouseButtonsUp |= MouseButtons.XButton2;
                }
            }
            if (fireup) { this.firemouseup(); }

            #endregion

            this.X = (int)MouseNormal.X;
            this.Y = (int)MouseNormal.Y;
            if (Old_MouseNormal != MouseNormal)
            {
                if (this.mouseMove != null)
                {
                    this.mouseMove(this);
                }
                Old_MouseNormal = MouseNormal;
            }

            if (this.PressedKeysArray != null) { this.PressedKeysArray(this.pressedKeys); }
        }

        private void firemouseup()
        {
            if (this.mouseUp != null) { this.mouseUp(this); }
        }

        private void firemousedown()
        {
            if (this.mouseDown != null) { this.mouseDown(this); }
        }
        public bool IsTextPressed_Continuous(string key)
        {

            return this.StringKeys.Contains<string>(key);
           
        }
        public bool IsTextPressed_Once(string key)
        {

            return this.StringKeys_Once.Contains<string>(key);

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

