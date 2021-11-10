package com.example.modulo1;

import android.util.Log;

public class MLogger {

    public static final MLogger _instance = new MLogger();

    public static MLogger GetInstance(){
        return _instance;
    }

    public void SendLog(String msg){
        Log.d( "ML=>", msg);
    }
}
