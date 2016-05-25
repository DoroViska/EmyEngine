using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EmyEngine.Platform.SDL2
{


    public unsafe class SDL
    {

        public static byte _nmNum(char _rw)
        {
          //  byte[] s = Encoding.ASCII.GetBytes(new char[] { _rw });
            return (byte)_rw;
        }


        public const string sdl2_lib = "SDL2.dll";
        public const string sdl2_image_lib = "SDL2_image.dll";


        public const uint SDL_INIT_VIDEO = 0x00000020;
        public const int SDL_WINDOWPOS_CENTERED = 0x2FFF0000 | 0;

        public const int SDL_BUTTON_LEFT = 1;
        public const int SDL_BUTTON_MIDDLE = 2;
        public const int SDL_BUTTON_RIGHT = 3;
        public const int SDL_BUTTON_WHEELUP = 4;
        public const int SDL_BUTTON_WHEELDOWN = 5;
        public const int SDL_BUTTON_X1 = 6;
        public const int SDL_BUTTON_X2 = 7;

        [DllImport(sdl2_lib)]
        public static extern IntPtr SDL_GL_GetProcAddress(string name);

        [DllImport(sdl2_lib)]
        public static extern void SDL_GL_DeleteContext(SDL_GLContext context);
        [DllImport(sdl2_lib)]
        public static extern void SDL_GetWindowPosition(SDL_Window* window,
                           int* x,
                           int* y);

        [DllImport(sdl2_lib)]
        public static extern IntPtr SDL_GL_GetCurrentContext();


        [DllImport(sdl2_lib)]
        public static extern void SDL_FreeSurface(SDL_Surface* surface);

        [DllImport(sdl2_lib)]
        public static extern void SDL_FreeRW(SDL_RWops* are);

        [DllImport(sdl2_lib)]
        public static extern int SDL_LockSurface(SDL_Surface* surface);
        [DllImport(sdl2_lib)]
        public static extern void SDL_UnlockSurface(SDL_Surface* surface);


        [DllImport(sdl2_image_lib)]
        public static extern SDL_Surface*  IMG_Load_RW(SDL_RWops* src, int freesrc);
        [DllImport(sdl2_image_lib)]
        public static extern SDL_Surface*  IMG_LoadPNG_RW(SDL_RWops* src);
        [DllImport(sdl2_image_lib)]
        public static extern SDL_Surface*  SDL_ConvertSurface(SDL_Surface* src, SDL_PixelFormat* fmt, uint flags);



        [DllImport(sdl2_lib)]
        public static extern SDL_RWops*  SDL_RWFromMem(void* mem, int size);

        [DllImport(sdl2_lib)]
        public static extern void SDL_SetWindowSize(SDL_Window* window, int w, int h);

        [DllImport(sdl2_lib)]
        public static extern void SDL_GetWindowSize(SDL_Window* window, int* w,int* h);

        [DllImport(sdl2_lib)]
        public static extern int SDL_Init(uint flags);

        [DllImport(sdl2_lib)]
        public static extern SDL_Window*  SDL_CreateWindow(string title,int x, int y, int w, int h, SDL_WindowFlags flags);

        [DllImport(sdl2_lib)]
        public static extern SDL_GLContext SDL_GL_CreateContext(SDL_Window* window);

        [DllImport(sdl2_lib)] 
        public static extern int SDL_GL_SetAttribute(SDL_GLattr attr, int value);

        [DllImport(sdl2_lib)]
        public static extern int SDL_GL_GetAttribute(SDL_GLattr attr, int* value);

        [DllImport(sdl2_lib)]
        public static extern void SDL_Quit();

        [DllImport(sdl2_lib)]
        public static extern void SDL_GL_SwapWindow(SDL_Window* window);

        [DllImport(sdl2_lib)]
        public static extern int SDL_GetCurrentDisplayMode(int displayIndex, SDL_DisplayMode* mode);

        [DllImport(sdl2_lib)]
        public static extern bool SDL_PollEvent(SDL_Event* eventt);

        [DllImport(sdl2_lib)]
        public static extern int SDL_SetWindowFullscreen(SDL_Window* window, uint flags);

        [DllImport(sdl2_lib)]
        public static extern void SDL_SetWindowTitle(SDL_Window* window,string title);

        [DllImport(sdl2_lib)]
        public static extern uint SDL_GetMouseState(int* x, int* y);
        [DllImport(sdl2_lib)]
        public static extern byte* SDL_GetKeyboardState(int* numkeys = null);
        [DllImport(sdl2_lib)]
        public static extern void SDL_WarpMouseInWindow(SDL_Window* window, ushort x, ushort y);
        [DllImport(sdl2_lib)]
        public static extern int SDL_SetRelativeMouseMode(SDL_bool enabled);
        public static int SDL_BUTTON(int X)
        {
            return(1 << ((X) - 1));
        }

    }

    public enum SDL_EventType : uint
    {
        SDL_FIRSTEVENT = 0,     /**< Unused (do not remove) */

        /* Application events */
        SDL_QUIT = 0x100, /**< User-requested quit */

        /* These application events have special meaning on iOS, see README-ios.txt for details */
        SDL_APP_TERMINATING,        /**< The application is being terminated by the OS
                                     Called on iOS in applicationWillTerminate()
                                     Called on Android in onDestroy()
                                */
        SDL_APP_LOWMEMORY,          /**< The application is low on memory, free memory if possible.
                                     Called on iOS in applicationDidReceiveMemoryWarning()
                                     Called on Android in onLowMemory()
                                */
        SDL_APP_WILLENTERBACKGROUND, /**< The application is about to enter the background
                                     Called on iOS in applicationWillResignActive()
                                     Called on Android in onPause()
                                */
        SDL_APP_DIDENTERBACKGROUND, /**< The application did enter the background and may not get CPU for some time
                                     Called on iOS in applicationDidEnterBackground()
                                     Called on Android in onPause()
                                */
        SDL_APP_WILLENTERFOREGROUND, /**< The application is about to enter the foreground
                                     Called on iOS in applicationWillEnterForeground()
                                     Called on Android in onResume()
                                */
        SDL_APP_DIDENTERFOREGROUND, /**< The application is now interactive
                                     Called on iOS in applicationDidBecomeActive()
                                     Called on Android in onResume()
                                */

        /* Window events */
        SDL_WINDOWEVENT = 0x200, /**< Window state change */
        SDL_SYSWMEVENT,             /**< System specific event */

        /* Keyboard events */
        SDL_KEYDOWN = 0x300, /**< Key pressed */
        SDL_KEYUP,                  /**< Key released */
        SDL_TEXTEDITING,            /**< Keyboard text editing (composition) */
        SDL_TEXTINPUT,              /**< Keyboard text input */

        /* Mouse events */
        SDL_MOUSEMOTION = 0x400, /**< Mouse moved */
        SDL_MOUSEBUTTONDOWN,        /**< Mouse button pressed */
        SDL_MOUSEBUTTONUP,          /**< Mouse button released */
        SDL_MOUSEWHEEL,             /**< Mouse wheel motion */

        /* Joystick events */
        SDL_JOYAXISMOTION = 0x600, /**< Joystick axis motion */
        SDL_JOYBALLMOTION,          /**< Joystick trackball motion */
        SDL_JOYHATMOTION,           /**< Joystick hat position change */
        SDL_JOYBUTTONDOWN,          /**< Joystick button pressed */
        SDL_JOYBUTTONUP,            /**< Joystick button released */
        SDL_JOYDEVICEADDED,         /**< A new joystick has been inserted into the system */
        SDL_JOYDEVICEREMOVED,       /**< An opened joystick has been removed */

        /* Game controller events */
        SDL_CONTROLLERAXISMOTION = 0x650, /**< Game controller axis motion */
        SDL_CONTROLLERBUTTONDOWN,          /**< Game controller button pressed */
        SDL_CONTROLLERBUTTONUP,            /**< Game controller button released */
        SDL_CONTROLLERDEVICEADDED,         /**< A new Game controller has been inserted into the system */
        SDL_CONTROLLERDEVICEREMOVED,       /**< An opened Game controller has been removed */
        SDL_CONTROLLERDEVICEREMAPPED,      /**< The controller mapping was updated */

        /* Touch events */
        SDL_FINGERDOWN = 0x700,
        SDL_FINGERUP,
        SDL_FINGERMOTION,

        /* Gesture events */
        SDL_DOLLARGESTURE = 0x800,
        SDL_DOLLARRECORD,
        SDL_MULTIGESTURE,

        /* Clipboard events */
        SDL_CLIPBOARDUPDATE = 0x900, /**< The clipboard changed */

        /* Drag and drop events */
        SDL_DROPFILE = 0x1000, /**< The system requests a file open */

        /* Render events */
        SDL_RENDER_TARGETS_RESET = 0x2000, /**< The render targets have been reset */

        /** Events ::SDL_USEREVENT through ::SDL_LASTEVENT are for your use,
         *  and should be allocated with SDL_RegisterEvents()
         */
        SDL_USEREVENT = 0x8000,

        /**
         *  This last event is only for bounding internal arrays
         */
        SDL_LASTEVENT = 0xFFFF
    }
    
    public enum SDL_GLattr : uint
    {
        SDL_GL_RED_SIZE,
        SDL_GL_GREEN_SIZE,
        SDL_GL_BLUE_SIZE,
        SDL_GL_ALPHA_SIZE,
        SDL_GL_BUFFER_SIZE,
        SDL_GL_DOUBLEBUFFER,
        SDL_GL_DEPTH_SIZE,
        SDL_GL_STENCIL_SIZE,
        SDL_GL_ACCUM_RED_SIZE,
        SDL_GL_ACCUM_GREEN_SIZE,
        SDL_GL_ACCUM_BLUE_SIZE,
        SDL_GL_ACCUM_ALPHA_SIZE,
        SDL_GL_STEREO,
        SDL_GL_MULTISAMPLEBUFFERS,
        SDL_GL_MULTISAMPLESAMPLES,
        SDL_GL_ACCELERATED_VISUAL,
        SDL_GL_RETAINED_BACKING,
        SDL_GL_CONTEXT_MAJOR_VERSION,
        SDL_GL_CONTEXT_MINOR_VERSION,
        SDL_GL_CONTEXT_EGL,
        SDL_GL_CONTEXT_FLAGS,
        SDL_GL_CONTEXT_PROFILE_MASK,
        SDL_GL_SHARE_WITH_CURRENT_CONTEXT,
        SDL_GL_FRAMEBUFFER_SRGB_CAPABLE
    }

    public enum SDL_WindowFlags : uint
    {
        SDL_WINDOW_FULLSCREEN = 0x00000001,         /**< fullscreen window */
        SDL_WINDOW_OPENGL = 0x00000002,             /**< window usable with OpenGL context */
        SDL_WINDOW_SHOWN = 0x00000004,              /**< window is visible */
        SDL_WINDOW_HIDDEN = 0x00000008,             /**< window is not visible */
        SDL_WINDOW_BORDERLESS = 0x00000010,         /**< no window decoration */
        SDL_WINDOW_RESIZABLE = 0x00000020,          /**< window can be resized */
        SDL_WINDOW_MINIMIZED = 0x00000040,          /**< window is minimized */
        SDL_WINDOW_MAXIMIZED = 0x00000080,          /**< window is maximized */
        SDL_WINDOW_INPUT_GRABBED = 0x00000100,      /**< window has grabbed input focus */
        SDL_WINDOW_INPUT_FOCUS = 0x00000200,        /**< window has input focus */
        SDL_WINDOW_MOUSE_FOCUS = 0x00000400,        /**< window has mouse focus */
        SDL_WINDOW_FULLSCREEN_DESKTOP = (SDL_WINDOW_FULLSCREEN | 0x00001000),
        SDL_WINDOW_FOREIGN = 0x00000800,            /**< window not created by SDL */
        SDL_WINDOW_ALLOW_HIGHDPI = 0x00002000       /**< window should be created in high-DPI mode if supported */
    }

    public enum SDL_Scancode : uint
    {
        SDL_SCANCODE_UNKNOWN = 0,

        /**
         *  \name Usage page 0x07
         *
         *  These values are from usage page 0x07 (USB keyboard page).
         */
        /* @{ */

        SDL_SCANCODE_A = 4,
        SDL_SCANCODE_B = 5,
        SDL_SCANCODE_C = 6,
        SDL_SCANCODE_D = 7,
        SDL_SCANCODE_E = 8,
        SDL_SCANCODE_F = 9,
        SDL_SCANCODE_G = 10,
        SDL_SCANCODE_H = 11,
        SDL_SCANCODE_I = 12,
        SDL_SCANCODE_J = 13,
        SDL_SCANCODE_K = 14,
        SDL_SCANCODE_L = 15,
        SDL_SCANCODE_M = 16,
        SDL_SCANCODE_N = 17,
        SDL_SCANCODE_O = 18,
        SDL_SCANCODE_P = 19,
        SDL_SCANCODE_Q = 20,
        SDL_SCANCODE_R = 21,
        SDL_SCANCODE_S = 22,
        SDL_SCANCODE_T = 23,
        SDL_SCANCODE_U = 24,
        SDL_SCANCODE_V = 25,
        SDL_SCANCODE_W = 26,
        SDL_SCANCODE_X = 27,
        SDL_SCANCODE_Y = 28,
        SDL_SCANCODE_Z = 29,

        SDL_SCANCODE_1 = 30,
        SDL_SCANCODE_2 = 31,
        SDL_SCANCODE_3 = 32,
        SDL_SCANCODE_4 = 33,
        SDL_SCANCODE_5 = 34,
        SDL_SCANCODE_6 = 35,
        SDL_SCANCODE_7 = 36,
        SDL_SCANCODE_8 = 37,
        SDL_SCANCODE_9 = 38,
        SDL_SCANCODE_0 = 39,

        SDL_SCANCODE_RETURN = 40,
        SDL_SCANCODE_ESCAPE = 41,
        SDL_SCANCODE_BACKSPACE = 42,
        SDL_SCANCODE_TAB = 43,
        SDL_SCANCODE_SPACE = 44,

        SDL_SCANCODE_MINUS = 45,
        SDL_SCANCODE_EQUALS = 46,
        SDL_SCANCODE_LEFTBRACKET = 47,
        SDL_SCANCODE_RIGHTBRACKET = 48,
        SDL_SCANCODE_BACKSLASH = 49, /**< Located at the lower left of the return
                                  *   key on ISO keyboards and at the right end
                                  *   of the QWERTY row on ANSI keyboards.
                                  *   Produces REVERSE SOLIDUS (backslash) and
                                  *   VERTICAL LINE in a US layout, REVERSE
                                  *   SOLIDUS and VERTICAL LINE in a UK Mac
                                  *   layout, NUMBER SIGN and TILDE in a UK
                                  *   Windows layout, DOLLAR SIGN and POUND SIGN
                                  *   in a Swiss German layout, NUMBER SIGN and
                                  *   APOSTROPHE in a German layout, GRAVE
                                  *   ACCENT and POUND SIGN in a French Mac
                                  *   layout, and ASTERISK and MICRO SIGN in a
                                  *   French Windows layout.
                                  */
        SDL_SCANCODE_NONUSHASH = 50, /**< ISO USB keyboards actually use this code
                                  *   instead of 49 for the same key, but all
                                  *   OSes I've seen treat the two codes
                                  *   identically. So, as an implementor, unless
                                  *   your keyboard generates both of those
                                  *   codes and your OS treats them differently,
                                  *   you should generate SDL_SCANCODE_BACKSLASH
                                  *   instead of this code. As a user, you
                                  *   should not rely on this code because SDL
                                  *   will never generate it with most (all?)
                                  *   keyboards.
                                  */
        SDL_SCANCODE_SEMICOLON = 51,
        SDL_SCANCODE_APOSTROPHE = 52,
        SDL_SCANCODE_GRAVE = 53, /**< Located in the top left corner (on both ANSI
                              *   and ISO keyboards). Produces GRAVE ACCENT and
                              *   TILDE in a US Windows layout and in US and UK
                              *   Mac layouts on ANSI keyboards, GRAVE ACCENT
                              *   and NOT SIGN in a UK Windows layout, SECTION
                              *   SIGN and PLUS-MINUS SIGN in US and UK Mac
                              *   layouts on ISO keyboards, SECTION SIGN and
                              *   DEGREE SIGN in a Swiss German layout (Mac:
                              *   only on ISO keyboards), CIRCUMFLEX ACCENT and
                              *   DEGREE SIGN in a German layout (Mac: only on
                              *   ISO keyboards), SUPERSCRIPT TWO and TILDE in a
                              *   French Windows layout, COMMERCIAL AT and
                              *   NUMBER SIGN in a French Mac layout on ISO
                              *   keyboards, and LESS-THAN SIGN and GREATER-THAN
                              *   SIGN in a Swiss German, German, or French Mac
                              *   layout on ANSI keyboards.
                              */
        SDL_SCANCODE_COMMA = 54,
        SDL_SCANCODE_PERIOD = 55,
        SDL_SCANCODE_SLASH = 56,

        SDL_SCANCODE_CAPSLOCK = 57,

        SDL_SCANCODE_F1 = 58,
        SDL_SCANCODE_F2 = 59,
        SDL_SCANCODE_F3 = 60,
        SDL_SCANCODE_F4 = 61,
        SDL_SCANCODE_F5 = 62,
        SDL_SCANCODE_F6 = 63,
        SDL_SCANCODE_F7 = 64,
        SDL_SCANCODE_F8 = 65,
        SDL_SCANCODE_F9 = 66,
        SDL_SCANCODE_F10 = 67,
        SDL_SCANCODE_F11 = 68,
        SDL_SCANCODE_F12 = 69,

        SDL_SCANCODE_PRINTSCREEN = 70,
        SDL_SCANCODE_SCROLLLOCK = 71,
        SDL_SCANCODE_PAUSE = 72,
        SDL_SCANCODE_INSERT = 73, /**< insert on PC, help on some Mac keyboards (but
                                   does send code 73, not 117) */
        SDL_SCANCODE_HOME = 74,
        SDL_SCANCODE_PAGEUP = 75,
        SDL_SCANCODE_DELETE = 76,
        SDL_SCANCODE_END = 77,
        SDL_SCANCODE_PAGEDOWN = 78,
        SDL_SCANCODE_RIGHT = 79,
        SDL_SCANCODE_LEFT = 80,
        SDL_SCANCODE_DOWN = 81,
        SDL_SCANCODE_UP = 82,

        SDL_SCANCODE_NUMLOCKCLEAR = 83, /**< num lock on PC, clear on Mac keyboards
                                     */
        SDL_SCANCODE_KP_DIVIDE = 84,
        SDL_SCANCODE_KP_MULTIPLY = 85,
        SDL_SCANCODE_KP_MINUS = 86,
        SDL_SCANCODE_KP_PLUS = 87,
        SDL_SCANCODE_KP_ENTER = 88,
        SDL_SCANCODE_KP_1 = 89,
        SDL_SCANCODE_KP_2 = 90,
        SDL_SCANCODE_KP_3 = 91,
        SDL_SCANCODE_KP_4 = 92,
        SDL_SCANCODE_KP_5 = 93,
        SDL_SCANCODE_KP_6 = 94,
        SDL_SCANCODE_KP_7 = 95,
        SDL_SCANCODE_KP_8 = 96,
        SDL_SCANCODE_KP_9 = 97,
        SDL_SCANCODE_KP_0 = 98,
        SDL_SCANCODE_KP_PERIOD = 99,

        SDL_SCANCODE_NONUSBACKSLASH = 100, /**< This is the additional key that ISO
                                        *   keyboards have over ANSI ones,
                                        *   located between left shift and Y.
                                        *   Produces GRAVE ACCENT and TILDE in a
                                        *   US or UK Mac layout, REVERSE SOLIDUS
                                        *   (backslash) and VERTICAL LINE in a
                                        *   US or UK Windows layout, and
                                        *   LESS-THAN SIGN and GREATER-THAN SIGN
                                        *   in a Swiss German, German, or French
                                        *   layout. */
        SDL_SCANCODE_APPLICATION = 101, /**< windows contextual menu, compose */
        SDL_SCANCODE_POWER = 102, /**< The USB document says this is a status flag,
                               *   not a physical key - but some Mac keyboards
                               *   do have a power key. */
        SDL_SCANCODE_KP_EQUALS = 103,
        SDL_SCANCODE_F13 = 104,
        SDL_SCANCODE_F14 = 105,
        SDL_SCANCODE_F15 = 106,
        SDL_SCANCODE_F16 = 107,
        SDL_SCANCODE_F17 = 108,
        SDL_SCANCODE_F18 = 109,
        SDL_SCANCODE_F19 = 110,
        SDL_SCANCODE_F20 = 111,
        SDL_SCANCODE_F21 = 112,
        SDL_SCANCODE_F22 = 113,
        SDL_SCANCODE_F23 = 114,
        SDL_SCANCODE_F24 = 115,
        SDL_SCANCODE_EXECUTE = 116,
        SDL_SCANCODE_HELP = 117,
        SDL_SCANCODE_MENU = 118,
        SDL_SCANCODE_SELECT = 119,
        SDL_SCANCODE_STOP = 120,
        SDL_SCANCODE_AGAIN = 121,   /**< redo */
        SDL_SCANCODE_UNDO = 122,
        SDL_SCANCODE_CUT = 123,
        SDL_SCANCODE_COPY = 124,
        SDL_SCANCODE_PASTE = 125,
        SDL_SCANCODE_FIND = 126,
        SDL_SCANCODE_MUTE = 127,
        SDL_SCANCODE_VOLUMEUP = 128,
        SDL_SCANCODE_VOLUMEDOWN = 129,
        /* not sure whether there's a reason to enable these */
        /*     SDL_SCANCODE_LOCKINGCAPSLOCK = 130,  */
        /*     SDL_SCANCODE_LOCKINGNUMLOCK = 131, */
        /*     SDL_SCANCODE_LOCKINGSCROLLLOCK = 132, */
        SDL_SCANCODE_KP_COMMA = 133,
        SDL_SCANCODE_KP_EQUALSAS400 = 134,

        SDL_SCANCODE_INTERNATIONAL1 = 135, /**< used on Asian keyboards, see
                                            footnotes in USB doc */
        SDL_SCANCODE_INTERNATIONAL2 = 136,
        SDL_SCANCODE_INTERNATIONAL3 = 137, /**< Yen */
        SDL_SCANCODE_INTERNATIONAL4 = 138,
        SDL_SCANCODE_INTERNATIONAL5 = 139,
        SDL_SCANCODE_INTERNATIONAL6 = 140,
        SDL_SCANCODE_INTERNATIONAL7 = 141,
        SDL_SCANCODE_INTERNATIONAL8 = 142,
        SDL_SCANCODE_INTERNATIONAL9 = 143,
        SDL_SCANCODE_LANG1 = 144, /**< Hangul/English toggle */
        SDL_SCANCODE_LANG2 = 145, /**< Hanja conversion */
        SDL_SCANCODE_LANG3 = 146, /**< Katakana */
        SDL_SCANCODE_LANG4 = 147, /**< Hiragana */
        SDL_SCANCODE_LANG5 = 148, /**< Zenkaku/Hankaku */
        SDL_SCANCODE_LANG6 = 149, /**< reserved */
        SDL_SCANCODE_LANG7 = 150, /**< reserved */
        SDL_SCANCODE_LANG8 = 151, /**< reserved */
        SDL_SCANCODE_LANG9 = 152, /**< reserved */

        SDL_SCANCODE_ALTERASE = 153, /**< Erase-Eaze */
        SDL_SCANCODE_SYSREQ = 154,
        SDL_SCANCODE_CANCEL = 155,
        SDL_SCANCODE_CLEAR = 156,
        SDL_SCANCODE_PRIOR = 157,
        SDL_SCANCODE_RETURN2 = 158,
        SDL_SCANCODE_SEPARATOR = 159,
        SDL_SCANCODE_OUT = 160,
        SDL_SCANCODE_OPER = 161,
        SDL_SCANCODE_CLEARAGAIN = 162,
        SDL_SCANCODE_CRSEL = 163,
        SDL_SCANCODE_EXSEL = 164,

        SDL_SCANCODE_KP_00 = 176,
        SDL_SCANCODE_KP_000 = 177,
        SDL_SCANCODE_THOUSANDSSEPARATOR = 178,
        SDL_SCANCODE_DECIMALSEPARATOR = 179,
        SDL_SCANCODE_CURRENCYUNIT = 180,
        SDL_SCANCODE_CURRENCYSUBUNIT = 181,
        SDL_SCANCODE_KP_LEFTPAREN = 182,
        SDL_SCANCODE_KP_RIGHTPAREN = 183,
        SDL_SCANCODE_KP_LEFTBRACE = 184,
        SDL_SCANCODE_KP_RIGHTBRACE = 185,
        SDL_SCANCODE_KP_TAB = 186,
        SDL_SCANCODE_KP_BACKSPACE = 187,
        SDL_SCANCODE_KP_A = 188,
        SDL_SCANCODE_KP_B = 189,
        SDL_SCANCODE_KP_C = 190,
        SDL_SCANCODE_KP_D = 191,
        SDL_SCANCODE_KP_E = 192,
        SDL_SCANCODE_KP_F = 193,
        SDL_SCANCODE_KP_XOR = 194,
        SDL_SCANCODE_KP_POWER = 195,
        SDL_SCANCODE_KP_PERCENT = 196,
        SDL_SCANCODE_KP_LESS = 197,
        SDL_SCANCODE_KP_GREATER = 198,
        SDL_SCANCODE_KP_AMPERSAND = 199,
        SDL_SCANCODE_KP_DBLAMPERSAND = 200,
        SDL_SCANCODE_KP_VERTICALBAR = 201,
        SDL_SCANCODE_KP_DBLVERTICALBAR = 202,
        SDL_SCANCODE_KP_COLON = 203,
        SDL_SCANCODE_KP_HASH = 204,
        SDL_SCANCODE_KP_SPACE = 205,
        SDL_SCANCODE_KP_AT = 206,
        SDL_SCANCODE_KP_EXCLAM = 207,
        SDL_SCANCODE_KP_MEMSTORE = 208,
        SDL_SCANCODE_KP_MEMRECALL = 209,
        SDL_SCANCODE_KP_MEMCLEAR = 210,
        SDL_SCANCODE_KP_MEMADD = 211,
        SDL_SCANCODE_KP_MEMSUBTRACT = 212,
        SDL_SCANCODE_KP_MEMMULTIPLY = 213,
        SDL_SCANCODE_KP_MEMDIVIDE = 214,
        SDL_SCANCODE_KP_PLUSMINUS = 215,
        SDL_SCANCODE_KP_CLEAR = 216,
        SDL_SCANCODE_KP_CLEARENTRY = 217,
        SDL_SCANCODE_KP_BINARY = 218,
        SDL_SCANCODE_KP_OCTAL = 219,
        SDL_SCANCODE_KP_DECIMAL = 220,
        SDL_SCANCODE_KP_HEXADECIMAL = 221,

        SDL_SCANCODE_LCTRL = 224,
        SDL_SCANCODE_LSHIFT = 225,
        SDL_SCANCODE_LALT = 226, /**< alt, option */
        SDL_SCANCODE_LGUI = 227, /**< windows, command (apple), meta */
        SDL_SCANCODE_RCTRL = 228,
        SDL_SCANCODE_RSHIFT = 229,
        SDL_SCANCODE_RALT = 230, /**< alt gr, option */
        SDL_SCANCODE_RGUI = 231, /**< windows, command (apple), meta */

        SDL_SCANCODE_MODE = 257,    /**< I'm not sure if this is really not covered
                                 *   by any of the above, but since there's a
                                 *   special KMOD_MODE for it I'm adding it here
                                 */

        /* @} *//* Usage page 0x07 */

        /**
         *  \name Usage page 0x0C
         *
         *  These values are mapped from usage page 0x0C (USB consumer page).
         */
        /* @{ */

        SDL_SCANCODE_AUDIONEXT = 258,
        SDL_SCANCODE_AUDIOPREV = 259,
        SDL_SCANCODE_AUDIOSTOP = 260,
        SDL_SCANCODE_AUDIOPLAY = 261,
        SDL_SCANCODE_AUDIOMUTE = 262,
        SDL_SCANCODE_MEDIASELECT = 263,
        SDL_SCANCODE_WWW = 264,
        SDL_SCANCODE_MAIL = 265,
        SDL_SCANCODE_CALCULATOR = 266,
        SDL_SCANCODE_COMPUTER = 267,
        SDL_SCANCODE_AC_SEARCH = 268,
        SDL_SCANCODE_AC_HOME = 269,
        SDL_SCANCODE_AC_BACK = 270,
        SDL_SCANCODE_AC_FORWARD = 271,
        SDL_SCANCODE_AC_STOP = 272,
        SDL_SCANCODE_AC_REFRESH = 273,
        SDL_SCANCODE_AC_BOOKMARKS = 274,

        /* @} *//* Usage page 0x0C */

        /**
         *  \name Walther keys
         *
         *  These are values that Christian Walther added (for mac keyboard?).
         */
        /* @{ */

        SDL_SCANCODE_BRIGHTNESSDOWN = 275,
        SDL_SCANCODE_BRIGHTNESSUP = 276,
        SDL_SCANCODE_DISPLAYSWITCH = 277, /**< display mirroring/dual display
                                           switch, video mode switch */
        SDL_SCANCODE_KBDILLUMTOGGLE = 278,
        SDL_SCANCODE_KBDILLUMDOWN = 279,
        SDL_SCANCODE_KBDILLUMUP = 280,
        SDL_SCANCODE_EJECT = 281,
        SDL_SCANCODE_SLEEP = 282,

        SDL_SCANCODE_APP1 = 283,
        SDL_SCANCODE_APP2 = 284,

        /* @} *//* Walther keys */

        /* Add any other keys here. */

        SDL_NUM_SCANCODES = 512 /**< not a key, just marks the number of scancodes
                                 for array bounds */
    }

    public enum SDL_WindowEventID : byte
    {
        SDL_WINDOWEVENT_NONE,           /**< Never used */
        SDL_WINDOWEVENT_SHOWN,          /**< Window has been shown */
        SDL_WINDOWEVENT_HIDDEN,         /**< Window has been hidden */
        SDL_WINDOWEVENT_EXPOSED,        /**< Window has been exposed and should be
                                         redrawn */
        SDL_WINDOWEVENT_MOVED,          /**< Window has been moved to data1, data2
                                     */
        SDL_WINDOWEVENT_RESIZED,        /**< Window has been resized to data1xdata2 */
        SDL_WINDOWEVENT_SIZE_CHANGED,   /**< The window size has changed, either as a result of an API call or through the system or user changing the window size. */
        SDL_WINDOWEVENT_MINIMIZED,      /**< Window has been minimized */
        SDL_WINDOWEVENT_MAXIMIZED,      /**< Window has been maximized */
        SDL_WINDOWEVENT_RESTORED,       /**< Window has been restored to normal size
                                         and position */
        SDL_WINDOWEVENT_ENTER,          /**< Window has gained mouse focus */
        SDL_WINDOWEVENT_LEAVE,          /**< Window has lost mouse focus */
        SDL_WINDOWEVENT_FOCUS_GAINED,   /**< Window has gained keyboard focus */
        SDL_WINDOWEVENT_FOCUS_LOST,     /**< Window has lost keyboard focus */
        SDL_WINDOWEVENT_CLOSE           /**< The window manager requests that the
                                         window be closed */
    }

    public enum SDL_bool : uint
    {
        SDL_FALSE = 0,
        SDL_TRUE = 1
    }

    public class Key
    {

        public const byte SDLK_UNKNOWN = 0;

        public static int SDLK_RETURN = SDL._nmNum('\r');
        public static int SDLK_ESCAPE = 27;
        public static int SDLK_BACKSPACE = SDL._nmNum('\b');
        public static int SDLK_TAB = SDL._nmNum('\t');
        public static int SDLK_SPACE = SDL._nmNum(' ');
        public static int SDLK_EXCLAIM = SDL._nmNum('!');
        public static int SDLK_QUOTEDBL = SDL._nmNum('"');
        public static int SDLK_HASH = SDL._nmNum('#');
        public static int SDLK_PERCENT = SDL._nmNum('%');
        public static int SDLK_DOLLAR = SDL._nmNum('$');
        public static int SDLK_AMPERSAND = SDL._nmNum('&');
        public static int SDLK_QUOTE = SDL._nmNum('\'');
        public static int SDLK_LEFTPAREN = SDL._nmNum('(');
        public static int SDLK_RIGHTPAREN = SDL._nmNum(')');
        public static int SDLK_ASTERISK = SDL._nmNum('*');
        public static int SDLK_PLUS = SDL._nmNum('+');
        public static int SDLK_COMMA = SDL._nmNum(',');
        public static int SDLK_MINUS = SDL._nmNum('-');
        public static int SDLK_PERIOD = SDL._nmNum('.');
        public static int SDLK_SLASH = SDL._nmNum('/');
        public static int SDLK_0 = SDL._nmNum('0');
        public static int SDLK_1 = SDL._nmNum('1');
        public static int SDLK_2 = SDL._nmNum('2');
        public static int SDLK_3 = SDL._nmNum('3');
        public static int SDLK_4 = SDL._nmNum('4');
        public static int SDLK_5 = SDL._nmNum('5');
        public static int SDLK_6 = SDL._nmNum('6');
        public static int SDLK_7 = SDL._nmNum('7');
        public static int SDLK_8 = SDL._nmNum('8');
        public static int SDLK_9 = SDL._nmNum('9');
        public static int SDLK_COLON = SDL._nmNum(':');
        public static int SDLK_SEMICOLON = SDL._nmNum(';');
        public static int SDLK_LESS = SDL._nmNum('<');
        public static int SDLK_EQUALS = SDL._nmNum('=');
        public static int SDLK_GREATER = SDL._nmNum('>');
        public static int SDLK_QUESTION = SDL._nmNum('?');
        public static int SDLK_AT = SDL._nmNum('@');
        /*
           Skip uppercase letters
         */
        public static int SDLK_LEFTBRACKET = SDL._nmNum('[');
        public static int SDLK_BACKSLASH = SDL._nmNum('\\');
        public static int SDLK_RIGHTBRACKET = SDL._nmNum(']');
        public static int SDLK_CARET = SDL._nmNum('^');
        public static int SDLK_UNDERSCORE = SDL._nmNum('_');
        public static int SDLK_BACKQUOTE = SDL._nmNum('`');
        public static int SDLK_a = SDL._nmNum('a');
        public static int SDLK_b = SDL._nmNum('b');
        public static int SDLK_c = SDL._nmNum('c');
        public static int SDLK_d = SDL._nmNum('d');
        public static int SDLK_e = SDL._nmNum('e');
        public static int SDLK_f = SDL._nmNum('f');
        public static int SDLK_g = SDL._nmNum('g');
        public static int SDLK_h = SDL._nmNum('h');
        public static int SDLK_i = SDL._nmNum('i');
        public static int SDLK_j = SDL._nmNum('j');
        public static int SDLK_k = SDL._nmNum('k');
        public static int SDLK_l = SDL._nmNum('l');
        public static int SDLK_m = SDL._nmNum('m');
        public static int SDLK_n = SDL._nmNum('n');
        public static int SDLK_o = SDL._nmNum('o');
        public static int SDLK_p = SDL._nmNum('p');
        public static int SDLK_q = SDL._nmNum('q');
        public static int SDLK_r = SDL._nmNum('r');
        public static int SDLK_s = SDL._nmNum('s');
        public static int SDLK_t = SDL._nmNum('t');
        public static int SDLK_u = SDL._nmNum('u');
        public static int SDLK_v = SDL._nmNum('v');
        public static int SDLK_w = SDL._nmNum('w');
        public static int SDLK_x = SDL._nmNum('x');
        public static int SDLK_y = SDL._nmNum('y');
        public static int SDLK_z = SDL._nmNum('z');



    }


    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_Color
    {
        public byte r;
        public byte g;
        public byte b;
        public byte a;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_Palette
    {
        public int ncolors;
        public SDL_Color* colors;
        public uint version;
        public int refcount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_PixelFormat
    {
        public uint format;
        public SDL_Palette* palette;
        public byte BitsPerPixel;
        public byte BytesPerPixel;
        public fixed byte padding[2];
        public uint Rmask;
        public uint Gmask;
        public uint Bmask;
        public uint Amask;
        public byte Rloss;
        public byte Gloss;
        public byte Bloss;
        public byte Aloss;
        public byte Rshift;
        public byte Gshift;
        public byte Bshift;
        public byte Ashift;
        public int refcount;
        public SDL_PixelFormat* next;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_Rect
    {
        public int x, y;
        public int w, h;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_BlitMap { }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_RWops { }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_Surface
    {
        public uint flags;               /**< Read-only */
        public SDL_PixelFormat* format;    /**< Read-only */
        public int w, h;                   /**< Read-only */
        public int pitch;                  /**< Read-only */
        public void* pixels;               /**< Read-write */

        /** Application data associated with the surface */
        public void* userdata;             /**< Read-write */

        /** information needed for surfaces requiring locks */
        public int locked;                 /**< Read-only */
        public void* lock_data;            /**< Read-only */

        /** clipping information */
        public SDL_Rect clip_rect;         /**< Read-only */

        /** info for fast blit mapping to other surfaces */
        public SDL_BlitMap* map;    /**< Private */

     /** Reference count -- used when freeing surface */
        int refcount;               /**< Read-mostly */
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_DisplayMode
    {
        public uint format;              /**< pixel format */
        public int w;                      /**< width */
        public int h;                      /**< height */
        public int refresh_rate;           /**< refresh rate (or zero for unspecified) */
        public void* driverdata;           /**< driver-specific data, initialize to 0 */
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_Window { }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_GLContext { public void* ptr; }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_Event
    {
        /*
        public SDL_KeyboardEvent window
        {
            fixed (void* p = &this)
            {
                return *((SDL_KeyboardEvent*)p);
            }
        }

        */

        public SDL_WindowEvent window
        {
            get
            {

                fixed (void* p = &this)
                {
                    return *((SDL_WindowEvent*)p);
                }

            }
        }

        public SDL_KeyboardEvent key { get {

                fixed (void* p = &this)
                {
                    return *((SDL_KeyboardEvent*)p);
                }

        }  }
       


        public uint type;
        public fixed byte rw[52];
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_Keysym
    {
        public SDL_Scancode scancode;      /**< SDL physical key code - see ::SDL_Scancode for details */
        public int sym;            /**< SDL virtual key code - see ::SDL_Keycode for details */
        public ushort mod;                 /**< current key modifiers */
        public uint unused;
    }
 
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_KeyboardEvent
    {
        public uint type;        /**< ::SDL_KEYDOWN or ::SDL_KEYUP */
        public uint timestamp;
        public uint windowID;    /**< The window with keyboard focus, if any */
        public byte state;        /**< ::SDL_PRESSED or ::SDL_RELEASED */
        public byte repeat;       /**< Non-zero if this is a key repeat */
        public byte padding2;
        public byte padding3;
        public SDL_Keysym keysym;  /**< The key that was pressed or released */
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SDL_WindowEvent
    {
        public uint type;        /**< SDL_WINDOWEVENT */
        public uint timestamp;
        public uint windowID;    /**< The associated window */
        public SDL_WindowEventID eventt;        /**< SDL_WindowEventID */
        public byte padding1;
        public byte padding2;
        public byte padding3;
        public int data1;       /**< event dependent data */
        public int data2;       /**< event dependent data */
     }
   
    
}
