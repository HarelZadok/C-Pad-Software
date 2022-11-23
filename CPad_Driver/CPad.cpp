#include "CPad.h"

#include <iostream>
#include <vector>
#include <cwchar>
#include <thread>
#include <Arduserial.h>
#include <filesystem>

//Array of the buttons
bool buttons[4]{};
bool buttonLooped[4]{};
int buttonsRunType[4]{};

void debug(wchar_t ch)
{
    std::vector<INPUT> vec;
    INPUT inp = { 0 };
    inp.type = INPUT_KEYBOARD;
    inp.ki.dwFlags = KEYEVENTF_UNICODE;
    inp.ki.wScan = ch;
    vec.push_back(inp);

    inp.ki.dwFlags |= KEYEVENTF_KEYUP;
    vec.push_back(inp);

    SendInput(vec.size(), vec.data(), sizeof INPUT);
}

WindowsSerial serial;

void connectToCPad()
{
    serial.setPort(9999);
    while (serial.getPort() == 9999)
    {
        for (unsigned int i = 0; i <= 30; ++i)
        {
            serial.setPort(i);
            if (serial.begin(115200))
            {
                while (!serial);

                std::string input;
                while (serial.available() && input != "CPAD READY")
                    input += std::string(1, serial.read());
                
                if (input.find("CPAD READY") != std::string::npos)
                {
                    connectionCheck = "CPAD READY";
                    break;
                }
                
                serial.end();
            }
            serial.setPort(9999);
        }
    }

    connected = true;
}

void setup()
{
    connectToCPad();

    std::thread t([]
    {
        while (true)
        {
            Sleep(500);
            connected = connectionCheck == "CPAD READY";
            connectionCheck.clear();
            if (!connected)
            {
                serial.end();
                connectToCPad();
            }
        }
    });
    t.detach();
}

void loop()
{    
    while (serial.available() < 2)
    {
        buttons[0] = false;
        buttons[1] = false;
        buttons[2] = false;
        buttons[3] = false;
        std::this_thread::sleep_for(std::chrono::microseconds(tickInterval));
    }

    while (!connected);
    
    std::string input;
    while (serial.available())
    {
        input += std::string(1, serial.read());
    }

    if (input.find("CPAD READY") != std::string::npos)
        connectionCheck = "CPAD READY";
    
    for (int i = 0; i < 4; ++i)
    {
        if (input.find('0' + i) == std::string::npos)
            buttons[i] = false;
    }

    for (const char value : input)
    {
        const int index = value - '0';
        
        if (!buttons[index])
        {
            keyPress(index, buttonsRunType[index]);
        }
        buttons[index] = true;
    }

    std::this_thread::sleep_for(std::chrono::microseconds(tickInterval));
}

bool isConnected()
{
    return connected;
}

void keyPress(int buttonIndex, int runType)
{    
    if (runType == 0)
    {
        keyFunction(buttonIndex);
    }
    else if (runType == 1)
    {
        if (buttonLooped[buttonIndex])
        {
            buttonLooped[buttonIndex] = false;
            return;
        }
        buttonLooped[buttonIndex] = true;
        std::thread t([buttonIndex]()
        {                
            while (true)
            {
                if (!buttonLooped[buttonIndex])
                    break;
                keyFunction(buttonIndex);
            }
        });
        t.detach();
    }
    else if (runType == 2)
    {
        if (buttonLooped[buttonIndex])
            return;
        
        buttonLooped[buttonIndex] = true;
        
        std::thread t([buttonIndex]()
        {
            while (buttons[buttonIndex])
                keyFunction(buttonIndex);
            buttonLooped[buttonIndex] = false;
        });
        t.detach();
    }
}

void keyRelease(int buttonIndex)
{
    switch (buttonIndex)
    {
    case 0:
        k0R();
        break;
    case 1:
        k1R();
        break;
    case 2:
        k2R();
        break;
    case 3:
        k3R();
        break;
    default:
        break;
    }
}

bool isKeyHeld(int buttonIndex) 
{
    return buttons[buttonIndex];
}

void addKeyToButton(int buttonIndex, WORD key, bool released)
{
    if (released)
        releaseButton(buttonIndex, key);
    else
        pressButton(buttonIndex, key);
}

void clearKeysFromButton(int buttonIndex)
{
    inputs[buttonIndex].clear();
}

void CPadInit()
{
    if (init && connected)
        return;
    
    if (!init)
    {
        setup();
        std::thread t([]()
       {
            while (guiDown);
            while(true) loop();
       });
        t.detach();
    }
    
    init = true;
}

void CPadClose()
{
    serial.end();
}

