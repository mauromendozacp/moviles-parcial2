package com.example.modulo1;

import android.app.Activity;
import android.util.Log;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;

public class FileManager {

    public static final String fileName = "logs.txt";

    public static final FileManager _instance = new FileManager();

    public static FileManager GetInstance(){
        return _instance;
    }

    public static Activity mainActivity;

    private void WriteFile(String data)
    {
        File path = mainActivity.getFilesDir();
        File file = new File(path, fileName);

        try
        {
            if (file.exists())
            {
                FileWriter file2 = new FileWriter(file,true);
                file2.write(data);
                file2.close();
            }
            else
            {
                FileOutputStream stream = new FileOutputStream(file);
                try
                {
                    stream.write(data.getBytes());
                }
                finally
                {
                    stream.close();
                }
            }
        }
        catch (IOException e)
        {
            Log.e("Exception", "File write failed: " + e.toString());
        }
    }

    private String ReadFile()
    {
        File path = mainActivity.getFilesDir();

        File file = new File(path, fileName);
        if (!file.exists())
            return "";

        int length = (int) file.length();
        byte[] bytes = new byte[length];

        try
        {
            FileInputStream stream = new FileInputStream(file);
            try
            {
                stream.read(bytes);
            }
            finally
            {
                stream.close();
            }
        }
        catch (IOException e)
        {
            Log.e("Exception", "File write failed: " + e.toString());
        }

        return new String(bytes);
    }
}
