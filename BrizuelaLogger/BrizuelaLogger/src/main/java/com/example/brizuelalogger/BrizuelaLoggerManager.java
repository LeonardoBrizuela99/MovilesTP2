package com.example.brizuelalogger;
import android.util.Log;
public class BrizuelaLoggerManager {
    private static final BrizuelaLoggerManager instance = new BrizuelaLoggerManager();
    public static BrizuelaLoggerManager getInstance() {return instance;}

    public long getTime()
    {
        return System.currentTimeMillis();
    }


    public void sendLong(String msg)
    {
        Log.i("BrizuelaLog", msg);
    }
}