void addFile(int buttonIndex, wchar_t* filePath)
{
    std::wstring s = std::wstring(L"\"") + filePath + L"\"";
    inputs[buttonIndex].emplace_back();
    inputs[buttonIndex][inputs[buttonIndex].size() - 1].type = FILE_TYPE;
    inputs[buttonIndex][inputs[buttonIndex].size() - 1].fileName = s;
}

void addDelay(int buttonIndex, unsigned long long ns)
{
    inputs[buttonIndex].emplace_back();
    inputs[buttonIndex][inputs[buttonIndex].size() - 1].type = DELAY_TYPE;
    inputs[buttonIndex][inputs[buttonIndex].size() - 1].timeout = ns;
}

void addText(int buttonIndex, wchar_t* text)
{
    inputs[buttonIndex].emplace_back();
    inputs[buttonIndex][inputs[buttonIndex].size() - 1].type = TEXT_TYPE;
    inputs[buttonIndex][inputs[buttonIndex].size() - 1].text = text;
}

void setButtonRunType(int buttonIndex, int runType)
{
    buttonsRunType[buttonIndex] = runType;
}

void guiUp()
{
    guiDown = false;
}

void cancelButtonLoop(int buttonIndex)
{
    buttonLooped[buttonIndex] = false;
}

void setTickInterval(unsigned interval)
{
    tickInterval = interval;
}

void openFile(std::wstring filePath)
{
    // filePath = L"echo " + filePath + L" && pause";
    _wsystem(filePath.c_str());
}

void keyFunction(int index)
{
    switch (index)
    {
    case 0:
        k0();
        break;
    case 1:
        k1();
        break;
    case 2:
        k2();
        break;
    case 3:
        k3();
        break;
    }
}

void k0()
{
    std::thread t(sendInput, 0);
    t.join();
}

void k1()
{
    std::thread t(sendInput, 1);
    t.join();
}

void k2()
{    
    std::thread t(sendInput, 2);
    t.join();
}

void k3()
{
    std::thread t(sendInput, 3);
    t.join();
}

void k0R()
{
    
}

void k1R()
{
    
}

void k2R()
{
    
}

void k3R()
{
    
}

void vectorToInputArray(std::vector<AInput>& inputs, AInput* in)
{
    unsigned int length = inputs.size();

    for (unsigned int i = 0; i < length; ++i)
    {
        in[i] = inputs[i];
    }
}

void pressButton(int buttonIndex, WORD key)
{
    inputs[buttonIndex].emplace_back();
    inputs[buttonIndex][inputs[buttonIndex].size() - 1].type = KEY_TYPE;
    inputs[buttonIndex][inputs[buttonIndex].size() - 1].input.type = INPUT_KEYBOARD;
    inputs[buttonIndex][inputs[buttonIndex].size() - 1].input.ki.wVk = key;
}

void releaseButton(int buttonIndex, WORD key)
{
    inputs[buttonIndex].emplace_back();
    inputs[buttonIndex][inputs[buttonIndex].size() - 1].type = KEY_TYPE;
    inputs[buttonIndex][inputs[buttonIndex].size() - 1].input.type = INPUT_KEYBOARD;
    inputs[buttonIndex][inputs[buttonIndex].size() - 1].input.ki.wVk = key;
    inputs[buttonIndex][inputs[buttonIndex].size() - 1].input.ki.dwFlags = KEYEVENTF_KEYUP;
}

void sendInput(int buttonIndex)
{
    for (int i = 0; i < inputs[buttonIndex].size(); ++i)
    {
        const AInput& input = inputs[buttonIndex][i];
        if (input.type == KEY_TYPE)
        {
            auto inp = new INPUT[1];
            inp[0] = input.input;
            SendInput(1, inp, sizeof(INPUT));
        }
        else if(input.type == FILE_TYPE)
        {
            std::thread t(openFile, input.fileName);
            t.detach();
        }
        else if (input.type == DELAY_TYPE)
        {
            std::this_thread::sleep_for(std::chrono::nanoseconds(input.timeout));
        }
        else if (input.type == TEXT_TYPE)
        {
            std::vector<INPUT> vec;
            for (auto ch : input.text)
            {
                if (ch == '\n')
                {
                    INPUT inp = { 0 };
                    inp.type = INPUT_KEYBOARD;
                    inp.ki.wVk = ENTER;
                    vec.push_back(inp);

                    inp.ki.dwFlags |= KEYEVENTF_KEYUP;
                    vec.push_back(inp);
                    
                    continue;
                }
                INPUT inp = { 0 };
                inp.type = INPUT_KEYBOARD;
                inp.ki.dwFlags = KEYEVENTF_UNICODE;
                inp.ki.wScan = ch;
                vec.push_back(inp);

                inp.ki.dwFlags |= KEYEVENTF_KEYUP;
                vec.push_back(inp);
            }
            SendInput(vec.size(), vec.data(), sizeof(INPUT));
        }
    }
}