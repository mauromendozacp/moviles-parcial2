package com.example.proyecto1;

import androidx.appcompat.app.AppCompatActivity;
import android.os.Bundle;
import android.app.Service;
import android.os.Vibrator;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    public void ClickShake()
    {
        long[] pattern = {0, 100, 1000, 300, 200, 100, 500, 200, 100};
        Vibrator mVibrator01 = (Vibrator)getApplication().getSystemService(Service.VIBRATOR_SERVICE);
        mVibrator01.vibrate(new long[]{100,10,100,1000}, - 1);
    }
}