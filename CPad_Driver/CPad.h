#pragma once
#include <chrono>
#include <string>
#include <vector>
#include <Windows.h>
#include <fstream>
#include "ArduSerial.h"

#ifdef CPAD_DRIVER_EXPORTS
#define CPAD_DRIVER_API __declspec(dllexport)
#else
#define CPAD_DRIVER_API __declspec(dllimport)
#endif

static bool init = false;

void setup();

void loop();

//Enumration of all the keyboard keys available to macro 
enum BTN
{
    BACKSPACE = 0x08,
    TAB,
    ENTER = 0x0D,
    SHIFT = 0x10,
    CONTROL,
    ALT,
    PAUSE,
    CAPS,
    ESCAPE = 0x1B,
    SPACE = 0x20,
    PAGE_UP,
    PAGE_DOWN,
    HOME,
    LEFT,
    UP,
    RIGHT,
    DOWN,
    SELECT,
    PRINT = 0x2A,
    EXECUTE,
    PRTSCRN,
    INSERT,
    DEL,
    HELP,
    KEY_0 = 0x30,
    KEY_1,
    KEY_2,
    KEY_3,
    KEY_4,
    KEY_5,
    KEY_6,
    KEY_7,
    KEY_8,
    KEY_9,
    KEY_A = 0x41,
    KEY_B,
    KEY_C,
    KEY_D,
    KEY_E,
    KEY_F,
    KEY_G,
    KEY_H,
    KEY_I,
    KEY_J,
    KEY_K,
    KEY_L,
    KEY_M,
    KEY_N,
    KEY_O,
    KEY_P,
    KEY_Q,
    KEY_R,
    KEY_S,
    KEY_T,
    KEY_U,
    KEY_V,
    KEY_W,
    KEY_X,
    KEY_Y,
    KEY_Z,
    LWIN = 0x5B,
    RWIN,
    APPS,
    SLEEP = 0x5F,
    NUMPAD_0,
    NUMPAD_1,
    NUMPAD_2,
    NUMPAD_3,
    NUMPAD_4,
    NUMPAD_5,
    NUMPAD_6,
    NUMPAD_7,
    NUMPAD_8,
    NUMPAD_9,
    MULTIPLY,
    ADD,
    SEPARATOR,
    SUBTRACT,
    DOT,
    DIVIDE,
    F1,
    F2,
    F3,
    F4,
    F5,
    F6,
    F7,
    F8,
    F9,
    F10,
    F11,
    F12,
    F13,
    F14,
    F15,
    F16,
    F17,
    F18,
    F19,
    F20,
    F21,
    F22,
    F23,
    F24,
    NUMLOCK = 0x90,
    SCROLL,
    LSHIFT = 0xA0,
    RSHIFT,
    LCONTROL,
    RCONTROL,
    LALT,
    RALT,
    VOLUME_MUTE = 0xAD,
    VOLUME_DOWN,
    VOLUME_UP,
    MEDIA_NEXT,
    MEDIA_PREVIOUS,
    MEDIA_STOP,
    MEDIA_PLAY_PAUSE
};

static bool connected;

extern "C" CPAD_DRIVER_API
bool isConnected();

//Key press simulation by index
extern "C" CPAD_DRIVER_API
void keyPress(int buttonIndex, int runType);

//Key release simulation by index
extern "C" CPAD_DRIVER_API
void keyRelease(int buttonIndex);

//Return true if button by index is being held down
extern "C" CPAD_DRIVER_API
bool isKeyHeld(int buttonIndex);

//Adds a keyboard key to a macro of a button by index 
extern "C" CPAD_DRIVER_API
void addKeyToButton(int buttonIndex, WORD key, bool released);

//Clears all the keys from a macro of a button by index
extern "C" CPAD_DRIVER_API
void clearKeysFromButton(int buttonIndex);

extern "C" CPAD_DRIVER_API
void CPadInit();

extern "C" CPAD_DRIVER_API
void CPadClose();

extern "C" CPAD_DRIVER_API
void addFile(int buttonIndex, wchar_t* filePath);

extern "C" CPAD_DRIVER_API
void addDelay(int buttonIndex, unsigned long long ns);

extern "C" CPAD_DRIVER_API
void addText(int buttonIndex, wchar_t* text);

extern "C" CPAD_DRIVER_API
void setButtonRunType(int buttonIndex, int runType);

extern "C" CPAD_DRIVER_API
void guiUp();

extern "C" CPAD_DRIVER_API
void cancelButtonLoop(int buttonIndex);

extern "C" CPAD_DRIVER_API
void setTickInterval(unsigned int interval);

static bool guiDown = true;

void openFile(std::wstring filePath);

static unsigned int tickInterval = 6000;

void connectToCPad();

static std::string connectionCheck;

enum INPUT_TYPE
{
    KEY_TYPE,
    FILE_TYPE,
    DELAY_TYPE,
    TEXT_TYPE
};

struct AInput
{
    INPUT input;
    std::wstring fileName;
    unsigned long long timeout;
    std::wstring text;
    INPUT_TYPE type;
};

//Array of macros, works by macro index to button index (macro indexes and button indexes must be synchronized)
static std::vector<AInput> inputs[4];

//Keys press simulation
void keyFunction(int index);
void k0();
void k1();
void k2();
void k3();

//Keys release simulation
void k0R();
void k1R();
void k2R();
void k3R();

//Converts a vector to an array
void vectorToInputArray(std::vector<AInput>& inputs, AInput* in);

//Adds a button press to a macro of a button by index
void pressButton(int buttonIndex, WORD key);

//Adds a button release to a macro of a button by index
void releaseButton(int buttonIndex, WORD key);

//Launch a macro of a button by index
void sendInput(int buttonIndex);
