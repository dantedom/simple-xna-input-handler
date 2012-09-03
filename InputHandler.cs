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

    public class InputHandler  : GameComponent
    {
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
        public InputHandler(Game game)
            : base(game)
        {


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
            this.mouseRightClicked = (this.MouseState.RightButton == ButtonState.Pressed) && (this.Old_MouseState.RightButton != this.MouseState.RightButton);
            if (this.MouseState.LeftButton == ButtonState.Pressed)
            {
                if (this.continuous_leftclick != null)
                {
                    this.continuous_leftclick(this);
                }
            }
            if (this.mouseLeftClicked)
            {
                if (this.leftclick != null)
                {
                    this.leftclick(this);
                }
            }
            if (this.MouseState.RightButton == ButtonState.Pressed)
            {
                if (this.continuous_rightclick != null)
                {
                    this.continuous_rightclick(this);
                }

            }
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
                string key =StringKeys[i];
                if (!Old_StringKeys.Contains<string>(key ))
                {

                    PressedKeysOnce.Add(key);
                }
            }
            this.StringKeys_Once = PressedKeysOnce.ToArray();
        }
        public bool IsTextPressed_Continuous(string key)
        {

            return this.StringKeys.Contains<string>(key);
           
        }
        public bool IsTextPressed_Once(string key)
        {

            return this.StringKeys_Once.Contains<string>(key);

        }

 
    }
}

